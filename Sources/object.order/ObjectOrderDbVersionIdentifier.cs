#region Usings
using System;
using DbVersioning;
#endregion

namespace @object.order
{
    public class ObjectOrderDbVersionIdentifier : IDbVersionIdentifier
    {
        public string Identifier { get; set; }

        public int CompareTo(IDbVersionIdentifier other)
        {
            throw new NotSupportedException("Order of the scripts is defined in the object.order files.");
        }

        public bool IsItZeroIdentifier
        {
            get { return Identifier == "zero.sql"; }
        }
    }
}