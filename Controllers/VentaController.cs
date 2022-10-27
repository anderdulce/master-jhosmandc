using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Modelss;
using WebApplication1.Repository;


namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        [HttpGet("GetVentas")]
        public List<Venta> Get()
        {
            return ADO_Venta.DevolverVenta();
        }

        [HttpPost]
        public void CargarVenta([FromBody] VentaProducto ventas)
        {
            ADO_Venta.CargarVenta(ventas);
        }
        
    }

}
