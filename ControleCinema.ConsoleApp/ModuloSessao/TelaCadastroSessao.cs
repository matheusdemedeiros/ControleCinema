using ControleCinema.ConsoleApp.Compartilhado;
using ControleCinema.ConsoleApp.ModuloFilme;
using ControleCinema.ConsoleApp.ModuloSala;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleCinema.ConsoleApp.ModuloSessao
{
    public class TelaCadastroSessao : TelaBase, ITelaCadastravel
    {

        private readonly IRepositorio<Sessao> repositorioSessao;
        private readonly IRepositorio<Filme> repositorioFilme;
        private readonly IRepositorio<Sala> repositorioSala;
        private readonly TelaCadastroFilme telaCadastroFilme;
        private readonly TelaCadastroSala telaCadastroSala;


        private readonly Notificador notificador;

        public TelaCadastroSessao(IRepositorio<Sessao> repositorioSessao,
            Notificador notificador,
            IRepositorio<Filme> repositorioFilme,
            IRepositorio<Sala> repositorioSala,
            TelaCadastroFilme telaCadastroFilme,
            TelaCadastroSala telaCadastroSala
            )
            : base("Cadastro de Sessões")
        {
            this.repositorioSessao = repositorioSessao;
            this.notificador = notificador;
            this.repositorioFilme = repositorioFilme;
            this.repositorioSala = repositorioSala;
            this.telaCadastroFilme = telaCadastroFilme;
            this.telaCadastroSala = telaCadastroSala;
        }

        public override string MostrarOpcoes()
        {
            MostrarTitulo(Titulo);

            Console.WriteLine("Digite 1 para Inserir");
            Console.WriteLine("Digite 2 para Editar");
            Console.WriteLine("Digite 3 para Excluir");
            Console.WriteLine("Digite 4 para Visualizar agrupadas por filme");
            Console.WriteLine("Digite 5 para Visualizar os detalhes");
            Console.WriteLine("Digite 6 para Vender ingressos");

            Console.WriteLine("Digite s para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }

        public void InserirRegistro()
        {
            MostrarTitulo("Cadastro de Sessao");

            Filme filme = ObtemFilme();

            Sala sala = ObtemSala();

            if (filme != null && sala != null)
                return;

            Sessao novaSessao = ObterSessao(filme, sala);

            repositorioSessao.Inserir(novaSessao);

            notificador.ApresentarMensagem("Sessao cadastrada com sucesso!", TipoMensagem.Sucesso);
        }

        public void EditarRegistro()
        {
            MostrarTitulo("Editando Sessao");

            bool temRegistrosCadastrados = VisualizarRegistros("Pesquisando");

            if (temRegistrosCadastrados == false)
            {
                notificador.ApresentarMensagem("Nenhuma sessão cadastrada para editar.", TipoMensagem.Atencao);
                return;
            }

            
            int idSessao = ObterNumeroRegistro();



            //Sessao SessaoAtualizada = ObterSessao();

            //bool conseguiuEditar = repositorioSessao.Editar(x => x.id == idSessao, SessaoAtualizada);

            //if (!conseguiuEditar)
            //    notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            //else
            //    notificador.ApresentarMensagem("Ssla editada com sucesso!", TipoMensagem.Sucesso);
        }

        public void ExcluirRegistro()
        {
            MostrarTitulo("Excluindo Sessao");

            bool temSessaosRegistradas = VisualizarRegistros("Pesquisando");

            if (temSessaosRegistradas == false)
            {
                notificador.ApresentarMensagem("Nenhuma Sessao cadastrada para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroSessao = ObterNumeroRegistro();

            bool conseguiuExcluir = repositorioSessao.Excluir(x => x.id == numeroSessao);

            if (!conseguiuExcluir)
                notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Sessao excluída com sucesso1", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Gêneros de Filme");

            List<Sessao> Sessaos = repositorioSessao.SelecionarTodos();

            if (Sessaos.Count == 0)
            {
                notificador.ApresentarMensagem("Nenhuma Sessao disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Sessao Sessao in Sessaos)
                Console.WriteLine(Sessao.ToString());

            Console.ReadLine();

            return true;
        }


        private Filme ObtemFilme()
        {
            bool temFilmesDisponiveis = telaCadastroFilme.VisualizarRegistros("");

            if (!temFilmesDisponiveis)
            {
                notificador.ApresentarMensagem("Você precisa cadastrar um filme antes de uma sessão!", TipoMensagem.Atencao);
                return null;
            }

            Console.Write("Digite o id do filme que deseja Selecionar: ");
            int idFilmeSelecionado = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Filme filmeSelecionado = repositorioFilme.SelecionarRegistro(idFilmeSelecionado);

            return filmeSelecionado;
        }

        private Sala ObtemSala()
        {
            bool temSalasDisponiveis = telaCadastroSala.VisualizarRegistros("");

            if (!temSalasDisponiveis)
            {
                notificador.ApresentarMensagem("Você precisa cadastrar uma sala antes de uma sessão!", TipoMensagem.Atencao);
                return null;
            }

            Console.Write("Digite o id do sala que deseja Selecionar: ");
            int idSalaSelecionada = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Sala salaSelecionada = repositorioSala.SelecionarRegistro(idSalaSelecionada);

            return salaSelecionada;
        }

        private Sessao ObterSessao(Filme filme, Sala sala)
        {
            Console.Write("Digite data e o horário da sessao (dd/MM/aaaa hh:mm:ss): ");
            DateTime dataHorario = DateTime.Parse(Console.ReadLine());

            return new Sessao(filme, dataHorario, sala);
        }

        public int ObterNumeroRegistro()
        {
            int idRegistro;
            bool idRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID da Sessao que deseja selecionar: ");
                idRegistro = Convert.ToInt32(Console.ReadLine());

                idRegistroEncontrado = repositorioSessao.ExisteRegistro(x => x.id == idRegistro);

                if (idRegistroEncontrado == false)
                    notificador.ApresentarMensagem("ID da Sessao não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (idRegistroEncontrado == false);

            return idRegistro;
        }

    }
}
