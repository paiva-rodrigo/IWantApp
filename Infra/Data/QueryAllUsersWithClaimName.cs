using Dapper;
using IWantApp.Endpoints.Categories;
using Microsoft.Data.SqlClient;

namespace IWantApp.Infra.Data;

public class QueryAllUsersWithClaimName
{
    private readonly IConfiguration configuration;

    public QueryAllUsersWithClaimName(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public IEnumerable<EmployeeResponse> Execute(int page, int rows)
    {
        //dapper é usado para facilitar as consultas possibilitando fazer consultas diretamente usando
        //os codigos do sql, ela esta presente no db.query
        var db = new SqlConnection(configuration["ConnectionStrings:IWantDb"]);
        var query =
            @"SELECT EMAIL, CLAIMVALUE AS NAME
            FROM AspNetUsers U INNER JOIN AspNetUserClaims C
            ON U.ID=C.USERID AND ClaimType = 'Name'
            order by name
            OFFSET (@page-1)*@rows ROWS FETCH NEXT @rows ROWS ONLY";

        return db.Query<EmployeeResponse>(
            query,
            new { page, rows });

    }
}
