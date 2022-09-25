// ------------------------------------------------------------
// <copyright file="DependentViewModel.cs" company="ElectroSonne">
// Copyright (c) ElectroSonne, Russia, 2022.
// </copyright>
// ------------------------------------------------------------

namespace Chat.Wpf
{
    /// <summary>
    /// Dependent view model from MainViewModel.
    /// </summary>
    public class DependentViewModel : ViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DependentViewModel"/> class.
        /// </summary>
        /// <param name="main">MainViewModel.</param>
        public DependentViewModel(MainViewModel main)
        {
            this.MainViewModel = main;
        }

        /// <summary>
        /// Gets or sets title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets MainViewModel.
        /// </summary>
        public MainViewModel MainViewModel { get; private set; }

        /// <summary>
        /// Gets ServerApi.
        /// </summary>
        public ServerApi Api => this.MainViewModel.Api;

        /// <summary>
        /// Gets a value indicating whether IsOnline.
        /// </summary>
        public bool IsOnline => this.MainViewModel.IsOnline;
    }
}
