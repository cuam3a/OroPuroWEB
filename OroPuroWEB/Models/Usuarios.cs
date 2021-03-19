using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OroPuroWEB.Models
{
    public class Usuarios
    {
        private Prod_OroPuroEntities db = new Prod_OroPuroEntities();

        public class LoginDatos
        {
            public string Usuario { get; set; }
            public string Contrasena { get; set; }
            public string Mensaje { get; set; }
            public string Nombre { get; set; }
        }

        public LoginDatos Login(string _Usuario = "", string _Contrasena = "")
        {
            var Resultado = new LoginDatos();
            var Autorizacion = db.SBO_SP_IMF_LoginWeb(_Usuario, _Contrasena).FirstOrDefault();
            if(Autorizacion != null)
            {
                switch(Autorizacion.Status)
                {
                    case 1:
                        Resultado.Nombre = (Autorizacion.Name?.ToUpper() ?? "") + " " + (Autorizacion.lastName?.ToUpper() ?? "");
                        Resultado.Mensaje = "OK";
                        break;
                    case 2:
                        Resultado.Mensaje = "USUARIO INACTIVO";
                        break;
                    case 3:
                        Resultado.Mensaje = "CONTRASEÑA INCORRECTA";
                        break;
                    default:
                        Resultado.Mensaje = "USUARIO NO EXISTE";
                        break;
                }
            }
            else
            {
                Resultado.Mensaje = "USUARIO NO EXISTE";
            }
            return Resultado;
        }
    }
}