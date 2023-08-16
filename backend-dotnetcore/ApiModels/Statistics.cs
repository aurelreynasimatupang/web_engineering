using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;

namespace WebEng.Properties.ApiModels;

public class Statistics{
    [BindNever]
    [SwaggerSchema(ReadOnly =true)]
    public string? City {get;set;}
    public int? SumRent {get;set;}
    public int? SumDeposit {get;set;}
    public int? NumRent{get;set;}
    public int? NumDeposit{get;set;}
    public double? MedianRent {get;set;}
    public double? MeanRent {get;set;}
    public double? SdRent {get;set;}

    public double? MedianDeposit {get;set;}
    public double? MeanDeposit {get;set;}
    public double? SdDeposit {get;set;}
    public List<PropertySummary> Properties {get;set;} = new ();

    public static Statistics FromDatabase(Models.Statistics stats) =>
        new()
        {
            City = stats.City,
            SumDeposit=stats.SumDeposit,
            SumRent = stats.SumRent,
            NumDeposit = stats.NumDeposit,
            NumRent = stats.NumRent,
            MedianRent = stats.MedianRent,
            MeanRent = stats.MeanRent,
            SdRent = stats.SdRent,
            MedianDeposit = stats.MedianDeposit,
            MeanDeposit = stats.MeanDeposit,
            SdDeposit = stats.SdDeposit,
            Properties = stats.Properties.Select(PropertySummary.FromDatabase).ToList()
        };
}