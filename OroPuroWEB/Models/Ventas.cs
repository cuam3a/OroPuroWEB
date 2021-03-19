using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OroPuroWEB.Models
{
    public class Ventas
    {
        private Prod_OroPuroEntities db = new Prod_OroPuroEntities();

        public List<VentaLitrosKilos> DatosVentaListrosKilo(DateTime _FechaInicio, DateTime _FechaFin, string _Ruta = "", string _Tienda = "", int _Tipo = 0)
        {
            var Resultado = new List<VentaLitrosKilos>();

            var ListaVenta = db.IMF_SP_Ventas(_FechaInicio, _FechaFin, (_Ruta != "" ? _Ruta : null), (_Tienda != "" ? _Tienda : null), (_Tipo != 0 ? _Tipo.ToString() : null), null).ToList();
            foreach(var Item in ListaVenta)
            {
                var Venta = new VentaLitrosKilos();
                Venta.Ruta = (Item.SlpName?.ToUpper() ?? "");
                Venta.FechaVenta = (Item.DocDate ?? DateTime.MinValue);
                Venta.CodigoCliente = (Item.U_CardCode?.ToUpper() ?? "");
                Venta.Cliente = (Item.U_CardName?.ToUpper() ?? "");
                Venta.CodigoProducto = (Item.ItemCode ?? "");
                Venta.Producto = (Item.Dscription?.ToUpper() ?? "");
                Venta.Cantidad = (Item.OpenQty ?? 0);
                Venta.Litros = (Item.Litros ?? 0);
                Venta.Kilos = (Item.Kilos ?? 0);
                Venta.PrecioU = (Item.Price ?? 0);
                Venta.Total = (Item.LineTotal ?? 0);
                Venta.Tienda = (Item.Descr?.ToUpper() ?? "");
                Venta.Folio = (Item.U_StrFolio?.ToUpper() ?? "");
                Venta.TipoVista = _Tipo;
                Venta.id = Item.DocNum ?? 0;
                Resultado.Add(Venta);
            }

            return Resultado;
        }

        public class VentaLitrosKilos
        {
            public string Ruta { get; set; }
            public DateTime FechaVenta { get; set; }
            public string CodigoCliente { get; set; }
            public string Cliente { get; set; }
            public string CodigoProducto { get; set; }
            public string Producto { get; set; }
            public decimal Cantidad { get; set; }
            public decimal Litros { get; set; }
            public decimal Kilos { get; set; }
            public decimal PrecioU { get; set; }
            public decimal Total { get; set; }
            public string Tienda { get; set; }
            public string Folio { get; set; }
            public int TipoVista { get; set; }
            public int id { get; set; }
        }
    }
}