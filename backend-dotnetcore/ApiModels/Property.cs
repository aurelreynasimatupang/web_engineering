using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;

namespace WebEng.Properties.ApiModels;

public class Property{
    [BindNever]
    [SwaggerSchema(ReadOnly =true)]
    public string? Id{get;set;}

    [BindRequired]
    public string? Title{get;set;}
    public string? PropertyType {get;set;}
    public int? AreaSqm {get;set;}
    [BindRequired]
    public float? CostSqm {get;set;}
    [BindRequired]
    public string? City {get;set;}
    [BindRequired, JsonPropertyName("is_active")]
    public string? IsActive {get;set;}
    public string? PostalCode {get;set;}
    [BindRequired]
    public double? Latitude {get;set;}
    [BindRequired]
    public double? Longitude {get;set;}
    [BindRequired]
    public int? Rent {get;set;}
    public int? Deposit {get;set;}
    public int? AdditionalCosts {get;set;}

    public UserSummary User {get;set;} = new();
    public PlaceInfoSummary PlaceInfo {get;set;} = new();
    public MatchTenantSummary MatchTenant {get;set;} = new();

    public static Property FromDatabase(Models.Property prop) => new(){
        Id = prop.Id,
        Title = prop.Title,
        PropertyType = prop.PropertyType,
        AreaSqm = prop.AreaSqm,
        CostSqm = prop.CostSqm,
        City = prop.City,
        IsActive = prop.IsActive,
        PostalCode = prop.PostalCode,
        Latitude = prop.Latitude,
        Longitude = prop.Longitude,
        Rent = prop.Rent,
        Deposit = prop.Deposit,
        AdditionalCosts = prop.AdditionalCosts,
        PlaceInfo = {
            Smoking = prop.PlaceInfo.Smoking,
            Pets = prop.PlaceInfo.Pets,
            RentDetails = prop.PlaceInfo.RentDetails,
            Roommates = prop.PlaceInfo.Roommates,
            Toilet = prop.PlaceInfo.Toilet,
            Gender = prop.PlaceInfo.Gender
        },
        MatchTenant = {
            MatchAge = prop.MatchTenant.MatchAge,
            MatchCapacity = prop.MatchTenant.MatchCapacity,
            MatchGender = prop.MatchTenant.MatchGender,
            MatchLanguages = prop.MatchTenant.MatchLanguages,
            MatchStatus = prop.MatchTenant.MatchStatus,
        },
        User = UserSummary.FromDatabase(prop.User),
    };

public class PlaceInfoSummary{
    public string? Smoking {get;set;}
    public string? Pets {get;set;}
    public string? RentDetails{get;set;}
    public string? Roommates {get;set;}
    public string? Toilet {get;set;}
    public string? Gender {get;set;}

}

public class MatchTenantSummary{
    public string? MatchAge {get;set;}
    public string? MatchCapacity {get;set;}
    public string? MatchGender{get;set;}
    public string? MatchLanguages {get;set;} 
    public string? MatchStatus {get;set;}

    }
}