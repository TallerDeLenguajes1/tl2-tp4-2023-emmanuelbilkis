using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CadeteriaAPI;

namespace CadeteriaAPI
{
    public class Cadete
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public int Telefono { get; set; }

        public Cadete(int id, string nombre, string direccion, int telefono)
        {
            Id = id;
            Nombre = nombre;
            Direccion = direccion;
            Telefono = telefono;
        }

        public string GetInformacionCadete()
        {
            string info = $"Cadete: {Nombre} - ID: {Id}";
            return info;
        }
    }
}
