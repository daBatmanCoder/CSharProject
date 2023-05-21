namespace FrontDamka
{
    partial class Damka
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
            this.labelPlayer1 = new System.Windows.Forms.Label();
            this.labelPlayer2 = new System.Windows.Forms.Label();
            this.labelPlayer2Points = new System.Windows.Forms.Label();
            this.labelPlayer1Points = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelPlayer1
            // 
            this.labelPlayer1.AutoSize = true;
            this.labelPlayer1.Font = new System.Drawing.Font("News Cycle", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPlayer1.Location = new System.Drawing.Point(18, 9);
            this.labelPlayer1.Name = "labelPlayer1";
            this.labelPlayer1.Size = new System.Drawing.Size(63, 24);
            this.labelPlayer1.TabIndex = 0;
            this.labelPlayer1.Text = "Player 1:";
            // 
            // labelPlayer2
            // 
            this.labelPlayer2.AutoSize = true;
            this.labelPlayer2.Font = new System.Drawing.Font("News Cycle", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPlayer2.Location = new System.Drawing.Point(133, 9);
            this.labelPlayer2.Name = "labelPlayer2";
            this.labelPlayer2.Size = new System.Drawing.Size(65, 24);
            this.labelPlayer2.TabIndex = 1;
            this.labelPlayer2.Text = "Player 2:";
            // 
            // labelPlayer2Points
            // 
            this.labelPlayer2Points.AutoSize = true;
            this.labelPlayer2Points.Font = new System.Drawing.Font("News Cycle", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPlayer2Points.Location = new System.Drawing.Point(184, 9);
            this.labelPlayer2Points.Name = "labelPlayer2Points";
            this.labelPlayer2Points.Size = new System.Drawing.Size(19, 24);
            this.labelPlayer2Points.TabIndex = 3;
            this.labelPlayer2Points.Text = "0";
            // 
            // labelPlayer1Points
            // 
            this.labelPlayer1Points.AutoSize = true;
            this.labelPlayer1Points.Font = new System.Drawing.Font("News Cycle", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPlayer1Points.Location = new System.Drawing.Point(63, 9);
            this.labelPlayer1Points.Name = "labelPlayer1Points";
            this.labelPlayer1Points.Size = new System.Drawing.Size(19, 24);
            this.labelPlayer1Points.TabIndex = 4;
            this.labelPlayer1Points.Text = "0";
            // 
            // Damka
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 254);
            this.Controls.Add(this.labelPlayer1Points);
            this.Controls.Add(this.labelPlayer2Points);
            this.Controls.Add(this.labelPlayer2);
            this.Controls.Add(this.labelPlayer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Damka";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Damka";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label labelPlayer1;
        private System.Windows.Forms.Label labelPlayer2;
        private System.Windows.Forms.Label labelPlayer2Points;
        private System.Windows.Forms.Label labelPlayer1Points;
    }
}