namespace ShortLinksService.Entities;

public class CreateLinkEntity
{
    public string Url { get; set; }
    public string Password { get; set; }
    public string ShortUrl { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
}