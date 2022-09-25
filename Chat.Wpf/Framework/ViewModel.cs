// ------------------------------------------------------------
// <copyright file="ViewModel.cs" company="ElectroSonne">
// Copyright (c) ElectroSonne, Russia, 2022.
// </copyright>
// ------------------------------------------------------------

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Chat.Wpf
{
    /// <summary>
    /// Based class for view models.
    /// </summary>
    public class ViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Event of property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// On property changed.
        /// </summary>
        /// <param name="name">Name of property.</param>
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
