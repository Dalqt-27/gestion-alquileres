namespace CapaAplicacion
{
    partial class FrmAlquiler
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbClientes = new System.Windows.Forms.ListBox();
            this.lbEquipos = new System.Windows.Forms.ListBox();
            this.tbCostoTotal = new System.Windows.Forms.TextBox();
            this.dtpInicio = new System.Windows.Forms.DateTimePicker();
            this.dtpFin = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvAlquileres = new System.Windows.Forms.DataGridView();
            this.btnRegistrar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnReporteActivos = new System.Windows.Forms.Button();
            this.btnReporteIngresos = new System.Windows.Forms.Button();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlquileres)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(96, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Lista de Clientes";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(250, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Lista de Equipos";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(411, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Fecha de Alquiler";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(411, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Fecha de Devolución";
            // 
            // lbClientes
            // 
            this.lbClientes.FormattingEnabled = true;
            this.lbClientes.Location = new System.Drawing.Point(84, 104);
            this.lbClientes.Name = "lbClientes";
            this.lbClientes.Size = new System.Drawing.Size(120, 95);
            this.lbClientes.TabIndex = 4;
            // 
            // lbEquipos
            // 
            this.lbEquipos.FormattingEnabled = true;
            this.lbEquipos.Location = new System.Drawing.Point(241, 104);
            this.lbEquipos.Name = "lbEquipos";
            this.lbEquipos.Size = new System.Drawing.Size(120, 95);
            this.lbEquipos.TabIndex = 5;
            // 
            // tbCostoTotal
            // 
            this.tbCostoTotal.Location = new System.Drawing.Point(414, 189);
            this.tbCostoTotal.Name = "tbCostoTotal";
            this.tbCostoTotal.ReadOnly = true;
            this.tbCostoTotal.Size = new System.Drawing.Size(100, 20);
            this.tbCostoTotal.TabIndex = 6;
            // 
            // dtpInicio
            // 
            this.dtpInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpInicio.Location = new System.Drawing.Point(414, 84);
            this.dtpInicio.Name = "dtpInicio";
            this.dtpInicio.Size = new System.Drawing.Size(106, 20);
            this.dtpInicio.TabIndex = 7;
            // 
            // dtpFin
            // 
            this.dtpFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFin.Location = new System.Drawing.Point(414, 134);
            this.dtpFin.Name = "dtpFin";
            this.dtpFin.Size = new System.Drawing.Size(106, 20);
            this.dtpFin.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(414, 170);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Monto Total";
            // 
            // dgvAlquileres
            // 
            this.dgvAlquileres.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAlquileres.Location = new System.Drawing.Point(47, 256);
            this.dgvAlquileres.Name = "dgvAlquileres";
            this.dgvAlquileres.Size = new System.Drawing.Size(547, 211);
            this.dgvAlquileres.TabIndex = 10;
            this.dgvAlquileres.SelectionChanged += new System.EventHandler(this.dgvAlquileres_SelectionChanged);
            // 
            // btnRegistrar
            // 
            this.btnRegistrar.Location = new System.Drawing.Point(635, 70);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(151, 34);
            this.btnRegistrar.TabIndex = 11;
            this.btnRegistrar.Text = "Registrar";
            this.btnRegistrar.UseVisualStyleBackColor = true;
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.Location = new System.Drawing.Point(635, 117);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(151, 34);
            this.btnModificar.TabIndex = 12;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(635, 165);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(151, 34);
            this.btnEliminar.TabIndex = 13;
            this.btnEliminar.Text = "Eliminar Logico";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnReporteActivos
            // 
            this.btnReporteActivos.Location = new System.Drawing.Point(635, 215);
            this.btnReporteActivos.Name = "btnReporteActivos";
            this.btnReporteActivos.Size = new System.Drawing.Size(151, 34);
            this.btnReporteActivos.TabIndex = 14;
            this.btnReporteActivos.Text = "Reporte Activos";
            this.btnReporteActivos.UseVisualStyleBackColor = true;
            this.btnReporteActivos.Click += new System.EventHandler(this.btnReporteActivos_Click);
            // 
            // btnReporteIngresos
            // 
            this.btnReporteIngresos.Location = new System.Drawing.Point(635, 267);
            this.btnReporteIngresos.Name = "btnReporteIngresos";
            this.btnReporteIngresos.Size = new System.Drawing.Size(151, 34);
            this.btnReporteIngresos.TabIndex = 15;
            this.btnReporteIngresos.Text = "Reporte Total Ingresos";
            this.btnReporteIngresos.UseVisualStyleBackColor = true;
            this.btnReporteIngresos.Click += new System.EventHandler(this.btnReporteIngresos_Click);
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(635, 320);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(151, 34);
            this.btnVolver.TabIndex = 16;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(635, 375);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(151, 34);
            this.btnSalir.TabIndex = 17;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // FrmAlquiler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 498);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.btnReporteIngresos);
            this.Controls.Add(this.btnReporteActivos);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnModificar);
            this.Controls.Add(this.btnRegistrar);
            this.Controls.Add(this.dgvAlquileres);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dtpFin);
            this.Controls.Add(this.dtpInicio);
            this.Controls.Add(this.tbCostoTotal);
            this.Controls.Add(this.lbEquipos);
            this.Controls.Add(this.lbClientes);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FrmAlquiler";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.FrmAlquiler_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlquileres)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox lbClientes;
        private System.Windows.Forms.ListBox lbEquipos;
        private System.Windows.Forms.TextBox tbCostoTotal;
        private System.Windows.Forms.DateTimePicker dtpInicio;
        private System.Windows.Forms.DateTimePicker dtpFin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgvAlquileres;
        private System.Windows.Forms.Button btnRegistrar;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnReporteActivos;
        private System.Windows.Forms.Button btnReporteIngresos;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnSalir;
    }
}

