namespace Control_Escolar
{
    partial class Alumno
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
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.txtEdad_A = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.label5 = new System.Windows.Forms.Label();
            this.txtLugarNac_A = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.txtAlergias_A = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.txtTelEme_A = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtCURP_A = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.txtApMat_A = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.txtApPat_A = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNombre_A = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnCerrar = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnPrincipal = new MaterialSkin.Controls.MaterialRaisedButton();
            this.Eliminar = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnModificar = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnBuscar = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnInscripcion = new MaterialSkin.Controls.MaterialRaisedButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtCP_A = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.txtColonia_C = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNum_A = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.txtCalle_A = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.btnSiguiente = new MaterialSkin.Controls.MaterialRaisedButton();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Controls.Add(this.dateTimePicker1);
            this.groupBox5.Controls.Add(this.txtEdad_A);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.txtLugarNac_A);
            this.groupBox5.Controls.Add(this.txtAlergias_A);
            this.groupBox5.Controls.Add(this.txtTelEme_A);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.groupBox5.Location = new System.Drawing.Point(528, 72);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBox5.Size = new System.Drawing.Size(304, 316);
            this.groupBox5.TabIndex = 50;
            this.groupBox5.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label2.Location = new System.Drawing.Point(67, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(159, 20);
            this.label2.TabIndex = 62;
            this.label2.Text = "Fecha de Nacimiento";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(50, 77);
            this.dateTimePicker1.MinDate = new System.DateTime(2006, 1, 1, 0, 0, 0, 0);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 32);
            this.dateTimePicker1.TabIndex = 61;
            // 
            // txtEdad_A
            // 
            this.txtEdad_A.Depth = 0;
            this.txtEdad_A.Hint = "Edad";
            this.txtEdad_A.Location = new System.Drawing.Point(13, 169);
            this.txtEdad_A.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtEdad_A.Name = "txtEdad_A";
            this.txtEdad_A.PasswordChar = '\0';
            this.txtEdad_A.SelectedText = "";
            this.txtEdad_A.SelectionLength = 0;
            this.txtEdad_A.SelectionStart = 0;
            this.txtEdad_A.Size = new System.Drawing.Size(264, 23);
            this.txtEdad_A.TabIndex = 45;
            this.txtEdad_A.UseSystemPasswordChar = false;
            this.txtEdad_A.Validating += new System.ComponentModel.CancelEventHandler(this.txtEdad_A_Validating);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.MenuText;
            this.label5.Location = new System.Drawing.Point(33, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(242, 26);
            this.label5.TabIndex = 44;
            this.label5.Text = "Información del Alumno";
            // 
            // txtLugarNac_A
            // 
            this.txtLugarNac_A.Depth = 0;
            this.txtLugarNac_A.Hint = "Lugar de nacimiento";
            this.txtLugarNac_A.Location = new System.Drawing.Point(13, 125);
            this.txtLugarNac_A.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtLugarNac_A.Name = "txtLugarNac_A";
            this.txtLugarNac_A.PasswordChar = '\0';
            this.txtLugarNac_A.SelectedText = "";
            this.txtLugarNac_A.SelectionLength = 0;
            this.txtLugarNac_A.SelectionStart = 0;
            this.txtLugarNac_A.Size = new System.Drawing.Size(267, 23);
            this.txtLugarNac_A.TabIndex = 35;
            this.txtLugarNac_A.UseSystemPasswordChar = false;
            this.txtLugarNac_A.Validating += new System.ComponentModel.CancelEventHandler(this.txtLugarNac_A_Validating);
            // 
            // txtAlergias_A
            // 
            this.txtAlergias_A.Depth = 0;
            this.txtAlergias_A.Hint = "Alergias";
            this.txtAlergias_A.Location = new System.Drawing.Point(13, 264);
            this.txtAlergias_A.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtAlergias_A.Name = "txtAlergias_A";
            this.txtAlergias_A.PasswordChar = '\0';
            this.txtAlergias_A.SelectedText = "";
            this.txtAlergias_A.SelectionLength = 0;
            this.txtAlergias_A.SelectionStart = 0;
            this.txtAlergias_A.Size = new System.Drawing.Size(262, 23);
            this.txtAlergias_A.TabIndex = 35;
            this.txtAlergias_A.UseSystemPasswordChar = false;
            this.txtAlergias_A.Validating += new System.ComponentModel.CancelEventHandler(this.txtAlergias_A_Validating);
            // 
            // txtTelEme_A
            // 
            this.txtTelEme_A.Depth = 0;
            this.txtTelEme_A.Hint = "Teléfono de emergencia";
            this.txtTelEme_A.Location = new System.Drawing.Point(13, 213);
            this.txtTelEme_A.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtTelEme_A.Name = "txtTelEme_A";
            this.txtTelEme_A.PasswordChar = '\0';
            this.txtTelEme_A.SelectedText = "";
            this.txtTelEme_A.SelectionLength = 0;
            this.txtTelEme_A.SelectionStart = 0;
            this.txtTelEme_A.Size = new System.Drawing.Size(264, 23);
            this.txtTelEme_A.TabIndex = 34;
            this.txtTelEme_A.UseSystemPasswordChar = false;
            this.txtTelEme_A.Validated += new System.EventHandler(this.txtTelEme_A_Validated);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox2.Controls.Add(this.txtCURP_A);
            this.groupBox2.Controls.Add(this.txtApMat_A);
            this.groupBox2.Controls.Add(this.txtApPat_A);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtNombre_A);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.groupBox2.Location = new System.Drawing.Point(200, 72);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBox2.Size = new System.Drawing.Size(301, 204);
            this.groupBox2.TabIndex = 48;
            this.groupBox2.TabStop = false;
            // 
            // txtCURP_A
            // 
            this.txtCURP_A.Depth = 0;
            this.txtCURP_A.Hint = "CURP";
            this.txtCURP_A.Location = new System.Drawing.Point(12, 169);
            this.txtCURP_A.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtCURP_A.Name = "txtCURP_A";
            this.txtCURP_A.PasswordChar = '\0';
            this.txtCURP_A.SelectedText = "";
            this.txtCURP_A.SelectionLength = 0;
            this.txtCURP_A.SelectionStart = 0;
            this.txtCURP_A.Size = new System.Drawing.Size(265, 23);
            this.txtCURP_A.TabIndex = 47;
            this.txtCURP_A.UseSystemPasswordChar = false;
            this.txtCURP_A.Validating += new System.ComponentModel.CancelEventHandler(this.txtCURP_A_Validating);
            // 
            // txtApMat_A
            // 
            this.txtApMat_A.Depth = 0;
            this.txtApMat_A.Hint = "Apellido Materno";
            this.txtApMat_A.Location = new System.Drawing.Point(12, 125);
            this.txtApMat_A.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtApMat_A.Name = "txtApMat_A";
            this.txtApMat_A.PasswordChar = '\0';
            this.txtApMat_A.SelectedText = "";
            this.txtApMat_A.SelectionLength = 0;
            this.txtApMat_A.SelectionStart = 0;
            this.txtApMat_A.Size = new System.Drawing.Size(265, 23);
            this.txtApMat_A.TabIndex = 46;
            this.txtApMat_A.UseSystemPasswordChar = false;
            this.txtApMat_A.Validating += new System.ComponentModel.CancelEventHandler(this.txtApMat_A_Validating);
            // 
            // txtApPat_A
            // 
            this.txtApPat_A.Depth = 0;
            this.txtApPat_A.Hint = "Apellido Paterno";
            this.txtApPat_A.Location = new System.Drawing.Point(12, 86);
            this.txtApPat_A.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtApPat_A.Name = "txtApPat_A";
            this.txtApPat_A.PasswordChar = '\0';
            this.txtApPat_A.SelectedText = "";
            this.txtApPat_A.SelectionLength = 0;
            this.txtApPat_A.SelectionStart = 0;
            this.txtApPat_A.Size = new System.Drawing.Size(265, 23);
            this.txtApPat_A.TabIndex = 45;
            this.txtApPat_A.UseSystemPasswordChar = false;
            this.txtApPat_A.Validating += new System.ComponentModel.CancelEventHandler(this.txtApPat_A_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.MenuText;
            this.label1.Location = new System.Drawing.Point(54, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(190, 26);
            this.label1.TabIndex = 44;
            this.label1.Text = "Nombre Completo";
            // 
            // txtNombre_A
            // 
            this.txtNombre_A.Depth = 0;
            this.txtNombre_A.Hint = "Nombre(s)";
            this.txtNombre_A.Location = new System.Drawing.Point(15, 43);
            this.txtNombre_A.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtNombre_A.Name = "txtNombre_A";
            this.txtNombre_A.PasswordChar = '\0';
            this.txtNombre_A.SelectedText = "";
            this.txtNombre_A.SelectionLength = 0;
            this.txtNombre_A.SelectionStart = 0;
            this.txtNombre_A.Size = new System.Drawing.Size(262, 23);
            this.txtNombre_A.TabIndex = 34;
            this.txtNombre_A.UseSystemPasswordChar = false;
            this.txtNombre_A.Validating += new System.ComponentModel.CancelEventHandler(this.txtNombre_A_Validating);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Dessert (100g serving)";
            this.columnHeader1.Width = 200;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Calories";
            this.columnHeader2.Width = 101;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Fat (g)";
            this.columnHeader3.Width = 94;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Protein (g)";
            this.columnHeader4.Width = 154;
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.SystemColors.WindowText;
            this.btnCerrar.Depth = 0;
            this.btnCerrar.Location = new System.Drawing.Point(906, 30);
            this.btnCerrar.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Primary = true;
            this.btnCerrar.Size = new System.Drawing.Size(130, 29);
            this.btnCerrar.TabIndex = 53;
            this.btnCerrar.Text = "Cerrar Sesión";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.BtnCerrar_Click);
            // 
            // btnPrincipal
            // 
            this.btnPrincipal.Depth = 0;
            this.btnPrincipal.Location = new System.Drawing.Point(853, 325);
            this.btnPrincipal.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnPrincipal.Name = "btnPrincipal";
            this.btnPrincipal.Primary = true;
            this.btnPrincipal.Size = new System.Drawing.Size(175, 50);
            this.btnPrincipal.TabIndex = 53;
            this.btnPrincipal.Text = "Volver al Menú Principal";
            this.btnPrincipal.UseVisualStyleBackColor = true;
            this.btnPrincipal.Click += new System.EventHandler(this.BtnPrincipal_Click);
            // 
            // Eliminar
            // 
            this.Eliminar.Depth = 0;
            this.Eliminar.Location = new System.Drawing.Point(0, 365);
            this.Eliminar.MouseState = MaterialSkin.MouseState.HOVER;
            this.Eliminar.Name = "Eliminar";
            this.Eliminar.Primary = true;
            this.Eliminar.Size = new System.Drawing.Size(178, 111);
            this.Eliminar.TabIndex = 55;
            this.Eliminar.Text = "Eliminar";
            this.Eliminar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Eliminar.UseVisualStyleBackColor = true;
            this.Eliminar.Click += new System.EventHandler(this.Eliminar_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.Depth = 0;
            this.btnModificar.Location = new System.Drawing.Point(0, 263);
            this.btnModificar.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Primary = true;
            this.btnModificar.Size = new System.Drawing.Size(178, 112);
            this.btnModificar.TabIndex = 56;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Depth = 0;
            this.btnBuscar.Location = new System.Drawing.Point(0, 170);
            this.btnBuscar.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Primary = true;
            this.btnBuscar.Size = new System.Drawing.Size(178, 106);
            this.btnBuscar.TabIndex = 57;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.BtnBuscar_Click);
            // 
            // btnInscripcion
            // 
            this.btnInscripcion.Depth = 0;
            this.btnInscripcion.Location = new System.Drawing.Point(0, 69);
            this.btnInscripcion.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnInscripcion.Name = "btnInscripcion";
            this.btnInscripcion.Primary = true;
            this.btnInscripcion.Size = new System.Drawing.Size(178, 102);
            this.btnInscripcion.TabIndex = 58;
            this.btnInscripcion.Text = "Inscripción";
            this.btnInscripcion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInscripcion.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox3.Controls.Add(this.txtCP_A);
            this.groupBox3.Controls.Add(this.txtColonia_C);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.txtNum_A);
            this.groupBox3.Controls.Add(this.txtCalle_A);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.groupBox3.Location = new System.Drawing.Point(200, 282);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBox3.Size = new System.Drawing.Size(309, 194);
            this.groupBox3.TabIndex = 59;
            this.groupBox3.TabStop = false;
            // 
            // txtCP_A
            // 
            this.txtCP_A.Depth = 0;
            this.txtCP_A.Hint = "Código Postal";
            this.txtCP_A.Location = new System.Drawing.Point(32, 154);
            this.txtCP_A.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtCP_A.Name = "txtCP_A";
            this.txtCP_A.PasswordChar = '\0';
            this.txtCP_A.SelectedText = "";
            this.txtCP_A.SelectionLength = 0;
            this.txtCP_A.SelectionStart = 0;
            this.txtCP_A.Size = new System.Drawing.Size(245, 23);
            this.txtCP_A.TabIndex = 46;
            this.txtCP_A.UseSystemPasswordChar = false;
            this.txtCP_A.Validating += new System.ComponentModel.CancelEventHandler(this.txtCP_A_Validating);
            // 
            // txtColonia_C
            // 
            this.txtColonia_C.Depth = 0;
            this.txtColonia_C.Hint = "Colonia";
            this.txtColonia_C.Location = new System.Drawing.Point(32, 118);
            this.txtColonia_C.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtColonia_C.Name = "txtColonia_C";
            this.txtColonia_C.PasswordChar = '\0';
            this.txtColonia_C.SelectedText = "";
            this.txtColonia_C.SelectionLength = 0;
            this.txtColonia_C.SelectionStart = 0;
            this.txtColonia_C.Size = new System.Drawing.Size(245, 23);
            this.txtColonia_C.TabIndex = 45;
            this.txtColonia_C.UseSystemPasswordChar = false;
            this.txtColonia_C.Validating += new System.ComponentModel.CancelEventHandler(this.txtColonia_C_Validating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.MenuText;
            this.label3.Location = new System.Drawing.Point(103, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 26);
            this.label3.TabIndex = 44;
            this.label3.Text = "Dirección";
            // 
            // txtNum_A
            // 
            this.txtNum_A.Depth = 0;
            this.txtNum_A.Hint = "Número";
            this.txtNum_A.Location = new System.Drawing.Point(32, 83);
            this.txtNum_A.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtNum_A.Name = "txtNum_A";
            this.txtNum_A.PasswordChar = '\0';
            this.txtNum_A.SelectedText = "";
            this.txtNum_A.SelectionLength = 0;
            this.txtNum_A.SelectionStart = 0;
            this.txtNum_A.Size = new System.Drawing.Size(245, 23);
            this.txtNum_A.TabIndex = 35;
            this.txtNum_A.UseSystemPasswordChar = false;
            this.txtNum_A.Validating += new System.ComponentModel.CancelEventHandler(this.txtNum_A_Validating);
            // 
            // txtCalle_A
            // 
            this.txtCalle_A.Depth = 0;
            this.txtCalle_A.Hint = "Calle";
            this.txtCalle_A.Location = new System.Drawing.Point(32, 45);
            this.txtCalle_A.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtCalle_A.Name = "txtCalle_A";
            this.txtCalle_A.PasswordChar = '\0';
            this.txtCalle_A.SelectedText = "";
            this.txtCalle_A.SelectionLength = 0;
            this.txtCalle_A.SelectionStart = 0;
            this.txtCalle_A.Size = new System.Drawing.Size(245, 23);
            this.txtCalle_A.TabIndex = 34;
            this.txtCalle_A.UseSystemPasswordChar = false;
            this.txtCalle_A.Validating += new System.ComponentModel.CancelEventHandler(this.txtCalle_A_Validating);
            // 
            // btnSiguiente
            // 
            this.btnSiguiente.Depth = 0;
            this.btnSiguiente.Location = new System.Drawing.Point(853, 158);
            this.btnSiguiente.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnSiguiente.Name = "btnSiguiente";
            this.btnSiguiente.Primary = true;
            this.btnSiguiente.Size = new System.Drawing.Size(175, 50);
            this.btnSiguiente.TabIndex = 60;
            this.btnSiguiente.Text = "Siguiente";
            this.btnSiguiente.UseVisualStyleBackColor = true;
            this.btnSiguiente.Click += new System.EventHandler(this.BtnSiguiente_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // Alumno
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 482);
            this.Controls.Add(this.btnSiguiente);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnInscripcion);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.btnModificar);
            this.Controls.Add(this.Eliminar);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.btnPrincipal);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox2);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Alumno";
            this.Text = "Alumnos";
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.GroupBox groupBox2;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtApMat_A;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtApPat_A;
        private System.Windows.Forms.Label label1;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtNombre_A;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label5;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtAlergias_A;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtLugarNac_A;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtTelEme_A;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtCURP_A;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtEdad_A;
        private MaterialSkin.Controls.MaterialRaisedButton btnCerrar;
        private MaterialSkin.Controls.MaterialRaisedButton btnPrincipal;
        private MaterialSkin.Controls.MaterialRaisedButton Eliminar;
        private MaterialSkin.Controls.MaterialRaisedButton btnModificar;
        private MaterialSkin.Controls.MaterialRaisedButton btnBuscar;
        private MaterialSkin.Controls.MaterialRaisedButton btnInscripcion;
        private System.Windows.Forms.GroupBox groupBox3;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtCP_A;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtColonia_C;
        private System.Windows.Forms.Label label3;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtNum_A;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtCalle_A;
        private MaterialSkin.Controls.MaterialRaisedButton btnSiguiente;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label2;
    }
}