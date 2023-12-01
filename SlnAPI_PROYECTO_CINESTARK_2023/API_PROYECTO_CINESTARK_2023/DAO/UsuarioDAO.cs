using API_PROYECTO_CINESTARK_2023.Models;
using System.Data.SqlClient;

namespace API_PROYECTO_CINESTARK_2023.DAO
{
    public class UsuarioDAO
    {
        private readonly string _con;

        public UsuarioDAO(IConfiguration configuration)
        {
            _con = configuration.GetConnectionString("con");
        }

        public List<Usuario> GetUsuarios()
        {
            List<Usuario> lstGen = new List<Usuario>();

            SqlDataReader dr = SqlHelper.ExecuteReader(_con, "SP_LISTA_USUARIOS");

            while (dr.Read())
            {
                lstGen.Add(new Usuario
                {
                    idUsu = dr.GetInt32(0),
                    nomUsu = dr.GetString(1),
                    apeUsu = dr.GetString(2),
                    descSexo = dr.GetString(3),
                    nroDoc = dr.GetString(4),
                    fecNac = dr.GetDateTime(5).ToString("dd/MM/yyyy"),
                    email = dr.GetString(6),
                    contra = dr.GetString(7),
                    celular = dr.GetString(8),
                    descTipo = dr.GetString(9),
                    dpto = dr.GetString(10),
                    prov = dr.GetString(11),
                    distrito = dr.GetString(12)
                });
            }
            dr.Close();
            return lstGen;
        }
        public Usuario FindUsuarioById(int idUsu)
        {
            Usuario? u = GetUsuarios().Find(x => x.idUsu.Equals(idUsu));

            if (u == null) return new Usuario();

            return u;
        }

        public Usuario FindUsuarioByEmailPassword(string email, string password)
        {
            Usuario? u = GetUsuarios().FindAll(x => x.contra.Equals(password)).Find(x => x.email.Equals(email));

            return u;
        }

        public string CreateUsuario(Usuario u)
        {
            string msj;
            try
            {
                SqlHelper.ExecuteNonQuery(_con, "SP_INSERT_CLIENTE", u.nomUsu, u.apeUsu, u.idSexo, u.nroDoc, u.fecNac, u.email, u.contra, u.celular, u.idUbigeo);
                msj = $"¡Bienvenido a CineStarK!, {u.nomUsu}. Por favor, inicia sesión.";
            }
            catch (Exception ex)
            {
                msj = ex.Message;
            }
            return msj;
        }

        public List<Ubigeo> GetDeptos()
        {
            List<Ubigeo> lstdpto = new List<Ubigeo>();
            lstdpto.Insert(0, new Ubigeo { dpto = "[Seleccione Departamento]" });
            SqlDataReader dr = SqlHelper.ExecuteReader(_con, "SP_LISTA_DEPTOS");
            while (dr.Read())
            {
                lstdpto.Add(new Ubigeo
                {
                    dpto = dr.GetString(0),
                });
            }
            dr.Close();
            return lstdpto;
        }

        public List<Ubigeo> GetProvs(string dpto)
        {
            List<Ubigeo> lstprovs = new List<Ubigeo>();

            SqlDataReader dr = SqlHelper.ExecuteReader(_con, "SP_LISTA_PROVS", dpto);
            while (dr.Read())
            {
                lstprovs.Add(new Ubigeo
                {
                    prov = dr.GetString(0)
                });
            }
            dr.Close();
            return lstprovs;
        }

        public List<Ubigeo> GetDistritos(string prov)
        {
            List<Ubigeo> lstdistritos = new List<Ubigeo>();

            SqlDataReader dr = SqlHelper.ExecuteReader(_con, "SP_LISTA_DISTRITOS", prov);
            while (dr.Read())
            {
                lstdistritos.Add(new Ubigeo
                {
                    idUbigeo = dr.GetInt32(0),
                    distrito = dr.GetString(1)
                });
            }
            dr.Close();
            return lstdistritos;
        }
    }
}
