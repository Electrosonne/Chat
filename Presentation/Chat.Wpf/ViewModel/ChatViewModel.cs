// ------------------------------------------------------------
// <copyright file="ChatViewModel.cs" company="ElectroSonne">
// Copyright (c) ElectroSonne, Russia, 2022.
// </copyright>
// ------------------------------------------------------------

using Chat.Application;
using Chat.Domain;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Chat.Wpf
{
    /// <summary>
    /// Client View Model.
    /// </summary>
    public class ChatViewModel : DependentViewModel
    {
        /// <summary>
        /// Send message.
        /// </summary>
        private readonly ICommand sendMessageCommand;

        /// <summary>
        /// Update messages.
        /// </summary>
        private readonly ICommand updateMessagesCommand;

        /// <summary>
        /// Message.
        /// </summary>
        private string message;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatViewModel"/> class.
        /// </summary>
        /// <param name="main">MainViewModel.</param>
        /// <param name="user">User.</param>
        public ChatViewModel(MainViewModel main, User user)
            : base(main)
        {
            this.Title = "Chat";
            this.Message = string.Empty;
            this.User = user;
            this.Messages = new ObservableCollection<MessageVm>();

            this.Api.StartHub();
            this.Api.MessageReceived += this.MessageReceived;
            this.sendMessageCommand = new RelayCommand(_ => this.SendMessageCommandExecution(), _ => this.SendMessageCommandCanExecute());
            this.updateMessagesCommand = new RelayCommand(_ => this.UpdateMessagesCommandExecution(), _ => this.UpdateMessagesCommandCanExecute());
        }

        /// <summary>
        /// Gets or sets message.
        /// </summary>
        public string Message
        {
            get
            {
                return this.message;
            }

            set
            {
                this.message = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets user.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Gets or sets messages in chat.
        /// </summary>
        public ObservableCollection<MessageVm> Messages { get; set; }

        /// <summary>
        /// Gets send message command.
        /// </summary>
        public ICommand SendMessageCommand
        {
            get { return this.sendMessageCommand; }
        }

        /// <summary>
        /// Gets send update messages command.
        /// </summary>
        public ICommand UpdateMessagesCommand
        {
            get { return this.updateMessagesCommand; }
        }

        /// <summary>
        /// Handler of message received event.
        /// </summary>
        /// <param name="message">Recieved message.</param>
        private void MessageReceived(MessageVm message)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                this.Messages.Add(message);
            });
        }

        /// <summary>
        /// Send message execution command.
        /// </summary>
        private async void SendMessageCommandExecution()
        {
            var message = new MessageVm(new UserVm { Nickname = this.User.Nickname }, this.Message, DateTime.Now);
            this.Messages.Add(message);
            this.Message = string.Empty;
            await this.Api.SendMessage(message);
        }

        /// <summary>
        /// Send message can be execute.
        /// </summary>
        /// <returns>Bool.</returns>
        private bool SendMessageCommandCanExecute()
        {
            return this.IsOnline && !this.Message.Equals(string.Empty);
        }

        /// <summary>
        /// Update messages execution command.
        /// </summary>
        private async void UpdateMessagesCommandExecution()
        {
            DateTime date;

            if (this.Messages.Count > 0)
            {
                date = this.Messages[0].Date;
            }
            else
            {
                date = DateTime.Now;
            }

            var messages = await this.Api.GetMessages(date);

            for (int i = 0; i < messages.Count; i++)
            {
                this.Messages.Insert(i, messages[i]);
            }
        }

        /// <summary>
        /// Update messages can be execute.
        /// </summary>
        /// <returns>Bool.</returns>
        private bool UpdateMessagesCommandCanExecute()
        {
            return this.IsOnline;
        }
    }
}