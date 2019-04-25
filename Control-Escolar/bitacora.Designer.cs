namespace Control_Escolar
{
    partial class bitacora
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(bitacora));
            this.btnPrincipal = new MaterialSkin.Controls.MaterialRaisedButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.GenerarPDF = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnCerrar = new MaterialSkin.Controls.MaterialRaisedButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.padre = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPrincipal
            // 
            this.btnPrincipal.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPrincipal.Depth = 0;
            this.btnPrincipal.Location = new System.Drawing.Point(196, 364);
            this.btnPrincipal.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnPrincipal.Name = "btnPrincipal";
            this.btnPrincipal.Primary = true;
            this.btnPrincipal.Size = new System.Drawing.Size(127, 46);
            this.btnPrincipal.TabIndex = 0;
            this.btnPrincipal.Text = "Volver al Menú Principal";
            this.btnPrincipal.UseVisualStyleBackColor = true;
            this.btnPrincipal.Click += new System.EventHandler(this.btnPrincipal_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(86, 123);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridView1.Size = new System.Drawing.Size(613, 221);
            this.dataGridView1.TabIndex = 1;
            // 
            // GenerarPDF
            // 
            this.GenerarPDF.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.GenerarPDF.Depth = 0;
            this.GenerarPDF.Location = new System.Drawing.Point(478, 364);
            this.GenerarPDF.MouseState = MaterialSkin.MouseState.HOVER;
            this.GenerarPDF.Name = "GenerarPDF";
            this.GenerarPDF.Primary = true;
            this.GenerarPDF.Size = new System.Drawing.Size(127, 46);
            this.GenerarPDF.TabIndex = 2;
            this.GenerarPDF.Text = "Generar PDF";
            this.GenerarPDF.UseVisualStyleBackColor = true;
            this.GenerarPDF.Click += new System.EventHandler(this.GenerarPDF_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.BackColor = System.Drawing.SystemColors.WindowText;
            this.btnCerrar.Depth = 0;
            this.btnCerrar.Location = new System.Drawing.Point(647, 29);
            this.btnCerrar.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Primary = true;
            this.btnCerrar.Size = new System.Drawing.Size(124, 31);
            this.btnCerrar.TabIndex = 3;
            this.btnCerrar.Text = "Cerrar Sesión";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.BtnCerrar_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.Image = global::Control_Escolar.Properties.Resources.icons8_exportar_pdf_48;
            this.pictureBox1.Location = new System.Drawing.Point(422, 361);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(50, 49);
            this.pictureBox1.TabIndex = 74;
            this.pictureBox1.TabStop = false;
            // 
            // padre
            // 
            this.padre.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.padre.AutoSize = true;
            this.padre.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.padre.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.padre.ForeColor = System.Drawing.SystemColors.MenuText;
            this.padre.Location = new System.Drawing.Point(352, 78);
            this.padre.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.padre.Name = "padre";
            this.padre.Size = new System.Drawing.Size(92, 26);
            this.padre.TabIndex = 75;
            this.padre.Text = "Bitácora";
            // 
            // bitacora
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 450);
            this.Controls.Add(this.padre);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.GenerarPDF);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnPrincipal);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "bitacora";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bitácora";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Bitacora_FormClosed);
            this.Load += new System.EventHandler(this.bitacora_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialRaisedButton btnPrincipal;
        private System.Windows.Forms.DataGridView dataGridView1;
        private MaterialSkin.Controls.MaterialRaisedButton GenerarPDF;
        private MaterialSkin.Controls.MaterialRaisedButton btnCerrar;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label padre;
    }
}