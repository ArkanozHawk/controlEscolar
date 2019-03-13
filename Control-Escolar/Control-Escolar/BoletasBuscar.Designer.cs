namespace Control_Escolar
{
    partial class BoletasBuscar
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.AgregarCalificaciones = new MaterialSkin.Controls.MaterialRaisedButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtAP_T = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.padre = new System.Windows.Forms.Label();
            this.dataGridViewbuscar = new System.Windows.Forms.DataGridView();
            this.btnPrincipal = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnCerrar = new MaterialSkin.Controls.MaterialRaisedButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewbuscar)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox1.Controls.Add(this.AgregarCalificaciones);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.txtAP_T);
            this.groupBox1.Controls.Add(this.padre);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.groupBox1.Location = new System.Drawing.Point(23, 79);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBox1.Size = new System.Drawing.Size(735, 169);
            this.groupBox1.TabIndex = 71;
            this.groupBox1.TabStop = false;
            // 
            // AgregarCalificaciones
            // 
            this.AgregarCalificaciones.Depth = 0;
            this.AgregarCalificaciones.Location = new System.Drawing.Point(283, 116);
            this.AgregarCalificaciones.MouseState = MaterialSkin.MouseState.HOVER;
            this.AgregarCalificaciones.Name = "AgregarCalificaciones";
            this.AgregarCalificaciones.Primary = true;
            this.AgregarCalificaciones.Size = new System.Drawing.Size(216, 35);
            this.AgregarCalificaciones.TabIndex = 73;
            this.AgregarCalificaciones.Text = "Agregar Calificaciones";
            this.AgregarCalificaciones.UseVisualStyleBackColor = true;
            this.AgregarCalificaciones.Click += new System.EventHandler(this.AgregarCalificaciones_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Control_Escolar.Properties.Resources.icons8_búsqueda_481;
            this.pictureBox1.Location = new System.Drawing.Point(245, 68);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(40, 33);
            this.pictureBox1.TabIndex = 55;
            this.pictureBox1.TabStop = false;
            // 
            // txtAP_T
            // 
            this.txtAP_T.Depth = 0;
            this.txtAP_T.Hint = "Introduce Filtro de Busqueda";
            this.txtAP_T.Location = new System.Drawing.Point(291, 78);
            this.txtAP_T.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtAP_T.Name = "txtAP_T";
            this.txtAP_T.PasswordChar = '\0';
            this.txtAP_T.SelectedText = "";
            this.txtAP_T.SelectionLength = 0;
            this.txtAP_T.SelectionStart = 0;
            this.txtAP_T.Size = new System.Drawing.Size(208, 23);
            this.txtAP_T.TabIndex = 45;
            this.txtAP_T.UseSystemPasswordChar = false;
            this.txtAP_T.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TxtAP_T_KeyUp);
            // 
            // padre
            // 
            this.padre.AutoSize = true;
            this.padre.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.padre.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.padre.ForeColor = System.Drawing.SystemColors.MenuText;
            this.padre.Location = new System.Drawing.Point(309, 0);
            this.padre.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.padre.Name = "padre";
            this.padre.Size = new System.Drawing.Size(161, 26);
            this.padre.TabIndex = 44;
            this.padre.Text = "Buscar Alumno";
            // 
            // dataGridViewbuscar
            // 
            this.dataGridViewbuscar.AllowUserToAddRows = false;
            this.dataGridViewbuscar.AllowUserToDeleteRows = false;
            this.dataGridViewbuscar.AllowUserToResizeColumns = false;
            this.dataGridViewbuscar.AllowUserToResizeRows = false;
            this.dataGridViewbuscar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewbuscar.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewbuscar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewbuscar.Location = new System.Drawing.Point(23, 254);
            this.dataGridViewbuscar.Name = "dataGridViewbuscar";
            this.dataGridViewbuscar.ReadOnly = true;
            this.dataGridViewbuscar.RowHeadersVisible = false;
            this.dataGridViewbuscar.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridViewbuscar.Size = new System.Drawing.Size(735, 221);
            this.dataGridViewbuscar.TabIndex = 70;
            this.dataGridViewbuscar.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewbuscar_CellContentClick);
            // 
            // btnPrincipal
            // 
            this.btnPrincipal.Depth = 0;
            this.btnPrincipal.Location = new System.Drawing.Point(306, 481);
            this.btnPrincipal.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnPrincipal.Name = "btnPrincipal";
            this.btnPrincipal.Primary = true;
            this.btnPrincipal.Size = new System.Drawing.Size(216, 35);
            this.btnPrincipal.TabIndex = 72;
            this.btnPrincipal.Text = "Volver al Menú Principal";
            this.btnPrincipal.UseVisualStyleBackColor = true;
            this.btnPrincipal.Click += new System.EventHandler(this.btnPrincipal_Click_1);
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.SystemColors.WindowText;
            this.btnCerrar.Depth = 0;
            this.btnCerrar.Location = new System.Drawing.Point(666, 27);
            this.btnCerrar.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Primary = true;
            this.btnCerrar.Size = new System.Drawing.Size(122, 35);
            this.btnCerrar.TabIndex = 72;
            this.btnCerrar.Text = "Cerrar Sesión";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click_1);
            // 
            // BoletasBuscar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 542);
            this.Controls.Add(this.btnPrincipal);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridViewbuscar);
            this.Name = "BoletasBuscar";
            this.Text = "Buscar Alumno";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewbuscar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtAP_T;
        private System.Windows.Forms.Label padre;
        private System.Windows.Forms.DataGridView dataGridViewbuscar;
        private MaterialSkin.Controls.MaterialRaisedButton btnPrincipal;
        private MaterialSkin.Controls.MaterialRaisedButton btnCerrar;
        private MaterialSkin.Controls.MaterialRaisedButton AgregarCalificaciones;
    }
}