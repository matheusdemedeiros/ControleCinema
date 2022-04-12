using ControleCinema.ConsoleApp.Compartilhado;
using ControleCinema.ConsoleApp.ModuloGenero;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleCinema.ConsoleApp.ModuloFilme
{
    public class Filme : EntidadeBase
    {
        private string titulo;
        private int duracaoMinutos;
        private Genero genero;

        public Filme(string titulo, int duracao, Genero genero)
        {
            this.titulo = titulo;
            this.duracaoMinutos = duracao;
            this.genero = genero;
        }

        public Genero Genero { get => genero; }

        public override string ToString()
        {
            string retorno =
                "ID: " + id +
                "\nTítulo: " + titulo +
                "\nDuração: " + duracaoMinutos + " minutos." +
                "\nGênero: " + genero.Descricao;
            return retorno;
        }
    }
}
