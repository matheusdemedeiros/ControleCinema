﻿namespace ControleCinema.ConsoleApp.Compartilhado
{
    public interface ITelaCadastravel
    {
        void InserirRegistro();
        void EditarRegistro();
        void ExcluirRegistro();
        bool VisualizarRegistros(string tipoVisualizacao);
    }
}
