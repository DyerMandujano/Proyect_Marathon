using Proyecto_Marathon2024.Entidades;

namespace Proyecto_Marathon2024.Services
{
    public interface IPerfil_Personal
    {
        Task<List<Perfil_Personal>> GetListaPerfil_Personal();

    }
}
