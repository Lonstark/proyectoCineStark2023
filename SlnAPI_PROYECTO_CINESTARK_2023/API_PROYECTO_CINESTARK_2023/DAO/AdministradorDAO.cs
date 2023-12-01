using API_PROYECTO_CINESTARK_2023.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace API_PROYECTO_CINESTARK_2023.DAO
{
    public class AdministradorDAO
    {
        private readonly string _con;

        public AdministradorDAO(IConfiguration configuration)
        {
            _con = configuration.GetConnectionString("con");
        }

        public List<Usuario> GetClientes()
        {
            List<Usuario> lst = new List<Usuario>();
            SqlDataReader dr = SqlHelper.ExecuteReader(_con, "SP_LISTA_CLIENTES");
            while (dr.Read())
            {
                lst.Add(new Usuario
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
            return lst;
        }

        public Usuario GetClientesById(int idUsu)
        {
            return GetClientes().Find(x=>x.idUsu.Equals(idUsu)) ?? new Usuario();
        }

        public List<Pelicula> GetPeliculas()
        {
            List<Pelicula> lst = new List<Pelicula>();
            SqlDataReader dr = SqlHelper.ExecuteReader(_con, "SP_LISTA_PELICULA_FOR_ADMIN", 1);
            while (dr.Read())
            {
                TimeSpan tiempo = TimeSpan.FromMinutes(dr.GetInt32(6));
                lst.Add(new Pelicula
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
                    formatos = dr.GetString(10),
                    idiomas = dr.GetString(11)
                });
            }
            dr.Close();
            return lst;            
        }

        public List<Cine> GetCines()
        {
            List<Cine> lst = new List<Cine>();
            SqlDataReader dr = SqlHelper.ExecuteReader(_con, "SP_LISTA_CINES");
            while (dr.Read())
            {
                lst.Add(new Cine
                {
                    idCine = dr.GetInt32(0),
                    nombre = dr.GetString(1),
                    direccion = dr.GetString(2),
                    idUbigeo = dr.GetInt32(3),
                    dpto = dr.GetString(4),
                    prov = dr.GetString(5),
                    distrito = dr.GetString(6)
                });
            }
            dr.Close();
            return lst;
        }

        public List<Sala> GetSalas()
        {
            List<Sala> lst = new List<Sala>();
            SqlDataReader dr = SqlHelper.ExecuteReader(_con, "SP_LISTA_SALAS");
            while(dr.Read())
            {
                lst.Add(new Sala
                {
                    idSala = dr.GetInt32(0),
                    nomCine = dr.GetString(1),
                    nombre = dr.GetString(2),
                    capacidad = dr.GetInt32(3),
                    descFormato = dr.GetString(4)
                });
            }
            dr.Close();
            return lst;
        }

        public List<FuncionForAdmin> GetFunciones()
        {
            List<FuncionForAdmin> lst = new List<FuncionForAdmin>();
            SqlDataReader dr = SqlHelper.ExecuteReader(_con, "SP_LISTA_FUNCIONES_FOR_ADMIN");
            while (dr.Read())
            {
                lst.Add(new FuncionForAdmin
                {
                    idFuncion = dr.GetInt32(0),
                    nomPeli = dr.GetString(1),
                    dpto = dr.GetString(2),
                    prov = dr.GetString(3),
                    distrito = dr.GetString(4),
                    nomCine = dr.GetString(5),
                    nomSala = dr.GetString(6),
                    descFormat = dr.GetString(7),
                    descIdioma = dr.GetString(8),
                    fecha = dr.GetDateTime(9).ToString("dd/MM/yyyy"),
                    hora = dr.GetString(10),
                    precio = dr.GetDecimal(11)
                });
            }
            dr.Close();
            return lst;
        }

        public List<Entrada> GetEntradas()
        {
            List<Entrada> lst = new List<Entrada>();
            SqlDataReader dr = SqlHelper.ExecuteReader(_con, "SP_LISTA_ENTRADAS");
            while (dr.Read())
            {
                lst.Add(new Entrada
                {
                    idEntrada = dr.GetString(0),
                    fechCompra = dr.GetDateTime(1).ToString("dd/MM/yyyy"),
                    hora = dr.GetString(2),
                    nomCli = dr.GetString(3),
                    idFuncion = dr.GetInt32(4),
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

        public int CantidadClientes()
        {
            int cant = 0;
            SqlDataReader dr = SqlHelper.ExecuteReader(_con, "SP_CANTIDAD_CLIENTES");
            if (dr.Read())
            {
                cant = dr.GetInt32(0);
            }
            dr.Close();
            return cant;
        }
        public int CantidadPeliculas()
        {
            int cant = 0;
            SqlDataReader dr = SqlHelper.ExecuteReader(_con, "SP_CANTIDAD_PELICULAS");
            if (dr.Read())
            {
                cant = dr.GetInt32(0);
            }
            dr.Close();
            return cant;
        }
        public int CantidadCines()
        {
            int cant = 0;
            SqlDataReader dr = SqlHelper.ExecuteReader(_con, "SP_CANTIDAD_CINES");
            if (dr.Read())
            {
                cant = dr.GetInt32(0);
            }
            dr.Close();
            return cant;
        }
        public int CantidadFunciones()
        {
            int cant = 0;
            SqlDataReader dr = SqlHelper.ExecuteReader(_con, "SP_CANTIDAD_FUNCIONES");
            if (dr.Read())
            {
                cant = dr.GetInt32(0);
            }
            dr.Close();
            return cant;
        }
        public int CantidadEntradas()
        {
            int cant = 0;
            SqlDataReader dr = SqlHelper.ExecuteReader(_con, "SP_CANTIDAD_ENTRADAS");
            if (dr.Read())
            {
                cant = dr.GetInt32(0);
            }
            dr.Close();
            return cant;
        }
    }
}
