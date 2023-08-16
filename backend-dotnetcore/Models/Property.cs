using System.ComponentModel.DataAnnotations;

namespace WebEng.Properties.Models;

public class Property{
    [Key]
    public string? Id {get; set;}="";
    

    [Required]
    public User User {get;set;} = new User();
    public string? Title{get;set;} = "";
    public string? PropertyType {get;set;}
    public int? AreaSqm {get;set;}
    public float? CostSqm {get;set;}
    public string City {get;set;}="";
    public string? IsActive {get;set;}
    public string? PostalCode {get;set;}
    public double? Latitude {get;set;}
    public double? Longitude {get;set;}
    public int? Rent {get;set;}
    public int? Deposit {get;set;}
    public int? AdditionalCosts {get;set;}
    public PlaceInfo PlaceInfo {get;set;} = new PlaceInfo();
    public MatchTenant MatchTenant {get;set;} = new MatchTenant();
}