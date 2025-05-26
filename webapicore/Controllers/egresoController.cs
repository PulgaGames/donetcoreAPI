using comun.viewmodels;
using logicanegocio.BLL;
using modelo.modelos;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class egresoController : ControllerBase
    {
        [HttpGet("leer-todo")]
        public ActionResult<RepuestaVMR<listadopaginadovmr<egresovmr>>> LeerTodo(int cantidad = 10, int pagina = 0, string? texto = null)
        {
            var respuesta = new RepuestaVMR<listadopaginadovmr<egresovmr>>();

            try
            {
                respuesta.datos = egresoBLL.leertodo(cantidad, pagina, texto);

                foreach (var egreso in respuesta.datos.elemento)
                {
                    egreso.medico = medicoBLL.leeruno(egreso.medicoid);
                    egreso.ingreso = ingresoBLL.leeruno(egreso.ingresoid);

                    if (egreso.ingreso != null)
                    {
                        egreso.ingreso.paciente = pacienteBLL.leeruno(egreso.ingreso.pacienteid);
                    }
                }
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
        public ActionResult<RepuestaVMR<egresovmr>> LeerUno(long id)
        {
            var respuesta = new RepuestaVMR<egresovmr>();

            try
            {
                var egreso = egresoBLL.leeruno(id);

                if (egreso != null)
                {
                    egreso.ingreso = ingresoBLL.leeruno(egreso.ingresoid);
                    egreso.medico = medicoBLL.leeruno(egreso.medicoid);
                }

                respuesta.datos = egreso;
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
        public ActionResult<RepuestaVMR<long?>> Crear([FromBody] egreso item)
        {
            var respuesta = new RepuestaVMR<long?>();

            try
            {
                respuesta.datos = egresoBLL.crear(item);
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
        public ActionResult<RepuestaVMR<bool>> Actualizar(long id, [FromBody] egresovmr item)
        {
            var respuesta = new RepuestaVMR<bool>();

            try
            {
                item.id = id;
                egresoBLL.actualizar(item);
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
                egresoBLL.eliminar(ids);
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
