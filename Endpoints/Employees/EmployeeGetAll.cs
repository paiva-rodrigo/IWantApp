using IWantApp.Endpoints.Categories;
using Microsoft.AspNetCore.Identity;

namespace IWantApp.Endpoints.Employees;

/*Essa classe usa o identity que é usado para auxiliar na parte de validações
 ele auxilia em muito, pois ja possui varios parametros pre-progamados
como or exemplo os criteriso para ser senha/email*/

public class EmployeeGetAll
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;
    //Essa parte da função mostra os resultados obtidos 

    public static IResult Action(UserManager<IdentityUser> userManager)
    {
        var users = userManager.Users.ToList();
        var employees = new List<EmployeeResponse>();
         
        foreach (var item in users)
        {
            var claims = userManager.GetClaimsAsync(item).Result;
            var claimName = claims.FirstOrDefault(c => c.Type == "Name");
            var userName = claimName != null ? claimName.Value : string.Empty;

            employees.Add(new EmployeeResponse(item.Email, userName));
        }

        return Results.Ok(employees);
    }

}
