using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static OroPuroWEB.Models.Usuarios;

namespace OroPuroWEB.Controllers
{
    public class OroPuroController : Controller
    {
        private static OroPuroWEB.Models.Usuarios oUsuario = new OroPuroWEB.Models.Usuarios();
        private static OroPuroWEB.Models.Ventas oVentas = new OroPuroWEB.Models.Ventas();
        // GET: OroPuro
        public ActionResult Index()
        {
            return View(new LoginDatos());
        }

        [HttpPost]
        public ActionResult Index(LoginDatos _Login)
        {
            var Resultado = oUsuario.Login(_Login.Usuario, _Login.Contrasena);
            if(Resultado.Mensaje == "OK")
            {
                Session["Nombre"] = Resultado.Nombre;
                return RedirectToAction("Inicio");
            }
            _Login.Mensaje = Resultado.Mensaje;
            return View(_Login);
        }

        public ActionResult Inicio()
        {
            if(Session["Nombre"] == null || Session["Nombre"].ToString() == "")
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        //VENTAS
        public ActionResult VentasLitrosKilos(string _FechaInicio = "", string _FechaFin = "", string _Ruta = "", string _Tienda = "", int _Tipo = 0)
        {
            if (Session["Nombre"] == null || Session["Nombre"].ToString() == "")
            {
                return RedirectToAction("Index");
            }

            var FechaInicio = DateTime.Now;
            var FechaFin = DateTime.Now;
            if(_FechaInicio != "")
            {
                DateTime.TryParse(_FechaInicio, out FechaInicio);
            }
            if (_FechaFin != "")
            {
                DateTime.TryParse(_FechaFin, out FechaFin);
            }

            var ListaVenta = oVentas.DatosVentaListrosKilo(FechaInicio, FechaFin, _Ruta, _Tienda, _Tipo);

            ViewBag.FechaInicio = FechaInicio.ToString("yyyy-MM-dd");
            ViewBag.FechaFin = FechaFin.ToString("yyyy-MM-dd");
            ViewBag.Ruta = _Ruta;
            ViewBag.Tienda = _Tienda;
            ViewBag.Tipo = _Tipo;

            return View(ListaVenta);
        }
    }
}