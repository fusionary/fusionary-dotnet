namespace Fusionary.Auth.Google;

using Fusionary.Auth.Abstractions.Models;

public class GoogleUserInfo : IPersonName
{
    public string EmailAddress { get; set; } = "";

    public string? ImageUrl { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string FullName() => this.FirstName + " " + this.LastName;

    public string Initials() => this.FirstName?.Substring(0, 1) + this.LastName?.Substring(0,1);
}
