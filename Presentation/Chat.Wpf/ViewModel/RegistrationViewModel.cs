// ------------------------------------------------------------
// <copyright file="RegistrationViewModel.cs" company="ElectroSonne">
// Copyright (c) ElectroSonne, Russia, 2022.
// </copyright>
// ------------------------------------------------------------

using Chat.Application;
using System;
using System.Windows.Input;

namespace Chat.Wpf
{
    /// <summary>
    /// Registration view model.
    /// </summary>
    public class RegistrationViewModel : DependentViewModel
    {
        /// <summary>
        /// Registration.
        /// </summary>
        private readonly ICommand registrationCommand;

        /// <summary>
        /// Warning.
        /// </summary>
        private string warning;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationViewModel"/> class.
        /// </summary>
        /// <param name="main">MainViewModel.</param>
        public RegistrationViewModel(MainViewModel main)
            : base(main)
        {
            this.Title = "Registration";
            this.Nickname = string.Empty;
            this.Password = string.Empty;
            this.RepeatPassword = string.Empty;
            this.warning = string.Empty;
            this.registrationCommand = new RelayCommand(_ => this.RegistrationCommandExecution());
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
        /// Gets or sets repeated password.
        /// </summary>
        public string RepeatPassword { get; set; }

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
        /// Gets registration command.
        /// </summary>
        public ICommand RegistrationCommand
        {
            get { return this.registrationCommand; }
        }

        /// <summary>
        /// Registration execution command.
        /// </summary>
        private async void RegistrationCommandExecution()
        {
            try
            {
                if (!this.Password.Equals(this.RepeatPassword))
                {
                    this.Warning = "Passwords must be equals.";
                    return;
                }

                var user = new UserVm() { Nickname = this.Nickname, Password = this.Password };

                if (await this.Api.Registration(user))
                {
                    this.MainViewModel.ActiveViewModel = new ChatViewModel(this.MainViewModel, user);
                }
            }
            catch (Exception ex)
            {
                this.Warning = ex.Message;
            }
        }
    }
}
