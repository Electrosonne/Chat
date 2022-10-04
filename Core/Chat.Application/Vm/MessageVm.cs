// ------------------------------------------------------------
// <copyright file="MessageVm.cs" company="ElectroSonne">
// Copyright (c) ElectroSonne, Russia, 2022.
// </copyright>
// ------------------------------------------------------------

using AutoMapper;
using Chat.Domain;
using System;

namespace Chat.Application
{
    /// <summary>
    /// MessageVm.
    /// </summary>
    public class MessageVm : IMapWith<Message>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageVm"/> class.
        /// </summary>
        public MessageVm()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageVm"/> class.
        /// </summary>
        /// <param name="user">UserVm.</param>
        /// <param name="text">Text.</param>
        /// <param name="date">Date.</param>
        public MessageVm(UserVm user, string text, DateTime date)
        {
            this.User = user;
            this.Text = text;
            this.Date = date;
        }

        /// <summary>
        /// Gets or sets user.
        /// </summary>
        public UserVm User { get; set; }

        /// <summary>
        /// Gets or sets text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets date of message.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Mapping.
        /// </summary>
        /// <param name="profile">Profile.</param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Message, MessageVm>()
                .ForMember(
                    messageVm => messageVm.Date,
                    opt => opt.MapFrom(message => message.Date))
                .ForMember(
                    messageVm => messageVm.Text,
                    opt => opt.MapFrom(message => message.Text))
                .ForMember(
                    messageVm => messageVm.User,
                    opt => opt.MapFrom(message => message.User));
        }
    }
}
