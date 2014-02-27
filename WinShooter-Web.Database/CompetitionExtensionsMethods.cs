// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompetitionExtensionsMethods.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   The competition extensions methods.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Database
{
    using System.Linq;

    /// <summary>
    /// The competition extensions methods.
    /// </summary>
    public static class CompetitionExtensionsMethods
    {
        /// <summary>
        /// Get the value of a certain competition parameter.
        /// </summary>
        /// <param name="competition">
        /// The competition.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetParameterValue(this Competition competition, CompetitionParamType type)
        {
            return
                (from param in competition.Parameters where param.CompetitionParameterType == type select param.Value)
                    .FirstOrDefault();
        }

        /// <summary>
        /// Get the integer value of a certain competition parameter.
        /// </summary>
        /// <param name="competition">
        /// The competition.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="defaultValue">
        /// The default value.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int GetIntParameter(this Competition competition, CompetitionParamType type, int defaultValue)
        {
            var value = competition.GetParameterValue(type);

            if (value == null)
            {
                return defaultValue;
            }

            int result;
            return !int.TryParse(value, out result) ? defaultValue : result;
        }

        /// <summary>
        /// Get the integer value of a certain competition parameter.
        /// </summary>
        /// <param name="competition">
        /// The competition.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int GetIntParameter(this Competition competition, CompetitionParamType type)
        {
            var defaultValue = CompetitionParamTypeDefaultValues.GetDefaultIntValue(type);

            return competition.GetIntParameter(type, defaultValue);
        }

        /// <summary>
        /// Get the boolean value of a certain competition parameter.
        /// </summary>
        /// <param name="competition">
        /// The competition.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="defaultValue">
        /// The default value.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static bool GetBoolParameter(this Competition competition, CompetitionParamType type, bool defaultValue)
        {
            var value = competition.GetParameterValue(type);

            if (value == null)
            {
                return defaultValue;
            }

            bool result;
            return !bool.TryParse(value, out result) ? defaultValue : result;
        }

        /// <summary>
        /// Get the boolean value of a certain competition parameter.
        /// </summary>
        /// <param name="competition">
        /// The competition.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static bool GetBoolParameter(this Competition competition, CompetitionParamType type)
        {
            var defaultValue = CompetitionParamTypeDefaultValues.GetDefaultBooleanValue(type);

            return competition.GetBoolParameter(type, defaultValue);
        }
    }
}
