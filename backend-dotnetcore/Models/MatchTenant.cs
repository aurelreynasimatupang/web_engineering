using Microsoft.EntityFrameworkCore;

namespace WebEng.Properties.Models;

[Owned]
public class MatchTenant{
    public string? MatchAge {get;set;}
    public string? MatchCapacity {get;set;}
    public string? MatchGender{get;set;}
    public string? MatchLanguages {get;set;} 
    public string? MatchStatus {get;set;}
}