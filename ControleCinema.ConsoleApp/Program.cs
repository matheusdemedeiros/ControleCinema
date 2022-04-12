using ControleCinema.ConsoleApp.Compartilhado;
using ControleCinema.ConsoleApp.ModuloSessao;

namespace ControleCinema.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            TelaMenuPrincipal telaMenuPrincipal = new TelaMenuPrincipal(new Notificador());

            while (true)
            {
                TelaBase telaSelecionada = telaMenuPrincipal.ObterTela();

                if (telaSelecionada is null)
                    break;

                string opcaoSelecionada = telaSelecionada.MostrarOpcoes();

                if (telaSelecionada is ITelaCadastravel)
                {
                    ITelaCadastravel telaCadastroBasico = (ITelaCadastravel)telaSelecionada;

                    if (opcaoSelecionada == "1")
                        telaCadastroBasico.InserirRegistro();

                    if (opcaoSelecionada == "2")
                        telaCadastroBasico.EditarRegistro();

                    if (opcaoSelecionada == "3")
                        telaCadastroBasico.ExcluirRegistro();

                    if (opcaoSelecionada == "4")
                        telaCadastroBasico.VisualizarRegistros("Tela");

                    //TelaCadastroSessao telaCadastroSessao = telaCadastroBasico as TelaCadastroSessao;

                    //if (telaCadastroSessao is null)
                    //    return;

                    //    if (opcaoSelecionada == "5")
                    //        //telaCadastroSessao.VisualizarAmigosComMulta("Tela");

                    //    else if (opcaoSelecionada == "6")
                    //        //telaCadastroSessao.PagarMulta();

                    //}
                }
            }
        }
    }
}
