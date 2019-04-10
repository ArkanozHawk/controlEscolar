namespace Control_Escolar
{
    partial class Modificar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Modificar));
            this.btnPrincipal = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnInscripcion = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnBuscar = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnModificar = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnCerrar = new MaterialSkin.Controls.MaterialRaisedButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtCP_A = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.txtColonia_C = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNum_A = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.txtCalle_A = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.BoxGenero = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.RadFemenino = new MaterialSkin.Controls.MaterialRadioButton();
            this.RadMasculino = new MaterialSkin.Controls.MaterialRadioButton();
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
            this.btnSiguiente = new MaterialSkin.Controls.MaterialRaisedButton();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox3.SuspendLayout();
            this.BoxGenero.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPrincipal
            // 
            this.btnPrincipal.Depth = 0;
            this.btnPrincipal.Location = new System.Drawing.Point(0, 371);
            this.btnPrincipal.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnPrincipal.Name = "btnPrincipal";
            this.btnPrincipal.Primary = true;
            this.btnPrincipal.Size = new System.Drawing.Size(178, 101);
            this.btnPrincipal.TabIndex = 72;
            this.btnPrincipal.Text = "Volver al Menú Principal";
            this.btnPrincipal.UseVisualStyleBackColor = true;
            this.btnPrincipal.Click += new System.EventHandler(this.btnPrincipal_Click);
            // 
            // btnInscripcion
            // 
            this.btnInscripcion.Depth = 0;
            this.btnInscripcion.Location = new System.Drawing.Point(0, 65);
            this.btnInscripcion.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnInscripcion.Name = "btnInscripcion";
            this.btnInscripcion.Primary = true;
            this.btnInscripcion.Size = new System.Drawing.Size(178, 102);
            this.btnInscripcion.TabIndex = 71;
            this.btnInscripcion.Text = "Inscripción";
            this.btnInscripcion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInscripcion.UseVisualStyleBackColor = true;
            this.btnInscripcion.Click += new System.EventHandler(this.btnInscripcion_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Depth = 0;
            this.btnBuscar.Location = new System.Drawing.Point(0, 166);
            this.btnBuscar.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Primary = true;
            this.btnBuscar.Size = new System.Drawing.Size(178, 106);
            this.btnBuscar.TabIndex = 70;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.Depth = 0;
            this.btnModificar.Location = new System.Drawing.Point(0, 259);
            this.btnModificar.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Primary = true;
            this.btnModificar.Size = new System.Drawing.Size(178, 112);
            this.btnModificar.TabIndex = 69;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.BackColor = System.Drawing.SystemColors.WindowText;
            this.btnCerrar.Depth = 0;
            this.btnCerrar.Location = new System.Drawing.Point(894, 26);
            this.btnCerrar.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Primary = true;
            this.btnCerrar.Size = new System.Drawing.Size(122, 35);
            this.btnCerrar.TabIndex = 73;
            this.btnCerrar.Text = "Cerrar Sesión";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox3.Controls.Add(this.txtCP_A);
            this.groupBox3.Controls.Add(this.txtColonia_C);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.txtNum_A);
            this.groupBox3.Controls.Add(this.txtCalle_A);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.groupBox3.Location = new System.Drawing.Point(196, 278);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBox3.Size = new System.Drawing.Size(301, 194);
            this.groupBox3.TabIndex = 76;
            this.groupBox3.TabStop = false;
            // 
            // txtCP_A
            // 
            this.txtCP_A.Depth = 0;
            this.txtCP_A.Hint = "Código Postal";
            this.txtCP_A.Location = new System.Drawing.Point(12, 141);
            this.txtCP_A.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtCP_A.Name = "txtCP_A";
            this.txtCP_A.PasswordChar = '\0';
            this.txtCP_A.SelectedText = "";
            this.txtCP_A.SelectionLength = 0;
            this.txtCP_A.SelectionStart = 0;
            this.txtCP_A.Size = new System.Drawing.Size(262, 23);
            this.txtCP_A.TabIndex = 46;
            this.txtCP_A.UseSystemPasswordChar = false;
            // 
            // txtColonia_C
            // 
            this.txtColonia_C.Depth = 0;
            this.txtColonia_C.Hint = "Colonia";
            this.txtColonia_C.Location = new System.Drawing.Point(12, 105);
            this.txtColonia_C.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtColonia_C.Name = "txtColonia_C";
            this.txtColonia_C.PasswordChar = '\0';
            this.txtColonia_C.SelectedText = "";
            this.txtColonia_C.SelectionLength = 0;
            this.txtColonia_C.SelectionStart = 0;
            this.txtColonia_C.Size = new System.Drawing.Size(262, 23);
            this.txtColonia_C.TabIndex = 45;
            this.txtColonia_C.UseSystemPasswordChar = false;
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
            this.txtNum_A.Location = new System.Drawing.Point(12, 70);
            this.txtNum_A.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtNum_A.Name = "txtNum_A";
            this.txtNum_A.PasswordChar = '\0';
            this.txtNum_A.SelectedText = "";
            this.txtNum_A.SelectionLength = 0;
            this.txtNum_A.SelectionStart = 0;
            this.txtNum_A.Size = new System.Drawing.Size(262, 23);
            this.txtNum_A.TabIndex = 35;
            this.txtNum_A.UseSystemPasswordChar = false;
            // 
            // txtCalle_A
            // 
            this.txtCalle_A.Depth = 0;
            this.txtCalle_A.Hint = "Calle";
            this.txtCalle_A.Location = new System.Drawing.Point(12, 32);
            this.txtCalle_A.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtCalle_A.Name = "txtCalle_A";
            this.txtCalle_A.PasswordChar = '\0';
            this.txtCalle_A.SelectedText = "";
            this.txtCalle_A.SelectionLength = 0;
            this.txtCalle_A.SelectionStart = 0;
            this.txtCalle_A.Size = new System.Drawing.Size(262, 23);
            this.txtCalle_A.TabIndex = 34;
            this.txtCalle_A.UseSystemPasswordChar = false;
            // 
            // BoxGenero
            // 
            this.BoxGenero.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.BoxGenero.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.BoxGenero.Controls.Add(this.label4);
            this.BoxGenero.Controls.Add(this.RadFemenino);
            this.BoxGenero.Controls.Add(this.RadMasculino);
            this.BoxGenero.Controls.Add(this.label2);
            this.BoxGenero.Controls.Add(this.dateTimePicker1);
            this.BoxGenero.Controls.Add(this.txtEdad_A);
            this.BoxGenero.Controls.Add(this.label5);
            this.BoxGenero.Controls.Add(this.txtLugarNac_A);
            this.BoxGenero.Controls.Add(this.txtAlergias_A);
            this.BoxGenero.Controls.Add(this.txtTelEme_A);
            this.BoxGenero.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.BoxGenero.Location = new System.Drawing.Point(524, 68);
            this.BoxGenero.Name = "BoxGenero";
            this.BoxGenero.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.BoxGenero.Size = new System.Drawing.Size(304, 404);
            this.BoxGenero.TabIndex = 75;
            this.BoxGenero.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label4.Location = new System.Drawing.Point(112, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 20);
            this.label4.TabIndex = 65;
            this.label4.Text = "Genero";
            // 
            // RadFemenino
            // 
            this.RadFemenino.AutoSize = true;
            this.RadFemenino.Depth = 0;
            this.RadFemenino.Font = new System.Drawing.Font("Roboto", 10F);
            this.RadFemenino.Location = new System.Drawing.Point(185, 162);
            this.RadFemenino.Margin = new System.Windows.Forms.Padding(0);
            this.RadFemenino.MouseLocation = new System.Drawing.Point(-1, -1);
            this.RadFemenino.MouseState = MaterialSkin.MouseState.HOVER;
            this.RadFemenino.Name = "RadFemenino";
            this.RadFemenino.Ripple = true;
            this.RadFemenino.Size = new System.Drawing.Size(90, 30);
            this.RadFemenino.TabIndex = 64;
            this.RadFemenino.TabStop = true;
            this.RadFemenino.Text = "Femenino";
            this.RadFemenino.UseVisualStyleBackColor = true;
            // 
            // RadMasculino
            // 
            this.RadMasculino.AutoSize = true;
            this.RadMasculino.Depth = 0;
            this.RadMasculino.Font = new System.Drawing.Font("Roboto", 10F);
            this.RadMasculino.Location = new System.Drawing.Point(13, 162);
            this.RadMasculino.Margin = new System.Windows.Forms.Padding(0);
            this.RadMasculino.MouseLocation = new System.Drawing.Point(-1, -1);
            this.RadMasculino.MouseState = MaterialSkin.MouseState.HOVER;
            this.RadMasculino.Name = "RadMasculino";
            this.RadMasculino.Ripple = true;
            this.RadMasculino.Size = new System.Drawing.Size(93, 30);
            this.RadMasculino.TabIndex = 63;
            this.RadMasculino.TabStop = true;
            this.RadMasculino.Text = "Masculino";
            this.RadMasculino.UseVisualStyleBackColor = true;
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
            this.txtEdad_A.Location = new System.Drawing.Point(13, 255);
            this.txtEdad_A.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtEdad_A.Name = "txtEdad_A";
            this.txtEdad_A.PasswordChar = '\0';
            this.txtEdad_A.SelectedText = "";
            this.txtEdad_A.SelectionLength = 0;
            this.txtEdad_A.SelectionStart = 0;
            this.txtEdad_A.Size = new System.Drawing.Size(264, 23);
            this.txtEdad_A.TabIndex = 45;
            this.txtEdad_A.UseSystemPasswordChar = false;
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
            this.txtLugarNac_A.Location = new System.Drawing.Point(13, 213);
            this.txtLugarNac_A.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtLugarNac_A.Name = "txtLugarNac_A";
            this.txtLugarNac_A.PasswordChar = '\0';
            this.txtLugarNac_A.SelectedText = "";
            this.txtLugarNac_A.SelectionLength = 0;
            this.txtLugarNac_A.SelectionStart = 0;
            this.txtLugarNac_A.Size = new System.Drawing.Size(267, 23);
            this.txtLugarNac_A.TabIndex = 35;
            this.txtLugarNac_A.UseSystemPasswordChar = false;
            // 
            // txtAlergias_A
            // 
            this.txtAlergias_A.Depth = 0;
            this.txtAlergias_A.Hint = "Alergias";
            this.txtAlergias_A.Location = new System.Drawing.Point(13, 351);
            this.txtAlergias_A.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtAlergias_A.Name = "txtAlergias_A";
            this.txtAlergias_A.PasswordChar = '\0';
            this.txtAlergias_A.SelectedText = "";
            this.txtAlergias_A.SelectionLength = 0;
            this.txtAlergias_A.SelectionStart = 0;
            this.txtAlergias_A.Size = new System.Drawing.Size(262, 23);
            this.txtAlergias_A.TabIndex = 35;
            this.txtAlergias_A.UseSystemPasswordChar = false;
            // 
            // txtTelEme_A
            // 
            this.txtTelEme_A.Depth = 0;
            this.txtTelEme_A.Hint = "Teléfono de emergencia";
            this.txtTelEme_A.Location = new System.Drawing.Point(13, 306);
            this.txtTelEme_A.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtTelEme_A.Name = "txtTelEme_A";
            this.txtTelEme_A.PasswordChar = '\0';
            this.txtTelEme_A.SelectedText = "";
            this.txtTelEme_A.SelectionLength = 0;
            this.txtTelEme_A.SelectionStart = 0;
            this.txtTelEme_A.Size = new System.Drawing.Size(264, 23);
            this.txtTelEme_A.TabIndex = 34;
            this.txtTelEme_A.UseSystemPasswordChar = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox2.Controls.Add(this.txtCURP_A);
            this.groupBox2.Controls.Add(this.txtApMat_A);
            this.groupBox2.Controls.Add(this.txtApPat_A);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtNombre_A);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.groupBox2.Location = new System.Drawing.Point(196, 68);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBox2.Size = new System.Drawing.Size(301, 204);
            this.groupBox2.TabIndex = 74;
            this.groupBox2.TabStop = false;
            // 
            // txtCURP_A
            // 
            this.txtCURP_A.Depth = 0;
            this.txtCURP_A.Hint = "CURP";
            this.txtCURP_A.Location = new System.Drawing.Point(12, 42);
            this.txtCURP_A.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtCURP_A.Name = "txtCURP_A";
            this.txtCURP_A.PasswordChar = '\0';
            this.txtCURP_A.SelectedText = "";
            this.txtCURP_A.SelectionLength = 0;
            this.txtCURP_A.SelectionStart = 0;
            this.txtCURP_A.Size = new System.Drawing.Size(265, 23);
            this.txtCURP_A.TabIndex = 47;
            this.txtCURP_A.UseSystemPasswordChar = false;
            // 
            // txtApMat_A
            // 
            this.txtApMat_A.Depth = 0;
            this.txtApMat_A.Hint = "Apellido Materno";
            this.txtApMat_A.Location = new System.Drawing.Point(12, 169);
            this.txtApMat_A.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtApMat_A.Name = "txtApMat_A";
            this.txtApMat_A.PasswordChar = '\0';
            this.txtApMat_A.SelectedText = "";
            this.txtApMat_A.SelectionLength = 0;
            this.txtApMat_A.SelectionStart = 0;
            this.txtApMat_A.Size = new System.Drawing.Size(265, 23);
            this.txtApMat_A.TabIndex = 46;
            this.txtApMat_A.UseSystemPasswordChar = false;
            // 
            // txtApPat_A
            // 
            this.txtApPat_A.Depth = 0;
            this.txtApPat_A.Hint = "Apellido Paterno";
            this.txtApPat_A.Location = new System.Drawing.Point(12, 125);
            this.txtApPat_A.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtApPat_A.Name = "txtApPat_A";
            this.txtApPat_A.PasswordChar = '\0';
            this.txtApPat_A.SelectedText = "";
            this.txtApPat_A.SelectionLength = 0;
            this.txtApPat_A.SelectionStart = 0;
            this.txtApPat_A.Size = new System.Drawing.Size(265, 23);
            this.txtApPat_A.TabIndex = 45;
            this.txtApPat_A.UseSystemPasswordChar = false;
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
            this.txtNombre_A.Location = new System.Drawing.Point(12, 86);
            this.txtNombre_A.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtNombre_A.Name = "txtNombre_A";
            this.txtNombre_A.PasswordChar = '\0';
            this.txtNombre_A.SelectedText = "";
            this.txtNombre_A.SelectionLength = 0;
            this.txtNombre_A.SelectionStart = 0;
            this.txtNombre_A.Size = new System.Drawing.Size(262, 23);
            this.txtNombre_A.TabIndex = 34;
            this.txtNombre_A.UseSystemPasswordChar = false;
            // 
            // btnSiguiente
            // 
            this.btnSiguiente.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSiguiente.Depth = 0;
            this.btnSiguiente.Location = new System.Drawing.Point(841, 230);
            this.btnSiguiente.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnSiguiente.Name = "btnSiguiente";
            this.btnSiguiente.Primary = true;
            this.btnSiguiente.Size = new System.Drawing.Size(175, 50);
            this.btnSiguiente.TabIndex = 77;
            this.btnSiguiente.Text = "Siguiente";
            this.btnSiguiente.UseVisualStyleBackColor = true;
            this.btnSiguiente.Click += new System.EventHandler(this.btnSiguiente_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // Modificar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 476);
            this.Controls.Add(this.btnSiguiente);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.BoxGenero);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.btnPrincipal);
            this.Controls.Add(this.btnInscripcion);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.btnModificar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Modificar";
            this.Text = "Modificar";
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.BoxGenero.ResumeLayout(false);
            this.BoxGenero.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialRaisedButton btnPrincipal;
        private MaterialSkin.Controls.MaterialRaisedButton btnInscripcion;
        private MaterialSkin.Controls.MaterialRaisedButton btnBuscar;
        private MaterialSkin.Controls.MaterialRaisedButton btnModificar;
        private MaterialSkin.Controls.MaterialRaisedButton btnCerrar;
        private System.Windows.Forms.GroupBox groupBox3;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtCP_A;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtColonia_C;
        private System.Windows.Forms.Label label3;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtNum_A;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtCalle_A;
        private System.Windows.Forms.GroupBox BoxGenero;
        private System.Windows.Forms.Label label4;
        private MaterialSkin.Controls.MaterialRadioButton RadFemenino;
        private MaterialSkin.Controls.MaterialRadioButton RadMasculino;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtEdad_A;
        private System.Windows.Forms.Label label5;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtLugarNac_A;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtAlergias_A;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtTelEme_A;
        private System.Windows.Forms.GroupBox groupBox2;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtCURP_A;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtApMat_A;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtApPat_A;
        private System.Windows.Forms.Label label1;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtNombre_A;
        private MaterialSkin.Controls.MaterialRaisedButton btnSiguiente;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}