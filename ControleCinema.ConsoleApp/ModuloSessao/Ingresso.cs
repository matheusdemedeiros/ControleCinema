using ControleCinema.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleCinema.ConsoleApp.ModuloSessao
{
    public class Ingresso
    {
        public int numeroPoltrona;
        public bool estaVendido;

        public Ingresso(int numeroPoltrona)
        {
            this.numeroPoltrona = numeroPoltrona;
            this.estaVendido = false;
        }
    }
}
