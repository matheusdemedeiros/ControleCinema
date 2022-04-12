using ControleCinema.ConsoleApp.Compartilhado;
using ControleCinema.ConsoleApp.ModuloSala;
using System;
using System.Collections.Generic;

namespace ControleCinema.ConsoleApp.ModuloSala
{
    public class TelaCadastroSala : TelaBase, ITelaCadastravel
    {
        private readonly IRepositorio<Sala> _repositorioSala;
        private readonly Notificador _notificador;

        public TelaCadastroSala(IRepositorio<Sala> repositorioSala, Notificador notificador)
            : base("Cadastro de Salas")
        {
            _repositorioSala = repositorioSala;
            _notificador = notificador;
        }

        public void InserirRegistro()
        {
            MostrarTitulo("Cadastro de sala");

            Sala novaSala = ObterSala();

            _repositorioSala.Inserir(novaSala);

            _notificador.ApresentarMensagem("Sala cadastrada com sucesso!", TipoMensagem.Sucesso);
        }

        public void EditarRegistro()
        {
            MostrarTitulo("Editando sala");

            bool temRegistrosCadastrados = VisualizarRegistros("Pesquisando");

            if (temRegistrosCadastrados == false)
            {
                _notificador.ApresentarMensagem("Nenhuma sala cadastrada para editar.", TipoMensagem.Atencao);
                return;
            }

            int idSala = ObterNumeroRegistro();

            Sala salaAtualizada = ObterSala();

            bool conseguiuEditar = _repositorioSala.Editar(x => x.id == idSala , salaAtualizada);

            if (!conseguiuEditar)
                _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Ssla editada com sucesso!", TipoMensagem.Sucesso);
        }

        public void ExcluirRegistro()
        {
            MostrarTitulo("Excluindo sala");

            bool temSalasRegistradas = VisualizarRegistros("Pesquisando");

            if (temSalasRegistradas == false)
            {
                _notificador.ApresentarMensagem("Nenhuma sala cadastrada para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroSala = ObterNumeroRegistro();

            bool conseguiuExcluir = _repositorioSala.Excluir(x => x.id == numeroSala);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Sala excluída com sucesso1", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Gêneros de Filme");

            List<Sala> salas = _repositorioSala.SelecionarTodos();

            if (salas.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhuma sala disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Sala Sala in salas)
                Console.WriteLine(Sala.ToString());

            Console.ReadLine();

            return true;
        }

        private Sala ObterSala()
        {
            Console.Write("Digite a quantidade de poltronas da sala: ");
            int qtdPoltronas = int.Parse(Console.ReadLine());
            
            Console.Write("Digite o nome da sala: ");
            string nomeSala = Console.ReadLine();

            return new Sala(qtdPoltronas,nomeSala);
        }

        public int ObterNumeroRegistro()
        {
            int idRegistro;
            bool idRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID da sala que deseja selecionar: ");
                idRegistro = Convert.ToInt32(Console.ReadLine());

                idRegistroEncontrado = _repositorioSala.ExisteRegistro(x=> x.id == idRegistro);

                if (idRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("ID da sala não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (idRegistroEncontrado == false);

            return idRegistro;
        }
    }
}
