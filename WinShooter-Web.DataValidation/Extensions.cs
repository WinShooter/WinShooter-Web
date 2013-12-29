using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinShooter.Web.DataValidation
{
    /// <summary>
    /// Extends the generic classes with a Require method.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// The require argument extension method.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <param name="argName">
        /// The argument name.
        /// </param>
        /// <typeparam name="T">
        /// The type that will be extended.
        /// </typeparam>
        /// <returns>
        /// The <see cref="Validation"/>.
        /// </returns>
        public static Validation<T> Require<T>(this T item, string argName)
        {
            return new Validation<T>(item, argName);
        }
    }
}
