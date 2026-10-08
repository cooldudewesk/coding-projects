// Forms/MainForm.Designer.cs
using System.Drawing;
using System.Windows.Forms;

namespace LanguageTutor.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer? components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(420, 240);
            this.Name = "MainForm";
            this.Text = "Language Tutor";
            this.ResumeLayout(false);
        }
    }
}
