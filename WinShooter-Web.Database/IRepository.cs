// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRepository.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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
//   The interface of the database repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Database
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// The interface of the database repository.
    /// </summary>
    /// <typeparam name="T">
    /// The database type.
    /// </typeparam>
    public interface IRepository<T>
        where T : IWinshooterDatabaseItem
    {
        /// <summary>
        ///     Adds a new entity to the database.
        /// </summary>
        /// <param name="item">
        ///     The entity to add.
        /// </param>
        /// <returns>
        ///     Returns true if add succeeded.
        /// </returns>
        bool Add(T item);

        /// <summary>
        ///     Adds new entities to the database.
        /// </summary>
        /// <param name="items">
        ///     The items to add.
        /// </param>
        /// <returns>
        ///     Returns true if add succeeded.
        /// </returns>
        bool Add(System.Collections.Generic.IEnumerable<T> items);

        /// <summary>
        ///     Update some items.
        /// </summary>
        /// <param name="item">
        ///     The item to be updated
        /// </param>
        /// <returns>
        ///     Returns true if update succeeded.
        /// </returns>
        bool Update(T item);

        /// <summary>
        ///     Deletes an item.
        /// </summary>
        /// <param name="item">
        ///     The item to delete.</param>
        /// <returns>
        ///     Returns true if the delete succeeded.
        /// </returns>
        bool Delete(T item);

        /// <summary>
        ///     Deletes some items.
        /// </summary>
        /// <param name="items">
        ///     The items to delete.</param>
        /// <returns>
        ///     Returns true if the delete succeeded.
        /// </returns>
        bool Delete(System.Collections.Generic.IEnumerable<T> items);

        /// <summary>
        ///     Returns all items.
        /// </summary>
        /// <returns>
        ///     All items.
        /// </returns>
        IQueryable<T> All();

        /// <summary>
        ///     Find one item with an expression.
        /// </summary>
        /// <param name="expression">
        ///     The query expression</param>
        /// <returns>
        ///     The item or null.
        /// </returns>
        T FindBy(Expression<Func<T, bool>> expression);

        /// <summary>
        ///     Find a number of items with an expression.
        /// </summary>
        /// <param name="expression">
        ///     The expression.</param>
        /// <returns>
        ///     The items.
        /// </returns>
        IQueryable<T> FilterBy(Expression<Func<T, bool>> expression);
    }
}