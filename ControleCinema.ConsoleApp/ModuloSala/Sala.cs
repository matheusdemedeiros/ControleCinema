using ControleCinema.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleCinema.ConsoleApp.ModuloSala
{
    public class Sala : EntidadeBase
    {
        private int qtdPoltronas;
        private string nomeSala;

        public Sala(int qtdPoltronas, string nomeSala)
        {
            this.qtdPoltronas = qtdPoltronas;
            this.nomeSala = nomeSala;
        }

        public int QtdPoltronas => qtdPoltronas;

        public override string ToString()
        {
            string retorno =
                "ID: " + id +
                "\nNome: " + nomeSala +
                "\nQTD de poltronas: " + qtdPoltronas + "\n";
            return retorno;
        }
    }
}
