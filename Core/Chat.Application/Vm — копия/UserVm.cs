// ------------------------------------------------------------
// <copyright file="UserVm.cs" company="ElectroSonne">
// Copyright (c) ElectroSonne, Russia, 2022.
// </copyright>
// ------------------------------------------------------------

using AutoMapper;
using Chat.Domain;

namespace Chat.Application
{
    /// <summary>
    /// UserVm.
    /// </summary>
    public class UserVm : IMapWith<User>
    {
        /// <summary>
        /// Gets or sets nickname of user.
        /// </summary>
        public string Nickname { get; set; }

        /// <summary>
        /// Gets or sets password of user.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Mapping.
        /// </summary>
        /// <param name="profile">Profile.</param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserVm>().
                ForMember(
                    userVm => userVm.Nickname,
                    opt => opt.MapFrom(user => user.Nickname));
        }
    }
}
