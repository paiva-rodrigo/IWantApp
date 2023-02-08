using Dapper;
using IWantApp.Endpoints.Categories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;

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

    public static IResult Action(int? page, int? rows, IConfiguration configuration)
    {
        //dapper é usado para facilitar as consultas possibilitando fazer consultas diretamente usando
        //os codigos do sql
        var db = new SqlConnection(configuration["ConnectionStrings:IWantDb"]);
        var query =
            @"SELECT EMAIL, CLAIMVALUE AS NAME
            FROM AspNetUsers U INNER JOIN AspNetUserClaims C
            ON U.ID=C.USERID AND ClaimType = 'Name'
            order by name
            OFFSET (@page-1)*@rows ROWS FETCH NEXT @rows ROWS ONLY";
            
        var employees = db.Query<EmployeeResponse>(
            query,
            new { page,rows });
            
        return Results.Ok(employees);
    }

}
