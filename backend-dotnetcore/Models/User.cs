using System.ComponentModel.DataAnnotations;

namespace WebEng.Properties.Models;

public class User{
    [Key]
    public int Id {get;set;}

    [Required]
    public string? DisplayName {get;set;}
    public string? LastLoggedOn {get;set;}
    public string? MemberSince {get;set;}
    public IList<Property> Properties {get;set;} = new List<Property>();
}