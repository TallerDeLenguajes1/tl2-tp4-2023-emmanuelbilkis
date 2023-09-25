using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadeteriaAPI;

namespace _Cadeteria
{
    public class Cadeteria
    {
        private string nombre;
        private int telefono;
        private List<Cadete> cadetes;
        private List<Pedido> pedidos;
        private static Cadeteria cadeteria;
        // aca agregaria acceso a datos pedidos
        public static Cadeteria GetCadeteria()
        {
            if (cadeteria == null)
            {
                cadeteria = new Cadeteria();
            }
            return cadeteria;
        }
        public Cadeteria() 
        {
            var data = new AccesoDatoCsv();
            var datosCadeteria = data.GetCadeteria("C:\\Repositorios-Taller2-2023\\tl2-tp4-2023-emmanuelbilkis\\CadeteriaAPI\\Conexion\\cadeteria.csv");
            this.nombre = datosCadeteria.Nombre;
            this.Telefono = datosCadeteria.Telefono;
            var datosCadetes = data.GetCadetes("C:\\Repositorios-Taller2-2023\\tl2-tp4-2023-emmanuelbilkis\\CadeteriaAPI\\Conexion\\cadetes.csv");
            this.cadetes = datosCadetes;
            this.Pedidos = new List<Pedido>();
        }

        public Cadeteria(string nombre, int telefono)
        {
            this.Nombre = nombre;
            this.Telefono = telefono;
            this.Cadetes = new List<Cadete>();
            this.Pedidos = new List<Pedido>();
        }

        public string Nombre { get => nombre; set => nombre = value; }
        public int Telefono { get => telefono; set => telefono = value; }
        public List<Cadete> Cadetes { get => cadetes; set => cadetes = value; }
        public List<Pedido> Pedidos { get => pedidos; set => pedidos = value; }

        public void CrearPedido(int numeroPedido, string observacionPedido, string nombreCliente, string direccionCliente, int telefonoCliente, string datosReferenciaDireccionCliente) 
        {
            Pedido pedido = new Pedido(numeroPedido, observacionPedido, nombreCliente, direccionCliente, telefonoCliente, datosReferenciaDireccionCliente);
            cadeteria.Pedidos.Add(pedido);
        }
   
        public void ReasignarPedido(int idDestino,int numeroPedido) 
        {
            var pedido = BuscarPedidoPorNro(numeroPedido);
            pedido.DesasignarCadete();
            AsignarCadeteAPedido(idDestino,numeroPedido);

        }

        public void AsignarCadeteAPedido(int idCadete, int idPedido) 
        {
            var cadete = BuscarCadetePorID(idCadete);
            var pedido = BuscarPedidoPorNro(idPedido);
            pedido.AsignarCadete(cadete);
            //aca agregaria un acceso a dato pedidos para guardarlo en el json 
        }
        public double CobrarJornalVersionJoin(int idCadete)
        {
            var resultado = Cadetes
                .Where(cadete => cadete.Id == idCadete) // Filtra por el idCadete proporcionado
                .Join(Pedidos,
                    cadete => cadete.Id,
                    pedido => pedido.Cadete.Id,
                    (cadete, pedido) => pedido) // Proyecta solo los pedidos relacionados con el cadete
                .ToList(); // Convierte el resultado en una lista

            int cantidadResultados = resultado.Count();
            double jornal = cantidadResultados * 500;
            return jornal;
        }

        public double CobrarJornal(int idCadete)
        {
            Cadete cadete = BuscarCadetePorID(idCadete); // Busca el cadete por su ID

            if (cadete != null) // Verifica si se encontró un cadete con el ID especificado
            {
                int cantidadPedidos = Pedidos
                    .Count(pedido => pedido.Cadete.Id == idCadete); // Cuenta los pedidos del cadete específico

                double jornal = cantidadPedidos * 500;
                return jornal;
            }
            else
            {
                return -1;
            }
        }

        private Cadete BuscarCadetePorID(int idCadete) 
        {
            var cadete  = this.Cadetes.FirstOrDefault(cadete => cadete.Id == idCadete);
            return cadete;
        }

        private Pedido BuscarPedidoPorNro(int nroPedido)
        {
            var cadete = this.Pedidos.FirstOrDefault(pedido => pedido.Numero == nroPedido);
            return cadete;
        }

        public List<string> GenerarInforme() 
        {
            string intro = $"Cadeteria: {this.Nombre} | Teléfono: {this.Telefono}";
            List<string> info = new List<string>();
            info.Add(intro);
            foreach (var p in cadeteria.Pedidos)
            {
                info.Add(p.GenerarInformePedido());
            }

            return info;
        }
        public void BorrarPedido(int nroPedido) 
        {
            var pedido = BuscarPedidoPorNro(nroPedido);
            this.Pedidos.Remove(pedido);
        }

        public void CambiarEstadoPedido(int nroPedido)
        {
            var pedido = BuscarPedidoPorNro(nroPedido);
            pedido.CambiarEstado();
        }
    }
}
