﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompetitionsTests.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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
//   Tests the <see cref="Competitions" /> class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Logic.Tests
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using WinShooter.Database;
    using WinShooter.Logic;

    /// <summary>
    /// Tests the <see cref="CompetitionsLogic"/> class.
    /// </summary>
    [TestClass]
    public class CompetitionsTests
    {
        /// <summary>
        /// Get competitions for an anonymous user.
        /// </summary>
        [TestMethod]
        public void GetAnonymousCompetitions()
        {
            var repository = new RepositoryForTests<Competition>();
            repository.TheContent.Add(new Competition
            {
                CompetitionType = CompetitionType.Field,
                Id = Guid.NewGuid(),
                IsPublic = true,
                Name = "Public1",
                StartDate = DateTime.Now,
                UseNorwegianCount = false
            });
            repository.TheContent.Add(new Competition
            {
                CompetitionType = CompetitionType.Field,
                Id = Guid.NewGuid(),
                IsPublic = false,
                Name = "Public1",
                StartDate = DateTime.Now,
                UseNorwegianCount = false
            });

            var competitions = new CompetitionsLogic(repository);

            var result = competitions.GetCompetitions(Guid.Empty);
            Assert.AreEqual(1, result.Count());
        }
    }
}
