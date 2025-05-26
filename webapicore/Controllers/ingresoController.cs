using comun.viewmodels;
using logicanegocio.BLL;
using modelo.modelos;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ingresoController : ControllerBase
    {
        [HttpGet("leer-todo")]
        public ActionResult<RepuestaVMR<listadopaginadovmr<ingresovmr>>> LeerTodo(int cantidad = 10, int pagina = 0, string? texto = null)
        {
            var respuesta = new RepuestaVMR<listadopaginadovmr<ingresovmr>>();

            try
            {
                respuesta.datos = ingresoBLL.leertodo(cantidad, pagina, texto);
            }
            catch (Exception e)
            {
                respuesta.codigo = HttpStatusCode.InternalServerError;
                respuesta.datos = null;
                respuesta.mensaje.Add(e.Message);
                respuesta.mensaje.Add(e.ToString());
            }

            return StatusCode((int)respuesta.codigo, respuesta);
        }

        [HttpGet("{id:long}")]
        public ActionResult<RepuestaVMR<ingresovmr>> LeerUno(long id)
        {
            var respuesta = new RepuestaVMR<ingresovmr>();

            try
            {
                var ingreso = ingresoBLL.leeruno(id);

                if (ingreso != null)
                {
                    ingreso.paciente = pacienteBLL.leeruno(ingreso.pacienteid);
                    ingreso.medico = medicoBLL.leeruno(ingreso.medicoid);
                }

                respuesta.datos = ingreso;
            }
            catch (Exception e)
            {
                respuesta.codigo = HttpStatusCode.InternalServerError;
                respuesta.datos = null;
                respuesta.mensaje.Add(e.Message);
                respuesta.mensaje.Add(e.ToString());
            }

            if (respuesta.datos == null && !respuesta.mensaje.Any())
            {
                respuesta.codigo = HttpStatusCode.NotFound;
                respuesta.mensaje.Add("No se encontró el registro");
            }
            else
            {
                respuesta.codigo = HttpStatusCode.OK;
            }

            return StatusCode((int)respuesta.codigo, respuesta);
        }

        [HttpPost]
        public ActionResult<RepuestaVMR<long?>> Crear([FromBody] ingreso item)
        {
            var respuesta = new RepuestaVMR<long?>();

            try
            {
                respuesta.datos = ingresoBLL.crear(item);
            }
            catch (Exception e)
            {
                respuesta.codigo = HttpStatusCode.InternalServerError;
                respuesta.datos = null;
                respuesta.mensaje.Add(e.Message);
                respuesta.mensaje.Add(e.ToString());
            }

            return StatusCode((int)respuesta.codigo, respuesta);
        }

        [HttpPut("{id:long}")]
        public ActionResult<RepuestaVMR<bool>> Actualizar(long id, [FromBody] ingresovmr item)
        {
            var respuesta = new RepuestaVMR<bool>();

            try
            {
                item.id = id;
                ingresoBLL.actualizar(item);
                respuesta.datos = true;
            }
            catch (Exception e)
            {
                respuesta.codigo = HttpStatusCode.InternalServerError;
                respuesta.datos = false;
                respuesta.mensaje.Add(e.Message);
                respuesta.mensaje.Add(e.ToString());
            }

            return StatusCode((int)respuesta.codigo, respuesta);
        }

        [HttpDelete]
        public ActionResult<RepuestaVMR<bool>> Eliminar([FromBody] List<long> ids)
        {
            var respuesta = new RepuestaVMR<bool>();

            try
            {
                ingresoBLL.eliminar(ids);
                respuesta.datos = true;
            }
            catch (Exception e)
            {
                respuesta.codigo = HttpStatusCode.InternalServerError;
                respuesta.datos = false;
                respuesta.mensaje.Add(e.Message);
                respuesta.mensaje.Add(e.ToString());
            }

            return StatusCode((int)respuesta.codigo, respuesta);
        }
    }
}
