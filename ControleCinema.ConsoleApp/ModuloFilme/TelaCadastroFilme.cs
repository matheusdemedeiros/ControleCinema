using ControleCinema.ConsoleApp.Compartilhado;
using ControleCinema.ConsoleApp.ModuloFuncionario;
using ControleCinema.ConsoleApp.ModuloGenero;
using System;
using System.Collections.Generic;

namespace ControleCinema.ConsoleApp.ModuloFilme
{
    public class TelaCadastroFilme : TelaBase, ITelaCadastravel
    {
        private readonly IRepositorio<Filme> repositorioFilme;
        private readonly IRepositorio<Genero> repositorioGenero;
        private readonly Notificador notificador;
        private readonly TelaCadastroGenero telaCadastroGenero;

        public TelaCadastroFilme(IRepositorio<Filme> repositorioFilme,
            Notificador notificador,
            TelaCadastroGenero telaCadastroGenero,
            IRepositorio<Genero> repositorioGenero
            )
            : base("Cadastro de Filmes")
        {
            this.repositorioFilme = repositorioFilme;
            this.notificador = notificador;
            this.telaCadastroGenero = telaCadastroGenero;
            this.repositorioGenero = repositorioGenero;
        }

        public void InserirRegistro()
        {
            MostrarTitulo("Inserindo novo Filme no acervo");

            Genero generoSelecionado = ObtemGenero();

            if (generoSelecionado == null)
                return;

            Filme novoFilme = ObterFilme(generoSelecionado);

            string statusValidacao = repositorioFilme.Inserir(novoFilme);

            if (statusValidacao == "REGISTRO_VALIDO")
                notificador.ApresentarMensagem("Filme cadastrado com sucesso!", TipoMensagem.Sucesso);
            else
                notificador.ApresentarMensagem(statusValidacao, TipoMensagem.Erro);
        }

        public void EditarRegistro()
        {
            MostrarTitulo("Editando Filme");

            bool temFilmesCadastrados = VisualizarRegistros("Pesquisando");

            if (temFilmesCadastrados == false)
            {
                notificador.ApresentarMensagem("Nenhum Filme cadastrada para poder editar", TipoMensagem.Atencao);
                return;
            }

            int idFilme = ObterIdFilme();

            Genero genero = repositorioFilme.SelecionarRegistro(idFilme).Genero;

            Filme filmeAtualizado = ObterFilme(genero);

            bool conseguiuEditar = repositorioFilme.Editar(x => x.id == idFilme, filmeAtualizado);

            if (!conseguiuEditar)
                notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Filme editada com sucesso", TipoMensagem.Sucesso);
        }

        public void ExcluirRegistro()
        {
            MostrarTitulo("Excluindo Filme");

            bool temFilmesCadastradss = VisualizarRegistros("Pesquisando");

            if (temFilmesCadastradss == false)
            {
                notificador.ApresentarMensagem(
                    "Nenhum Filme cadastrada para poder excluir", TipoMensagem.Atencao);
                return;
            }

            int idFilme = ObterIdFilme();

            bool conseguiuExcluir = repositorioFilme.Excluir(x => x.id == idFilme);

            if (!conseguiuExcluir)
                notificador.ApresentarMensagem("Não foi possível excluir", TipoMensagem.Sucesso);
            else
                notificador.ApresentarMensagem("Filme excluída com sucesso", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipo)
        {
            if (tipo == "Tela")
                MostrarTitulo("Visualização de Filmes");

            List<Filme> filmes = repositorioFilme.SelecionarTodos();

            if (filmes.Count == 0)
            {
                notificador.ApresentarMensagem("Não há nenhum Filme disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Filme filme in filmes)
                Console.WriteLine(filme.ToString());

            Console.ReadLine();

            return true;
        }

        #region Métodos privados
        private int ObterIdFilme()
        {
            int idFilme;
            bool numeroCadastroEncontrado;

            do
            {
                Console.Write("Digite o número da Filme que deseja selecionar: ");
                idFilme = Convert.ToInt32(Console.ReadLine());

                numeroCadastroEncontrado = repositorioFilme.ExisteRegistro(x => x.id == idFilme);

                if (numeroCadastroEncontrado == false)
                    notificador.ApresentarMensagem("Número de cadastro não encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroCadastroEncontrado == false);

            return idFilme;
        }

        private Genero ObtemGenero()
        {
            bool temGenerosDisponiveis = telaCadastroGenero.VisualizarRegistros("");

            if (!temGenerosDisponiveis)
            {
                notificador.ApresentarMensagem("Você precisa cadastrar um gênero antes de um filme!", TipoMensagem.Atencao);
                return null;
            }

            Console.Write("Digite o id do gênero do filme: ");
            int idGeneroSelecionado = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Genero generoSelecionado = repositorioGenero.SelecionarRegistro(idGeneroSelecionado);

            return generoSelecionado;
        }

        private Filme ObterFilme(Genero genero)
        {
            Console.Write("Digite o título do Filme: ");
            string tituloFilme = Console.ReadLine();

            Console.Write("Digite a duração do filme em minutos: ");
            int duracao = int.Parse(Console.ReadLine());

            Filme novoFilme = new Filme(tituloFilme, duracao, genero);

            return novoFilme;
        }
        #endregion
    }
}