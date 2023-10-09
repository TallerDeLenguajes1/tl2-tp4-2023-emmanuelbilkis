using _Cadeteria;
using CadeteriaAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Cadeteria
{
    public abstract class AccesoDato
    {
        public abstract List<Cadete> GetCadetes(string path);
        public abstract Cadeteria GetCadeteria(string path);
    }
}
