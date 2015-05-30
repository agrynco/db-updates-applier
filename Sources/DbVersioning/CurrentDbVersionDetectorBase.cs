#region Usings
using System.Data;
using Lib.Data.DataProviders;
using Lib.Data.DbVersioning.Exceptions;
#endregion

namespace Lib.Data.DbVersioning
{
    public abstract class CurrentDbVersionDetector<TDbVersionIdentifier> : ICurrentDbVersionDetector
        where TDbVersionIdentifier : class, IDbVersionIdentifier
    {
        #region Fields (private)
        private readonly IDataProvider _dataProvider;
        private readonly string _getVersionSqlCommandText;
        private readonly string _checkDbSupportVersioningCommandText;
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataProvider"></param>
        /// <param name="getVersionSqlCommandText"></param>
        /// <param name="checkDbSupportVersioningCommandText">Must be a SQL query which returns 1 af support, otherwise 0</param>
        public CurrentDbVersionDetector(IDataProvider dataProvider, string getVersionSqlCommandText,
            string checkDbSupportVersioningCommandText)
        {
            _dataProvider = dataProvider;
            _getVersionSqlCommandText = getVersionSqlCommandText;
            _checkDbSupportVersioningCommandText = checkDbSupportVersioningCommandText;
        }
        #endregion

        #region ICurrentDbVersionDetector Methods
        public bool CheckOnDbSupportVersioning()
        {
            return DataProvider.ExecuteScalar<int>(_checkDbSupportVersioningCommandText, CommandType.Text) == 1;
        }

        IDbVersionIdentifier ICurrentDbVersionDetector.Detect()
        {
            return Detect();
        }

        public abstract IDbVersionIdentifier CreateZeroIdentifier();
        public bool CheckOnDbExists()
        {
            return DataProvider.CheckOnDbExists();
        }
        #endregion

        #region Abstract Methods
        protected abstract TDbVersionIdentifier DoDetect();
        #endregion

        #region Methods (private)
        private TDbVersionIdentifier Detect()
        {
//            if (!CheckOnDbSupportVersioning())
//            {
//                return null;
//            }

            EnsureGetVersionSqlCommandText();

            return DoDetect();
        }

        private void EnsureGetVersionSqlCommandText()
        {
            if (string.IsNullOrEmpty(GetVersionSqlCommandText))
            {
                throw new DbVersionDetectionException("Please specify property GetVersionSqlCommandText. It is should be not null or empty.");
            }
        }
        #endregion

        #region Properties (protected)
        protected IDataProvider DataProvider
        {
            get { return _dataProvider; }
        }

        protected string GetVersionSqlCommandText
        {
            get { return _getVersionSqlCommandText; }
        }
        #endregion
    }
}