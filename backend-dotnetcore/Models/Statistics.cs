using System.ComponentModel.DataAnnotations;

namespace WebEng.Properties.Models;

public class Statistics{
    [Key]
    public string City {get;set;}="";

    [Required]
    public int? SumRent {get;set;}
    public int? SumDeposit {get;set;}
    public int? NumRent{get;set;}
    public int? NumDeposit{get;set;}
    public double? MeanRent {get;set;}
    public double? MedianRent {get;set;}
    public double? SdRent{get;set;}
    public double? MeanDeposit {get;set;}
    public double? MedianDeposit {get;set;}
    public double? SdDeposit{get;set;}
    public ICollection<Property> Properties {get;set;} = new List<Property>();
}