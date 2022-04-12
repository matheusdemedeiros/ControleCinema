﻿using System;
using System.Collections.Generic;

namespace ControleCinema.ConsoleApp.Compartilhado
{
    public interface IRepositorio<T> where T : EntidadeBase
    {
        string Inserir(T entidade);
        //bool Editar(int idSelecionado, T novaEntidade);
        bool Editar(Predicate<T> condicao, T novaEntidade);
        bool Excluir(Predicate<T> condicao);
        bool ExisteRegistro(Predicate<T> condicao);
        T SelecionarRegistro(int idSelecionado);
        List<T> Filtrar(Predicate<T> condicao);
        List<T> SelecionarTodos();

    }
}