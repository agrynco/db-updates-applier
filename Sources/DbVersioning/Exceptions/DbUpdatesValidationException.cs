#region Usings
using AGrynCo.Lib.Validation;
#endregion

namespace DbVersioning.Exceptions
{
    public class DbUpdatesValidationException : DbVersioningException
    {
        #region Fields (private)
        private readonly ValidationResultList<DbUpdateList, DbUpdateValidationResult> _validationResult;
        #endregion

        #region Constructors
        public DbUpdatesValidationException(string message, ValidationResultList<DbUpdateList, DbUpdateValidationResult> validationResult)
            : base(message)
        {
            _validationResult = validationResult;
        }

        public ValidationResultList<DbUpdateList, DbUpdateValidationResult> ValidationResult
        {
            get { return _validationResult; }
        }
        #endregion
    }
}