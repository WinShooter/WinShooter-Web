// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Repository.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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
//   The database repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Database
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using NHibernate;
    using NHibernate.Linq;

    /// <summary>
    /// The database repository.
    /// </summary>
    /// <typeparam name="T">
    /// One of the database classes.
    /// </typeparam>
    public class Repository<T> : IRepository<T>
        where T : IWinshooterDatabaseItem
    {
        /// <summary>
        /// The session.
        /// </summary>
        private readonly ISession session;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{T}"/> class.
        /// </summary>
        /// <param name="session">
        /// The session.
        /// </param>
        public Repository(ISession session)
        {
            this.session = session;
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
            this.session.Save(item);
            return true;
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
        public bool Add(System.Collections.Generic.IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                this.session.Save(item);
            }

            return true;
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
            this.session.Update(item);
            return true;
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Delete(T entity)
        {
            this.session.Delete(entity);
            return true;
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="entities">
        /// The entities.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Delete(System.Collections.Generic.IEnumerable<T> entities)
        {
            foreach (T entity in entities)
            {
                this.session.Delete(entity);
            }

            return true;
        }

        /// <summary>
        /// The all.
        /// </summary>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
        public IQueryable<T> All()
        {
            return this.session.Query<T>();
        }

        /// <summary>
        /// The find by.
        /// </summary>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T FindBy(Expression<Func<T, bool>> expression)
        {
            return this.FilterBy(expression).SingleOrDefault();
        }

        /// <summary>
        /// The filter by.
        /// </summary>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
        public IQueryable<T> FilterBy(Expression<Func<T, bool>> expression)
        {
            return this.All().Where(expression).AsQueryable();
        }
    }
}
