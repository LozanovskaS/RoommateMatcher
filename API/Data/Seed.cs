using System;
using System.Security.Cryptography;
using System.Text.Json;
using API.DTOs;
using API.Enttities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class Seed
{
    public static async Task SeedUsers(AppDbContext context)
    {
        Console.WriteLine("=== SEED METHOD STARTED ===");

        if (await context.Users.AnyAsync())
        {
            Console.WriteLine("Users already exist, skipping seed.");
            return;
        }

        Console.WriteLine("No users found, starting seed...");

        var memberData = await File.ReadAllTextAsync("Data/UserSeedData.json");
        Console.WriteLine($"JSON file read successfully. Length: {memberData.Length}");

        var members = JsonSerializer.Deserialize<List<SeedUserDTO>>(memberData);
        Console.WriteLine($"Deserialized {members?.Count ?? 0} members");

        if (members == null)
        {
            Console.WriteLine("No member data found to seed.");
            return;
        }

        foreach (var member in members)
        {
            using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                Id = member.Id,
                Email = member.Email,
                ImageUrl = member.ImageUrl,
                DisplayName = member.DisplayName,
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("Pa$$w0rd")),
                PasswordSalt = hmac.Key,
                Member = new Member
                {
                    Id = member.Id,
                    DisplayName = member.DisplayName,
                    Description = member.Description,
                    DateOfBirth = member.DateOfBirth,
                    ImageUrl = member.ImageUrl,
                    Gender = member.Gender,
                    City = member.City,
                    Country = member.Country,
                    Occupation = member.Occupation,
                    Smoking = member.Smoking,
                    Pets = member.Pets,
                    Budget = member.Budget,
                    MoveInDate = member.MoveInDate,
                    Preferences = member.Preferences,
                    Created = member.Created,
                    LastActive = member.LastActive
                }
            };

            user.Member.Photos.Add(new Photo
            {
                Url = member.ImageUrl!,
                MemberId = member.Id,
            });

            context.Users.Add(user);
        }

        Console.WriteLine("Saving to database...");
        await context.SaveChangesAsync();
        Console.WriteLine("=== SEED COMPLETED SUCCESSFULLY ===");
    }
}
