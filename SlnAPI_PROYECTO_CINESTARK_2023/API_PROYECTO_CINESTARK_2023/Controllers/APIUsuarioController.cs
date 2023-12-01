using API_PROYECTO_CINESTARK_2023.DAO;
using API_PROYECTO_CINESTARK_2023.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_PROYECTO_CINESTARK_2023.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIUsuarioController : ControllerBase
    {
        public readonly UsuarioDAO dao_usuario;
        public APIUsuarioController(UsuarioDAO _dao_usuario)
        {
            dao_usuario = _dao_usuario;
        }

        [HttpGet("GetUsuarios")]
        public List<Usuario> GetUsuarios()
        {
            return dao_usuario.GetUsuarios();
        }

        [HttpGet("FindUsuarioById/{idUsu}")]
        public Usuario FindUsuarioById(int idUsu)
        {
            return dao_usuario.FindUsuarioById(idUsu);
        }

        [HttpGet("FindUsuarioByEmailPassword/{email}/{password}")]
        public Usuario FindUsuarioByEmailPassword(string email, string password)
        {
            return dao_usuario.FindUsuarioByEmailPassword(email, password);
        }

        [HttpPost("PostCliente")]
        public string Post([FromBody] Usuario obj)
        {
            string msj = dao_usuario.CreateUsuario(obj);
            return msj;
        }

        [HttpGet("GetDeptos")]
        public List<Ubigeo> GetDeptos()
        {
            return dao_usuario.GetDeptos();
        }

        [HttpGet("GetProvs/{dpto}")]
        public List<Ubigeo> GetProvs(string dpto)
        {
            return dao_usuario.GetProvs(dpto);
        }

        [HttpGet("GetDistritos/{prov}")]
        public List<Ubigeo> GetDistritos(string prov)
        {
            return dao_usuario.GetDistritos(prov);
        }
        /*
        // POST api/<APIUsuarioController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<APIUsuarioController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<APIUsuarioController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
    }
}
