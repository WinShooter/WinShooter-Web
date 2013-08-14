// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NHibernateHelper.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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
//   The helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Database
{
    using System.Configuration;

    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;

    using NHibernate;
    using NHibernate.Tool.hbm2ddl;

    /// <summary>
    /// The helper.
    /// </summary>
    public class NHibernateHelper
    {
        /// <summary>
        /// The session factory.
        /// </summary>
        private static ISessionFactory sessionFactory;

        /// <summary>
        /// Gets the session factory.
        /// </summary>
        private static ISessionFactory SessionFactory
        {
            get
            {
                if (sessionFactory == null)
                {
                    InitializeSessionFactory();
                }

                return sessionFactory;
            }
        }

        /// <summary>
        /// Opens a NHibernate session.
        /// </summary>
        /// <returns>
        /// The <see cref="ISession"/>.
        /// </returns>
        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }

        /// <summary>
        /// The initialize session factory.
        /// </summary>
        private static void InitializeSessionFactory()
        {
            var connectionString = ConfigurationManager.ConnectionStrings[DatabaseConsts.DatabaseConnectionStringName].ConnectionString;

            sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008
                .ConnectionString(connectionString)
                .ShowSql())

                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<User>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UserLoginInfo>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Right>())

                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Competition>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Role>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<RoleRightsInfo>())

                .ExposeConfiguration(cfg => new SchemaExport(cfg))

                .BuildSessionFactory();
        }
    }
}
