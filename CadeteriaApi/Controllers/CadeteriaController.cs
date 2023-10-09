using _Cadeteria;
using Microsoft.AspNetCore.Mvc;
using CadeteriaAPI.AccesoData;

namespace CadeteriaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CadeteriaController : ControllerBase
    {
      
        private readonly ILogger<CadeteriaController> _logger;
        private Cadeteria cadeteria;
        public CadeteriaController(ILogger<CadeteriaController> logger)
        {
            _logger = logger;
            AccesoDatosPedidos accesoDatosPedidos = new AccesoDatosPedidos();
            AccesoDatosCadeteria accesoDatosCadeteria = new AccesoDatosCadeteria();
            AccesoDatosCadetes accesoDatosCadetes = new AccesoDatosCadetes();
            cadeteria = Cadeteria.GetCadeteria(accesoDatosPedidos, accesoDatosCadeteria, accesoDatosCadetes);
        }

        [HttpGet(Name = "GetNombre")]
        public ActionResult<string> GetNombre()
        {
            if (!String.IsNullOrEmpty(cadeteria.Nombre))
            {
                return Ok(cadeteria.Nombre);
            }
            else
            {
                return BadRequest("Todo mal"); 
            }
            
        }

        [HttpGet]
        [Route("Cadetes")]
        public ActionResult<IEnumerable<Cadete>> GetCadetes()
        {
            var cadetes = cadeteria.Cadetes;
            return Ok(cadetes);
        }

        [HttpGet]
        [Route("NombreCadete2")]
        public ActionResult<IEnumerable<Cadete>> GetCadeteNombre(int id)
        {
            //var cadetes = cadeteria.Cadetes;
            var cad = cadeteria.BuscarCadetePorID(id);
            if (cad is not null)
            {
                return Ok("Lo encontramos xdxdxdxd wiiiii");
            }
            else
            {
                return NotFound("no se encontro nada");  
            }
            
            
        }

        [HttpGet]
        [Route("Informe")]
        public ActionResult<IEnumerable<string>> GetInforme()
        {
            var info = cadeteria.GenerarInforme();
            return Ok(info);
        }

        [HttpPost]
        [Route("AgregarPedido")]
        public ActionResult CrearPedido(int numeroPedido, string observacionPedido, string nombreCliente, string direccionCliente, int telefonoCliente, string datosReferenciaDireccionCliente)
        {
            cadeteria.CrearPedido(numeroPedido, observacionPedido, nombreCliente,direccionCliente, telefonoCliente,datosReferenciaDireccionCliente);
            return Ok();
        }

        [HttpPut]
        [Route("AsignarPedido")]
        public ActionResult AsignarPedido(int idCadete,int idPedido)
        {
            cadeteria.AsignarCadeteAPedido(idCadete,idPedido);
            return Ok();
        }

        [HttpPut]
        [Route("CambiarEstado")]
        public ActionResult CambiarEstado(int idPedido)
        {
            cadeteria.CambiarEstadoPedido(idPedido);
            return Ok();
        }

        [HttpPut]
        [Route("ReasignarPedido")]
        public ActionResult ReasignarPedido(int idDestino, int numeroPedido)
        {
            cadeteria.ReasignarPedido(idDestino,numeroPedido);
            return Ok();
        }

        [HttpGet]
        [Route("GetPedido")]
        public ActionResult<Pedido> GetPedido(int numero)
        {
            var pedido = cadeteria.BuscarPedidoPorNro(numero);
            if (pedido is not null)
            {
                return Ok(pedido);
            }
            else
            {
                return NotFound("No existe tal pedido");  
            }
            
        }

        [HttpGet]
        [Route("GetCadete")]
        public ActionResult<Cadete> GetCadete(int id)
        {
            var cadete = cadeteria.BuscarCadetePorID(id);
            if (cadete is not null)
            {
                return Ok(cadete);
            }
            else
            {
                return NotFound("No existe el cadete");
            }

        }

        [HttpPost]
        [Route("AgregarCadete")]
        public ActionResult AgrregarCadete(Cadete cadete)
        {
            if (cadete is not null)
            {
                cadeteria.agregarCadete(cadete);
                return Ok();
            }
            else
            {
                return BadRequest("No es valido el cadete");    
            }   
        }
    }
}