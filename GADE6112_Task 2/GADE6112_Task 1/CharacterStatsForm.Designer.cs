
namespace GADE6112_Task_1
{
    partial class CharacterStatsForm
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
            this.CharacterStatsTxt = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // CharacterStatsTxt
            // 
            this.CharacterStatsTxt.Location = new System.Drawing.Point(-1, 2);
            this.CharacterStatsTxt.Multiline = true;
            this.CharacterStatsTxt.Name = "CharacterStatsTxt";
            this.CharacterStatsTxt.Size = new System.Drawing.Size(288, 246);
            this.CharacterStatsTxt.TabIndex = 0;
            // 
            // CharacterStatsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(287, 248);
            this.Controls.Add(this.CharacterStatsTxt);
            this.Name = "CharacterStatsForm";
            this.Text = "CharacterStatsForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox CharacterStatsTxt;
    }
}