using _Cadeteria;
using System.Text.Json;

namespace CadeteriaAPI.AccesoData
{
    public class AccesoDatosPedidos
    {
        public List<Pedido> Obtener(string path)
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                string textoJson = File.ReadAllText(path);
                try
                {
                    List<Pedido> nuevaLista = JsonSerializer.Deserialize<List<Pedido>>(textoJson);
                    return nuevaLista;
                }
                catch (Exception)
                {

                    List<Pedido> nuevaLista = new List<Pedido>();
                    return nuevaLista;
                } 
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public void Guardar(List<Pedido> datos)
        {
            string fileName = "C:\\Repositorios-Taller2-2023\\tl2-tp4-2023-emmanuelbilkis\\CadeteriaApi\\Datos\\Pedidos.json";
            string jsonString = Serializar(datos);

            FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate);
            using (StreamWriter strwriter = new StreamWriter(fs))
            {
                strwriter.WriteLine(jsonString);
                strwriter.Close();
            }
        }

        private string Serializar(List<Pedido> pedidos)
        {
            string jsonString = JsonSerializer.Serialize<List<Pedido>>(pedidos);
            return jsonString;
        }
    }
}
