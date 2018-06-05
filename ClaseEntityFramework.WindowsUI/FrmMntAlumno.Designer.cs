namespace ClaseEntityFramework.WindowsUI
{
    partial class FrmMntAlumno
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label nombresLabel;
            System.Windows.Forms.Label apellidosLabel;
            System.Windows.Forms.Label correoLabel;
            System.Windows.Forms.Label fechaInscripcionLabel;
            System.Windows.Forms.Label telefonoLabel;
            this.alumnoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.nombresTextBox = new System.Windows.Forms.TextBox();
            this.apellidosTextBox = new System.Windows.Forms.TextBox();
            this.correoTextBox = new System.Windows.Forms.TextBox();
            this.fechaInscripcionDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.btnGrabar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.telefonoTextBox = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            nombresLabel = new System.Windows.Forms.Label();
            apellidosLabel = new System.Windows.Forms.Label();
            correoLabel = new System.Windows.Forms.Label();
            fechaInscripcionLabel = new System.Windows.Forms.Label();
            telefonoLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.alumnoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // nombresLabel
            // 
            nombresLabel.AutoSize = true;
            nombresLabel.Location = new System.Drawing.Point(24, 19);
            nombresLabel.Name = "nombresLabel";
            nombresLabel.Size = new System.Drawing.Size(52, 13);
            nombresLabel.TabIndex = 1;
            nombresLabel.Text = "Nombres:";
            // 
            // apellidosLabel
            // 
            apellidosLabel.AutoSize = true;
            apellidosLabel.Location = new System.Drawing.Point(24, 45);
            apellidosLabel.Name = "apellidosLabel";
            apellidosLabel.Size = new System.Drawing.Size(52, 13);
            apellidosLabel.TabIndex = 3;
            apellidosLabel.Text = "Apellidos:";
            // 
            // correoLabel
            // 
            correoLabel.AutoSize = true;
            correoLabel.Location = new System.Drawing.Point(24, 71);
            correoLabel.Name = "correoLabel";
            correoLabel.Size = new System.Drawing.Size(41, 13);
            correoLabel.TabIndex = 5;
            correoLabel.Text = "Correo:";
            // 
            // fechaInscripcionLabel
            // 
            fechaInscripcionLabel.AutoSize = true;
            fechaInscripcionLabel.Location = new System.Drawing.Point(24, 98);
            fechaInscripcionLabel.Name = "fechaInscripcionLabel";
            fechaInscripcionLabel.Size = new System.Drawing.Size(94, 13);
            fechaInscripcionLabel.TabIndex = 7;
            fechaInscripcionLabel.Text = "Fecha Inscripcion:";
            // 
            // telefonoLabel
            // 
            telefonoLabel.AutoSize = true;
            telefonoLabel.Location = new System.Drawing.Point(24, 123);
            telefonoLabel.Name = "telefonoLabel";
            telefonoLabel.Size = new System.Drawing.Size(52, 13);
            telefonoLabel.TabIndex = 10;
            telefonoLabel.Text = "Telefono:";
            // 
            // alumnoBindingSource
            // 
            this.alumnoBindingSource.DataSource = typeof(ClaseEntityFramework.LogicaNegocio.AlumnoRoot);
            // 
            // nombresTextBox
            // 
            this.nombresTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.alumnoBindingSource, "Nombres", true));
            this.nombresTextBox.Location = new System.Drawing.Point(124, 16);
            this.nombresTextBox.Name = "nombresTextBox";
            this.nombresTextBox.Size = new System.Drawing.Size(100, 20);
            this.nombresTextBox.TabIndex = 0;
            // 
            // apellidosTextBox
            // 
            this.apellidosTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.alumnoBindingSource, "Apellidos", true));
            this.apellidosTextBox.Location = new System.Drawing.Point(124, 42);
            this.apellidosTextBox.Name = "apellidosTextBox";
            this.apellidosTextBox.Size = new System.Drawing.Size(100, 20);
            this.apellidosTextBox.TabIndex = 1;
            // 
            // correoTextBox
            // 
            this.correoTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.alumnoBindingSource, "Correo", true));
            this.correoTextBox.Location = new System.Drawing.Point(124, 68);
            this.correoTextBox.Name = "correoTextBox";
            this.correoTextBox.Size = new System.Drawing.Size(200, 20);
            this.correoTextBox.TabIndex = 2;
            // 
            // fechaInscripcionDateTimePicker
            // 
            this.fechaInscripcionDateTimePicker.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.alumnoBindingSource, "FechaInscripcion", true));
            this.fechaInscripcionDateTimePicker.Location = new System.Drawing.Point(124, 94);
            this.fechaInscripcionDateTimePicker.Name = "fechaInscripcionDateTimePicker";
            this.fechaInscripcionDateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.fechaInscripcionDateTimePicker.TabIndex = 3;
            // 
            // btnGrabar
            // 
            this.btnGrabar.Location = new System.Drawing.Point(118, 160);
            this.btnGrabar.Name = "btnGrabar";
            this.btnGrabar.Size = new System.Drawing.Size(75, 23);
            this.btnGrabar.TabIndex = 5;
            this.btnGrabar.Text = "&Grabar";
            this.btnGrabar.UseVisualStyleBackColor = true;
            this.btnGrabar.Click += new System.EventHandler(this.btnGrabar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(199, 160);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 6;
            this.btnCancelar.Text = "&Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // telefonoTextBox
            // 
            this.telefonoTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.alumnoBindingSource, "Telefono", true));
            this.telefonoTextBox.Location = new System.Drawing.Point(124, 120);
            this.telefonoTextBox.Name = "telefonoTextBox";
            this.telefonoTextBox.Size = new System.Drawing.Size(100, 20);
            this.telefonoTextBox.TabIndex = 4;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            this.errorProvider1.DataSource = this.alumnoBindingSource;
            // 
            // FrmMntAlumno
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 203);
            this.Controls.Add(telefonoLabel);
            this.Controls.Add(this.telefonoTextBox);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGrabar);
            this.Controls.Add(fechaInscripcionLabel);
            this.Controls.Add(this.fechaInscripcionDateTimePicker);
            this.Controls.Add(correoLabel);
            this.Controls.Add(this.correoTextBox);
            this.Controls.Add(apellidosLabel);
            this.Controls.Add(this.apellidosTextBox);
            this.Controls.Add(nombresLabel);
            this.Controls.Add(this.nombresTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMntAlumno";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mantenimiento de Alumno";
            this.Load += new System.EventHandler(this.FrmMntAlumno_Load);
            ((System.ComponentModel.ISupportInitialize)(this.alumnoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource alumnoBindingSource;
        private System.Windows.Forms.TextBox nombresTextBox;
        private System.Windows.Forms.TextBox apellidosTextBox;
        private System.Windows.Forms.TextBox correoTextBox;
        private System.Windows.Forms.DateTimePicker fechaInscripcionDateTimePicker;
        private System.Windows.Forms.Button btnGrabar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.TextBox telefonoTextBox;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}