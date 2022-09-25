// ------------------------------------------------------------
// <copyright file="User.cs" company="ElectroSonne">
// Copyright (c) ElectroSonne, Russia, 2022.
// </copyright>
// ------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Chat.Domain
{
    /// <summary>
    /// Player.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        public User()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="nickname">Nickname.</param>
        public User(string nickname)
        {
            this.Nickname = nickname;
        }

        /// <summary>
        /// Gets or sets unique id.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets nickname of user.
        /// </summary>
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "{0} length must be between {2} and {1}.")]
        public string Nickname { get; set; }

        /// <summary>
        /// Gets or sets password of user.
        /// </summary>
        [Required]
        [StringLength(int.MaxValue, MinimumLength = 5, ErrorMessage = "{0} length must be more 5.")]
        public string Password { get; set; }

        /// <summary>
        /// Validate object.
        /// </summary>
        /// <returns>First of validation error.</returns>
        public string ValidationError()
        {
            var context = new ValidationContext(this);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(this, context, results, true);

            if (results.Count > 0)
            {
                return results[0].ErrorMessage;
            }

            string pattern = "[A-Z]";
            if (!Regex.Match(this.Password, pattern).Success)
            {
                return "Password must contain A-Z.";
            }

            pattern = "[a-z]";
            if (!Regex.Match(this.Password, pattern).Success)
            {
                return "Password must contain a-z.";
            }

            pattern = "[0-9]";
            if (!Regex.Match(this.Password, pattern).Success)
            {
                return "Password must contain 0-9.";
            }

            return string.Empty;
        }
    }
}
