using _Cadeteria;
using System.Text.Json;

namespace CadeteriaAPI.AccesoData
{
    public class AccesoDatosCadetes
    {
        public List<Cadete> Obtener(string path)
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                string textoJson = File.ReadAllText(path);
                List<Cadete> nuevaLista = JsonSerializer.Deserialize<List<Cadete>>(textoJson);
                return nuevaLista;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }


        public void Guardar(List<Cadete> datos)
        {
            string fileName = "C:\\Repositorios-Taller2-2023\\tl2-tp4-2023-emmanuelbilkis\\CadeteriaApi\\Datos\\Cadetes.json";
            string jsonString = Serializar(datos);

            FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate);
            using (StreamWriter strwriter = new StreamWriter(fs))
            {
                strwriter.WriteLine(jsonString);
                strwriter.Close();
            }
        }

        private string Serializar(List<Cadete> cadetes)
        {
            string jsonString = JsonSerializer.Serialize<List<Cadete>>(cadetes);
            return jsonString;
        }


    }
}
