using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;

namespace WebEng.Properties.ApiModels;
public class User{

    [BindNever]
    [SwaggerSchema(ReadOnly =true)]
    public int Id {get;set;}
    public string? DisplayName {get;set;}
    public string? LastLoggedOn {get;set;}
    public string? MemberSince {get;set;}
    public List<PropertySummary> Properties {get;set;} = new ();

    public static User FromDatabase(Models.User user) =>
        new()
        {
            Id = user.Id,
            DisplayName = user.DisplayName,
            LastLoggedOn = user.LastLoggedOn,
            MemberSince = user.MemberSince,
           Properties = user.Properties.Select(PropertySummary.FromDatabase).ToList()
        };
}