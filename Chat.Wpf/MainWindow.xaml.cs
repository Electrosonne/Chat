// ------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="ElectroSonne">
// Copyright (c) ElectroSonne, Russia, 2022.
// </copyright>
// ------------------------------------------------------------

using System.Windows;

namespace Chat.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
            this.DataContext = this;

            this.MainViewModel = new MainViewModel();
            this.MainViewModel.ActiveViewModel = new AuthorizationViewModel(this.MainViewModel);
        }

        /// <summary>
        /// Gets or sets main view model.
        /// </summary>
        public MainViewModel MainViewModel { get; set; }
    }
}
