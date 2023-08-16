using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;


namespace WebEng.Properties.ApiModels;

public class UserSummary{

    [BindNever]
    [SwaggerSchema(ReadOnly =true)]
    public int Id {get;set;} = 0;
    public string? DisplayName {get;set;}

    public static UserSummary FromDatabase(Models.User user) =>
        new()
        {
            Id = user.Id,
            DisplayName = user.DisplayName
        };
}