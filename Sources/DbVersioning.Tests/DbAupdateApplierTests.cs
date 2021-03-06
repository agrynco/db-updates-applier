﻿#region Usings
using System.Collections.Generic;
using AGrynCo.Lib.Data.DataProviders;
using AGrynCo.Lib.ResourcesUtils;
using DbVersioning.Exceptions;
using Moq;
using NUnit.Framework;
#endregion

namespace DbVersioning.Tests
{
    [TestFixture]
    public class DbAupdateApplierTests
    {
        private const string _VERSION_STRING_TEMPLATE = "SET @newDbVersion = {0}";
        private static readonly string _OLD_VERSION_STRING = string.Format(_VERSION_STRING_TEMPLATE, 5);

        [Test]
        public void ApplyTest()
        {
            Mock<IDBUpdatesScanner> dBUpdatesScannerMock = new Mock<IDBUpdatesScanner>();
            dBUpdatesScannerMock.Setup(scanner => scanner.GetUpdates()).Returns(new[] {"1", "3", "2"});

            string dbUpdateStatementSample = ResourceReader.ReadAsString(GetType(), Constants.DbMigrations.NUMERIC);

            Mock<IDbUpdateLoader> dbUpdateLoaderMock = new Mock<IDbUpdateLoader>();
            dbUpdateLoaderMock.Setup(loader => loader.Load("1")).Returns(dbUpdateStatementSample.Replace(_OLD_VERSION_STRING, string.Format(_VERSION_STRING_TEMPLATE, 1)));
            dbUpdateLoaderMock.Setup(loader => loader.Load("2")).Returns(dbUpdateStatementSample.Replace(_OLD_VERSION_STRING, string.Format(_VERSION_STRING_TEMPLATE, 2)));
            dbUpdateLoaderMock.Setup(loader => loader.Load("3")).Returns(dbUpdateStatementSample.Replace(_OLD_VERSION_STRING, string.Format(_VERSION_STRING_TEMPLATE, 3)));

            Mock<ICurrentDbVersionDetector> currentDbVersionDetectorMock = new Mock<ICurrentDbVersionDetector>();
            currentDbVersionDetectorMock.Setup(detector => detector.Detect()).Returns(new NumericDbVersionIdentifier(1));
            currentDbVersionDetectorMock.Setup(detector => detector.CheckOnDbExists()).Returns(true);

            List<IDbUpdate> executedUpdates = new List<IDbUpdate>();

            Mock<IDbUpdateExecutor> dbUpdateExecutorMock = new Mock<IDbUpdateExecutor>();
            dbUpdateExecutorMock.Setup(executor => executor.Execute(It.IsAny<IDbVersionIdentifier>(), It.IsAny<IDbUpdate>()))
                .Callback((IDbVersionIdentifier dbVersionIdentifier, IDbUpdate dbUpdate) => executedUpdates.Add(dbUpdate));

            Mock<IDatabaseManager> databaseManagerMock = new Mock<IDatabaseManager>();
            databaseManagerMock.Setup(manager => manager.Drop()).Callback(() => { });

            var dbAupdateApplier =
                new DbAupdatesApplier(
                    new[]
                        {
                            new DbUpdateSourceDefinition(
                                typeof (SqlDbUpdate<NumericDbVersionIdentifier>),
                                currentDbVersionDetectorMock.Object,
                                dBUpdatesScannerMock.Object,
                                dbUpdateLoaderMock.Object,
                                new NumericSqlDbUpdateBuilder(),
                                dbUpdateExecutorMock.Object,
                                databaseManagerMock.Object)
                        },
                    false);
            dbAupdateApplier.Apply();

            Assert.AreEqual(2, ((NumericDbVersionIdentifier) executedUpdates[0].NewDbVersion).Number);
            Assert.AreEqual(3, ((NumericDbVersionIdentifier) executedUpdates[1].NewDbVersion).Number);
        }

        [Test]
        public void ApplyTestShouldRaiseDbVersioningException()
        {
            Mock<IDBUpdatesScanner> dBUpdatesScannerMock = new Mock<IDBUpdatesScanner>();
            dBUpdatesScannerMock.Setup(scanner => scanner.GetUpdates()).Returns(new[] {"1", "3", "2"});

            string dbUpdateStatementSample = ResourceReader.ReadAsString(GetType(), Constants.DbMigrations.NUMERIC);

            Mock<IDbUpdateLoader> dbUpdateLoaderMock = new Mock<IDbUpdateLoader>();
            dbUpdateLoaderMock.Setup(loader => loader.Load("1")).Returns(dbUpdateStatementSample.Replace(_OLD_VERSION_STRING, string.Format(_VERSION_STRING_TEMPLATE, 1)));
            dbUpdateLoaderMock.Setup(loader => loader.Load("2")).Returns(dbUpdateStatementSample.Replace(_OLD_VERSION_STRING, string.Format(_VERSION_STRING_TEMPLATE, 2)));
            dbUpdateLoaderMock.Setup(loader => loader.Load("3")).Returns(dbUpdateStatementSample.Replace(_OLD_VERSION_STRING, string.Format(_VERSION_STRING_TEMPLATE, 2)));

            Mock<ICurrentDbVersionDetector> currentDbVersionDetectorMock = new Mock<ICurrentDbVersionDetector>();
            currentDbVersionDetectorMock.Setup(detector => detector.Detect()).Returns(new NumericDbVersionIdentifier(0));

            Mock<IDatabaseManager> databaseManagerMock = new Mock<IDatabaseManager>();
            databaseManagerMock.Setup(manager => manager.Drop()).Callback(() => { });

            Mock<IDbUpdateExecutor> dbUpdateExecutorMock = new Mock<IDbUpdateExecutor>();

            DbAupdatesApplier dbAupdateApplier =
                new DbAupdatesApplier(
                    new[]
                        {
                            new DbUpdateSourceDefinition(
                                typeof (SqlDbUpdate<NumericDbVersionIdentifier>),
                                currentDbVersionDetectorMock.Object,
                                dBUpdatesScannerMock.Object,
                                dbUpdateLoaderMock.Object,
                                new NumericSqlDbUpdateBuilder(),
                                dbUpdateExecutorMock.Object,
                                databaseManagerMock.Object)
                        },
                    false);
            Assert.Throws<DbUpdatesValidationException>(() => dbAupdateApplier.Apply());
        }
    }
}