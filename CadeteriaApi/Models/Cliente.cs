using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadeteriaAPI;

namespace _Cadeteria
{
    public class Cliente
    {
        private string nombre;
        private string direccion;
        private int telefono;
        private string datosReferenciaDireccion;
        
        public Cliente(string nombre, string direccion, int telefono, string datosReferenciaDireccion)
        {
            this.nombre = nombre;
            this.direccion = direccion;
            this.telefono = telefono;
            this.datosReferenciaDireccion = datosReferenciaDireccion;
        }

        public string Nombre { get => nombre; set => nombre = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public int Telefono { get => telefono; set => telefono = value; }
        public string DatosReferenciaDireccion { get => datosReferenciaDireccion; set => datosReferenciaDireccion = value; }

        public string InformacionCliente() 
        {
            string info = Nombre + "-" + Telefono + "-" + Direccion;
            return info;
        }
    }
}
