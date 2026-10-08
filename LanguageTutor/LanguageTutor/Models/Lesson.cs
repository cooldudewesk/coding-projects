// Models/Lesson.cs
namespace LanguageTutor.Models;
public sealed class Lesson
{
    public string Title { get; set; } = "";
    public string Topic { get; set; } = "";
    public string Description { get; set; } = "";
    public int EstimatedMinutes { get; set; }
    public List<Card> Cards { get; } = new();
}
