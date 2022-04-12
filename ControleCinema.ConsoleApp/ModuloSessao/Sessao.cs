using ControleCinema.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControleCinema.ConsoleApp.ModuloFilme;
using ControleCinema.ConsoleApp.ModuloSala;

namespace ControleCinema.ConsoleApp.ModuloSessao
{
    public class Sessao : EntidadeBase
    {
        private Filme filmeApresentado;
        private DateTime dataHorario;
        private Sala salaDeApresentacao;
        private string status;
        private List<Ingresso> listaDeIngressos;


        public int MaximoDeIngressos => salaDeApresentacao.QtdPoltronas;


        public Sessao(Filme filmeApresentado, DateTime dataHorario, Sala salaDeApresentacao)
        {
            this.filmeApresentado = filmeApresentado;
            this.dataHorario = dataHorario;
            this.salaDeApresentacao = salaDeApresentacao;
            listaDeIngressos= new List<Ingresso>();
        }

    }
}
