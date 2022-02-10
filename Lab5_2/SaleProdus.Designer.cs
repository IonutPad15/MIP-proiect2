
namespace Lab5_2
{
    partial class SaleProdus
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
            this.lblProdus = new System.Windows.Forms.Label();
            this.lblCantitate = new System.Windows.Forms.Label();
            this.txtCantitate = new System.Windows.Forms.TextBox();
            this.btnSale = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblProdus
            // 
            this.lblProdus.AutoSize = true;
            this.lblProdus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProdus.Location = new System.Drawing.Point(111, 64);
            this.lblProdus.Name = "lblProdus";
            this.lblProdus.Size = new System.Drawing.Size(62, 20);
            this.lblProdus.TabIndex = 0;
            this.lblProdus.Text = "Produs";
            // 
            // lblCantitate
            // 
            this.lblCantitate.AutoSize = true;
            this.lblCantitate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCantitate.Location = new System.Drawing.Point(111, 97);
            this.lblCantitate.Name = "lblCantitate";
            this.lblCantitate.Size = new System.Drawing.Size(76, 20);
            this.lblCantitate.TabIndex = 1;
            this.lblCantitate.Text = "Cantitate";
            // 
            // txtCantitate
            // 
            this.txtCantitate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCantitate.Location = new System.Drawing.Point(115, 151);
            this.txtCantitate.Name = "txtCantitate";
            this.txtCantitate.Size = new System.Drawing.Size(100, 27);
            this.txtCantitate.TabIndex = 2;
            // 
            // btnSale
            // 
            this.btnSale.BackColor = System.Drawing.Color.MediumOrchid;
            this.btnSale.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSale.Location = new System.Drawing.Point(266, 151);
            this.btnSale.Name = "btnSale";
            this.btnSale.Size = new System.Drawing.Size(127, 28);
            this.btnSale.TabIndex = 3;
            this.btnSale.Text = "Vanzare";
            this.btnSale.UseVisualStyleBackColor = false;
            this.btnSale.Click += new System.EventHandler(this.btnSale_Click);
            // 
            // SaleProdus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkOrange;
            this.ClientSize = new System.Drawing.Size(490, 198);
            this.Controls.Add(this.btnSale);
            this.Controls.Add(this.txtCantitate);
            this.Controls.Add(this.lblCantitate);
            this.Controls.Add(this.lblProdus);
            this.MaximizeBox = false;
            this.Name = "SaleProdus";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SaleProdus";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblProdus;
        private System.Windows.Forms.Label lblCantitate;
        private System.Windows.Forms.TextBox txtCantitate;
        private System.Windows.Forms.Button btnSale;
    }
}