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
            this.Eliminar = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnPrincipal = new MaterialSkin.Controls.MaterialRaisedButton();
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
            // Eliminar
            // 
            this.Eliminar.Depth = 0;
            this.Eliminar.Location = new System.Drawing.Point(0, 360);
            this.Eliminar.MouseState = MaterialSkin.MouseState.HOVER;
            this.Eliminar.Name = "Eliminar";
            this.Eliminar.Primary = true;
            this.Eliminar.Size = new System.Drawing.Size(178, 111);
            this.Eliminar.TabIndex = 63;
            this.Eliminar.Text = "Eliminar";
            this.Eliminar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Eliminar.UseVisualStyleBackColor = true;
            // 
            // btnPrincipal
            // 
            this.btnPrincipal.Depth = 0;
            this.btnPrincipal.Location = new System.Drawing.Point(793, 411);
            this.btnPrincipal.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnPrincipal.Name = "btnPrincipal";
            this.btnPrincipal.Primary = true;
            this.btnPrincipal.Size = new System.Drawing.Size(175, 50);
            this.btnPrincipal.TabIndex = 67;
            this.btnPrincipal.Text = "Volver al Menú Principal";
            this.btnPrincipal.UseVisualStyleBackColor = true;
            this.btnPrincipal.Click += new System.EventHandler(this.BtnPrincipal_Click);
            // 
            // Buscar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 473);
            this.Controls.Add(this.btnPrincipal);
            this.Controls.Add(this.btnInscripcion);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.btnModificar);
            this.Controls.Add(this.Eliminar);
            this.Controls.Add(this.btnCerrar);
            this.Name = "Buscar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Buscar";
            this.Load += new System.EventHandler(this.Buscar_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialRaisedButton btnCerrar;
        private MaterialSkin.Controls.MaterialRaisedButton btnInscripcion;
        private MaterialSkin.Controls.MaterialRaisedButton btnBuscar;
        private MaterialSkin.Controls.MaterialRaisedButton btnModificar;
        private MaterialSkin.Controls.MaterialRaisedButton Eliminar;
        private MaterialSkin.Controls.MaterialRaisedButton btnPrincipal;
    }
}