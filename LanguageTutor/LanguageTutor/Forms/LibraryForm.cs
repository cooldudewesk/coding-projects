// Forms/LibraryForm.cs
using System.Linq;
using LanguageTutor.Models;

namespace LanguageTutor.Forms
{
    public sealed class LibraryForm : Form
    {
        private readonly LessonLibrary _lib;
        private readonly ListView lv = new() { Dock = DockStyle.Fill, View = View.Details, FullRowSelect = true, MultiSelect = false };

        public LibraryForm(LessonLibrary lib)
        {
            _lib = lib;
            Text = "Lesson Library";
            Width = 700; Height = 420;

            lv.Columns.Add("Title", 220);
            lv.Columns.Add("Topic", 160);
            lv.Columns.Add("Minutes", 80);
            lv.Columns.Add("# Cards", 80);

            var btnStart = new Button { Text = "Start", Dock = DockStyle.Bottom, Height = 40 };
            btnStart.Click += (s, e) => StartSelected();

            Controls.Add(lv);
            Controls.Add(btnStart);
            Load += (_, __) => RefreshList();
        }

        private void RefreshList()
        {
            lv.Items.Clear();
            foreach (var (l, idx) in _lib.Lessons.Select((x, i) => (x, i)))
            {
                var it = new ListViewItem(l.Title) { Tag = idx };
                it.SubItems.Add(l.Topic);
                it.SubItems.Add(l.EstimatedMinutes.ToString());
                it.SubItems.Add(l.Cards.Count.ToString());
                lv.Items.Add(it);
            }
        }

        private void StartSelected()
        {
            if (lv.SelectedItems.Count == 0) return;
            var idx = (int)lv.SelectedItems[0].Tag!;
            using var p = new PlayerForm(_lib.Lessons[idx]);
            p.ShowDialog();
        }
    }
}
