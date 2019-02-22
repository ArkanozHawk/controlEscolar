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
            this.btnPrincipal = new MaterialSkin.Controls.MaterialRaisedButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.GenerarPDF = new MaterialSkin.Controls.MaterialRaisedButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPrincipal
            // 
            this.btnPrincipal.Depth = 0;
            this.btnPrincipal.Location = new System.Drawing.Point(343, 448);
            this.btnPrincipal.Margin = new System.Windows.Forms.Padding(4);
            this.btnPrincipal.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnPrincipal.Name = "btnPrincipal";
            this.btnPrincipal.Primary = true;
            this.btnPrincipal.Size = new System.Drawing.Size(169, 57);
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
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(303, 145);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridView1.Size = new System.Drawing.Size(504, 272);
            this.dataGridView1.TabIndex = 1;
            // 
            // GenerarPDF
            // 
            this.GenerarPDF.Depth = 0;
            this.GenerarPDF.Location = new System.Drawing.Point(583, 448);
            this.GenerarPDF.Margin = new System.Windows.Forms.Padding(4);
            this.GenerarPDF.MouseState = MaterialSkin.MouseState.HOVER;
            this.GenerarPDF.Name = "GenerarPDF";
            this.GenerarPDF.Primary = true;
            this.GenerarPDF.Size = new System.Drawing.Size(169, 57);
            this.GenerarPDF.TabIndex = 2;
            this.GenerarPDF.Text = "Generar PDF";
            this.GenerarPDF.UseVisualStyleBackColor = true;
            this.GenerarPDF.Click += new System.EventHandler(this.GenerarPDF_Click);
            // 
            // bitacora
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1046, 554);
            this.Controls.Add(this.GenerarPDF);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnPrincipal);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "bitacora";
            this.Text = "bitacora";
            this.Load += new System.EventHandler(this.bitacora_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialRaisedButton btnPrincipal;
        private System.Windows.Forms.DataGridView dataGridView1;
        private MaterialSkin.Controls.MaterialRaisedButton GenerarPDF;
    }
}