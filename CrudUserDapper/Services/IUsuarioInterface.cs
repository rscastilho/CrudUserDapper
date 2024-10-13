using CrudUserDapper.DTO;
using CrudUserDapper.Model;

namespace CrudUserDapper.Services
{
    public interface IUsuarioInterface
    {

        Task<ResponseModel<List<ListarUsuariosDTO>>> ListarUsuarios();
        Task<ResponseModel<ListarUsuariosDTO>> BuscarUsuarioPorId(int usuarioid);
        Task<ResponseModel<List<ListarUsuariosDTO>>> CriarUsuario(CriarUsuarioDto criarUsuarioDto);
        Task<ResponseModel<List<ListarUsuariosDTO>>> EditarUsuario(EditarUsuarioDto editarUsuarioDto);
        Task<ResponseModel<List<ListarUsuariosDTO>>> RemoverUsuario(int usuarioId);


    }
}
