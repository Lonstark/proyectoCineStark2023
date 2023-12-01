using API_PROYECTO_CINESTARK_2023.DAO;
using API_PROYECTO_CINESTARK_2023.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_PROYECTO_CINESTARK_2023.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIPeliculaController : ControllerBase
    {
        public readonly PeliculaDAO dao_pelicula;
        public readonly UsuarioDAO dao_usuario;
        public APIPeliculaController(PeliculaDAO _dao_pelicula, UsuarioDAO _dao_usuario)
        {
            dao_pelicula = _dao_pelicula;
            dao_usuario = _dao_usuario;
        }
                
        [HttpGet("GetGenero")]
        public List<Genero> GetGenero()
        {
            return dao_pelicula.GetGenero();
        }

        [HttpGet("GetIdioma")]
        public List<Idioma> GetIdioma()
        {
            return dao_pelicula.GetIdioma();
        }

        [HttpGet("GetFormato")]
        public List<Formato> GetFormato()
        {
            return dao_pelicula.GetFormato();
        }

        [HttpGet("GetClasificacion")]
        public List<Clasificacion> GetClasificacion()
        {
            return dao_pelicula.GetClasificacion();
        }

        [HttpGet("GetIdiomaByPelicula/{idPeli}")]
        public List<Idioma> GetIdiomaByPelicula(int idPeli)
        {
            return dao_pelicula.GetIdiomaByPelicula(idPeli);
        }

        [HttpGet("GetFormatoByPelicula/{idPeli}")]
        public string GetFormatoByPelicula(int idPeli)
        {
            return dao_pelicula.GetFormatoByPelicula(idPeli);
        }

        [HttpGet("GetPeliculas")]
        public List<Pelicula> GetPeliculas()
        {
            return dao_pelicula.GetPeliculas();
        }

        [HttpGet("BuscarPelicula/{idPeli}")]
        public Pelicula BuscarPelicula(int idPeli)
        {
            return dao_pelicula.BuscarPelicula(idPeli);
        }

        [HttpGet("ObtenerCodigoEntrada")]
        public int ObtenerCodigoEntrada()
        {
            return dao_pelicula.ObtenerCodigoEntrada();
        }

        [HttpPost("InsertCompraEntrada")]
        public string Post([FromBody] Entrada e)
        {
            return dao_pelicula.InsertCompraEntrada(e);            
        }

        [HttpPost("InsertDetalleEntrada")]
        public void Post([FromBody] DetalleEntrada e)
        {
            dao_pelicula.InsertDetalleEntrada(e);
        }

        [HttpGet("GetBoletasEntradas/{idUsu}")]
        public List<Boleta> GetBoletasEntradas(int idUsu)
        {
            return dao_pelicula.GetBoletasEntradas(idUsu);
        }

        /**************funciones*******************/
        [HttpGet("GetFunciones/{idPeli}")]
        public List<FuncionCine> GetFunciones(int idPeli)
        {
            return dao_pelicula.GetFunciones(idPeli);
        }

        [HttpGet("GetFuncion/{idPeli}/{idFuncion}")]
        public FuncionCine GetFuncion(int idPeli, int idFuncion)
        {
            return dao_pelicula.GetFuncion(idPeli, idFuncion);
        }

        [HttpGet("GetButacas/{idSala}")]
        public List<Butaca> GetButacas(int idSala)
        {
            return dao_pelicula.GetButacas(idSala);
        }

        [HttpGet("GetCiudades/{idPeli}")]
        public List<Cine> GetCiudades(int idPeli)
        {
            return dao_pelicula.GetCiudades(idPeli);
        }

        [HttpGet("GetCines/{idPeli}/{idUbigeo}")]
        public List<Cine> GetCines(int idPeli, int idUbigeo)
        {
            return dao_pelicula.GetCines(idPeli, idUbigeo);
        }

        [HttpGet("GetFechas/{idPeli}/{idCine}")]
        public List<FuncionCine> GetFechas(int idPeli, int idCine)
        {
            return dao_pelicula.GetFechas(idPeli, idCine);
        }
        [HttpGet("CboFechas/{idPeli}")]
        public List<FuncionCine> CboFechas(int idPeli)
        {
            return dao_pelicula.CboFechas(idPeli);
        }
        /*
        // GET api/<APIPeliculaController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<APIPeliculaController>
        

        // PUT api/<APIPeliculaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<APIPeliculaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
    }
}
