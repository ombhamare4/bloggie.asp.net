using System;

namespace API.DTOs;

public class PhotoDTO
{
    public int Id { get; set; }
    public string? URL { get; set; }
    public bool IsMain { get; set; }
    public List<string>? Tags { get; set; }
}
