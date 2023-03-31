namespace CatJAM
{
    partial class Form1
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
            if (disposing && (components != null)) {
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
            this.thelabel = new CatJAM.OutlineLabel();
            this.SuspendLayout();
            // 
            // thelabel
            // 
            this.thelabel.AutoSize = true;
            this.thelabel.BorderColor = System.Drawing.Color.Black;
            this.thelabel.BorderSize = 4F;
            this.thelabel.Font = new System.Drawing.Font("Comic Sans MS", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.thelabel.ForeColor = System.Drawing.Color.White;
            this.thelabel.Location = new System.Drawing.Point(0, 0);
            this.thelabel.Name = "thelabel";
            this.thelabel.Size = new System.Drawing.Size(176, 76);
            this.thelabel.TabIndex = 0;
            this.thelabel.Text = "label1";
            this.thelabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.thelabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Form1";
            this.MouseEnter += new System.EventHandler(this.Form1_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.Form1_MouseLeave);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OutlineLabel thelabel;
    }
}

