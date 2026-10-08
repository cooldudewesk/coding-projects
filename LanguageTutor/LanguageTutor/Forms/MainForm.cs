// Forms/MainForm.cs
using System;
using System.Windows.Forms;
using LanguageTutor.Models;

namespace LanguageTutor.Forms
{
    public partial class MainForm : Form
    {
        private readonly LessonLibrary _lib = new();

        public MainForm()
        {
            InitializeComponent();  // 

            
            Text = "Language Tutor";
            StartPosition = FormStartPosition.CenterScreen;
            Width = 420; Height = 240;

            var btnBuild = new Button { Text = "Create Lesson", Left = 30, Top = 40, Width = 150, Height = 50 };
            var btnLibrary = new Button { Text = "View Library", Left = 220, Top = 40, Width = 150, Height = 50 };
            Controls.AddRange(new Control[] { btnBuild, btnLibrary });

            btnBuild.Click += (s, e) => { using var f = new LessonBuilderForm(_lib); f.ShowDialog(this); };
            btnLibrary.Click += (s, e) => { using var f = new LibraryForm(_lib); f.ShowDialog(this); };
        }

        protected override void OnLoad(EventArgs e) { base.OnLoad(e); _lib.Load(); }
        protected override void OnFormClosing(FormClosingEventArgs e) { _lib.Save(); base.OnFormClosing(e); }
    }
}
