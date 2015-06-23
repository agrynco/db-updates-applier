#region Usings
using System;
using System.Data;
using System.IO;
#endregion

namespace Lib.Data.DbVersioning
{
    public class NewDbMajorMinorFromFileNameVersionDetector : SqlDbVersionDetectorBase<MajorMinorDbVersionIdentifier>
    {
        public override MajorMinorDbVersionIdentifier Detect(string fullSourceName, string content)
        {
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fullSourceName);
            if (fileNameWithoutExtension != null)
            {
                string[] strings = fileNameWithoutExtension.Split(new[] { "-" }, StringSplitOptions.None);

                return new MajorMinorDbVersionIdentifier(int.Parse(strings[0]), int.Parse(strings[1]));
            }

            throw new NoNullAllowedException(string.Format("fileNameWithoutExtension can not be null! fullSourceName = {0}", fullSourceName));
        }
    }
}