// Models/Card.cs
namespace LanguageTutor.Models;
public sealed class Card
{
    public string Prompt { get; set; } = "";
    public string Answer { get; set; } = "";
    public string ImageUrl { get; set; } = "";
}
