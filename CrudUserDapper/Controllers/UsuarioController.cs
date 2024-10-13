using CrudUserDapper.DTO;
using CrudUserDapper.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudUserDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly IUsuarioInterface _usuarioInterface;
        public UsuarioController(IUsuarioInterface usuarioInterface)
        {
            _usuarioInterface = usuarioInterface;
        }

        [HttpGet]
        public async Task<IActionResult> ListarUsuarios()
        {
            var usuarios = await _usuarioInterface.ListarUsuarios();
            if (usuarios.Status == false)
            {
                return NotFound(usuarios);
            }
            return Ok(usuarios);
        }

        [HttpGet("{usuarioId}")]
        public async Task<IActionResult> UsuarioPorId(int usuarioId)
        {
            var usuario = await _usuarioInterface.BuscarUsuarioPorId(usuarioId);
            if (usuario.Status == false)
            {
                return NotFound(usuario);
            }
            return Ok(usuario);
            {

            }

        }

        [HttpPost]
        public async Task<IActionResult> CriarUsuario(CriarUsuarioDto criarUsaurioDto)
        {
            var usuarios = await _usuarioInterface.CriarUsuario(criarUsaurioDto);
            if (usuarios.Status == false)
            {
                return BadRequest(usuarios);
            }
            return Ok(usuarios);
        }

        [HttpPut]
        public async Task<IActionResult> EditarUsuario(EditarUsuarioDto editarUsuarioDto)
        {
            var usuarios = await _usuarioInterface.EditarUsuario(editarUsuarioDto);
            if (usuarios.Status == false)
            {
                return BadRequest(usuarios);
            }
            return Ok(usuarios);


        }
        [HttpDelete]
        public async Task<IActionResult> RemoverUsuario(int usuarioId)
        {

            var usuarios = await _usuarioInterface.RemoverUsuario(usuarioId);
            if (usuarios.Status == false)
            {
                return BadRequest(usuarios);
            }
            return Ok(usuarios);

        }
    }
    }
