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
    }
}
