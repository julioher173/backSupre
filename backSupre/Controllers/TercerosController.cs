using backSupre.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Data;
using System.Data.SqlClient;


namespace backSupre.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TercerosController : ControllerBase
    {
        private readonly string cadenaSQL;

        public TercerosController(IConfiguration config)
        {
            cadenaSQL = config.GetConnectionString("Connexion");
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<TercerosModels> lista = new List<TercerosModels>();

            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("listar_user", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {

                        while (rd.Read())
                        {

                            lista.Add(new TercerosModels
                            {
                                Id = Convert.ToInt32(rd["id"]),
                                primer_nombre = rd["primer_nombre"].ToString(),
                                segundo_nombre = rd["segundo_nombre"].ToString(),
                                primer_apellido = rd["primer_apellido"].ToString(),
                                segundo_apellido = rd["segundo_apellido"].ToString(),
                                iden = rd["iden"].ToString(),
                                tipo_iden = rd["tipo_iden"].ToString(),
                                direccion = rd["direccion"].ToString(),
                                email = rd["email"].ToString(),
                            });
                        }

                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });
            }
            catch (Exception error)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, response = lista });

            }
        }



        [HttpGet]
        [Route("obtener/{id:int}")]
        public IActionResult Obtener(int id)
        {
            List<TercerosModels> lista = new List<TercerosModels>();
            TercerosModels User = new TercerosModels();

            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("listar_user", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {

                        while (rd.Read())
                        {

                            lista.Add(new TercerosModels
                            {
                                Id = Convert.ToInt32(rd["id"]),
                                primer_nombre = rd["primer_nombre"].ToString(),
                                segundo_nombre = rd["segundo_nombre"].ToString(),
                                primer_apellido = rd["primer_apellido"].ToString(),
                                segundo_apellido = rd["segundo_apellido"].ToString(),
                                iden = rd["iden"].ToString(),
                                tipo_iden = rd["tipo_iden"].ToString(),
                                direccion = rd["direccion"].ToString(),
                            });
                        }

                    }
                }

                User = lista.Where(item => item.Id == id).FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = User });
            }
            catch (Exception error)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, response = User });

            }
        }


        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] TercerosModels objeto)
        {
            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("create_user", conexion);
                    cmd.Parameters.AddWithValue("tipo_iden", objeto.tipo_iden);
                    cmd.Parameters.AddWithValue("iden", objeto.iden);
                    cmd.Parameters.AddWithValue("primer_nombre", objeto.primer_nombre);
                    cmd.Parameters.AddWithValue("segundo_nombre", objeto.segundo_nombre);
                    cmd.Parameters.AddWithValue("primer_apellido", objeto.primer_apellido);
                    cmd.Parameters.AddWithValue("segundo_apellido", objeto.segundo_apellido);
                    cmd.Parameters.AddWithValue("razon_social", objeto.razon_social);
                    cmd.Parameters.AddWithValue("fecha_expedicion", objeto.fecha_expedicion);
                    cmd.Parameters.AddWithValue("fecha_nacimiento", objeto.fecha_nacimiento);
                    cmd.Parameters.AddWithValue("direccion", objeto.direccion);
                    cmd.Parameters.AddWithValue("email", objeto.email);
                    cmd.Parameters.AddWithValue("cecular", objeto.cecular);
                    cmd.Parameters.AddWithValue("estado", objeto.estado);



                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "agregado" });
            }
            catch (Exception error)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });

            }
        }

        [HttpPut]
        [Route("Editar")]
        public IActionResult Editar([FromBody] TercerosModels objeto)
        {
            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("update_estado_user", conexion);
                    cmd.Parameters.AddWithValue("id", objeto.Id == 0 ? DBNull.Value : objeto.Id);
                    cmd.Parameters.AddWithValue("estado", objeto.estado == 0 ? DBNull.Value : objeto.estado);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "editado" });
            }
            catch (Exception error)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });

            }
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public IActionResult Eliminar(int id)
        {
            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("delete_user", conexion);
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "eliminado" });
            }
            catch (Exception error)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });

            }
        }


    }
}
