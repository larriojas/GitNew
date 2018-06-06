using Alemana.Nucleo.Common.ComponentModel;
using Alemana.Nucleo.Infrastructure.Models;
using Alemana.Nucleo.Shared.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;

namespace Alemana.Nucleo.Estadisticas.Wpf.ViewModels
{
    [Export]
    public class PacientesDiagnosticadosViewModel : ViewModelBase
    {
        [ImportingConstructor]
        public PacientesDiagnosticadosViewModel(IComponentContainer componentContainer)
        {
        }

        public override string Title
        {
            get { return "Pacientes Diagnosticados"; }
        }

        public override void OnDataPassed(object data)
        {
            Pacientes = new ObservableCollection<Paciente>(data as List<Paciente>);
        }

        #region Pacientes Diagnosticados

        private ObservableCollection<Paciente> pacientes = new ObservableCollection<Paciente>();

        public ObservableCollection<Paciente> Pacientes
        {
            get { return pacientes; }
            set
            {
                pacientes = value;
                RaisePropertyChanged(() => this.Pacientes);
            }
        }

        #endregion Pacientes Diagnosticados
    }
}