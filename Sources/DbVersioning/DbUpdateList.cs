#region Usings
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Lib.Utils.Collections;
#endregion

namespace Lib.Data.DbVersioning
{
    public class DbUpdateList : CustomList<IDbUpdate>
    {
        #region Methods (public)
        public void Add(IDbUpdate dbUpdate)
        {
            if (Count > 0)
            {
                this.Last().IsLast = false;
            }
            dbUpdate.IsLast = true;
            ((ICustomList<IDbUpdate>) this).Add(dbUpdate);
        }

        public void Sort(IComparer<IDbUpdate> comparer)
        {
            ((ICustomList<IDbUpdate>) this).Sort(comparer);

            foreach (IDbUpdate dbUpdate in this)
            {
                dbUpdate.IsLast = false;
            }

            UpdateLastUpdate();
        }

        public void RemoveAt(int index)
        {
            if (index == Count - 1)
            {
                UpdateLastUpdate();
            }

            ((ICustomList<IDbUpdate>) this).RemoveAt(index);
        }

        private void UpdateLastUpdate()
        {
            if (Count > 0)
            {
                this.Last().IsLast = true;
            }
        }
        #endregion
    }
}