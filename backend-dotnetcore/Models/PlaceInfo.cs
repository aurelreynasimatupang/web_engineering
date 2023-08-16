using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WebEng.Properties.Models;

[Owned]
public class PlaceInfo {
    public string? Smoking {get;set;}
    public string? Pets {get;set;}
    public string? RentDetails{get;set;}
    public string? Roommates {get;set;}
    public string? Toilet {get;set;}
    public string? Gender {get;set;}
}