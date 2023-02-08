using IWantApp.Domain.Products;
using IWantApp.Endpoints.Security;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IWantApp.Endpoints.Employees;

/*Essa classe usa o identity que é usado para auxiliar na parte de validações
 ele auxilia em muito, pois ja possui varios parametros pre-progamados
como or exemplo os criteriso para ser senha/email*/

public class TokenPost
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;
    //Essa parte da função mostra os resultados obtidos 

    public static IResult Action(LoginRequest loginRequest, UserManager<IdentityUser> userManager)
    {
       var user = userManager.FindByEmailAsync(loginRequest.Email).Result;
        if (user == null)
            Results.BadRequest();
        if (!userManager.CheckPasswordAsync(user, loginRequest.Password).Result)
            Results.BadRequest();

        var key = Encoding.ASCII.GetBytes("A@fderwfQQSDXCCer34");
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Email, loginRequest.Email),
            }),
            SigningCredentials =
            new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Audience = "IWantApp",
            Issuer = "Issuer"
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return Results.Ok(new
        {
            token = tokenHandler.WriteToken(token)
        }) ;
    }

}
