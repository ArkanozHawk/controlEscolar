namespace Control_Escolar
{
    partial class Form3
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
            this.btnInscripcion = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnBuscar = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnModificar = new MaterialSkin.Controls.MaterialRaisedButton();
            this.Eliminar = new MaterialSkin.Controls.MaterialRaisedButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtAM_T = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.txtAP_T = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.padre = new System.Windows.Forms.Label();
            this.txtnombre_T = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.txtCel_T = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.txtTelf_T = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.txtLugTrab_T = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.txtprof_T = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtCP_T = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.txtColonia_T = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNum_T = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.txtCalle_T = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.materialRaisedButton1 = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnPrincipal = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnCerrar = new MaterialSkin.Controls.MaterialRaisedButton();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnInscripcion
            // 
            this.btnInscripcion.Depth = 0;
            this.btnInscripcion.Location = new System.Drawing.Point(0, 67);
            this.btnInscripcion.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnInscripcion.Name = "btnInscripcion";
            this.btnInscripcion.Primary = true;
            this.btnInscripcion.Size = new System.Drawing.Size(178, 102);
            this.btnInscripcion.TabIndex = 62;
            this.btnInscripcion.Text = "Inscripción";
            this.btnInscripcion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInscripcion.UseVisualStyleBackColor = true;
            this.btnInscripcion.Click += new System.EventHandler(this.btnInscripcion_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Depth = 0;
            this.btnBuscar.Location = new System.Drawing.Point(0, 168);
            this.btnBuscar.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Primary = true;
            this.btnBuscar.Size = new System.Drawing.Size(178, 106);
            this.btnBuscar.TabIndex = 61;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.BtnBuscar_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.Depth = 0;
            this.btnModificar.Location = new System.Drawing.Point(0, 261);
            this.btnModificar.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Primary = true;
            this.btnModificar.Size = new System.Drawing.Size(178, 112);
            this.btnModificar.TabIndex = 60;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnModificar.UseVisualStyleBackColor = true;
            // 
            // Eliminar
            // 
            this.Eliminar.Depth = 0;
            this.Eliminar.Location = new System.Drawing.Point(0, 363);
            this.Eliminar.MouseState = MaterialSkin.MouseState.HOVER;
            this.Eliminar.Name = "Eliminar";
            this.Eliminar.Primary = true;
            this.Eliminar.Size = new System.Drawing.Size(178, 111);
            this.Eliminar.TabIndex = 59;
            this.Eliminar.Text = "Eliminar";
            this.Eliminar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Eliminar.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox1.Controls.Add(this.txtAM_T);
            this.groupBox1.Controls.Add(this.txtAP_T);
            this.groupBox1.Controls.Add(this.padre);
            this.groupBox1.Controls.Add(this.txtnombre_T);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.groupBox1.Location = new System.Drawing.Point(203, 75);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBox1.Size = new System.Drawing.Size(291, 183);
            this.groupBox1.TabIndex = 63;
            this.groupBox1.TabStop = false;
            // 
            // txtAM_T
            // 
            this.txtAM_T.Depth = 0;
            this.txtAM_T.Hint = "Apellido Materno";
            this.txtAM_T.Location = new System.Drawing.Point(23, 139);
            this.txtAM_T.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtAM_T.Name = "txtAM_T";
            this.txtAM_T.PasswordChar = '\0';
            this.txtAM_T.SelectedText = "";
            this.txtAM_T.SelectionLength = 0;
            this.txtAM_T.SelectionStart = 0;
            this.txtAM_T.Size = new System.Drawing.Size(251, 23);
            this.txtAM_T.TabIndex = 46;
            this.txtAM_T.UseSystemPasswordChar = false;
            // 
            // txtAP_T
            // 
            this.txtAP_T.Depth = 0;
            this.txtAP_T.Hint = "Apellido Paterno";
            this.txtAP_T.Location = new System.Drawing.Point(23, 93);
            this.txtAP_T.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtAP_T.Name = "txtAP_T";
            this.txtAP_T.PasswordChar = '\0';
            this.txtAP_T.SelectedText = "";
            this.txtAP_T.SelectionLength = 0;
            this.txtAP_T.SelectionStart = 0;
            this.txtAP_T.Size = new System.Drawing.Size(251, 23);
            this.txtAP_T.TabIndex = 45;
            this.txtAP_T.UseSystemPasswordChar = false;
            // 
            // padre
            // 
            this.padre.AutoSize = true;
            this.padre.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.padre.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.padre.ForeColor = System.Drawing.SystemColors.MenuText;
            this.padre.Location = new System.Drawing.Point(71, 0);
            this.padre.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.padre.Name = "padre";
            this.padre.Size = new System.Drawing.Size(143, 26);
            this.padre.TabIndex = 44;
            this.padre.Text = "Padre o Tutor";
            // 
            // txtnombre_T
            // 
            this.txtnombre_T.Depth = 0;
            this.txtnombre_T.Hint = "Nombre(s)";
            this.txtnombre_T.Location = new System.Drawing.Point(23, 48);
            this.txtnombre_T.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtnombre_T.Name = "txtnombre_T";
            this.txtnombre_T.PasswordChar = '\0';
            this.txtnombre_T.SelectedText = "";
            this.txtnombre_T.SelectionLength = 0;
            this.txtnombre_T.SelectionStart = 0;
            this.txtnombre_T.Size = new System.Drawing.Size(251, 23);
            this.txtnombre_T.TabIndex = 34;
            this.txtnombre_T.UseSystemPasswordChar = false;
            // 
            // txtCel_T
            // 
            this.txtCel_T.Depth = 0;
            this.txtCel_T.Hint = "Celular";
            this.txtCel_T.Location = new System.Drawing.Point(18, 93);
            this.txtCel_T.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtCel_T.Name = "txtCel_T";
            this.txtCel_T.PasswordChar = '\0';
            this.txtCel_T.SelectedText = "";
            this.txtCel_T.SelectionLength = 0;
            this.txtCel_T.SelectionStart = 0;
            this.txtCel_T.Size = new System.Drawing.Size(251, 23);
            this.txtCel_T.TabIndex = 53;
            this.txtCel_T.UseSystemPasswordChar = false;
            // 
            // txtTelf_T
            // 
            this.txtTelf_T.Depth = 0;
            this.txtTelf_T.Hint = "Teléfono";
            this.txtTelf_T.Location = new System.Drawing.Point(18, 48);
            this.txtTelf_T.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtTelf_T.Name = "txtTelf_T";
            this.txtTelf_T.PasswordChar = '\0';
            this.txtTelf_T.SelectedText = "";
            this.txtTelf_T.SelectionLength = 0;
            this.txtTelf_T.SelectionStart = 0;
            this.txtTelf_T.Size = new System.Drawing.Size(251, 23);
            this.txtTelf_T.TabIndex = 46;
            this.txtTelf_T.UseSystemPasswordChar = false;
            // 
            // txtLugTrab_T
            // 
            this.txtLugTrab_T.Depth = 0;
            this.txtLugTrab_T.Hint = "Lugar de trabajo";
            this.txtLugTrab_T.Location = new System.Drawing.Point(18, 100);
            this.txtLugTrab_T.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtLugTrab_T.Name = "txtLugTrab_T";
            this.txtLugTrab_T.PasswordChar = '\0';
            this.txtLugTrab_T.SelectedText = "";
            this.txtLugTrab_T.SelectionLength = 0;
            this.txtLugTrab_T.SelectionStart = 0;
            this.txtLugTrab_T.Size = new System.Drawing.Size(251, 23);
            this.txtLugTrab_T.TabIndex = 45;
            this.txtLugTrab_T.UseSystemPasswordChar = false;
            // 
            // txtprof_T
            // 
            this.txtprof_T.Depth = 0;
            this.txtprof_T.Hint = "Profesión";
            this.txtprof_T.Location = new System.Drawing.Point(18, 51);
            this.txtprof_T.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtprof_T.Name = "txtprof_T";
            this.txtprof_T.PasswordChar = '\0';
            this.txtprof_T.SelectedText = "";
            this.txtprof_T.SelectionLength = 0;
            this.txtprof_T.SelectionStart = 0;
            this.txtprof_T.Size = new System.Drawing.Size(251, 23);
            this.txtprof_T.TabIndex = 34;
            this.txtprof_T.UseSystemPasswordChar = false;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox3.Controls.Add(this.txtCP_T);
            this.groupBox3.Controls.Add(this.txtColonia_T);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.txtNum_T);
            this.groupBox3.Controls.Add(this.txtCalle_T);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.groupBox3.Location = new System.Drawing.Point(203, 280);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBox3.Size = new System.Drawing.Size(291, 194);
            this.groupBox3.TabIndex = 64;
            this.groupBox3.TabStop = false;
            // 
            // txtCP_T
            // 
            this.txtCP_T.Depth = 0;
            this.txtCP_T.Hint = "Código Postal";
            this.txtCP_T.Location = new System.Drawing.Point(23, 154);
            this.txtCP_T.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtCP_T.Name = "txtCP_T";
            this.txtCP_T.PasswordChar = '\0';
            this.txtCP_T.SelectedText = "";
            this.txtCP_T.SelectionLength = 0;
            this.txtCP_T.SelectionStart = 0;
            this.txtCP_T.Size = new System.Drawing.Size(251, 23);
            this.txtCP_T.TabIndex = 46;
            this.txtCP_T.UseSystemPasswordChar = false;
            // 
            // txtColonia_T
            // 
            this.txtColonia_T.Depth = 0;
            this.txtColonia_T.Hint = "Colonia";
            this.txtColonia_T.Location = new System.Drawing.Point(23, 114);
            this.txtColonia_T.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtColonia_T.Name = "txtColonia_T";
            this.txtColonia_T.PasswordChar = '\0';
            this.txtColonia_T.SelectedText = "";
            this.txtColonia_T.SelectionLength = 0;
            this.txtColonia_T.SelectionStart = 0;
            this.txtColonia_T.Size = new System.Drawing.Size(251, 23);
            this.txtColonia_T.TabIndex = 45;
            this.txtColonia_T.UseSystemPasswordChar = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.MenuText;
            this.label3.Location = new System.Drawing.Point(88, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 26);
            this.label3.TabIndex = 44;
            this.label3.Text = "Dirección";
            // 
            // txtNum_T
            // 
            this.txtNum_T.Depth = 0;
            this.txtNum_T.Hint = "Número";
            this.txtNum_T.Location = new System.Drawing.Point(23, 70);
            this.txtNum_T.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtNum_T.Name = "txtNum_T";
            this.txtNum_T.PasswordChar = '\0';
            this.txtNum_T.SelectedText = "";
            this.txtNum_T.SelectionLength = 0;
            this.txtNum_T.SelectionStart = 0;
            this.txtNum_T.Size = new System.Drawing.Size(251, 23);
            this.txtNum_T.TabIndex = 35;
            this.txtNum_T.UseSystemPasswordChar = false;
            // 
            // txtCalle_T
            // 
            this.txtCalle_T.Depth = 0;
            this.txtCalle_T.Hint = "Calle";
            this.txtCalle_T.Location = new System.Drawing.Point(23, 31);
            this.txtCalle_T.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtCalle_T.Name = "txtCalle_T";
            this.txtCalle_T.PasswordChar = '\0';
            this.txtCalle_T.SelectedText = "";
            this.txtCalle_T.SelectionLength = 0;
            this.txtCalle_T.SelectionStart = 0;
            this.txtCalle_T.Size = new System.Drawing.Size(251, 23);
            this.txtCalle_T.TabIndex = 34;
            this.txtCalle_T.UseSystemPasswordChar = false;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox2.Controls.Add(this.txtCel_T);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtTelf_T);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.groupBox2.Location = new System.Drawing.Point(510, 75);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBox2.Size = new System.Drawing.Size(298, 162);
            this.groupBox2.TabIndex = 65;
            this.groupBox2.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.MenuText;
            this.label1.Location = new System.Drawing.Point(103, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 26);
            this.label1.TabIndex = 44;
            this.label1.Text = "Contacto";
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.txtprof_T);
            this.groupBox4.Controls.Add(this.txtLugTrab_T);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.groupBox4.Location = new System.Drawing.Point(510, 280);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBox4.Size = new System.Drawing.Size(298, 161);
            this.groupBox4.TabIndex = 66;
            this.groupBox4.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.MenuText;
            this.label2.Location = new System.Drawing.Point(103, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 26);
            this.label2.TabIndex = 44;
            this.label2.Text = "Trabajo";
            // 
            // materialRaisedButton1
            // 
            this.materialRaisedButton1.Depth = 0;
            this.materialRaisedButton1.Location = new System.Drawing.Point(826, 123);
            this.materialRaisedButton1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialRaisedButton1.Name = "materialRaisedButton1";
            this.materialRaisedButton1.Primary = true;
            this.materialRaisedButton1.Size = new System.Drawing.Size(178, 68);
            this.materialRaisedButton1.TabIndex = 67;
            this.materialRaisedButton1.Text = "Registrar Alumno";
            this.materialRaisedButton1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.materialRaisedButton1.UseVisualStyleBackColor = true;
            this.materialRaisedButton1.Click += new System.EventHandler(this.materialRaisedButton1_Click);
            // 
            // btnPrincipal
            // 
            this.btnPrincipal.Depth = 0;
            this.btnPrincipal.Location = new System.Drawing.Point(826, 335);
            this.btnPrincipal.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnPrincipal.Name = "btnPrincipal";
            this.btnPrincipal.Primary = true;
            this.btnPrincipal.Size = new System.Drawing.Size(178, 68);
            this.btnPrincipal.TabIndex = 68;
            this.btnPrincipal.Text = "Volver al Menú Principal";
            this.btnPrincipal.UseVisualStyleBackColor = true;
            this.btnPrincipal.Click += new System.EventHandler(this.BtnPrincipal_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.SystemColors.WindowText;
            this.btnCerrar.Depth = 0;
            this.btnCerrar.Location = new System.Drawing.Point(882, 26);
            this.btnCerrar.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Primary = true;
            this.btnCerrar.Size = new System.Drawing.Size(122, 35);
            this.btnCerrar.TabIndex = 69;
            this.btnCerrar.Text = "Cerrar Sesión";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.BtnCerrar_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1015, 478);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.btnPrincipal);
            this.Controls.Add(this.materialRaisedButton1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnInscripcion);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.btnModificar);
            this.Controls.Add(this.Eliminar);
            this.Name = "Form3";
            this.Text = "Inscripción";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialRaisedButton btnInscripcion;
        private MaterialSkin.Controls.MaterialRaisedButton btnBuscar;
        private MaterialSkin.Controls.MaterialRaisedButton btnModificar;
        private MaterialSkin.Controls.MaterialRaisedButton Eliminar;
        private System.Windows.Forms.GroupBox groupBox1;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtAM_T;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtAP_T;
        private System.Windows.Forms.Label padre;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtnombre_T;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtCel_T;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtTelf_T;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtLugTrab_T;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtprof_T;
        private System.Windows.Forms.GroupBox groupBox3;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtCP_T;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtColonia_T;
        private System.Windows.Forms.Label label3;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtNum_T;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtCalle_T;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label2;
        private MaterialSkin.Controls.MaterialRaisedButton materialRaisedButton1;
        private MaterialSkin.Controls.MaterialRaisedButton btnPrincipal;
        private MaterialSkin.Controls.MaterialRaisedButton btnCerrar;
    }
}