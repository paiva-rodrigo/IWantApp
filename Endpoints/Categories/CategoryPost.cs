using IWantApp.Domain.Products;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics.CodeAnalysis;

namespace IWantApp.Endpoints.Categories;

public class CategoryPost
{
    public static string Template => "/categories";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;
    //Essa parte da função mostra os resultados obtidos 

     // [AllowAnonymous]//permite todo mundo usar
    [Authorize]//somente usuarios authorizads podem usar
    public static IResult Action(CategoryRequest categoryRequest, ApplicationDbContext context)
    {

        var category = new Category(categoryRequest.Name, "Test", "Test","DESC");

        if (!category.IsValid)
        {
            return Results.ValidationProblem(category.Notifications.ConvertToProblemDetails());
        }

        context.Categories.Add(category);
        context.SaveChanges();

        return Results.Created($"/categories/{category.Id}", category.Id);
    }

}
