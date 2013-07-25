// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegisterExternalLoginModel.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
//   This program is free software; you can redistribute it and/or
//   modify it under the terms of the GNU General Public License
//   as published by the Free Software Foundation; either version 2
//   of the License, or (at your option) any later version.
//   
//   This program is distributed in the hope that it will be useful,
//   but WITHOUT ANY WARRANTY; without even the implied warranty of
//   MERCHANTABILITY OR FITNESS FOR A PARTICULAR PURPOSE. See the
//   GNU General Public License for more details.
//   
//   You should have received a copy of the GNU General Public License
//   along with this program; if not, write to the Free Software
//   Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
// </copyright>
// <summary>
//   The register external login model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The register external login model.
    /// </summary>
    public class RegisterExternalLoginModel
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [Required]
        [Display(Name = "Epost")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the given name.
        /// </summary>
        [Required]
        [Display(Name = "Förnamn")]
        public string GivenName { get; set; }

        /// <summary>
        /// Gets or sets the surname.
        /// </summary>
        [Required]
        [Display(Name = "Efteramn")]
        public string Surname { get; set; }

        /// <summary>
        /// Gets or sets the shooter card number.
        /// </summary>
        [Display(Name = "Skyttekortsnummer (lämnas blankt om du inte har något)")]
        public string ShooterCardNumber { get; set; }

        /// <summary>
        /// Gets or sets the external login data.
        /// </summary>
        public string ExternalLoginData { get; set; }
    }
}