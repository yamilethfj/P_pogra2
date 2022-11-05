using Microsoft.AspNetCore.Mvc;
using parcialE.Models;
using parcialE.Contexts;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.RazorPages;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace parcialE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GestionController : ControllerBase
    {
        private readonly ConexionSQLServer context;

        public GestionController(ConexionSQLServer context)
        {
            this.context = context;
        }

        // GET: api/<GestionController>
        [HttpGet]
        [Route("all")]
        public IActionResult GetAll()
        {
            try
            {
                List<Gestion> list = new List<Gestion>();
                SqlConnection conexion = (SqlConnection)context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "crudGestion";
                comando.Parameters.Add("@opcion", SqlDbType.Int).Value = 4;
                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Gestion obj = new Gestion();
                    obj.Id = (int)reader["id"];
                    obj.Nombre = (string)reader["nombre"];
                    obj.Descripcion = (string)reader["descripcion"];
                    obj.Fecha = (string)reader["fecha"];
                    obj.Responsable = (int)reader["responsable"];
                    obj.Estado = (int)reader["estado"];
                    list.Add(obj);
                }
                conexion.Close();
                return Ok(list);
            }
            catch
            {
                return BadRequest("Error.");
            }
        }

        // GET api/<GestionController>/5
        [HttpGet]
        [Route("one/{id}")]
        public IActionResult One(int id)
        {
            try
            {
                List<Gestion> list = new List<Gestion>();
                SqlConnection conexion = (SqlConnection)context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "crudGestion";
                comando.Parameters.Add("@id", SqlDbType.Int).Value = id;
                comando.Parameters.Add("@opcion", SqlDbType.Int).Value = 5;
                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Gestion obj = new Gestion();
                    obj.Id = (int)reader["id"];
                    obj.Nombre = (string)reader["nombre"];
                    obj.Descripcion = (string)reader["descripcion"];
                    obj.Fecha = (string)reader["fecha"];
                    obj.Responsable = (int)reader["responsable"];
                    obj.Estado = (int)reader["estado"];
                    list.Add(obj);
                }
                conexion.Close();
                return Ok(list);
            }
            catch
            {
                return BadRequest("Error.");
            }
        }

        // POST api/<GestionController>
        [HttpPost]
        [Route("store")]
        public IActionResult store(JObject request)
        {
            try
            {
                int NoGestion = Int32.Parse(request.GetValue("NoGestion").ToString());
                string nombre = request.GetValue("nombre").ToString();
                string descripcion = request.GetValue("descripcion").ToString();
                string fecha = request.GetValue("fecha").ToString();
                int responsable = Int32.Parse(request.GetValue("responsable").ToString());
                int estado = Int32.Parse(request.GetValue("estado").ToString());

                SqlConnection conexion = (SqlConnection)context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "crudGestion";
                comando.Parameters.AddWithValue("@nombre", nombre);
                comando.Parameters.AddWithValue("@descripcion", descripcion);
                comando.Parameters.AddWithValue("@NoGestion", NoGestion);
                comando.Parameters.AddWithValue("@fecha", fecha);
                comando.Parameters.AddWithValue("@responsable", responsable);
                comando.Parameters.AddWithValue("@estado", estado);
                comando.Parameters.AddWithValue("@opcion", 1);
                SqlDataAdapter adapter = new SqlDataAdapter(comando);
                DataSet setter = new DataSet();
                adapter.Fill(setter, "tabla");
                dynamic resultado = new JObject();
                resultado.response = 1;
                resultado.message = "Gestion Agregada con Exito";
                resultado.value = 1;

                conexion.Close();

                return Ok(resultado);

            }
            catch
            {
                return BadRequest("Error.");
            }
        }

        // PUT api/<GestionController>/5
        [HttpPost]
        [Route("update")]
        public IActionResult update(JObject request)
        {
            try
            {
                int id = Int32.Parse(request.GetValue("id").ToString());
                int NoGestion = Int32.Parse(request.GetValue("NoGestion").ToString());
                string nombre = request.GetValue("nombre").ToString();
                string descripcion = request.GetValue("descripcion").ToString();
                string fecha = request.GetValue("fecha").ToString();
                int responsable = Int32.Parse(request.GetValue("responsable").ToString());
                int estado = Int32.Parse(request.GetValue("estado").ToString());

                SqlConnection conexion = (SqlConnection)context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "crudGestion";
                comando.Parameters.AddWithValue("@id", id);
                comando.Parameters.AddWithValue("@nombre", nombre);
                comando.Parameters.AddWithValue("@descripcion", descripcion);
                comando.Parameters.AddWithValue("@NoGestion", NoGestion);
                comando.Parameters.AddWithValue("@fecha", fecha);
                comando.Parameters.AddWithValue("@responsable", responsable);
                comando.Parameters.AddWithValue("@estado", estado);
                comando.Parameters.AddWithValue("@opcion", 2);
                SqlDataAdapter adapter = new SqlDataAdapter(comando);
                DataSet setter = new DataSet();
                adapter.Fill(setter, "tabla");
                dynamic resultado = new JObject();
                resultado.response = 1;
                resultado.message = "Gestion Modificada con Exito";
                resultado.value = 1;

                conexion.Close();

                return Ok(resultado);

            }
            catch
            {
                return BadRequest("Error.");
            }
        }

        // DELETE api/<GestionController>/5
        [HttpPost]
        [Route("destroy")]
        public IActionResult Destroy(JObject request)
        {
            try
            {
                int id = Int32.Parse(request.GetValue("id").ToString());
                int estado = Int32.Parse(request.GetValue("estado").ToString());

                SqlConnection conexion = (SqlConnection)context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "crudGestion";
                comando.Parameters.AddWithValue("@id", id);
                comando.Parameters.AddWithValue("@estado", estado);
                comando.Parameters.AddWithValue("@opcion", 3);
                SqlDataAdapter adapter = new SqlDataAdapter(comando);
                DataSet setter = new DataSet();
                adapter.Fill(setter, "tabla");
                dynamic resultado = new JObject();
                resultado.response = 1;
                resultado.message = "Gestion Eliminada con Exito";
                resultado.value = 1;

                conexion.Close();

                return Ok(resultado);

            }
            catch
            {
                return BadRequest("Error.");
            }
        }
    }
}
