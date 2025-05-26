using comun.viewmodels;
using logicanegocio.BLL;
using modelo.modelos;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class medicoController : ControllerBase
    {
        [HttpGet("leer-todo")]
        public ActionResult<RepuestaVMR<listadopaginadovmr<medicovmr>>> LeerTodo(int cantidad = 10, int pagina = 0, string? texto = null)
        {
            var respuesta = new RepuestaVMR<listadopaginadovmr<medicovmr>>();

            try
            {
                respuesta.datos = medicoBLL.leertodo(cantidad, pagina, texto);
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
        public ActionResult<RepuestaVMR<medicovmr>> LeerUno(long id)
        {
            var respuesta = new RepuestaVMR<medicovmr>();

            try
            {
                respuesta.datos = medicoBLL.leeruno(id);
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
        public ActionResult<RepuestaVMR<long?>> Crear([FromBody] medico item)
        {
            var respuesta = new RepuestaVMR<long?>();

            try
            {
                respuesta.datos = medicoBLL.crear(item);
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
        public ActionResult<RepuestaVMR<bool>> Actualizar(long id, [FromBody] medicovmr item)
        {
            var respuesta = new RepuestaVMR<bool>();

            try
            {
                item.id = id;
                medicoBLL.actualizar(item);
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
                medicoBLL.eliminar(ids);
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
