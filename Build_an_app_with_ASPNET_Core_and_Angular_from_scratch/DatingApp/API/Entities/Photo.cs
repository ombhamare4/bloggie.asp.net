using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities;

[Table("Photos")]
public class Photo
{
    public int Id { get; set; }
    public required string URL { get; set; }
    public bool IsMain { get; set; }
    public string? PublicId { get; set; }
    public List<string>? Tags { get; set; } = [];
    //Navigation Properties
    public int AppUserId { get; set; }
    public AppUser AppUser { get; set; } = null;
}