using System.Security.Claims;

namespace Transformese.MVC.Services
{
    public interface IUsuarioService
    {
        int GetUnidadeIdDoUsuario(ClaimsPrincipal user);
    }
}