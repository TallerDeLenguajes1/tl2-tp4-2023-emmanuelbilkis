using _Cadeteria;
using CadeteriaAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace _Cadeteria
{
    public class AccesoDatoJson : AccesoDato
    {
        /*public static void CrearJson(Datos datos)
        {
            string fileName = "Respuestas.json";
            string jsonString = Serializar(datos);

            FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate);
            using (StreamWriter strwriter = new StreamWriter(fs))
            {
                strwriter.WriteLine(jsonString);
                strwriter.Close();
            }
        }

        private static string Serializar(Datos datos)
        {
            string jsonString = JsonSerializer.Serialize<Datos>(datos);
            //Console.WriteLine(jsonString);
            return jsonString;
        }*/

        public override Cadeteria GetCadeteria(string path)
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

        public override List<Cadete> GetCadetes(string path)
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
