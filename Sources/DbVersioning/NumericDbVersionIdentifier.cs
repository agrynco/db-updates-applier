#region Usings
using System;
using Lib.Utils.ObjUtils;
#endregion

namespace Lib.Data.DbVersioning
{
    public class NumericDbVersionIdentifier : BaseClass, IDbVersionIdentifier
    {
        #region Constructors
        public NumericDbVersionIdentifier(uint number)
        {
            Number = number;
            IsItZeroIdentifier = number == 0;
        }
        #endregion

        #region IDbVersionIdentifier Methods
        int IComparable<IDbVersionIdentifier>.CompareTo(IDbVersionIdentifier other)
        {
            return CompareTo((NumericDbVersionIdentifier) other);
        }
        #endregion

        #region Methods (public)
        public int CompareTo(NumericDbVersionIdentifier other)
        {
            return Number.CompareTo(other.Number);
        }

        public override bool Equals(object obj)
        {
            return Number == ((NumericDbVersionIdentifier) obj).Number;
        }

        public override int GetHashCode()
        {
            return (int) Number;
        }
        #endregion

        #region Methods (protected)
        protected bool Equals(NumericDbVersionIdentifier other)
        {
            return Number == other.Number;
        }
        #endregion

        #region Properties (public)
        public uint Number { get; private set; }
        #endregion

        public static bool operator >(NumericDbVersionIdentifier a, NumericDbVersionIdentifier b)
        {
            int i = a.CompareTo(b);
            return i > 0;
        }

        public static bool operator <(NumericDbVersionIdentifier a, NumericDbVersionIdentifier b)
        {
            int i = a.CompareTo(b);
            return i < 0;
        }

        public static bool operator ==(NumericDbVersionIdentifier a, NumericDbVersionIdentifier b)
        {
            int i = a.CompareTo(b);
            return i == 0;
        }

        public static bool operator !=(NumericDbVersionIdentifier a, NumericDbVersionIdentifier b)
        {
            int i = a.CompareTo(b);
            return i != 0;
        }

        public static bool operator >=(NumericDbVersionIdentifier a, NumericDbVersionIdentifier b)
        {
            if (a == b)
                return true;

            return a > b;
        }

        public static bool operator <=(NumericDbVersionIdentifier a, NumericDbVersionIdentifier b)
        {
            if (a == b)
                return true;

            return a < b;
        }

        public bool IsItZeroIdentifier { get; private set; }
    }
}