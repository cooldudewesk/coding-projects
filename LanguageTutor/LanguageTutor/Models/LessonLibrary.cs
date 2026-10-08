// Models/LessonLibrary.cs
using System.Text;
namespace LanguageTutor.Models;

public sealed class LessonLibrary
{
    public List<Lesson> Lessons { get; } = new();
    private const string FileName = "lessons.txt";

    public void Load()
    {
        Lessons.Clear();
        if (!File.Exists(FileName)) return;
        Lesson? cur = null;
        foreach (var line in File.ReadAllLines(FileName))
        {
            if (string.IsNullOrWhiteSpace(line)) { if (cur != null) { Lessons.Add(cur); cur = null; } continue; }
            var p = Split(line);
            if (p[0] == "L")
            {
                cur = new Lesson
                {
                    Title = p[1],
                    Topic = p[2],
                    Description = p[3],
                    EstimatedMinutes = int.TryParse(p[4], out var m) ? m : 0
                };
            }
            else if (p[0] == "C" && cur != null)
            {
                cur.Cards.Add(new Card { Prompt = p[1], Answer = p[2], ImageUrl = p[3] });
            }
        }
        if (cur != null) Lessons.Add(cur);
    }

    public void Save()
    {
        var sb = new StringBuilder();
        foreach (var l in Lessons)
        {
            sb.AppendLine($"L|{Esc(l.Title)}|{Esc(l.Topic)}|{Esc(l.Description)}|{l.EstimatedMinutes}");
            foreach (var c in l.Cards)
                sb.AppendLine($"C|{Esc(c.Prompt)}|{Esc(c.Answer)}|{Esc(c.ImageUrl)}");
            sb.AppendLine();
        }
        File.WriteAllText(FileName, sb.ToString());
    }

    private static string Esc(string s) => s.Replace("\\", "\\\\").Replace("|", "\\|");
    private static string[] Split(string line)
    {
        var parts = new List<string>(); var cur = new StringBuilder(); bool esc = false;
        foreach (var ch in line)
        {
            if (esc) { cur.Append(ch); esc = false; }
            else if (ch == '\\') esc = true;
            else if (ch == '|') { parts.Add(cur.ToString()); cur.Clear(); }
            else cur.Append(ch);
        }
        parts.Add(cur.ToString());
        return parts.ToArray();
    }
}
