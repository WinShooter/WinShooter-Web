// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RepositoryForTests.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   The repository for tests.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Logic.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Linq.Expressions;

    using NHibernate;

    using WinShooter.Database;

    /// <summary>
    ///     The repository for tests.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of database.
    /// </typeparam>
    public class RepositoryForTests<T> : IRepository<T> where T : IWinshooterDatabaseItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryForTests{T}"/> class.
        /// </summary>
        public RepositoryForTests()
        {
            this.TheContent = new List<T>();
        }

        /// <summary>
        /// Gets or sets the content that will be queried.
        /// </summary>
        public List<T> TheContent { get; set; }

        /// <summary>
        /// Starts a transaction with the session.
        /// </summary>
        /// <param name="isolationLevel">
        /// The isolation Level.
        /// </param>
        /// <returns>
        /// The transaction.
        /// </returns>
        public ITransaction StartTransaction(IsolationLevel isolationLevel)
        {
            return new TransactionForTests();
        }

        /// <summary>
        /// Starts a transaction with the session.
        /// </summary>
        /// <returns>
        /// The transaction.
        /// </returns>
        public ITransaction StartTransaction()
        {
            return new TransactionForTests();
        }

        /// <summary>
        ///     Adds a new entity to the database.
        /// </summary>
        /// <param name="item">
        ///     The entity to add.
        /// </param>
        /// <returns>
        ///     Returns true if add succeeded.
        /// </returns>
        public bool Add(T item)
        {
            try
            {
                this.TheContent.Add(item);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        ///     Adds new entities to the database.
        /// </summary>
        /// <param name="items">
        ///     The items to add.
        /// </param>
        /// <returns>
        ///     Returns true if add succeeded.
        /// </returns>
        public bool Add(IEnumerable<T> items)
        {
            try
            {
                this.TheContent.AddRange(items);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        ///     Update some items.
        /// </summary>
        /// <param name="item">
        ///     The item to be updated
        /// </param>
        /// <returns>
        ///     Returns true if update succeeded.
        /// </returns>
        public bool Update(T item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Deletes an item.
        /// </summary>
        /// <param name="item">
        ///     The item to delete.</param>
        /// <returns>
        ///     Returns true if the delete succeeded.
        /// </returns>
        public bool Delete(T item)
        {
            try
            {
                if (this.TheContent.Contains(item))
                {
                    this.TheContent.Remove(item);
                }
                else
                {
                    return false;
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        ///     Deletes some items.
        /// </summary>
        /// <param name="items">
        ///     The items to delete.</param>
        /// <returns>
        ///     Returns true if the delete succeeded.
        /// </returns>
        public bool Delete(IEnumerable<T> items)
        {
            try
            {
                foreach (var item in items)
                {
                    if (this.TheContent.Contains(item))
                    {
                        this.TheContent.Remove(item);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Returns all items.
        /// </summary>
        /// <returns>
        ///     All items.
        /// </returns>
        public IQueryable<T> All()
        {
            return this.TheContent.AsQueryable();
        }

        /// <summary>
        ///     Find one item with an expression.
        /// </summary>
        /// <param name="expression">
        ///     The query expression</param>
        /// <returns>
        ///     The item or null.
        /// </returns>
        public T FindBy(Expression<Func<T, bool>> expression)
        {
            return this.TheContent.AsQueryable().Where(expression).FirstOrDefault();
        }

        /// <summary>
        ///     Find a number of items with an expression.
        /// </summary>
        /// <param name="expression">
        ///     The expression.</param>
        /// <returns>
        ///     The items.
        /// </returns>
        public IQueryable<T> FilterBy(Expression<Func<T, bool>> expression)
        {
            return this.TheContent.AsQueryable().Where(expression);
        }
    }
}
