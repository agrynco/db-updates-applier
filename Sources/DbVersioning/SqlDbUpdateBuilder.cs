﻿namespace DbVersioning
{
    public class SqlDbUpdateBuilder<TDbVersionIdentifier, TNewDbVersionDetector, TExpectedDbVersionDetector, TDbUpdateLoader> 
        : BaseDbUpdateBuilder<SqlDbUpdate<TDbVersionIdentifier>, TDbUpdateLoader>
        where TDbVersionIdentifier : IDbVersionIdentifier
        where TNewDbVersionDetector : SqlDbVersionDetectorBase<TDbVersionIdentifier>, new()
        where TExpectedDbVersionDetector : SqlDbVersionDetectorBase<TDbVersionIdentifier>, new()
        where TDbUpdateLoader : IDbUpdateLoader
    {
        #region Methods (protected)
        public override SqlDbUpdate<TDbVersionIdentifier> Build(DbUpdateSourceDescriptor dbUpdateSourceDescriptor, 
            TDbUpdateLoader dbUpdateLoader)
        {
            string content = dbUpdateLoader.Load(dbUpdateSourceDescriptor);

            TDbVersionIdentifier newDbVersion = (new TNewDbVersionDetector()).Detect(dbUpdateSourceDescriptor, content);
            TDbVersionIdentifier expectedDbVersion = (new TExpectedDbVersionDetector()).Detect(dbUpdateSourceDescriptor, content);

            return new SqlDbUpdate<TDbVersionIdentifier>(dbUpdateSourceDescriptor, content, expectedDbVersion, newDbVersion);
        }
        #endregion
    }
}