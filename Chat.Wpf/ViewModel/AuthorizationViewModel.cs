// ------------------------------------------------------------
// <copyright file="AuthorizationViewModel.cs" company="ElectroSonne">
// Copyright (c) ElectroSonne, Russia, 2022.
// </copyright>
// ------------------------------------------------------------

using Chat.Domain;
using System;
using System.Windows.Input;

namespace Chat.Wpf
{
    /// <summary>
    /// Authorization view model.
    /// </summary>
    public class AuthorizationViewModel : DependentViewModel
    {
        /// <summary>
        /// Authorization.
        /// </summary>
        private readonly ICommand authorizationCommand;

        /// <summary>
        /// Registration.
        /// </summary>
        private readonly ICommand registrationCommand;

        /// <summary>
        /// Warning.
        /// </summary>
        private string warning;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationViewModel"/> class.
        /// </summary>
        /// <param name="main">MainViewModel.</param>
        public AuthorizationViewModel(MainViewModel main)
            : base(main)
        {
            this.Title = "Authorization";
            this.Nickname = string.Empty;
            this.Password = string.Empty;
            this.authorizationCommand = new RelayCommand(_ => this.AuthorizationCommandExecution());
            this.registrationCommand = new RelayCommand(_ => this.RegistrationCommandExecution());
        }

        /// <summary>
        /// Gets authorization command.
        /// </summary>
        public ICommand AuthorizationCommand
        {
            get { return this.authorizationCommand; }
        }

        /// <summary>
        /// Gets or sets nickname.
        /// </summary>
        public string Nickname { get; set; }

        /// <summary>
        /// Gets or sets password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets warning.
        /// </summary>
        public string Warning
        {
            get
            {
                return this.warning;
            }

            set
            {
                this.warning = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets send registration command.
        /// </summary>
        public ICommand RegistrationCommand
        {
            get { return this.registrationCommand; }
        }

        /// <summary>
        /// Authorization execution command.
        /// </summary>
        private async void AuthorizationCommandExecution()
        {
            try
            {
                var user = new User() { Nickname = this.Nickname, Password = this.Password };

                if (await this.Api.Authorization(user))
                {
                    this.MainViewModel.ActiveViewModel = new ChatViewModel(this.MainViewModel, user);
                }
            }
            catch (Exception ex)
            {
                this.Warning = ex.Message;
            }
        }

        /// <summary>
        /// Registration execution command.
        /// </summary>
        private void RegistrationCommandExecution()
        {
            this.MainViewModel.ActiveViewModel = new RegistrationViewModel(this.MainViewModel);
        }
    }
}
