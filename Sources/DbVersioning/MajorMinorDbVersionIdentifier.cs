#region Usings
using System;
using Lib.Utils.ObjUtils;
#endregion

namespace Lib.Data.DbVersioning
{
    public class MajorMinorDbVersionIdentifier : BaseClass, IDbVersionIdentifier
    {
        #region Constructors
        public MajorMinorDbVersionIdentifier(int major, int minor)
        {
            Major = major;
            Minor = minor;

            IsItZeroIdentifier = Major == 0 && Minor == 0;
        }
        #endregion

        #region IDbVersionIdentifier Properties
        public bool IsItZeroIdentifier { get; private set; }
        #endregion

        #region IDbVersionIdentifier Methods
        int IComparable<IDbVersionIdentifier>.CompareTo(IDbVersionIdentifier other)
        {
            return CompareTo((MajorMinorDbVersionIdentifier) other);
        }
        #endregion

        #region Static Methods (public)
        public static MajorMinorDbVersionIdentifier Parse(string s)
        {
            string[] strings = s.Split(new[] {'.'}, StringSplitOptions.None);

            return new MajorMinorDbVersionIdentifier(int.Parse(strings[0]), int.Parse(strings[1]));
        }
        #endregion

        #region Methods (public)
        public int CompareTo(MajorMinorDbVersionIdentifier other)
        {
            if (Major == other.Major)
            {
                if (Minor == other.Minor)
                    return 0;

                if (Minor > other.Minor)
                    return 1;

                return -1;
            }

            if (Major > other.Major)
                return 1;

            return -1;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((MajorMinorDbVersionIdentifier) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) Major * 397) ^ (int) Minor;
            }
        }
        #endregion

        #region Methods (protected)
        protected bool Equals(MajorMinorDbVersionIdentifier other)
        {
            return Major == other.Major && Minor == other.Minor;
        }
        #endregion

        #region Properties (public)
        public int Major { get; set; }
        public int Minor { get; set; }
        #endregion

        public static bool operator >(MajorMinorDbVersionIdentifier a, MajorMinorDbVersionIdentifier b)
        {
            int i = a.CompareTo(b);
            return i > 0;
        }

        public static bool operator <(MajorMinorDbVersionIdentifier a, MajorMinorDbVersionIdentifier b)
        {
            int i = a.CompareTo(b);
            return i < 0;
        }

        public static bool operator ==(MajorMinorDbVersionIdentifier a, MajorMinorDbVersionIdentifier b)
        {
            if (a == null && b == null)
            {
                return true;
            }

            if (a == null)
            {
                throw new NullReferenceException();
            }

            int i = a.CompareTo(b);
            return i == 0;
        }

        public static bool operator !=(MajorMinorDbVersionIdentifier a, MajorMinorDbVersionIdentifier b)
        {
            if (a == null && b == null)
            {
                return false;
            }

            if (a == null)
            {
                throw new NullReferenceException();
            }

            int i = a.CompareTo(b);
            return i != 0;
        }

        public static bool operator >=(MajorMinorDbVersionIdentifier a, MajorMinorDbVersionIdentifier b)
        {
            if (a == b)
                return true;

            return a > b;
        }

        public static bool operator <=(MajorMinorDbVersionIdentifier a, MajorMinorDbVersionIdentifier b)
        {
            if (a == b)
                return true;

            return a < b;
        }
    }
}