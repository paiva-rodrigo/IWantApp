using IWantApp.Domain.Products; 
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IWantApp.Endpoints.Employees;

/*Essa classe usa o identity que é usado para auxiliar na parte de validações
 ele auxilia em muito, pois ja possui varios parametros pre-progamados
como or exemplo os criteriso para ser senha/email*/

public class EmployeePost
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;
    //Essa parte da função mostra os resultados obtidos 

    public static IResult Action(EmployeeRequest employeeRequest, UserManager<IdentityUser> userManager)
    {
        var user = new IdentityUser { UserName = employeeRequest.Email,
            Email = employeeRequest.Email };

        var result = userManager.CreateAsync(user, employeeRequest.Password).Result;

        if (!result.Succeeded) return Results.ValidationProblem(result.Errors.ConvertToProblemDetails());

        var userClaims = new List<Claim>
        {
            new Claim("EmployeeCode", employeeRequest.EmployeeCode),
            new Claim("Name", employeeRequest.Name)
        };

        var claimResult = 
            userManager.AddClaimsAsync(user, userClaims).Result;

        if(!claimResult.Succeeded)
            return Results.BadRequest(claimResult.Errors.First());

        return Results.Created($"/employees/{user.Id}", user.Id);
    }

}
