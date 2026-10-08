// Forms/LessonBuilderForm.cs
using LanguageTutor.Models;

namespace LanguageTutor.Forms
{
    public sealed class LessonBuilderForm : Form
    {
        private readonly LessonLibrary _lib;
        private readonly TextBox txtTitle = new() { PlaceholderText = "Title" };
        private readonly TextBox txtTopic = new() { PlaceholderText = "Topic" };
        private readonly TextBox txtDesc = new() { PlaceholderText = "Description", Multiline = true, Height = 60 };
        private readonly NumericUpDown numMin = new() { Minimum = 0, Maximum = 600, Value = 10 };
        private readonly FlowLayoutPanel flowCards = new() { AutoScroll = true, Dock = DockStyle.Fill };

        public LessonBuilderForm(LessonLibrary lib)
        {
            _lib = lib;
            Text = "Create Lesson";
            Width = 800; Height = 560;

            var top = new TableLayoutPanel { Dock = DockStyle.Top, Height = 120, ColumnCount = 4 };
            top.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            top.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            top.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35));
            top.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15));
            top.Controls.AddRange(new Control[] { txtTitle, txtTopic, txtDesc, numMin });

            var btnAdd = new Button { Text = "Add Card" };
            var btnSave = new Button { Text = "Save Lesson" };
            btnAdd.Click += (s, e) => flowCards.Controls.Add(MakeCardRow());
            btnSave.Click += (s, e) => SaveLesson();

            var buttons = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 48 };
            buttons.Controls.AddRange(new Control[] { btnAdd, btnSave });

            Controls.Add(flowCards);
            Controls.Add(buttons);
            Controls.Add(top);
        }

        private Panel MakeCardRow(Card? c = null)
        {
            var row = new Panel { Height = 34, Dock = DockStyle.Top };
            var t1 = new TextBox { Name = "Prompt", Left = 0, Width = 220, Text = c?.Prompt ?? "", PlaceholderText = "Prompt" };
            var t2 = new TextBox { Name = "Answer", Left = 230, Width = 220, Text = c?.Answer ?? "", PlaceholderText = "Answer" };
            var t3 = new TextBox { Name = "Url", Left = 460, Width = 220, Text = c?.ImageUrl ?? "", PlaceholderText = "Image URL" };
            var del = new Button { Text = "X", Left = 690, Width = 34 };
            del.Click += (s, e) => flowCards.Controls.Remove(row);
            row.Controls.AddRange(new Control[] { t1, t2, t3, del });
            return row;
        }

        private void SaveLesson()
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text)) { MessageBox.Show("Title required"); return; }
            var lesson = new Lesson
            {
                Title = txtTitle.Text.Trim(),
                Topic = txtTopic.Text.Trim(),
                Description = txtDesc.Text.Trim(),
                EstimatedMinutes = (int)numMin.Value
            };
            foreach (Panel row in flowCards.Controls.OfType<Panel>())
            {
                var prompt = row.Controls["Prompt"] as TextBox;
                var answer = row.Controls["Answer"] as TextBox;
                var url = row.Controls["Url"] as TextBox;
                if (!string.IsNullOrWhiteSpace(prompt?.Text))
                    lesson.Cards.Add(new Card
                    {
                        Prompt = prompt!.Text.Trim(),
                        Answer = answer?.Text.Trim() ?? "",
                        ImageUrl = url?.Text.Trim() ?? ""
                    });
            }
            if (lesson.Cards.Count == 0) { MessageBox.Show("Add at least one card."); return; }
            _lib.Lessons.Add(lesson);
            MessageBox.Show("Lesson saved.");
            Close();
        }
    }
}
