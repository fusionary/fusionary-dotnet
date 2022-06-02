using Fusionary.Auth.Abstractions.Models;

namespace Fusionary.Auth.Google;

public class GoogleUserInfo : IPersonName {
    public string EmailAddress { get; set; } = "";

    public string? ImageUrl { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string FullName()
    {
        return FirstName + " " + LastName;
    }

    public string Initials()
    {
        return $"{FirstName?[..1]}{LastName?[..1]}".Trim();
    }
}
