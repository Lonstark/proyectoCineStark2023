using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PrjPROYECTO_CINESTARK_2023.Models;

namespace PrjPROYECTO_CINESTARK_2023.Controllers
{
    public class AdministradorController : Controller
    {
        public ActionResult PanelAdmin()
        {
            var userIngresed = JsonConvert.DeserializeObject<Usuario>(
                            HttpContext.Session.GetString("usuario")
                        );

            var nomUser = $"{userIngresed.nomUsu} {userIngresed.apeUsu}";

            ViewBag.nomUser = nomUser;
            return View();
        }

        public async Task<ActionResult> InfoCards()
        {
            ViewBag.cantClientes = await CantidadClientes();
            ViewBag.cantPeliculas = await CantidadPeliculas();
            ViewBag.cantCines = await CantidadCines();
            ViewBag.cantFunciones = await CantidadFunciones();
            ViewBag.cantEntradas = await CantidadEntradas();
            return View();
        }

        public async Task<ActionResult> ListaClientes()
        {
            var lst = new List<Usuario>();
            using(HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("http://www.apicinebot.somee.com/api/APIAdministrador/GetClientes");
                string responseAPI = await response.Content.ReadAsStringAsync();
                lst = JsonConvert.DeserializeObject<List<Usuario>>(responseAPI);
            }
            return View(lst);
        }

        public async Task<ActionResult> ListaPeliculas()
        {
            var lst = new List<Pelicula>();
            using(HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("http://www.apicinebot.somee.com/api/APIAdministrador/GetPeliculas");
                string responseAPI = await response.Content.ReadAsStringAsync();
                lst = JsonConvert.DeserializeObject<List<Pelicula>>(responseAPI);
            }
            return View(lst);
        }

        public async Task<ActionResult> ListaCines()
        {
            var lst = new List<Cine>();
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("http://www.apicinebot.somee.com/api/APIAdministrador/GetCines");
                string responseAPI = await response.Content.ReadAsStringAsync();
                lst = JsonConvert.DeserializeObject<List<Cine>>(responseAPI);
            }
            return View(lst);
        }

        public async Task<ActionResult> ListaSalas()
        {
            var lst = new List<Sala>();
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("http://www.apicinebot.somee.com/api/APIAdministrador/GetSalas");
                string responseAPI = await response.Content.ReadAsStringAsync();
                lst = JsonConvert.DeserializeObject<List<Sala>>(responseAPI);
            }
            return View(lst);
        }

        public async Task<ActionResult> ListaFunciones()
        {
            var lst = new List<FuncionForAdmin>();
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("http://www.apicinebot.somee.com/api/APIAdministrador/GetFunciones");
                string responseAPI = await response.Content.ReadAsStringAsync();
                lst = JsonConvert.DeserializeObject<List<FuncionForAdmin>>(responseAPI);
            }
            return View(lst);
        }

        public async Task<ActionResult> ListaEntradas()
        {
            var lst = new List<Entrada>();
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("http://www.apicinebot.somee.com/api/APIAdministrador/GetEntradas");
                string responseAPI = await response.Content.ReadAsStringAsync();
                lst = JsonConvert.DeserializeObject<List<Entrada>>(responseAPI);
            }
            return View(lst);
        }

        public async Task<int> CantidadClientes()
        {
            int cant = 0;
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("http://www.apicinebot.somee.com/api/APIAdministrador/CantidadClientes");
                string responseAPI = await response.Content.ReadAsStringAsync();
                cant = JsonConvert.DeserializeObject<int>(responseAPI);
            }
            return cant;
        }
        public async Task<int> CantidadPeliculas()
        {
            int cant = 0;
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("http://www.apicinebot.somee.com/api/APIAdministrador/CantidadPeliculas");
                string responseAPI = await response.Content.ReadAsStringAsync();
                cant = JsonConvert.DeserializeObject<int>(responseAPI);
            }
            return cant;
        }
        public async Task<int> CantidadCines()
        {
            int cant = 0;
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("http://www.apicinebot.somee.com/api/APIAdministrador/CantidadCines");
                string responseAPI = await response.Content.ReadAsStringAsync();
                cant = JsonConvert.DeserializeObject<int>(responseAPI);
            }
            return cant;
        }
        public async Task<int> CantidadFunciones()
        {
            int cant = 0;
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("http://www.apicinebot.somee.com/api/APIAdministrador/CantidadFunciones");
                string responseAPI = await response.Content.ReadAsStringAsync();
                cant = JsonConvert.DeserializeObject<int>(responseAPI);
            }
            return cant;
        }
        public async Task<int> CantidadEntradas()
        {
            int cant = 0;
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("http://www.apicinebot.somee.com/api/APIAdministrador/CantidadEntradas");
                string responseAPI = await response.Content.ReadAsStringAsync();
                cant = JsonConvert.DeserializeObject<int>(responseAPI);
            }
            return cant;
        }
    }
}
