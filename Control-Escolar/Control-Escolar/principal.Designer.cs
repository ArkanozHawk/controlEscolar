namespace Control_Escolar
{
    partial class principal
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
            this.lblBienvenida = new System.Windows.Forms.Label();
            this.btnBitacora = new MaterialSkin.Controls.MaterialRaisedButton();
            this.SuspendLayout();
            // 
            // btnCerrar
            // 
            this.btnCerrar.Depth = 0;
            this.btnCerrar.Location = new System.Drawing.Point(404, 304);
            this.btnCerrar.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Primary = true;
            this.btnCerrar.Size = new System.Drawing.Size(178, 60);
            this.btnCerrar.TabIndex = 0;
            this.btnCerrar.Text = "Cerrar Sesión";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // lblBienvenida
            // 
            this.lblBienvenida.AutoSize = true;
            this.lblBienvenida.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblBienvenida.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBienvenida.ForeColor = System.Drawing.SystemColors.MenuText;
            this.lblBienvenida.Location = new System.Drawing.Point(172, 135);
            this.lblBienvenida.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblBienvenida.Name = "lblBienvenida";
            this.lblBienvenida.Size = new System.Drawing.Size(446, 26);
            this.lblBienvenida.TabIndex = 45;
            this.lblBienvenida.Text = "Hola \"Usuario\" Bienvenido al Control Escolar";
            // 
            // btnBitacora
            // 
            this.btnBitacora.Depth = 0;
            this.btnBitacora.Location = new System.Drawing.Point(177, 304);
            this.btnBitacora.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnBitacora.Name = "btnBitacora";
            this.btnBitacora.Primary = true;
            this.btnBitacora.Size = new System.Drawing.Size(178, 60);
            this.btnBitacora.TabIndex = 46;
            this.btnBitacora.Text = "Bitacora";
            this.btnBitacora.UseVisualStyleBackColor = true;
            this.btnBitacora.Click += new System.EventHandler(this.btnBitacora_Click);
            // 
            // principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 500);
            this.Controls.Add(this.btnBitacora);
            this.Controls.Add(this.lblBienvenida);
            this.Controls.Add(this.btnCerrar);
            this.MaximizeBox = false;
            this.Name = "principal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "principal";
            this.Load += new System.EventHandler(this.principal_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialRaisedButton btnCerrar;
        private System.Windows.Forms.Label lblBienvenida;
        private MaterialSkin.Controls.MaterialRaisedButton btnBitacora;
    }
}