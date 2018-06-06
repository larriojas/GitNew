using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Alemana.Nucleo.Common.Tracing
{
    /// <summary>
    /// Trace listener que rota los archivos de log cada un determinado lapso de tiempo.
    /// </summary>
    /// <remarks>
    /// En el atributo 'initializeData' de la configuración del listener se
    /// pueden especificar tres parámetros separados por pipes ('|'):
    /// 1. Nombre de archivo a tomar como base
    /// 2. Lapso de tiempo en minutos a transcurrir entre rotaciones de log.
    /// 3. Cantidad máxima de archivos a generar (cuando llega al último sobreescribe el primero)
    /// 
    /// Ejemplo de configuración:
    /// 
    /// <code>
    /// 
    /// <listeners>
    ///      <add name="File" 
    ///           type="Alemana.Nucleo.Common.Tracing.TimeRolledListener, Alemana.Nucleo.Common" 
    ///           initializeData="AlemanaLog.txt|60|1000"
    ///           />
    /// </listeners>
    /// 
    /// </code>
    /// Esta configuración generará archivos de nombre AlemanaLog_0001.txt, AlemanaLog_0002.txt, etc.
    /// hasta llegar a los 1000 archivos. Al llegar al archivo AlemanaLog_0999.txt sobreescirbira el 
    /// primero (AlemanaLog_0001.txt)
    /// </remarks>
    public class TimeRolledListener : TextWriterTraceListener
    {
        #region Fields
        // Tiempo que debe transcurrir para rotar el log, por defecto una hora
        const string _timeSliceMinsAttribute = "timeSliceMins";
        private decimal _timeSliceMins = 60;

        // Cantidad máxima de archivos a rotar, por defecto 24
        // Cuando llega al último se sobre escibe el primero.
        const string _maxFilesAttribute = "maxFiles";
        private int _maxFiles = 24;

        const string _defaultFileName = "TraceLog.txt";

        // Stream cricular que almacenará los eventos.
        private static RollingFileStream _stream;

        //Timer para realizar el chequeo y rotar el archivo
        Timer _timer;
        #endregion

        #region .ctor

        public TimeRolledListener()
            : base(_stream = new RollingFileStream(_defaultFileName))
        {
            Initialize(String.Empty);
        }

        public TimeRolledListener(string initData)
            : base(_stream = new RollingFileStream(initData))
        {
            Initialize(initData);
        }

        private void Initialize(string initData)
        {
            var parts = initData.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            // Obtengo el lapso de rotación
            if (parts.Length > 1 && !String.IsNullOrWhiteSpace(parts[1]))
                Decimal.TryParse(parts[1], out _timeSliceMins);

            // Obtengo la cantidad max de archivos
            if (parts.Length > 2 && !String.IsNullOrWhiteSpace(parts[2]))
                Int32.TryParse(parts[2], out _maxFiles);

            SetTimerFirstTime();
        }

        private void SetTimerFirstTime()
        {
            bool isMultiple = (60 % _timeSliceMins == 0);
            decimal prox = _timeSliceMins;

            // Si el time-slice es múltiplo de 60
            // seteo el timer para que realice el roll
            // en el próximo múltiplo. De esa forma, si el time-slice es 60, 
            // el archivo se rotará exactamente cuando cambie la hora.
            if (isMultiple)
            {
                decimal minNow = DateTime.Now.Minute;
                decimal slice = _timeSliceMins;
                //prox = minNow + (slice - minNow % slice);
                prox = slice - minNow % slice;

                if (prox > 60)
                    prox = 0;
            }

            // Calculo el lapso en milisegundos
            int sliceMs = Convert.ToInt32(_timeSliceMins * 60 * 1000);
            int timerStartMs = Convert.ToInt32(prox * 60 * 1000);

            // Configuro el timer para que realice el chequeo
            _timer = new Timer(new TimerCallback(CheckAndRoll), null, timerStartMs, sliceMs);
        }

        private void SetTimer()
        {
            // Calculo el lapso en milisegundos
            int sliceMs = Convert.ToInt32(_timeSliceMins * 60 * 1000);

            // Configuro el timer para que realice el chequeo
            _timer = new Timer(new TimerCallback(CheckAndRoll), null, sliceMs, sliceMs);
        }
        #endregion

        #region Private Stuff
        private void CheckAndRoll(object obj)
        {
            _timer.Dispose();

            try
            {
                int nextFile;

                if (_stream.CurrentFileNo == _maxFiles - 1)
                    nextFile = 0;
                else
                    nextFile = _stream.CurrentFileNo + 1;

                _stream.Roll(nextFile);
            }
            catch { }

            SetTimer();
        }
        #endregion
    }

    #region Internal classes
    /// <summary>
    /// Stream circular, permite rotar la escritura en diferentes streams.
    /// </summary>
    internal class RollingFileStream : Stream, IDisposable
    {
        private FileStream _currentStream;

        private int _currentFileNo = 0;
        public int CurrentFileNo
        {
            get { return _currentFileNo; }
        }

        private string _filePath;
        private string _fileBase;
        private string _fileExt;
        private int _zeroPadCount;

        /// <summary>
        /// Stream circular, permite rotar la escritura en diferentes streams.
        /// </summary>
        /// <param name="baseFileName">Nombre base para los archivos a generar</param>
        /// <param name="maxFiles">Cantidad máxima de archivos a generar</param>
        public RollingFileStream(string initData)
        {
            var parts = initData.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            string baseFileName = parts.Length > 0 ? parts[0] : "TraceLog.txt";

            // Obtengo la cantidad max de archivos
            int maxFiles = 24;
            if (parts.Length > 2 && !String.IsNullOrWhiteSpace(parts[2]))
                Int32.TryParse(parts[2], out maxFiles);

            _zeroPadCount = maxFiles.ToString().Length;
            _filePath = Path.GetDirectoryName(baseFileName);
            _fileBase = Path.GetFileNameWithoutExtension(baseFileName);
            _fileExt = Path.GetExtension(baseFileName);

            if (string.IsNullOrEmpty(_filePath))
                _filePath = AppDomain.CurrentDomain.BaseDirectory;

            string fileNo = _currentFileNo.ToString().PadLeft(_zeroPadCount, '0');

            // Inicializo el stream.
            try
            {
                _currentStream = new FileStream(Path.Combine(_filePath, _fileBase + "_" + fileNo + _fileExt), FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
            }
            catch (IOException)
            {
                _currentFileNo++;
                fileNo = _currentFileNo.ToString().PadLeft(_zeroPadCount, '0');
                _currentStream = new FileStream(Path.Combine(_filePath, _fileBase + "_" + fileNo + _fileExt), FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
            }
        }

        public void Roll(int nextFileNo)
        {
            var previousStream = _currentStream;
            _currentFileNo = nextFileNo;

            string fileNo = _currentFileNo.ToString().PadLeft(_zeroPadCount, '0');

            var newStream = new FileStream(Path.Combine(_filePath, _fileBase + "_" + fileNo + _fileExt), FileMode.Create, FileAccess.ReadWrite, FileShare.Read);

            // Inicializo un nuevo stream y cambio la referencia.
            Interlocked.Exchange<FileStream>(ref _currentStream, newStream);

            previousStream.Flush();
            previousStream.Close();
            previousStream.Dispose();
        }

        public override bool CanRead
        {
            get { return _currentStream.CanRead; }
        }

        public override bool CanSeek
        {
            get { return _currentStream.CanSeek; }
        }

        public override long Length
        {
            get { return _currentStream.Length; }
        }

        public override long Position
        {
            get { return _currentStream.Position; }
            set { _currentStream.Position = Position; }
        }

        public override bool CanWrite
        {
            get { return _currentStream.CanWrite; }
        }

        public override void Flush()
        {
            _currentStream.Flush();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _currentStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            _currentStream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _currentStream.Write(buffer, offset, count);
            _currentStream.Flush(true);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _currentStream.Read(buffer, offset, count);
        }

        public override void Close()
        {
            _currentStream.Close();
        }

        void IDisposable.Dispose()
        {
            _currentStream.Dispose();
        }
    }
    #endregion
}
