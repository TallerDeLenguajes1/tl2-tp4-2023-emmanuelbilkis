using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CadeteriaAPI;
using CadeteriaAPI.AccesoData;

namespace _Cadeteria
{
    public class Cadeteria
    {
        private string nombre;
        private int telefono;
        private List<Cadete> cadetes;
        private List<Pedido> pedidos;
        private static Cadeteria cadeteria;
        private AccesoDatosPedidos accesoDatosPedidos;
        public static Cadeteria GetCadeteria(AccesoDatosPedidos accesoDatosPedidos, AccesoDatosCadeteria accesoDatosCadeteria, AccesoDatosCadetes accesoDatosCadetes)
        {
            if (cadeteria == null)
            {

                cadeteria = accesoDatosCadeteria.Obtener("C:\\Repositorios-Taller2-2023\\tl2-tp4-2023-emmanuelbilkis\\CadeteriaApi\\Datos\\Cadeteria.json");
                cadeteria.accesoDatosPedidos = accesoDatosPedidos;
                cadeteria.Cadetes = accesoDatosCadetes.Obtener("C:\\Repositorios-Taller2-2023\\tl2-tp4-2023-emmanuelbilkis\\CadeteriaApi\\Datos\\Cadetes.json");
                cadeteria.Pedidos = cadeteria.accesoDatosPedidos.Obtener("C:\\Repositorios-Taller2-2023\\tl2-tp4-2023-emmanuelbilkis\\CadeteriaApi\\Datos\\Pedidos.json");
            }
            return cadeteria;
        }
        public Cadeteria() 
        {
            this.pedidos = new List<Pedido>();
            this.cadetes = new List<Cadete>();
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

        public void GuardarPedidos(List<Pedido> pedidos)
        {
            cadeteria.accesoDatosPedidos.Guardar(pedidos);
        }

        public void CrearPedido(int numeroPedido, string observacionPedido, string nombreCliente, string direccionCliente, int telefonoCliente, string datosReferenciaDireccionCliente) 
        {
            Pedido pedido = new Pedido(numeroPedido, observacionPedido, nombreCliente, direccionCliente, telefonoCliente, datosReferenciaDireccionCliente);
            cadeteria.Pedidos.Add(pedido);
            accesoDatosPedidos.Guardar(cadeteria.Pedidos);
        }
   
        public void ReasignarPedido(int idDestino,int numeroPedido) 
        {
            var pedido = BuscarPedidoPorNro(numeroPedido);
            pedido.DesasignarCadete();
            AsignarCadeteAPedido(idDestino,numeroPedido);
            accesoDatosPedidos.Guardar(cadeteria.Pedidos);
        }

        public void AsignarCadeteAPedido(int idCadete, int idPedido) 
        {
            try
            {
                var cadete = BuscarCadetePorID(idCadete);
                var pedido = BuscarPedidoPorNro(idPedido);

                accesoDatosPedidos.Guardar(cadeteria.Pedidos);
            }
            catch (NullReferenceException e)
            {

                Console.WriteLine(e.Message);
            }
            
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

        public Cadete BuscarCadetePorID(int idCadete) 
        {
            var cadete  = cadeteria.Cadetes.FirstOrDefault(cadete => cadete.Id == idCadete);
            if (cadete == null) 
            {
              throw new NullReferenceException("No existe el cadete");
            }

            return cadete;
        }

        public Pedido BuscarPedidoPorNro(int nroPedido)
        {
            var pedido = cadeteria.Pedidos.FirstOrDefault(pedido => pedido.Numero == nroPedido);
            if (pedido==null)
            {
                throw new NullReferenceException("No existe el pedido");
            }

            return pedido;
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
            accesoDatosPedidos.Guardar(cadeteria.Pedidos);
        }

        public void CambiarEstadoPedido(int nroPedido)
        {
            var pedido = BuscarPedidoPorNro(nroPedido);
            pedido.CambiarEstado();
            accesoDatosPedidos.Guardar(cadeteria.Pedidos);
        }
    }
}
