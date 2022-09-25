// ------------------------------------------------------------
// <copyright file="MainViewModel.cs" company="ElectroSonne">
// Copyright (c) ElectroSonne, Russia, 2022.
// </copyright>
// ------------------------------------------------------------

using System.Threading.Tasks;

namespace Chat.Wpf
{
    /// <summary>
    /// Main view model.
    /// </summary>
    public class MainViewModel : ViewModel
    {
        /// <summary>
        /// Active view model.
        /// </summary>
        private ViewModel activeViewModel;

        /// <summary>
        /// Is Online.
        /// </summary>
        private bool isOnline;

        /// <summary>
        /// Is view model disposed.
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        public MainViewModel()
        {
            this.IsOnline = false;
            this.Api = new ServerApi();
            Task.Run(this.Ping);
        }

        /// <summary>
        /// Gets or sets server api.
        /// </summary>
        public ServerApi Api { get; set; }

        /// <summary>
        /// Gets or sets active view model.
        /// </summary>
        public ViewModel ActiveViewModel
        {
            get
            {
                return this.activeViewModel;
            }

            set
            {
                this.activeViewModel = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether is online.
        /// </summary>
        public bool IsOnline
        {
            get
            {
                return this.isOnline;
            }

            set
            {
                this.isOnline = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Ping to server.
        /// </summary>
        /// <returns>Task.</returns>
        private async Task Ping()
        {
            while (!this.disposed)
            {
                this.IsOnline = await this.Api.Ping();

                await Task.Delay(1000);
            }
        }
    }
}
