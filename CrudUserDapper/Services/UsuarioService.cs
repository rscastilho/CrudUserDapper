using AutoMapper;
using CrudUserDapper.DTO;
using CrudUserDapper.Model;
using Dapper;
using System.Data.SqlClient;

namespace CrudUserDapper.Services
{
    public class UsuarioService : IUsuarioInterface
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UsuarioService(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;

        }

        public async Task<ResponseModel<ListarUsuariosDTO>> BuscarUsuarioPorId(int usuarioid)
        {
            //instanciando a model para retorno de informações;
            ResponseModel<ListarUsuariosDTO> response = new ResponseModel<ListarUsuariosDTO>();

            //criando conexao com bando de dados. 
            //utiliznado using - fechamento da conexao apos fechar o bloco;
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                
                //query quando tem retorno de dados - getall ou get id
                //execute quando nao tem retorno de dados - post / put / delete
                var usuarioPorId = await connection.QueryFirstOrDefaultAsync<Usuario>("select * from usuarios where id = @id", new
                {
                    id = usuarioid
                });

                if (usuarioPorId == null)
                {
                    response.Mensagem = "Nenhum usuario localizado";
                    response.Status = false;
                    return response;
                }

                //mapeando o retorno para dto utiliznado o mapper
                
                var usuarioMapeado = _mapper.Map<ListarUsuariosDTO>(usuarioPorId);
                response.Dados = usuarioMapeado;
                response.Mensagem = "usuario localizado";
                response.Status = true;

            }
            return response;
        }

        public async Task<ResponseModel<List<ListarUsuariosDTO>>> CriarUsuario(CriarUsuarioDto criarUsuarioDto)
        {
            ResponseModel<List<ListarUsuariosDTO>> response = new ResponseModel<List<ListarUsuariosDTO>>();
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuarioBanco = await connection.ExecuteAsync("insert into usuarios (nome, email, CPF, Situacao, password) values(@nome, @email, @CPF, @Situacao, @password)", criarUsuarioDto);
                if (usuarioBanco == 0)
                {
                    response.Mensagem = "Ocorreu algum erro ao cadastrar";
                    response.Status = false;
                    return response;
                }

                var usuarios = await connection.QueryAsync<Usuario>("select * from usuarios");
                var usuarioMapeado = _mapper.Map<List<ListarUsuariosDTO>>(usuarios);
                response.Dados = usuarioMapeado;
                response.Status = true;
                response.Mensagem = "Usuarios cadastrado com sucesso";

            }
            return response;
        }

        public async Task<ResponseModel<List<ListarUsuariosDTO>>> EditarUsuario(EditarUsuarioDto editarUsuarioDto)
        {
            ResponseModel<List<ListarUsuariosDTO>> response = new ResponseModel<List<ListarUsuariosDTO>>();
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuarioBanco = await connection.ExecuteAsync("update usuarios set nome = @nome, email = @email, CPF=@CPF, situacao = @situacao where id = @id", editarUsuarioDto);

                if (usuarioBanco == 0)
                {
                    response.Mensagem = "Erro ao editar usuario";
                    response.Status = false;
                    return response;
                }
                var usuarios = await connection.QueryAsync<Usuario>("select * from usuarios");
                var usuarioMapeado = _mapper.Map<List<ListarUsuariosDTO>>(usuarios);
                response.Dados = usuarioMapeado;
                response.Status = true;
                response.Mensagem = "Usuario atualizado com sucesso!";

            }
            return response;

        }

        public async Task<ResponseModel<List<ListarUsuariosDTO>>> ListarUsuarios()
        {
            ResponseModel<List<ListarUsuariosDTO>> response = new ResponseModel<List<ListarUsuariosDTO>>();


            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuariosBanco = await connection.QueryAsync<Usuario>("select * from usuarios");
                if (usuariosBanco.Count() == 0)
                {
                    response.Mensagem = "Nenhum usuario localizado";
                    response.Status = false;
                    return response;

                }
                var usuarioMapeado = _mapper.Map<List<ListarUsuariosDTO>>(usuariosBanco);

                response.Dados = usuarioMapeado;
                response.Mensagem = "Usuarios localizados com sucesso";


            }
            return response;
        }

        public async Task<ResponseModel<List<ListarUsuariosDTO>>> RemoverUsuario(int usuarioId)
        {
            ResponseModel<List<ListarUsuariosDTO>> response = new ResponseModel<List<ListarUsuariosDTO>>();
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuarioBanco = await connection.ExecuteAsync("delete from usuarios where id = @id", new { id = usuarioId });
                if (usuarioBanco == 0)
                {
                    response.Mensagem = "Erro ao editar usuario";
                    response.Status = false;
                    return response;
                }
                var usuarios = await connection.QueryAsync<Usuario>("select * from usuarios");
                var usuarioMapeado = _mapper.Map<List<ListarUsuariosDTO>>(usuarios);
                response.Dados = usuarioMapeado;
                response.Status = true;
                response.Mensagem = "Usuario deletado com sucesso!";

            }
            return response;



        }
    }
}
