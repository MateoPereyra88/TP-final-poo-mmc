using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cl
{
    public class Archivo
    {
        private string contenido;
        private string nombre;
        public Archivo(string contenido, string nombre)
        {
            this.contenido = contenido;
            this.nombre = nombre;
        }
        public string Ccontenido => contenido;
        public string Nombre => nombre;
    }
}
