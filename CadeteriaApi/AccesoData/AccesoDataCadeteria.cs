using _Cadeteria;
using System.Text.Json;

namespace CadeteriaAPI.AccesoData
{
    public class AccesoDatosCadeteria
    {
        public Cadeteria Obtener(string path)
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                string textoJson = File.ReadAllText(path);
                Cadeteria cadeteria = JsonSerializer.Deserialize<Cadeteria>(textoJson);
                return cadeteria;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }
    }
}
