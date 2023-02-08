using IWantApp.Infra.Data;

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

    public static IResult Action(int? page, int? rows, QueryAllUsersWithClaimName query)
    {
        
            
        return Results.Ok(query.Execute(page.Value,rows.Value));
    }

}
