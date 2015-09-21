#region Usings
using System;
using System.Data;
using System.IO;
#endregion

namespace DbVersioning
{
    public class NewDbMajorMinorFromFileNameVersionDetector : SqlDbVersionDetectorBase<MajorMinorDbVersionIdentifier>
    {
        public override MajorMinorDbVersionIdentifier Detect(DbUpdateSourceDescriptor dbUpdateSourceDescriptor, string content)
        {
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(dbUpdateSourceDescriptor.Path);
            if (fileNameWithoutExtension != null)
            {
                string[] strings = fileNameWithoutExtension.Split(new[] {"-"}, StringSplitOptions.None);

                return new MajorMinorDbVersionIdentifier(int.Parse(strings[0]), int.Parse(strings[1]));
            }

            throw new NoNullAllowedException(string.Format("fileNameWithoutExtension can not be null! dbUpdateSourceDescriptor = {0}", dbUpdateSourceDescriptor));
        }
    }
}