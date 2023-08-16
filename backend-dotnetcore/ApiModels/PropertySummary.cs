using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;

namespace WebEng.Properties.ApiModels;

public class PropertySummary{
 [BindNever]
    [SwaggerSchema(ReadOnly = true)]
    public string? Id { get; set; }
    public string? Title { get; set; }
    public string? IsActive { get; set; }
    public string? City { get; set; }
    public int? Rent { get; set; }
    public int? Deposit { get; set; }


    public static PropertySummary FromDatabase(Models.Property prop) =>
        new()
        {
            Id = prop.Id,
            Title = prop.Title,
            IsActive = prop.IsActive,
            City = prop.City,
            Rent = prop.Rent,
            Deposit = prop.Deposit
        };
}