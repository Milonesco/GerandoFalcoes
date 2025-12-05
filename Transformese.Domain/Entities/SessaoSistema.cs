using Transformese.Domain.Entities;

namespace Transformese.Desktop
{
    public static class SessaoSistema
    {
        // Variável estática que guarda o usuário logado na memória
        public static Funcionario UsuarioLogado { get; set; }
    }
}