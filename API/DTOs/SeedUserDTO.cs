using System;

namespace API.DTOs;

public class SeedUserDTO
{
    public required string Id { get; set; } = null!;
     
     public required string Email { get; set; }

    public DateOnly DateOfBirth { get; set; }
    
    public string? ImageUrl { get; set; }
    
    public required string DisplayName { get; set; }

    public DateTime Created { get; set; }

    public DateTime LastActive { get; set; }

    public required string Gender { get; set; }

    public string? Description { get; set; }

    public required string City { get; set; }

    public required string Country { get; set; }

    public string? Occupation { get; set; }
    
    public bool Smoking { get; set; } = false;
    
    public bool Pets { get; set; } = false;
    
    public int? Budget { get; set; }
    
    public DateTime? MoveInDate { get; set; }
    
    public string? Preferences { get; set; } // Store as comma-separated or JSON string

}
