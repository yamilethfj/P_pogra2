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
    public class DetalleActividadController : ControllerBase
    {

        private readonly ConexionSQLServer context;

        public DetalleActividadController(ConexionSQLServer context)
        {
            this.context = context;
        }

        // GET: api/<DetalleActividadController>
        [HttpGet]
        [Route("all")]
        public IActionResult GetAll()
        {
            try
            {
                List<DetalleActividad> list = new List<DetalleActividad>();
                SqlConnection conexion = (SqlConnection)context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "crudDetalleActividad";
                comando.Parameters.Add("@opcion", SqlDbType.Int).Value = 4;
                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    DetalleActividad obj = new DetalleActividad();
                    obj.Id = (int)reader["id"];
                    obj.IdDetalleGestion = (int)reader["IdDetalleGestion"];
                    obj.IdAccion = (int)reader["IdAccion"];
                    obj.Porcentaje = (double)reader["porcentaje"];
                    obj.FechaInicio = reader["fechaInicio"].ToString();
                    obj.FechaFin = reader["fechaFin"].ToString();
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

        // GET api/<DetalleActividadController>/5
        [HttpGet]
        [Route("one")]
        public IActionResult One(JObject request)
        {
            int id = Int32.Parse(request.GetValue("id").ToString());
            try
            {
                List<DetalleActividad> list = new List<DetalleActividad>();
                SqlConnection conexion = (SqlConnection)context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "crudDetalleActividad";
                comando.Parameters.Add("@id", SqlDbType.Int).Value = id;
                comando.Parameters.Add("@opcion", SqlDbType.Int).Value = 5;
                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    DetalleActividad obj = new DetalleActividad();
                    obj.Id = (int)reader["id"];
                    obj.IdDetalleGestion = (int)reader["IdDetalleGestion"];
                    obj.IdAccion = (int)reader["IdAccion"];
                    obj.Porcentaje = (double)reader["porcentaje"];
                    obj.FechaInicio = reader["fechaInicio"].ToString();
                    obj.FechaFin = reader["fechaFin"].ToString();
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

        // POST api/<DetalleActividadController>
        [HttpPost]
        [Route("store")]
        public IActionResult store(JObject request)
        {
            DateTime date = DateTime.Now;

            try
            {
                int IdDetalleGestion = Int32.Parse(request.GetValue("IdDetalleGestion").ToString());
                int IdAccion = Int32.Parse(request.GetValue("IdAccion").ToString());
                Decimal Porcentaje = Decimal.Parse(request.GetValue("porcentaje").ToString());
                string FechaInicio = request.GetValue("Fechainicio").ToString();
                string FechaFin = request.GetValue("fechafin").ToString();
                int Estado = Int32.Parse(request.GetValue("estado").ToString());

                SqlConnection conexion = (SqlConnection)context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "crudDetalleActividad";
                comando.Parameters.AddWithValue("@IdDetalleGestion", IdDetalleGestion);
                comando.Parameters.AddWithValue("@idAccion", IdAccion);
                comando.Parameters.AddWithValue("@Porcentaje", Porcentaje);
                comando.Parameters.AddWithValue("@fechainicio", FechaInicio);
                comando.Parameters.AddWithValue("@fechafin", FechaFin);
                comando.Parameters.AddWithValue("@estado", Estado);
                comando.Parameters.AddWithValue("@opcion", 1);
                SqlDataAdapter adapter = new SqlDataAdapter(comando);
                DataSet setter = new DataSet();
                adapter.Fill(setter, "tabla");
                dynamic resultado = new JObject();
                resultado.response = 1;
                resultado.message = "DetalleActividad Agregada con Exito";
                resultado.value = 1;

                conexion.Close();

                return Ok(resultado);

            }
            catch
            {
                return BadRequest("Error.");
            }
        }

        // PUT api/<DetalleActividadController>/5
        [HttpPost]
        [Route("update")]
        public IActionResult update(JObject request)
        {
            try
            {
                int Id = Int32.Parse(request.GetValue("Id").ToString());
                int IdDetalleGestion = Int32.Parse(request.GetValue("IdDetalleGestion").ToString());
                int IdAccion = Int32.Parse(request.GetValue("IdAccion").ToString());
                decimal Porcentaje = decimal.Parse(request.GetValue("porcentaje").ToString());
                string FechaInicio = request.GetValue("Fechainicio").ToString();
                string FechaFin = request.GetValue("fechafin").ToString();
                int Estado = Int32.Parse(request.GetValue("estado").ToString());

                SqlConnection conexion = (SqlConnection)context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "crudDetalleActividad";
                comando.Parameters.AddWithValue("@id", Id);
                comando.Parameters.AddWithValue("@IdDetalleGestion", IdDetalleGestion);
                comando.Parameters.AddWithValue("@idAccion", IdAccion);
                comando.Parameters.AddWithValue("@Porcentaje", Porcentaje);
                comando.Parameters.AddWithValue("@fechainicio", FechaInicio);
                comando.Parameters.AddWithValue("@fechafin", FechaFin);
                comando.Parameters.AddWithValue("@estado", Estado);
                comando.Parameters.AddWithValue("@opcion", 2);
                SqlDataAdapter adapter = new SqlDataAdapter(comando);
                DataSet setter = new DataSet();
                adapter.Fill(setter, "tabla");
                dynamic resultado = new JObject();
                resultado.response = 1;
                resultado.message = "DetalleActividad Modificada con Exito";
                resultado.value = 1;

                conexion.Close();

                return Ok(resultado);

            }
            catch
            {
                return BadRequest("Error.");
            }
        }

        // DELETE api/<DetalleActividadController>/5
        [HttpPost]
        [Route("destroy")]
        public IActionResult Destroy(JObject request)
        {
            try
            {
                int Id = Int32.Parse(request.GetValue("id").ToString());
                int estado = Int32.Parse(request.GetValue("estado").ToString());

                SqlConnection conexion = (SqlConnection)context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "crudDetalleActividad";
                comando.Parameters.AddWithValue("@id", Id);
                comando.Parameters.AddWithValue("@estado", estado);
                comando.Parameters.AddWithValue("@opcion", 3);
                SqlDataAdapter adapter = new SqlDataAdapter(comando);
                DataSet setter = new DataSet();
                adapter.Fill(setter, "tabla");
                dynamic resultado = new JObject();
                resultado.response = 1;
                resultado.message = "DetalleActividad Eliminada con Exito";
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
