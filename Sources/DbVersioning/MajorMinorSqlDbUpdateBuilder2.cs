#region Usings
using System.Text;
using System.Text.RegularExpressions;
using AGrynCo.Lib.ResourcesUtils;
using DbVersioning.Exceptions;
#endregion

namespace DbVersioning
{
    public class MajorMinorSqlDbUpdateBuilder2 
        : SqlDbUpdateBuilder<MajorMinorDbVersionIdentifier, NewDbMajorMinorFromFileNameVersionDetector, DummyExpectedMajorMinorDbVersionDetector, FileSystemDbUpdateLoader>
    {
        #region Methods (private)
        private bool ContainsChangesBlock(string content)
        {
            Match match = Regex.Match(content, @"-- BEGIN CHANGES([\s\S]*)-- END CHANGES");

            return match.Success;
        }

        private bool ContainsChekingPossibilityOfUsingBlock(string content)
        {
            Match match = Regex.Match(content, @"-- Start checking possibility of using([\s\S]*)-- End checking possibility of using");

            return match.Success;
        }

        private bool ContainsWrintingNewVersionBlock(string content)
        {
            Match match = Regex.Match(content, @"-- Start writing new version info([\s\S]*)-- End writing new version info");

            return match.Success;
        }

        private bool IsThisIsAFullScript(string content)
        {
            return ContainsChangesBlock(content) && ContainsChekingPossibilityOfUsingBlock(content) && ContainsWrintingNewVersionBlock(content);
        }
        #endregion

        #region Methods (protected)
        public override SqlDbUpdate<MajorMinorDbVersionIdentifier> Build(DbUpdateSourceDescriptor dbUpdateSourceDescriptor, FileSystemDbUpdateLoader dbUpdateLoader)
        {
            string content = dbUpdateLoader.Load(dbUpdateSourceDescriptor);

            if (dbUpdateSourceDescriptor.Path.ToLower().EndsWith("0-0.sql"))
            {
                return new SqlDbUpdate<MajorMinorDbVersionIdentifier>(dbUpdateSourceDescriptor, content, null, new MajorMinorDbVersionIdentifier(0, 0));
            }

            NewDbMajorMinorFromFileNameVersionDetector newDbMajorMinorFromFileNameVersionDetector = new NewDbMajorMinorFromFileNameVersionDetector();

            MajorMinorDbVersionIdentifier newDbVersion = newDbMajorMinorFromFileNameVersionDetector.Detect(dbUpdateSourceDescriptor, content);

            if (IsThisIsAFullScript(content))
            {
                return new SqlDbUpdate<MajorMinorDbVersionIdentifier>(dbUpdateSourceDescriptor, content, null, newDbVersion);
            }

            Match notesMatch = Regex.Match(content, @"\/\*@Notes\s([\s\S]*)\s\*\/");
            if (!notesMatch.Success)
            {
                throw new DbUpdateBuildException(string.Format("There is no notes in the '{0}'", dbUpdateSourceDescriptor));
            }

            return BuildFullSqlDbUpdate(dbUpdateSourceDescriptor, content, newDbVersion, notesMatch);
        }

        private SqlDbUpdate<MajorMinorDbVersionIdentifier> BuildFullSqlDbUpdate(DbUpdateSourceDescriptor dbUpdateSourceDescriptor, string content, MajorMinorDbVersionIdentifier newDbVersion, Match notesMatch)
        {
            StringBuilder scriptContent = new StringBuilder();

            scriptContent.Append(ResourceReader.ReadAsString(GetType(), "Lib.Data.DbVersioning.Templates.MsSqlTemplate.sql"));
            scriptContent.Replace("@newDbVersion = '0.1'", string.Format("@newDbVersion = '{0}.{1}'", newDbVersion.Major, newDbVersion.Minor));

            scriptContent.Replace("-- SQL CHANGES", content);
            scriptContent.Replace(notesMatch.Value, string.Empty);
            scriptContent.Replace("{Notes}", notesMatch.Groups[1].Value.Trim());

            //UpdateOriginalUpdate(fullSourceName, scriptContent);

            return new SqlDbUpdate<MajorMinorDbVersionIdentifier>(dbUpdateSourceDescriptor, scriptContent.ToString(), null, newDbVersion);
        }

        //        private static void UpdateOriginalUpdate(string fullSourceName, StringBuilder scriptContent)
        //        {
        //            using (StreamWriter streamWrter = new StreamWriter(fullSourceName))
        //            {
        //                streamWrter.Write(scriptContent.ToString());
        //            }
        //        }
        #endregion
    }
}