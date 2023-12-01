using Microsoft.AspNetCore.Mvc;
using PrjPROYECTO_CINESTARK_2023.Models;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PrjPROYECTO_CINESTARK_2023.Controllers
{
    public class LogueoController : Controller
    {
        public ActionResult InicioSesion()
        {
            if (HttpContext.Session.GetString("usuario") == null)
            {
                HttpContext.Session.SetString("usuario", JsonConvert.SerializeObject(new Usuario()));
            }

            ViewBag.msjError = null;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> InicioSesion(string email, string password)
        {
            var u = new Usuario();
            using(HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"http://www.apicinebot.somee.com/api/APIUsuario/FindUsuarioByEmailPassword/{email}/{password}");
                string responseAPI = await response.Content.ReadAsStringAsync();
                u = JsonConvert.DeserializeObject<Usuario>(responseAPI);
            }

            if (u != null)
            {
                Usuario usu = new Usuario()
                {
                    idUsu = u.idUsu,
                    nomUsu = u.nomUsu,
                    apeUsu = u.apeUsu,
                    descSexo = u.descSexo,
                    nroDoc = u.nroDoc,
                    fecNac = u.fecNac,
                    email = u.email,
                    contra = u.contra,
                    celular = u.celular,
                    descTipo = u.descTipo,
                    dpto = u.dpto,
                    prov = u.prov,
                    distrito = u.distrito
                };

                var userIngresed = JsonConvert.DeserializeObject<Usuario>(
                        HttpContext.Session.GetString("usuario")
                    );

                userIngresed = usu;

                HttpContext.Session.SetString("usuario", JsonConvert.SerializeObject(userIngresed));

                if (u.descTipo == "Administrador")
                {                    
                    return RedirectToAction("PanelAdmin", "Administrador");
                }
                else
                {
                    return RedirectToAction("ListaPeliculas", "Pelicula");
                }
            }
            else
            {
                ViewBag.msjError = "Correo y Contraseña NO válidos";
                return View();
            }
        }

        public ActionResult CerrarSesion()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("InicioSesion", "Logueo");
        }
        
        public async Task<ActionResult> RegistroCliente()
        {            
            ViewBag.dptos = new SelectList(await GetDeptos(), "dpto", "dpto");
            string? mensaje = TempData["Mensaje"] as string;
            TempData.Keep("Mensaje");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegistroCliente(Usuario u)
        {            
            try
            {
                TempData["Mensaje"] = null;
                u.descSexo = "";
                u.descTipo = "";
                u.dpto = "";
                u.prov = "";
                u.distrito = "";
                using(HttpClient client = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(u), Encoding.UTF8, "application/json");

                    var response = await client.PostAsync($"http://www.apicinebot.somee.com/api/APIUsuario/PostCliente", content);
                    string responseAPI = await response.Content.ReadAsStringAsync();
                    TempData["Mensaje"] = responseAPI;
                    return RedirectToAction("InicioSesion");
                }
            }
            catch (Exception ex)
            {
                ViewBag.msj = ex.Message;
                ViewBag.dptos = new SelectList(await GetDeptos(), "dpto", "dpto");
                return View();
            }
        }

        public async Task<ActionResult> VerPerfil()
        {
            var perfilEncontrado = new Usuario();
            var userSession = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("usuario"));
            using(HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"http://www.apicinebot.somee.com/api/APIAdministrador/GetClientesById/{userSession.idUsu}");
                string responseAPI = await response.Content.ReadAsStringAsync();
                perfilEncontrado = JsonConvert.DeserializeObject<Usuario>(responseAPI);
            }
            var nomUser = $"{userSession.nomUsu} {userSession.apeUsu}";
            ViewBag.nomUser = nomUser;
            return View(perfilEncontrado);
        }

        public async Task<IEnumerable<Ubigeo>> GetDeptos()
        {
            var lst = new List<Ubigeo>();
            using(HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"http://www.apicinebot.somee.com/api/APIUsuario/GetDeptos");
                string responseAPI = await response.Content.ReadAsStringAsync();
                lst = JsonConvert.DeserializeObject<List<Ubigeo>>(responseAPI);
            }
            return lst;
        }

        public async Task<ActionResult> CboProvincias(string dpto)
        {
            var options = new List<SelectListItem>();
            options.Add(new SelectListItem { Value = "[Seleccione Provincia]", Text = "[Seleccione Provincia]" });
            var provs = new List<Ubigeo>();
            if (dpto != "[Seleccione Departamento]")
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync($"http://www.apicinebot.somee.com/api/APIUsuario/GetProvs/{dpto}");
                    string responseAPI = await response.Content.ReadAsStringAsync();
                    provs = JsonConvert.DeserializeObject<List<Ubigeo>>(responseAPI);
                }
                for (int i = 0; i < provs.Count; i++)
                {
                    options.Add(new SelectListItem { Value = provs[i].prov.ToString(), Text = provs[i].prov.ToString() });
                }
            }
            return Json(options.Select(o => new { value = o.Value, text = o.Text }));
        }

        public async Task<ActionResult> CboDistritos(string prov)
        {
            var options = new List<SelectListItem>();
            options.Add(new SelectListItem { Value = "[Seleccione Distrito]", Text = "[Seleccione Distrito]" });
            var distritos = new List<Ubigeo>();
            if (prov != "[Seleccione Provincia]")
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync($"http://www.apicinebot.somee.com/api/APIUsuario/GetDistritos/{prov}");
                    string responseAPI = await response.Content.ReadAsStringAsync();
                    distritos = JsonConvert.DeserializeObject<List<Ubigeo>>(responseAPI);
                }
                for (int i = 0; i < distritos.Count; i++)
                {
                    options.Add(new SelectListItem { Value = distritos[i].idUbigeo.ToString(), Text = distritos[i].distrito.ToString() });
                }
            }
            return Json(options.Select(o => new { value = o.Value, text = o.Text }));
        }
    }
}
