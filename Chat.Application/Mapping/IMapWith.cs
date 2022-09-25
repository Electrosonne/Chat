// ------------------------------------------------------------
// <copyright file="IMapWith.cs" company="ElectroSonne">
// Copyright (c) ElectroSonne, Russia, 2022.
// </copyright>
// ------------------------------------------------------------

using AutoMapper;

namespace Chat.Application
{
    /// <summary>
    /// Map with type.
    /// </summary>
    /// <typeparam name="T">Type.</typeparam>
    public interface IMapWith<T>
    {
        /// <summary>
        /// Mapping.
        /// </summary>
        /// <param name="profile">Profile.</param>
        void Mapping(Profile profile)
        {
            profile.CreateMap(typeof(T), this.GetType());
        }
    }
}
