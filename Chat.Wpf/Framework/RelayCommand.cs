// ------------------------------------------------------------
// <copyright file="RelayCommand.cs" company="ElectroSonne">
// Copyright (c) ElectroSonne, Russia, 2022.
// </copyright>
// ------------------------------------------------------------

using System;
using System.Windows.Input;

namespace Chat.Wpf
{
    /// <summary>
    /// Implement of relay command.
    /// </summary>
    public class RelayCommand : ICommand
    {
        /// <summary>
        /// Execute action.
        /// </summary>
        private Action<object> execute;

        /// <summary>
        /// Can execute func.
        /// </summary>
        private Func<object, bool> canExecute;

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand"/> class.
        /// </summary>
        /// <param name="execute">Action.</param>
        /// <param name="canExecute">Can execute func.</param>
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// Can execute changed event.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Can execute.
        /// </summary>
        /// <param name="parameter">Param.</param>
        /// <returns>Bool.</returns>
        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }

        /// <summary>
        /// Execute.
        /// </summary>
        /// <param name="parameter">Parameter.</param>
        public void Execute(object parameter)
        {
            this.execute(parameter);
        }
    }
}
