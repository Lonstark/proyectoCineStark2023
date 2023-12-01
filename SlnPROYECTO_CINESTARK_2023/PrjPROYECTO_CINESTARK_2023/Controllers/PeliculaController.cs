using Microsoft.AspNetCore.Mvc;
using PrjPROYECTO_CINESTARK_2023.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PrjPROYECTO_CINESTARK_2023.Controllers
{
    public class PeliculaController : Controller
    {
        public async Task<ActionResult> ListaPeliculas(int nropag = 0)
        {
            var lst = new List<Pelicula>();
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("http://www.apicinebot.somee.com/api/APIPelicula/GetPeliculas");
                string responseAPI = await response.Content.ReadAsStringAsync();
                lst = JsonConvert.DeserializeObject<List<Pelicula>>(responseAPI);
            }
            int filas_pag = 12;
            int cantidad = lst.Count;
            int pag = 0;
            if (cantidad % filas_pag > 0)
                pag = (cantidad / filas_pag) + 1;
            else
                pag = cantidad / filas_pag;

            var userIngresed = JsonConvert.DeserializeObject<Usuario>(
                            HttpContext.Session.GetString("usuario")
                        );

            var nomUser = $"{userIngresed.nomUsu} {userIngresed.apeUsu}";

            ViewBag.nomUser = nomUser;
            ViewBag.pag = pag;
            ViewBag.generos = await GetGeneros();
            ViewBag.idiomas = await GetIdiomas();
            ViewBag.formatos = await GetFormatos();
            ViewBag.clasificacion = await GetClasificacion();

            return View(lst.Skip(nropag * filas_pag).Take(filas_pag));
        }
        
        public async Task<ActionResult> DetallePelicula(int idPelicula)
        {
            var peliEncontrada = new Pelicula();
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"http://www.apicinebot.somee.com/api/APIPelicula/BuscarPelicula/{idPelicula}");
                string responseAPI = await response.Content.ReadAsStringAsync();
                peliEncontrada = JsonConvert.DeserializeObject<Pelicula>(responseAPI);
            }

            var userIngresed = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("usuario"));
            var nomUser = $"{userIngresed.nomUsu} {userIngresed.apeUsu}";
            ViewBag.nomUser = nomUser;

            
            ViewBag.idiomas = await GetIdiomaByPelicula(idPelicula);
            ViewBag.formatos = await GetFormatoByPelicula(idPelicula);
            ViewBag.ciudades = await GetCiudades(idPelicula);
            ViewBag.funciones = await GetFunciones(idPelicula);
            ViewBag.fechas = new SelectList(await CboFechas(idPelicula), "fecha", "fecha");
            return View(peliEncontrada);
        }

        public async Task<ActionResult> CompraEntrada(int idPelicula, int idFuncion)
        {
            FuncionCine funcion = await GetFuncion(idPelicula, idFuncion);

            if (HttpContext.Session.GetString("funcion") == null)
            {
                HttpContext.Session.SetString("funcion", JsonConvert.SerializeObject(new FuncionCine()));
            }

            var funcionSession = JsonConvert.DeserializeObject<FuncionCine>(HttpContext.Session.GetString("funcion"));

            funcionSession = funcion;

            HttpContext.Session.SetString("funcion", JsonConvert.SerializeObject(funcionSession));

            ViewBag.butacas = await GetButacas(funcion.idSala);
            return View(funcion);
        }

        [HttpPost]
        public async Task<ActionResult> CompraEntrada(string lstCheck)
        {
            int cod = await ObtenerCodigoEntrada() + 1;
            var checkboxIds = lstCheck.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList();
            List<string> listaCheckboxIds = new List<string>(checkboxIds);

            var userSession = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("usuario"));
            var funcionSession = JsonConvert.DeserializeObject<FuncionCine>(HttpContext.Session.GetString("funcion"));
            decimal total = listaCheckboxIds.Count * funcionSession.precio;

            using(HttpClient client = new HttpClient())
            {
                Entrada e = new Entrada
                {
                    nroEntrada = cod,
                    idEntrada = "",
                    fechCompra = "",
                    hora = "",
                    idUsu = userSession.idUsu,
                    nomCli = "",
                    idFuncion = funcionSession.idFuncion,
                    nomPeli = "",
                    butacas = "",
                    precio = 0,
                    cantidad = listaCheckboxIds.Count,
                    total = total
                };
                StringContent content = new StringContent(JsonConvert.SerializeObject(e), Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"http://www.apicinebot.somee.com/api/APIPelicula/InsertCompraEntrada", content);
                string responseAPI = await response.Content.ReadAsStringAsync();
                ViewBag.msj = responseAPI;
                foreach (var idB in listaCheckboxIds)
                {
                    DetalleEntrada de = new DetalleEntrada
                    {
                        idEntrada = cod,
                        idButaca = int.Parse(idB)
                    };
                    InsertDetalleEntrada(de);
                }
            }

            return RedirectToAction("VerEntradas", "Pelicula");
        }
        
        public async Task<int> ObtenerCodigoEntrada()
        {
            int cod = 0;
            using(HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("http://www.apicinebot.somee.com/api/APIPelicula/ObtenerCodigoEntrada");
                string responseAPI = await response.Content.ReadAsStringAsync();
                cod = JsonConvert.DeserializeObject<int>(responseAPI);
            }
            return cod;
        }

        public async void InsertDetalleEntrada(DetalleEntrada de)
        {
            using(HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(de), Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"http://www.apicinebot.somee.com/api/APIPelicula/InsertDetalleEntrada", content);
                string responseAPI = await response.Content.ReadAsStringAsync();
            }
        }

        public async Task<ActionResult> VerEntradas()
        {
            var lst = new List<Boleta>();
            var userSession = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("usuario"));
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"http://www.apicinebot.somee.com/api/APIPelicula/GetBoletasEntradas/{userSession.idUsu}");
                string responseAPI = await response.Content.ReadAsStringAsync();
                lst = JsonConvert.DeserializeObject<List<Boleta>>(responseAPI);
            }
            var nomUser = $"{userSession.nomUsu} {userSession.apeUsu}";
            ViewBag.nomUser = nomUser;
            return View(lst);
        }
        public async Task<IEnumerable<FuncionCine>> GetFunciones(int idPelicula)
        {
            var lst = new List<FuncionCine>();
            using(HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"http://www.apicinebot.somee.com/api/APIPelicula/GetFunciones/{idPelicula}");
                string responseAPI = await response.Content.ReadAsStringAsync();
                lst = JsonConvert.DeserializeObject<List<FuncionCine>>(responseAPI);
            }
            return lst;
        }

        public async Task<FuncionCine> GetFuncion(int idPelicula, int idFuncion)
        {
            var funcion = new FuncionCine();
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"http://www.apicinebot.somee.com/api/APIPelicula/GetFuncion/{idPelicula}/{idFuncion}");
                string responseAPI = await response.Content.ReadAsStringAsync();
                funcion = JsonConvert.DeserializeObject<FuncionCine>(responseAPI);
            }
            return funcion;
        }

        public async Task<IEnumerable<Butaca>> GetButacas(int idSala)
        {
            var lst = new List<Butaca>();
            using(HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"http://www.apicinebot.somee.com/api/APIPelicula/GetButacas/{idSala}");
                string responseAPI = await response.Content.ReadAsStringAsync();
                lst = JsonConvert.DeserializeObject<List<Butaca>>(responseAPI);
            }
            return lst;
        }

        public async Task<ActionResult> CboCines(int idPelicula, int idUbigeo)
        {
            var options = new List<SelectListItem>();
            options.Add(new SelectListItem { Value = 0.ToString(), Text = "[Seleccione Cine]" });
            var cines = new List<Cine>();
            
            if (idUbigeo != 0)
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync($"http://www.apicinebot.somee.com/api/APIPelicula/GetCines/{idPelicula}/{idUbigeo}");
                    string responseAPI = await response.Content.ReadAsStringAsync();
                    cines = JsonConvert.DeserializeObject<List<Cine>>(responseAPI);
                }
                for (int i = 0; i < cines.Count; i++)
                {
                    options.Add(new SelectListItem { Value = cines[i].idCine.ToString(), Text = cines[i].nombre });
                }
            }
            return Json(options.Select(o => new { value = o.Value, text = o.Text }));
        }

        /*public async Task<ActionResult> GetFechas(int idPelicula, int idCine)
        {
            var options = new List<SelectListItem>();
            var funciones = new List<FuncionCine>();

            if (idCine != 0)
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync($"https://localhost:7169/api/APIPelicula/GetFechas/{idPelicula}/{idCine}");
                    string responseAPI = await response.Content.ReadAsStringAsync();
                    funciones = JsonConvert.DeserializeObject<List<FuncionCine>>(responseAPI);
                }
                for (int i = 0; i < funciones.Count; i++)
                {
                    options.Add(new SelectListItem { Value = funciones[i].fecha, Text = funciones[i].fecha });
                }
            }
            return Json(options.Select(o => new { value = o.Value, text = o.Text }));
        }*/

        public async Task<IEnumerable<FuncionCine>> CboFechas(int idPelicula)
        {
            var lst = new List<FuncionCine>();
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"http://www.apicinebot.somee.com/api/APIPelicula/CboFechas/{idPelicula}");
                string responseAPI = await response.Content.ReadAsStringAsync();
                lst = JsonConvert.DeserializeObject<List<FuncionCine>>(responseAPI);
            }
            return lst;
        }

        public async Task<IEnumerable<Genero>> GetGeneros()
        {
            var lst = new List<Genero>();
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("http://www.apicinebot.somee.com/api/APIPelicula/GetGenero");
                string responseAPI = await response.Content.ReadAsStringAsync();
                lst = JsonConvert.DeserializeObject<List<Genero>>(responseAPI);
            }
            return lst;
        }

        public async Task<IEnumerable<Idioma>> GetIdiomas()
        {
            var lst = new List<Idioma>();
            using(HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("http://www.apicinebot.somee.com/api/APIPelicula/GetIdioma");
                string responseAPI = await response.Content.ReadAsStringAsync();
                lst = JsonConvert.DeserializeObject<List<Idioma>>(responseAPI);
            }
            return lst;
        }

        public async Task<IEnumerable<Formato>> GetFormatos()
        {
            var lst = new List<Formato>();
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("http://www.apicinebot.somee.com/api/APIPelicula/GetFormato");
                string responseAPI = await response.Content.ReadAsStringAsync();
                lst = JsonConvert.DeserializeObject<List<Formato>>(responseAPI);
            }
            return lst;
        }

        public async Task<IEnumerable<Clasificacion>> GetClasificacion()
        {
            var lst = new List<Clasificacion>();
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("http://www.apicinebot.somee.com/api/APIPelicula/GetClasificacion");
                string responseAPI = await response.Content.ReadAsStringAsync();
                lst = JsonConvert.DeserializeObject<List<Clasificacion>>(responseAPI);
            }
            return lst;
        }
    
        public async Task<IEnumerable<Idioma>> GetIdiomaByPelicula(int idPelicula)
        {
            var lst = new List<Idioma>();
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"http://www.apicinebot.somee.com/api/APIPelicula/GetIdiomaByPelicula/{idPelicula}");
                string responseAPI = await response.Content.ReadAsStringAsync();
                lst = JsonConvert.DeserializeObject<List<Idioma>>(responseAPI);
            }
            return lst;
        }

        public async Task<string> GetFormatoByPelicula(int idPelicula)
        {
            string lst = "";
            using (HttpClient client = new HttpClient ())
            {
                var response = await client.GetAsync($"http://www.apicinebot.somee.com/api/APIPelicula/GetFormatoByPelicula/{idPelicula}");
                string responseAPI = await response.Content.ReadAsStringAsync();
                lst = responseAPI;
            }
            return lst;
        }

        public async Task<IEnumerable<Cine>> GetCiudades(int idPelicula)
        {
            var lst = new List<Cine>();
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"http://www.apicinebot.somee.com/api/APIPelicula/GetCiudades/{idPelicula}");
                string responseAPI = await response.Content.ReadAsStringAsync();
                lst = JsonConvert.DeserializeObject<List<Cine>>(responseAPI);
            }
            return lst;
        }
    }
}
