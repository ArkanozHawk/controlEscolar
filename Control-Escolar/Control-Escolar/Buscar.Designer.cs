namespace Control_Escolar
{
    partial class Buscar
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
            this.btnCerrar = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnInscripcion = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnBuscar = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnModificar = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnPrincipal = new MaterialSkin.Controls.MaterialRaisedButton();
            this.dataGridViewbuscar = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnBuscarAlum = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnModificarAlum = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnEliminar = new MaterialSkin.Controls.MaterialRaisedButton();
            this.txtCURP_A = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.txtAM_T = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.txtAP_T = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.padre = new System.Windows.Forms.Label();
            this.txtnombre_T = new MaterialSkin.Controls.MaterialSingleLineTextField();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewbuscar)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.SystemColors.WindowText;
            this.btnCerrar.Depth = 0;
            this.btnCerrar.Location = new System.Drawing.Point(846, 27);
            this.btnCerrar.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Primary = true;
            this.btnCerrar.Size = new System.Drawing.Size(122, 35);
            this.btnCerrar.TabIndex = 1;
            this.btnCerrar.Text = "Cerrar Sesión";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.BtnCerrar_Click);
            // 
            // btnInscripcion
            // 
            this.btnInscripcion.Depth = 0;
            this.btnInscripcion.Location = new System.Drawing.Point(0, 64);
            this.btnInscripcion.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnInscripcion.Name = "btnInscripcion";
            this.btnInscripcion.Primary = true;
            this.btnInscripcion.Size = new System.Drawing.Size(178, 102);
            this.btnInscripcion.TabIndex = 66;
            this.btnInscripcion.Text = "Inscripción";
            this.btnInscripcion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInscripcion.UseVisualStyleBackColor = true;
            this.btnInscripcion.Click += new System.EventHandler(this.BtnInscripcion_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Depth = 0;
            this.btnBuscar.Location = new System.Drawing.Point(0, 165);
            this.btnBuscar.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Primary = true;
            this.btnBuscar.Size = new System.Drawing.Size(178, 106);
            this.btnBuscar.TabIndex = 65;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.BtnBuscar_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.Depth = 0;
            this.btnModificar.Location = new System.Drawing.Point(0, 258);
            this.btnModificar.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Primary = true;
            this.btnModificar.Size = new System.Drawing.Size(178, 112);
            this.btnModificar.TabIndex = 64;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnModificar.UseVisualStyleBackColor = true;
            // 
            // btnPrincipal
            // 
            this.btnPrincipal.Depth = 0;
            this.btnPrincipal.Location = new System.Drawing.Point(0, 365);
            this.btnPrincipal.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnPrincipal.Name = "btnPrincipal";
            this.btnPrincipal.Primary = true;
            this.btnPrincipal.Size = new System.Drawing.Size(178, 106);
            this.btnPrincipal.TabIndex = 67;
            this.btnPrincipal.Text = "Volver al Menú Principal";
            this.btnPrincipal.UseVisualStyleBackColor = true;
            this.btnPrincipal.Click += new System.EventHandler(this.BtnPrincipal_Click);
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
            this.dataGridViewbuscar.Location = new System.Drawing.Point(195, 250);
            this.dataGridViewbuscar.Name = "dataGridViewbuscar";
            this.dataGridViewbuscar.ReadOnly = true;
            this.dataGridViewbuscar.RowHeadersVisible = false;
            this.dataGridViewbuscar.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridViewbuscar.Size = new System.Drawing.Size(773, 221);
            this.dataGridViewbuscar.TabIndex = 68;
            this.dataGridViewbuscar.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewbuscar_CellContentClick);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox1.Controls.Add(this.pictureBox3);
            this.groupBox1.Controls.Add(this.pictureBox2);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.btnBuscarAlum);
            this.groupBox1.Controls.Add(this.btnModificarAlum);
            this.groupBox1.Controls.Add(this.btnEliminar);
            this.groupBox1.Controls.Add(this.txtCURP_A);
            this.groupBox1.Controls.Add(this.txtAM_T);
            this.groupBox1.Controls.Add(this.txtAP_T);
            this.groupBox1.Controls.Add(this.padre);
            this.groupBox1.Controls.Add(this.txtnombre_T);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.groupBox1.Location = new System.Drawing.Point(195, 75);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBox1.Size = new System.Drawing.Size(773, 169);
            this.groupBox1.TabIndex = 69;
            this.groupBox1.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::Control_Escolar.Properties.Resources.icons8_editar_481;
            this.pictureBox3.Location = new System.Drawing.Point(574, 125);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(36, 37);
            this.pictureBox3.TabIndex = 54;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Control_Escolar.Properties.Resources.icons8_papelera_de_reciclaje_481;
            this.pictureBox2.Location = new System.Drawing.Point(38, 126);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(34, 37);
            this.pictureBox2.TabIndex = 53;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Control_Escolar.Properties.Resources.icons8_búsqueda_481;
            this.pictureBox1.Location = new System.Drawing.Point(295, 126);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(38, 37);
            this.pictureBox1.TabIndex = 52;
            this.pictureBox1.TabStop = false;
            // 
            // btnBuscarAlum
            // 
            this.btnBuscarAlum.Depth = 0;
            this.btnBuscarAlum.Location = new System.Drawing.Point(339, 125);
            this.btnBuscarAlum.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnBuscarAlum.Name = "btnBuscarAlum";
            this.btnBuscarAlum.Primary = true;
            this.btnBuscarAlum.Size = new System.Drawing.Size(105, 38);
            this.btnBuscarAlum.TabIndex = 51;
            this.btnBuscarAlum.Text = "Buscar";
            this.btnBuscarAlum.UseVisualStyleBackColor = true;
            // 
            // btnModificarAlum
            // 
            this.btnModificarAlum.Depth = 0;
            this.btnModificarAlum.Location = new System.Drawing.Point(616, 125);
            this.btnModificarAlum.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnModificarAlum.Name = "btnModificarAlum";
            this.btnModificarAlum.Primary = true;
            this.btnModificarAlum.Size = new System.Drawing.Size(105, 38);
            this.btnModificarAlum.TabIndex = 50;
            this.btnModificarAlum.Text = "Modificar";
            this.btnModificarAlum.UseVisualStyleBackColor = true;
            // 
            // btnEliminar
            // 
            this.btnEliminar.Depth = 0;
            this.btnEliminar.Location = new System.Drawing.Point(78, 125);
            this.btnEliminar.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Primary = true;
            this.btnEliminar.Size = new System.Drawing.Size(105, 38);
            this.btnEliminar.TabIndex = 49;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.BtnEliminar_Click);
            // 
            // txtCURP_A
            // 
            this.txtCURP_A.Depth = 0;
            this.txtCURP_A.Hint = "CURP";
            this.txtCURP_A.Location = new System.Drawing.Point(284, 90);
            this.txtCURP_A.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtCURP_A.Name = "txtCURP_A";
            this.txtCURP_A.PasswordChar = '\0';
            this.txtCURP_A.SelectedText = "";
            this.txtCURP_A.SelectionLength = 0;
            this.txtCURP_A.SelectionStart = 0;
            this.txtCURP_A.Size = new System.Drawing.Size(208, 23);
            this.txtCURP_A.TabIndex = 48;
            this.txtCURP_A.UseSystemPasswordChar = false;
            // 
            // txtAM_T
            // 
            this.txtAM_T.Depth = 0;
            this.txtAM_T.Hint = "Apellido Materno";
            this.txtAM_T.Location = new System.Drawing.Point(284, 41);
            this.txtAM_T.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtAM_T.Name = "txtAM_T";
            this.txtAM_T.PasswordChar = '\0';
            this.txtAM_T.SelectedText = "";
            this.txtAM_T.SelectionLength = 0;
            this.txtAM_T.SelectionStart = 0;
            this.txtAM_T.Size = new System.Drawing.Size(208, 23);
            this.txtAM_T.TabIndex = 46;
            this.txtAM_T.UseSystemPasswordChar = false;
            // 
            // txtAP_T
            // 
            this.txtAP_T.Depth = 0;
            this.txtAP_T.Hint = "Apellido Paterno";
            this.txtAP_T.Location = new System.Drawing.Point(38, 41);
            this.txtAP_T.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtAP_T.Name = "txtAP_T";
            this.txtAP_T.PasswordChar = '\0';
            this.txtAP_T.SelectedText = "";
            this.txtAP_T.SelectionLength = 0;
            this.txtAP_T.SelectionStart = 0;
            this.txtAP_T.Size = new System.Drawing.Size(208, 23);
            this.txtAP_T.TabIndex = 45;
            this.txtAP_T.UseSystemPasswordChar = false;
            this.txtAP_T.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TxtAP_T_KeyUp_1);
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
            // txtnombre_T
            // 
            this.txtnombre_T.Depth = 0;
            this.txtnombre_T.Hint = "Nombre(s)";
            this.txtnombre_T.Location = new System.Drawing.Point(541, 41);
            this.txtnombre_T.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtnombre_T.Name = "txtnombre_T";
            this.txtnombre_T.PasswordChar = '\0';
            this.txtnombre_T.SelectedText = "";
            this.txtnombre_T.SelectionLength = 0;
            this.txtnombre_T.SelectionStart = 0;
            this.txtnombre_T.Size = new System.Drawing.Size(208, 23);
            this.txtnombre_T.TabIndex = 34;
            this.txtnombre_T.UseSystemPasswordChar = false;
            // 
            // Buscar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 481);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridViewbuscar);
            this.Controls.Add(this.btnPrincipal);
            this.Controls.Add(this.btnInscripcion);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.btnModificar);
            this.Controls.Add(this.btnCerrar);
            this.Name = "Buscar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Buscar";
            this.Load += new System.EventHandler(this.Buscar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewbuscar)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialRaisedButton btnCerrar;
        private MaterialSkin.Controls.MaterialRaisedButton btnInscripcion;
        private MaterialSkin.Controls.MaterialRaisedButton btnBuscar;
        private MaterialSkin.Controls.MaterialRaisedButton btnModificar;
        private MaterialSkin.Controls.MaterialRaisedButton btnPrincipal;
        private System.Windows.Forms.DataGridView dataGridViewbuscar;
        private System.Windows.Forms.GroupBox groupBox1;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtAM_T;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtAP_T;
        private System.Windows.Forms.Label padre;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtnombre_T;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtCURP_A;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private MaterialSkin.Controls.MaterialRaisedButton btnBuscarAlum;
        private MaterialSkin.Controls.MaterialRaisedButton btnModificarAlum;
        private MaterialSkin.Controls.MaterialRaisedButton btnEliminar;
    }
}