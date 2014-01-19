// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TransactionForTests.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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
//   The transaction for tests.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Logic.Tests
{
    using System.Data;

    using NHibernate;
    using NHibernate.Transaction;

    /// <summary>
    /// The transaction for tests.
    /// </summary>
    public class TransactionForTests : ITransaction
    {
        /// <summary>
        /// Gets a value indicating whether the transaction in progress
        /// </summary>
        public bool IsActive { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the transaction rolled back or set to rollback only
        /// </summary>
        public bool WasRolledBack { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the transaction successfully committed
        /// </summary>
        /// <remarks>
        /// This method could return <see langword="false"/> even after successful invocation of <c>Commit()</c>
        /// </remarks>
        public bool WasCommitted { get; private set; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
        }

        /// <summary>
        /// Begin the transaction with the default isolation level.
        /// </summary>
        public void Begin()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Begin the transaction with the specified isolation level.
        /// </summary>
        /// <param name="isolationLevel">Isolation level of the transaction</param>
        public void Begin(IsolationLevel isolationLevel)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Flush the associated <c>ISession</c> and end the unit of work.
        /// </summary>
        /// <remarks>
        /// This method will commit the underlying transaction if and only if the transaction
        ///             was initiated by this object.
        /// </remarks>
        public void Commit()
        {
        }

        /// <summary>
        /// Force the underlying transaction to roll back.
        /// </summary>
        public void Rollback()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Enlist the <see cref="T:System.Data.IDbCommand"/> in the current Transaction.
        /// </summary>
        /// <param name="command">The <see cref="T:System.Data.IDbCommand"/> to enlist.</param>
        /// <remarks>
        /// It is okay for this to be a no op implementation.
        /// </remarks>
        public void Enlist(IDbCommand command)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Register a user synchronization callback for this transaction.
        /// </summary>
        /// <param name="synchronization">The <see cref="T:NHibernate.Transaction.ISynchronization"/> callback to register.</param>
        public void RegisterSynchronization(ISynchronization synchronization)
        {
            throw new System.NotImplementedException();
        }
    }
}
