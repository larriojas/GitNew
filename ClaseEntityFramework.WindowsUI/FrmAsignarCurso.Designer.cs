namespace ClaseEntityFramework.WindowsUI
{
    partial class FrmAsignarCurso
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
            this.label2 = new System.Windows.Forms.Label();
            this.cboCursos = new System.Windows.Forms.ComboBox();
            this.cursosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.alumnoRootBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cursoNameValueListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnAsignar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGrabar = new System.Windows.Forms.Button();
            this.nombresTextBox = new System.Windows.Forms.TextBox();
            this.apellidosTextBox = new System.Windows.Forms.TextBox();
            this.lstCursos = new System.Windows.Forms.ListBox();
            this.btnEliminar = new System.Windows.Forms.Button();
            nombresLabel = new System.Windows.Forms.Label();
            apellidosLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.cursosBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.alumnoRootBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cursoNameValueListBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // nombresLabel
            // 
            nombresLabel.AutoSize = true;
            nombresLabel.Location = new System.Drawing.Point(13, 19);
            nombresLabel.Name = "nombresLabel";
            nombresLabel.Size = new System.Drawing.Size(52, 13);
            nombresLabel.TabIndex = 11;
            nombresLabel.Text = "Nombres:";
            // 
            // apellidosLabel
            // 
            apellidosLabel.AutoSize = true;
            apellidosLabel.Location = new System.Drawing.Point(13, 45);
            apellidosLabel.Name = "apellidosLabel";
            apellidosLabel.Size = new System.Drawing.Size(52, 13);
            apellidosLabel.TabIndex = 12;
            apellidosLabel.Text = "Apellidos:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Curso:";
            // 
            // cboCursos
            // 
            this.cboCursos.DataSource = this.cursoNameValueListBindingSource;
            this.cboCursos.DisplayMember = "Value";
            this.cboCursos.FormattingEnabled = true;
            this.cboCursos.Location = new System.Drawing.Point(71, 78);
            this.cboCursos.Name = "cboCursos";
            this.cboCursos.Size = new System.Drawing.Size(226, 21);
            this.cboCursos.TabIndex = 1;
            this.cboCursos.ValueMember = "Key";
            // 
            // cursosBindingSource
            // 
            this.cursosBindingSource.DataMember = "Cursos";
            this.cursosBindingSource.DataSource = this.alumnoRootBindingSource;
            // 
            // alumnoRootBindingSource
            // 
            this.alumnoRootBindingSource.DataSource = typeof(ClaseEntityFramework.LogicaNegocio.AlumnoRoot);
            // 
            // cursoNameValueListBindingSource
            // 
            this.cursoNameValueListBindingSource.DataSource = typeof(ClaseEntityFramework.LogicaNegocio.CursoNameValueList);
            // 
            // btnAsignar
            // 
            this.btnAsignar.Location = new System.Drawing.Point(312, 76);
            this.btnAsignar.Name = "btnAsignar";
            this.btnAsignar.Size = new System.Drawing.Size(75, 23);
            this.btnAsignar.TabIndex = 3;
            this.btnAsignar.Text = "&Asignar";
            this.btnAsignar.UseVisualStyleBackColor = true;
            this.btnAsignar.Click += new System.EventHandler(this.btnAsignar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(207, 276);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 10;
            this.btnCancelar.Text = "&Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnGrabar
            // 
            this.btnGrabar.Location = new System.Drawing.Point(126, 276);
            this.btnGrabar.Name = "btnGrabar";
            this.btnGrabar.Size = new System.Drawing.Size(75, 23);
            this.btnGrabar.TabIndex = 11;
            this.btnGrabar.Text = "&Grabar";
            this.btnGrabar.UseVisualStyleBackColor = true;
            this.btnGrabar.Click += new System.EventHandler(this.btnGrabar_Click);
            // 
            // nombresTextBox
            // 
            this.nombresTextBox.BackColor = System.Drawing.Color.LightYellow;
            this.nombresTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.alumnoRootBindingSource, "Nombres", true));
            this.nombresTextBox.Location = new System.Drawing.Point(71, 16);
            this.nombresTextBox.Name = "nombresTextBox";
            this.nombresTextBox.ReadOnly = true;
            this.nombresTextBox.Size = new System.Drawing.Size(226, 20);
            this.nombresTextBox.TabIndex = 12;
            // 
            // apellidosTextBox
            // 
            this.apellidosTextBox.BackColor = System.Drawing.Color.LightYellow;
            this.apellidosTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.alumnoRootBindingSource, "Apellidos", true));
            this.apellidosTextBox.Location = new System.Drawing.Point(71, 42);
            this.apellidosTextBox.Name = "apellidosTextBox";
            this.apellidosTextBox.ReadOnly = true;
            this.apellidosTextBox.Size = new System.Drawing.Size(226, 20);
            this.apellidosTextBox.TabIndex = 13;
            // 
            // lstCursos
            // 
            this.lstCursos.DataSource = this.cursosBindingSource;
            this.lstCursos.DisplayMember = "NombreCurso";
            this.lstCursos.FormattingEnabled = true;
            this.lstCursos.Location = new System.Drawing.Point(16, 118);
            this.lstCursos.Name = "lstCursos";
            this.lstCursos.Size = new System.Drawing.Size(342, 134);
            this.lstCursos.TabIndex = 14;
            this.lstCursos.ValueMember = "Id";
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(364, 118);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(75, 23);
            this.btnEliminar.TabIndex = 15;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // FrmAsignarCurso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 318);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.lstCursos);
            this.Controls.Add(apellidosLabel);
            this.Controls.Add(this.apellidosTextBox);
            this.Controls.Add(nombresLabel);
            this.Controls.Add(this.nombresTextBox);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGrabar);
            this.Controls.Add(this.btnAsignar);
            this.Controls.Add(this.cboCursos);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAsignarCurso";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Asignar Curso al Alumno";
            ((System.ComponentModel.ISupportInitialize)(this.cursosBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.alumnoRootBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cursoNameValueListBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboCursos;
        private System.Windows.Forms.Button btnAsignar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnGrabar;
        private System.Windows.Forms.BindingSource alumnoRootBindingSource;
        private System.Windows.Forms.TextBox nombresTextBox;
        private System.Windows.Forms.TextBox apellidosTextBox;
        private System.Windows.Forms.BindingSource cursosBindingSource;
        private System.Windows.Forms.ListBox lstCursos;
        private System.Windows.Forms.BindingSource cursoNameValueListBindingSource;
        private System.Windows.Forms.Button btnEliminar;
    }
}