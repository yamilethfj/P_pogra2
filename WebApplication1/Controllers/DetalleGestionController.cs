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
    public class DetalleGestionController : ControllerBase
    {
        private readonly ConexionSQLServer context;

        public DetalleGestionController(ConexionSQLServer context)
        {
            this.context = context;
        }

        // GET: api/<DetalleGestionController>
        [HttpGet]
        [Route("all")]
        public IActionResult GetAll()
        {
            try
            {
                List<DetalleGestion> list = new List<DetalleGestion>();
                SqlConnection conexion = (SqlConnection)context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "crudDetalleGestion";
                comando.Parameters.Add("@opcion", SqlDbType.Int).Value = 4;
                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    DetalleGestion obj = new DetalleGestion();
                    obj.Id = (int)reader["id"];
                    obj.Operador = (int)reader["operador"];
                    obj.IdActividad = (int)reader["idActividad"];
                    obj.Fechainicio = (string)reader["fechainicio"];
                    obj.Fechafin = (string)reader["fechafin"];
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

        // GET api/<DetalleGestionController>/5
        [HttpGet]
        [Route("one/{id}")]
        public IActionResult One(int id)
        {
            try
            {
                List<DetalleGestion> list = new List<DetalleGestion>();
                SqlConnection conexion = (SqlConnection)context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "crudDetalleGestion";
                comando.Parameters.Add("@id", SqlDbType.Int).Value = id;
                comando.Parameters.Add("@opcion", SqlDbType.Int).Value = 5;
                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    DetalleGestion obj = new DetalleGestion();
                    obj.Id = (int)reader["id"];
                    obj.Operador = (int)reader["operador"];
                    obj.IdActividad = (int)reader["idActividad"];
                    obj.Fechainicio = (string)reader["fechainicio"];
                    obj.Fechafin = (string)reader["fechafin"];
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

        // POST api/<DetalleGestionController>
        [HttpPost]
        [Route("store")]
        public IActionResult store(JObject request)
        {
            try
            {
                int operador = Int32.Parse(request.GetValue("operador").ToString());
                int idActividad = Int32.Parse(request.GetValue("idActividad").ToString());
                string fechainicio = request.GetValue("fechainicio").ToString();
                string fechafin = request.GetValue("fechafin").ToString();
                int estado = Int32.Parse(request.GetValue("estado").ToString());

                SqlConnection conexion = (SqlConnection)context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "crudDetalleGestion";
                comando.Parameters.AddWithValue("@operador", operador);
                comando.Parameters.AddWithValue("@idActividad", idActividad);
                comando.Parameters.AddWithValue("@fechainicio", fechainicio);
                comando.Parameters.AddWithValue("@fechafin", fechafin);
                comando.Parameters.AddWithValue("@estado", estado);
                comando.Parameters.AddWithValue("@opcion", 1);
                SqlDataAdapter adapter = new SqlDataAdapter(comando);
                DataSet setter = new DataSet();
                adapter.Fill(setter, "tabla");
                dynamic resultado = new JObject();
                resultado.response = 1;
                resultado.message = "DetalleGestion Agregada con Exito";
                resultado.value = 1;

                conexion.Close();

                return Ok(resultado);

            }
            catch
            {
                return BadRequest("Error.");
            }
        }

        // PUT api/<DetalleGestionController>/5
        [HttpPost]
        [Route("update")]
        public IActionResult update(JObject request)
        {
            try
            {
                int id = Int32.Parse(request.GetValue("id").ToString());
                int operador = Int32.Parse(request.GetValue("operador").ToString());
                int idActividad = Int32.Parse(request.GetValue("idActividad").ToString());
                string fechainicio = request.GetValue("fechainicio").ToString();
                string fechafin = request.GetValue("fechafin").ToString();
                int estado = Int32.Parse(request.GetValue("estado").ToString());

                SqlConnection conexion = (SqlConnection)context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "crudDetalleGestion";
                comando.Parameters.AddWithValue("@id", id);
                comando.Parameters.AddWithValue("@operador", operador);
                comando.Parameters.AddWithValue("@idActividad", idActividad);
                comando.Parameters.AddWithValue("@fechainicio", fechainicio);
                comando.Parameters.AddWithValue("@fechafin", fechafin);
                comando.Parameters.AddWithValue("@estado", estado);
                comando.Parameters.AddWithValue("@opcion", 2);
                SqlDataAdapter adapter = new SqlDataAdapter(comando);
                DataSet setter = new DataSet();
                adapter.Fill(setter, "tabla");
                dynamic resultado = new JObject();
                resultado.response = 1;
                resultado.message = "DetalleGestion Modificada con Exito";
                resultado.value = 1;

                conexion.Close();

                return Ok(resultado);

            }
            catch
            {
                return BadRequest("Error.");
            }
        }

        // DELETE api/<DetalleGestionController>/5
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
                comando.CommandText = "crudDetalleGestion";
                comando.Parameters.AddWithValue("@id", id);
                comando.Parameters.AddWithValue("@estado", estado);
                comando.Parameters.AddWithValue("@opcion", 3);
                SqlDataAdapter adapter = new SqlDataAdapter(comando);
                DataSet setter = new DataSet();
                adapter.Fill(setter, "tabla");
                dynamic resultado = new JObject();
                resultado.response = 1;
                resultado.message = "DetalleGestion Eliminada con Exito";
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
