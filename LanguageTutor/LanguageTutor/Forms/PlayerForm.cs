// Forms/PlayerForm.cs
using System.Diagnostics;
using LanguageTutor.Models;

namespace LanguageTutor.Forms
{
    public sealed class PlayerForm : Form
    {
        private readonly List<Card> _queue;
        private int _i = 0, _correct = 0;
        private readonly Stopwatch _sw = new();

        private readonly Label lblTitle = new() { AutoSize = true };
        private readonly Label lblPrompt = new() { AutoSize = true, Top = 36 };
        private readonly Label lblProgress = new() { AutoSize = true, Top = 64 };
        private readonly PictureBox pic = new() { Top = 90, Width = 360, Height = 220, SizeMode = PictureBoxSizeMode.Zoom, BorderStyle = BorderStyle.FixedSingle };
        private readonly TextBox txt = new() { Top = 320, Width = 360, Name = "txtAnswer" };
        private readonly Label lblFb = new() { Top = 350, AutoSize = true };
        private readonly Button btnSubmit = new() { Text = "Submit", Top = 380, Width = 100 };
        private readonly Button btnNext = new() { Text = "Next", Top = 380, Left = 110, Width = 100 };

        public PlayerForm(Lesson lesson)
        {
            Text = $"Playing: {lesson.Title}";
            Width = 400; Height = 460;

            Controls.AddRange(new Control[] { lblTitle, lblPrompt, lblProgress, pic, txt, lblFb, btnSubmit, btnNext });
            lblTitle.Text = $"{lesson.Title} — {lesson.Cards.Count} cards";
            _queue = lesson.Cards.ToList();
            Shuffle(_queue);

            Load += (_, __) => { _sw.Start(); ShowCard(); };
            btnSubmit.Click += (_, __) => Grade();
            btnNext.Click += (_, __) => ShowCard();
            txt.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; btnSubmit.PerformClick(); } };
        }

        private void ShowCard()
        {
            if (_i >= _queue.Count) { Finish(); return; }
            var c = _queue[_i];
            lblPrompt.Text = $"Prompt: {c.Prompt}";
            lblProgress.Text = $"{_i + 1}/{_queue.Count}";
            lblFb.Text = "";
            txt.Clear(); txt.Focus();
            try { if (!string.IsNullOrWhiteSpace(c.ImageUrl)) pic.Load(c.ImageUrl); else pic.Image = null; }
            catch { pic.Image = null; }
        }

        private void Grade()
        {
            if (_i >= _queue.Count) return;
            var c = _queue[_i];
            bool ok = string.Equals(txt.Text.Trim(), c.Answer, StringComparison.InvariantCultureIgnoreCase);
            if (ok) _correct++;
            lblFb.Text = ok ? "✅ Correct!" : $"❌ Correct: {c.Answer}";
            _i++;
        }

        private void Finish()
        {
            _sw.Stop();
            int total = _queue.Count;
            double pct = total == 0 ? 0 : 100.0 * _correct / total;
            MessageBox.Show($"Score: {_correct}/{total} ({pct:F1}%)\nTime: {_sw.Elapsed:mm\\:ss}", "Lesson complete");
            Close();
        }

        private static void Shuffle<T>(IList<T> a)
        {
            var r = new Random();
            for (int i = a.Count - 1; i > 0; i--) { int j = r.Next(i + 1); (a[i], a[j]) = (a[j], a[i]); }
        }
    }
}
