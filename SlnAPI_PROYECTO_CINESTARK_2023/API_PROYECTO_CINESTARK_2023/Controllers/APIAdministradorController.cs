using API_PROYECTO_CINESTARK_2023.DAO;
using API_PROYECTO_CINESTARK_2023.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_PROYECTO_CINESTARK_2023.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIAdministradorController : ControllerBase
    {
        public readonly AdministradorDAO dao_admin;

        public APIAdministradorController(AdministradorDAO _dao_admin)
        {
            dao_admin = _dao_admin;
        }

        [HttpGet("GetClientes")]
        public List<Usuario> GetClientes()
        {
            return dao_admin.GetClientes();
        }

        [HttpGet("GetClientesById/{idUsu}")]
        public Usuario GetClientesById(int idUsu)
        {
            return dao_admin.GetClientesById(idUsu);
        }

        [HttpGet("GetPeliculas")]
        public List<Pelicula> GetPeliculas()
        {
            return dao_admin.GetPeliculas();
        }

        [HttpGet("GetCines")]
        public List<Cine> GetCines()
        {
            return dao_admin.GetCines();
        }

        [HttpGet("GetSalas")]
        public List<Sala> GetSalas()
        {
            return dao_admin.GetSalas();
        }

        [HttpGet("GetFunciones")]
        public List<FuncionForAdmin> GetFunciones()
        {
            return dao_admin.GetFunciones();
        }

        [HttpGet("GetEntradas")]
        public List<Entrada> GetEntradas()
        {
            return dao_admin.GetEntradas();
        }

        [HttpGet("CantidadClientes")]
        public int CantidadClientes()
        {
            return dao_admin.CantidadClientes();
        }

        [HttpGet("CantidadPeliculas")]
        public int CantidadPeliculas()
        {
            return dao_admin.CantidadPeliculas();
        }

        [HttpGet("CantidadCines")]
        public int CantidadCines()
        {
            return dao_admin.CantidadCines();
        }

        [HttpGet("CantidadFunciones")]
        public int CantidadFunciones()
        {
            return dao_admin.CantidadFunciones();
        }

        [HttpGet("CantidadEntradas")]
        public int CantidadEntradas()
        {
            return dao_admin.CantidadEntradas();
        }

        /*// GET api/<APIAdministradorController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<APIAdministradorController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<APIAdministradorController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<APIAdministradorController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
    }
}
