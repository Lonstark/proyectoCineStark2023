using API_PROYECTO_CINESTARK_2023.Models;
using System.Data.SqlClient;

namespace API_PROYECTO_CINESTARK_2023.DAO
{
    public class PeliculaDAO
    {
        private readonly string _con;

        public PeliculaDAO(IConfiguration configuration)
        {
            _con = configuration.GetConnectionString("con");
        }

        public List<Genero> GetGenero()
        {
            List<Genero> lstGen = new List<Genero>();
            SqlDataReader dr = SqlHelper.ExecuteReader(_con, "SP_LISTA_GENERO");
            while (dr.Read())
            {
                lstGen.Add(new Genero
                {
                    idGen = dr.GetInt32(0),
                    descripcion = dr.GetString(1)
                });
            }
            dr.Close();
            return lstGen;
        }

        public List<Idioma> GetIdioma()
        {
            List<Idioma> lstIdioma = new List<Idioma>();
            SqlDataReader dr = SqlHelper.ExecuteReader(_con, "SP_LISTA_IDIOMA");
            while (dr.Read())
            {
                lstIdioma.Add(new Idioma
                {
                    idIdioma = dr.GetInt32(0),
                    descripcion = dr.GetString(1)
                });
            }
            dr.Close();
            return lstIdioma;
        }

        public List<Formato> GetFormato()
        {
            List<Formato> lstFormat = new List<Formato>();
            SqlDataReader dr = SqlHelper.ExecuteReader(_con, "SP_LISTA_FORMATO");
            while (dr.Read())
            {
                lstFormat.Add(new Formato
                {
                    idFormat = dr.GetInt32(0),
                    descripcion = dr.GetString(1)
                });
            }
            dr.Close();
            return lstFormat;
        }

        public List<Clasificacion> GetClasificacion()
        {
            List<Clasificacion> lstClasif = new List<Clasificacion>();
            SqlDataReader dr = SqlHelper.ExecuteReader(_con, "SP_LISTA_CLASIFICACION");
            while (dr.Read())
            {
                lstClasif.Add(new Clasificacion
                {
                    idClasif = dr.GetInt32(0),
                    descripcion = dr.GetString(1)
                });
            }
            dr.Close();
            return lstClasif;
        }

        public List<Idioma> GetIdiomaByPelicula(int idPeli)
        {
            List<Idioma> lstIdiomas = new List<Idioma>();
            SqlDataReader dr = SqlHelper.ExecuteReader(_con, "SP_LISTA_IDIOMAxPELICULA", idPeli);
            while (dr.Read())
            {
                lstIdiomas.Add(new Idioma
                {
                    idIdioma = dr.GetInt32(0),
                    descripcion = dr.GetString(1)
                });
            }
            dr.Close();
            return lstIdiomas;
        }

        public string GetFormatoByPelicula(int idPeli)
        {
            string lstFormatos = "";
            SqlDataReader dr = SqlHelper.ExecuteReader(_con, "SP_LISTA_FORMATOxPELICULA", idPeli);
            while (dr.Read())
            {
                lstFormatos = dr.GetString(0);
            }
            dr.Close();
            return lstFormatos;
        }

        public List<Pelicula> GetPeliculas()
        {
            List<Pelicula> lstPeli = new List<Pelicula>();
            SqlDataReader dr = SqlHelper.ExecuteReader(_con, "SP_LISTA_PELICULAS");
            while (dr.Read())
            {
                TimeSpan tiempo = TimeSpan.FromMinutes(dr.GetInt32(6));
                lstPeli.Add(new Pelicula
                {
                    idPelicula = dr.GetInt32(0),
                    nombre = dr.GetString(1),
                    img = dr.GetString(2),
                    sinopsis = dr.GetString(3),
                    trailer = dr.GetString(4),
                    fechEstreno = dr.GetDateTime(5).ToString("dd/MM/yyyy"),
                    duracion = string.Format("{0}h {1}min", tiempo.Hours, tiempo.Minutes),
                    director = dr.GetString(7),
                    descGen = dr.GetString(8),
                    descClas = dr.GetString(9),
                    estado = dr.GetString(10)
                });
            }
            dr.Close();
            return lstPeli;
        }

        public Pelicula BuscarPelicula(int idPelicula)
        {
            Pelicula? buscada = GetPeliculas().Find(p => p.idPelicula.Equals(idPelicula));
            if (buscada == null)
                buscada = new Pelicula();
            return buscada;
        }

        public int ObtenerCodigoEntrada()
        {
            int nuevoId = 0;
            SqlDataReader dr = SqlHelper.ExecuteReader(_con, "SP_ULTIMO_CODIGO");
            if (dr.Read())
            {
                nuevoId = dr.GetInt32(0);
            }
            return nuevoId;
        }

        public string InsertCompraEntrada(Entrada e)
        {
            string msj;
            try
            {
                SqlHelper.ExecuteNonQuery(_con, "SP_INSERT_ENTRADA", e.nroEntrada, e.idUsu, e.idFuncion, e.cantidad, e.total);
                msj = "Tu compra se ha realizado con éxito.";
            }
            catch (Exception ex)
            {
                msj = ex.Message;
            }
            return msj;
        }

        public void InsertDetalleEntrada(DetalleEntrada de)
        {
            SqlHelper.ExecuteNonQuery(_con, "SP_INSERTAR_DETALLE_ENTRADA", de.idEntrada, de.idButaca);
        }

        public List<Boleta> GetBoletasEntradas (int idUsu)
        {
            List<Boleta> lst = new List<Boleta>();

            SqlDataReader dr = SqlHelper.ExecuteReader(_con, "REPORTE_BOLETAS", idUsu);

            while (dr.Read())
            {
                lst.Add(new Boleta
                {
                    idEntrada = dr.GetString(0),
                    fechCompra = dr.GetDateTime(1).ToString("dd/MM/yyyy"),
                    hora = dr.GetString(2),
                    nomCine = dr.GetString(3),
                    nomSala = dr.GetString(4),
                    nomPeli = dr.GetString(5),
                    butacas = dr.GetString(6),
                    precio = dr.GetDecimal(7),
                    cantidad = dr.GetInt32(8),
                    total = dr.GetDecimal(9)
                });
            }
            dr.Close();
            return lst;
        }

        /**************funciones*******************/
        public List<FuncionCine> GetFunciones(int idPelicula)
        {
            List<FuncionCine> lstFunPeli = new List<FuncionCine>();

            SqlDataReader dr = SqlHelper.ExecuteReader(_con, "SP_LISTA_FUNCIONES_FOR_CLIENTE", idPelicula);

            while (dr.Read())
            {
                lstFunPeli.Add(new FuncionCine
                {
                    idFuncion = dr.GetInt32(0),
                    idPelicula = dr.GetInt32(1),
                    nomPelicula = dr.GetString(2),
                    dpto = dr.GetString(3),
                    idUbigeo=dr.GetInt32(4),
                    idCine = dr.GetInt32(5),
                    nomCine = dr.GetString(6),
                    idSala = dr.GetInt32(7),
                    nomSala = dr.GetString(8),
                    formato = dr.GetString(9),
                    idioma = dr.GetString(10),
                    fecha = dr.GetDateTime(11).ToString("dd/MM/yyyy"),
                    hora = dr.GetString(12),
                    precio = dr.GetDecimal(13)
                });
            }
            dr.Close();
            return lstFunPeli;
        }

        public FuncionCine GetFuncion(int idPelicula, int idFuncion)
        {
            FuncionCine encontrado = GetFunciones(idPelicula).Find(x => x.idFuncion.Equals(idFuncion)) ?? new FuncionCine();
            return encontrado;
        }

        public List<Butaca> GetButacas(int idSala)
        {
            List<Butaca> lst = new List<Butaca>();
            SqlDataReader dr = SqlHelper.ExecuteReader(_con, "SP_LISTA_BUTACAS", idSala);
            while (dr.Read())
            {
                lst.Add(new Butaca
                {
                    idButaca = dr.GetInt32(0),
                    idSala = dr.GetInt32(1),
                    fila = dr.GetString(2),
                    nro = dr.GetInt32(3),
                    disponible = dr.GetInt32(4)
                });
            }
            dr.Close();
            return lst;
        }

        public List<Cine> GetCiudades(int idPelicula)
        {
            List<Cine> lstCiudades = new List<Cine>();

            SqlDataReader dr = SqlHelper.ExecuteReader(_con, "SP_LISTA_CIUDAD_FUNCION", idPelicula);

            while (dr.Read())
            {
                lstCiudades.Add(new Cine
                {
                    idUbigeo = dr.GetInt32(0),
                    dpto = dr.GetString(1)
                });
            }
            dr.Close();
            return lstCiudades;
        }

        public List<Cine> GetCines(int idPelicula, int idUbigeo)
        {
            List<Cine> lstCines = new List<Cine>();

            SqlDataReader dr = SqlHelper.ExecuteReader(_con, "SP_LISTA_CINES_FUNCION", idPelicula, idUbigeo);

            while (dr.Read())
            {
                lstCines.Add(new Cine
                {
                    idCine = dr.GetInt32(0),
                    nombre = dr.GetString(1)
                });
            }
            dr.Close();
            return lstCines;
        }

        public List<FuncionCine> GetFechas(int idPelicula, int idCine)
        {
            List<FuncionCine> lstfechas = new List<FuncionCine>();

            SqlDataReader dr = SqlHelper.ExecuteReader(_con, "SP_LISTA_FECHAS_FUNCION", idPelicula, idCine);

            while (dr.Read())
            {
                lstfechas.Add(new FuncionCine
                {                    
                    fecha = dr.GetDateTime(0).ToString("dd/MM/yyyy")
                });
            }
            dr.Close();
            return lstfechas;
        }

        public List<FuncionCine> CboFechas(int idPelicula)
        {
            List<FuncionCine> lstfechas = new List<FuncionCine>();

            SqlDataReader dr = SqlHelper.ExecuteReader(_con, "SP_LISTA_FECHAS_BY_PELICULA", idPelicula);

            while (dr.Read())
            {
                lstfechas.Add(new FuncionCine
                {
                    fecha = dr.GetDateTime(0).ToString("dd/MM/yyyy")
                });
            }
            dr.Close();
            return lstfechas;
        }
    }
}
