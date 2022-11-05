using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json.Linq;
using parcialE.Contexts;
using System.Collections.Generic;
using System.Data;
using System;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BitacoraController : ControllerBase
    {
        private readonly ConexionSQLServer context;

        public BitacoraController(ConexionSQLServer context)
        {
            this.context = context;
        }

        // GET: api/<BitacoraController>
        [HttpGet]
        [Route("all")]
        public IActionResult GetAll()
        {
            DateTime date = DateTime.Now;

            try
            {
                List<Bitacora> list = new List<Bitacora>();
                SqlConnection conexion = (SqlConnection)context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "crudBitacora";
                comando.Parameters.Add("@opcion", SqlDbType.Int).Value = 4;
                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Bitacora obj = new Bitacora();
                    obj.Id = (int)reader["id"];
                    obj.Tabla = (string)reader["Tabla"];
                    obj.IdTabla = (int)reader["IdTabla"];
                    obj.CamposJson = (string)reader["CamposJson"];
                    obj.Usuario = (int)reader["usuario"];
                    obj.Fecha = (string)reader["Fecha"];
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

        // GET api/<BitacoraController>/5
        [HttpGet]
        [Route("one")]
        public IActionResult One(JObject request)
        {
            int id = Int32.Parse(request.GetValue("id").ToString());
            try
            {
                List<Bitacora> list = new List<Bitacora>();
                SqlConnection conexion = (SqlConnection)context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "crudBitacora";
                comando.Parameters.Add("@id", SqlDbType.Int).Value = id;
                comando.Parameters.Add("@opcion", SqlDbType.Int).Value = 5;
                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Bitacora obj = new Bitacora();
                    obj.Id = (int)reader["id"];
                    obj.Tabla = (string)reader["Tabla"];
                    obj.IdTabla = (int)reader["IdTabla"];
                    obj.CamposJson = (string)reader["CamposJson"];
                    obj.Usuario = (int)reader["usuario"];
                    obj.Fecha = (string)reader["Fecha"];
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

        // POST api/<BitacoraController>
        [HttpPost]
        [Route("store")]
        public IActionResult store(JObject request)
        {
            try
            {
                string Tabla = request.GetValue("Tabla").ToString();
                int IdTabla = Int32.Parse(request.GetValue("IdTabla").ToString());
                string CamposJson = request.GetValue("CamposJson").ToString();
                int Usuario = Int32.Parse(request.GetValue("Usuario").ToString());
                string Fecha = request.GetValue("Fecha").ToString();

                SqlConnection conexion = (SqlConnection)context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "crudBitacora";
                comando.Parameters.AddWithValue("@tabla", Tabla);
                comando.Parameters.AddWithValue("@idtabla", IdTabla);
                comando.Parameters.AddWithValue("@CamposJson", CamposJson);
                comando.Parameters.AddWithValue("@Usuario", Usuario);
                comando.Parameters.AddWithValue("@Fecha", Fecha);
                comando.Parameters.AddWithValue("@opcion", 1);
                SqlDataAdapter adapter = new SqlDataAdapter(comando);
                DataSet setter = new DataSet();
                adapter.Fill(setter, "tabla");
                dynamic resultado = new JObject();
                resultado.response = 1;
                resultado.message = "Bitacora Agregada con Exito";
                resultado.value = 1;

                conexion.Close();

                return Ok(resultado);

            }
            catch
            {
                return BadRequest("Error.");
            }
        }

        // PUT api/<BitacoraController>/5
        [HttpPost]
        [Route("update")]
        public IActionResult update(JObject request)
        {
            try
            {
                int id = Int32.Parse(request.GetValue("id").ToString());
                string Tabla = request.GetValue("Tabla").ToString();
                int IdTabla = Int32.Parse(request.GetValue("IdTabla").ToString());
                string CamposJson = request.GetValue("CamposJson").ToString();
                int Usuario = Int32.Parse(request.GetValue("Usuario").ToString());
                string Fecha = request.GetValue("Fecha").ToString();

                SqlConnection conexion = (SqlConnection)context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "crudBitacora";
                comando.Parameters.AddWithValue("id", id);
                comando.Parameters.AddWithValue("@tabla", Tabla);
                comando.Parameters.AddWithValue("@idtabla", IdTabla);
                comando.Parameters.AddWithValue("@CamposJson", CamposJson);
                comando.Parameters.AddWithValue("@Usuario", Usuario);
                comando.Parameters.AddWithValue("@Fecha", Fecha);
                comando.Parameters.AddWithValue("@opcion", 2);
                SqlDataAdapter adapter = new SqlDataAdapter(comando);
                DataSet setter = new DataSet();
                adapter.Fill(setter, "tabla");
                dynamic resultado = new JObject();
                resultado.response = 1;
                resultado.message = "Bitacora Modificada con Exito";
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
