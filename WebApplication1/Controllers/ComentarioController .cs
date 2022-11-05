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
    public class ComentarioController : ControllerBase
    {
        private readonly ConexionSQLServer context;

        public ComentarioController(ConexionSQLServer context)
        {
            this.context = context;
        }

        // GET: api/<ComentarioController>
        [HttpGet]
        [Route("all")]
        public IActionResult GetAll()
        {
            try
            {
                List<Comentario> list = new List<Comentario>();
                SqlConnection conexion = (SqlConnection)context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "crudComentario";
                comando.Parameters.Add("@opcion", SqlDbType.Int).Value = 4;
                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Comentario obj = new Comentario();
                    obj.Id = (int)reader["id"];
                    obj.comentario = (string)reader["comentario"];
                    obj.IdPersona = (int)reader["idPersona"];
                    obj.IdDetalleGestion = (int)reader["idDetalleGestion"];
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

        // GET api/<ComentarioController>/5
        [HttpGet]
        [Route("one/{id}")]
        public IActionResult One(int id)
        {
            try
            {
                List<Comentario> list = new List<Comentario>();
                SqlConnection conexion = (SqlConnection)context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "crudComentario";
                comando.Parameters.Add("@id", SqlDbType.Int).Value = id;
                comando.Parameters.Add("@opcion", SqlDbType.Int).Value = 5;
                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Comentario obj = new Comentario();
                    obj.Id = (int)reader["id"];
                    obj.comentario = (string)reader["comentario"];
                    obj.IdPersona = (int)reader["idPersona"];
                    obj.IdDetalleGestion = (int)reader["idDetalleGestion"];
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

        // POST api/<ComentarioController>
        [HttpPost]
        [Route("store")]
        public IActionResult store(JObject request)
        {
            try
            {
                string comentario = request.GetValue("comentario").ToString();
                int idDetalleGestion = Int32.Parse(request.GetValue("idDetalleGestion").ToString());
                int idPersona = Int32.Parse(request.GetValue("idPersona").ToString());
                int estado = Int32.Parse(request.GetValue("estado").ToString());

                SqlConnection conexion = (SqlConnection)context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "crudComentario";
                comando.Parameters.AddWithValue("@comentario", comentario);
                comando.Parameters.AddWithValue("@idDetalleGestion", idDetalleGestion);
                comando.Parameters.AddWithValue("@idPersona", idPersona);
                comando.Parameters.AddWithValue("@estado", estado);
                comando.Parameters.AddWithValue("@opcion", 1);
                SqlDataAdapter adapter = new SqlDataAdapter(comando);
                DataSet setter = new DataSet();
                adapter.Fill(setter, "tabla");
                dynamic resultado = new JObject();
                resultado.response = 1;
                resultado.message = "Comentario Agregada con Exito";
                resultado.value = 1;

                conexion.Close();

                return Ok(resultado);

            }
            catch
            {
                return BadRequest("Error.");
            }
        }

        // PUT api/<ComentarioController>/5
        [HttpPost]
        [Route("update")]
        public IActionResult update(JObject request)
        {
            try
            {
                int id = Int32.Parse(request.GetValue("id").ToString());
                string comentario = request.GetValue("comentario").ToString();
                int idDetalleGestion = Int32.Parse(request.GetValue("idDetalleGestion").ToString());
                int idPersona = Int32.Parse(request.GetValue("idPersona").ToString());
                int estado = Int32.Parse(request.GetValue("estado").ToString());

                SqlConnection conexion = (SqlConnection)context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "crudComentario";
                comando.Parameters.AddWithValue("@id", id);
                comando.Parameters.AddWithValue("@comentario", comentario);
                comando.Parameters.AddWithValue("@idDetalleGestion", idDetalleGestion);
                comando.Parameters.AddWithValue("@idPersona", idPersona);
                comando.Parameters.AddWithValue("@estado", estado);
                comando.Parameters.AddWithValue("@opcion", 2);
                SqlDataAdapter adapter = new SqlDataAdapter(comando);
                DataSet setter = new DataSet();
                adapter.Fill(setter, "tabla");
                dynamic resultado = new JObject();
                resultado.response = 1;
                resultado.message = "Comentario Modificada con Exito";
                resultado.value = 1;

                conexion.Close();

                return Ok(resultado);

            }
            catch
            {
                return BadRequest("Error.");
            }
        }

        // DELETE api/<ComentarioController>/5
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
                comando.CommandText = "crudComentario";
                comando.Parameters.AddWithValue("@id", id);
                comando.Parameters.AddWithValue("@estado", estado);
                comando.Parameters.AddWithValue("@opcion", 3);
                SqlDataAdapter adapter = new SqlDataAdapter(comando);
                DataSet setter = new DataSet();
                adapter.Fill(setter, "tabla");
                dynamic resultado = new JObject();
                resultado.response = 1;
                resultado.message = "Comentario Eliminada con Exito";
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
