using ControleCinema.ConsoleApp.Compartilhado;
using ControleCinema.ConsoleApp.ModuloGenero;
using System;
using System.Collections.Generic;

namespace ControleCinema.ConsoleApp.ModuloFuncionario
{
    public class TelaCadastroGenero : TelaBase, ITelaCadastravel
    {
        private readonly IRepositorio<Genero> _repositorioGenero;
        private readonly Notificador _notificador;

        public TelaCadastroGenero(IRepositorio<Genero> repositorioGenero, Notificador notificador)
            : base("Cadastro de Gêneros de Filme")
        {
            _repositorioGenero = repositorioGenero;
            _notificador = notificador;
        }

        public void InserirRegistro()
        {
            MostrarTitulo("Cadastro de Gênero de Filme");

            Genero novoGenero = ObterGenero();

            _repositorioGenero.Inserir(novoGenero);

            _notificador.ApresentarMensagem("Gênero de Filme cadastrado com sucesso!", TipoMensagem.Sucesso);
        }

        public void EditarRegistro()
        {
            MostrarTitulo("Editando Gênero de Filme");

            bool temRegistrosCadastrados = VisualizarRegistros("Pesquisando");

            if (temRegistrosCadastrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum gênero de filme cadastrado para editar.", TipoMensagem.Atencao);
                return;
            }

            int idGenero = ObterNumeroRegistro();

            Genero generoAtualizado = ObterGenero();

            bool conseguiuEditar = _repositorioGenero.Editar(x => x.id == generoAtualizado.id , generoAtualizado);

            if (!conseguiuEditar)
                _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Gênero de Filme editado com sucesso!", TipoMensagem.Sucesso);
        }

        public void ExcluirRegistro()
        {
            MostrarTitulo("Excluindo Gênero de Filme");

            bool temFuncionariosRegistrados = VisualizarRegistros("Pesquisando");

            if (temFuncionariosRegistrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum gênero de filme cadastrado para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroGenero = ObterNumeroRegistro();

            bool conseguiuExcluir = _repositorioGenero.Excluir(x => x.id == numeroGenero);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Gênero de Filme excluído com sucesso1", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Gêneros de Filme");

            List<Genero> generos = _repositorioGenero.SelecionarTodos();

            if (generos.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhum gênero de filme disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Genero genero in generos)
                Console.WriteLine(genero.ToString());

            Console.ReadLine();

            return true;
        }

        private Genero ObterGenero()
        {
            Console.Write("Digite a descrição do gênero: ");
            string descricao = Console.ReadLine();

            return new Genero(descricao);
        }

        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID do gênero de filme que deseja selecionar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = _repositorioGenero.ExisteRegistro(x=> x.id == numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("ID do gênero de filme não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }
    }
}
