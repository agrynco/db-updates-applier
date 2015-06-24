#region Usings
using System.Collections.Generic;
using System.Linq;

using AGrynco.Lib.Validation;
#endregion

namespace Lib.Data.DbVersioning
{
    public static class DbUpdateListValidator
    {
        #region Methods (public)
        public static ValidationResultList<DbUpdateList, DbUpdateValidationResult> Validate(IEnumerable<IDbUpdate> dbUpdateList)
        {
            ValidationResultList<DbUpdateList, DbUpdateValidationResult> validationResultList = new ValidationResultList<DbUpdateList, DbUpdateValidationResult>();

            validationResultList.AddRange(ValidateOnUniqueNewDbVersion(dbUpdateList));

            return validationResultList;
        }
        #endregion

        #region Methods (private)
        private static List<DbUpdateValidationResult> ValidateOnUniqueNewDbVersion(IEnumerable<IDbUpdate> dbUpdateList)
        {
            IEnumerable<IGrouping<IDbVersionIdentifier, IDbUpdate>> groups = dbUpdateList.GroupBy(x => x.NewDbVersion).Where(y => y.Count() > 1);
            List<IGrouping<IDbVersionIdentifier, IDbUpdate>> list = groups as List<IGrouping<IDbVersionIdentifier, IDbUpdate>> ?? groups.ToList();

            List<DbUpdateValidationResult> result = new List<DbUpdateValidationResult>();
            if (list.Any())
            {
                for (int i = 0; i < list.Count; i++)
                {
                    DbUpdateValidationResult dbUpdateValidationResult = new DbUpdateValidationResult();
                    result.Add(dbUpdateValidationResult);

                    dbUpdateValidationResult.ValidatedObject = new DbUpdateList();
                    dbUpdateValidationResult.ValidationMessage = "Updates has equal new DB version definition";
                    foreach (IDbUpdate dbUpdate in list[i])
                    {
                        dbUpdateValidationResult.ValidatedObject.Add(dbUpdate);
                    }
                }
                return result;
            }

            return result;
        }
        #endregion
    }
}