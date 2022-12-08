
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace SetupProblem
{
    public static class CreateDay
    {
        public static string Year => DateTime.Now.Year.ToString();
        public static string templatePath = Path.GetFullPath($@"..\..\..\AdventOfCode{Year}\Source\Template.cs");
        public static string inputPath = Path.GetFullPath($@"..\..\..\AdventOfCode{Year}\Input");
        public static string sourcePath = Path.GetFullPath($@"..\..\..\AdventOfCode{Year}\Source");
        public static string driverPath = Path.GetFullPath($@"..\..\..\AdventOfCode{Year}\Source\Program.cs");

        public static void SetupProblem(string currentDay)
        {

            // create input directory and files
            string todaysInputPath = $@"{inputPath}\{currentDay}";
            Directory.CreateDirectory(todaysInputPath);
            File.Create($@"{todaysInputPath}\test.txt");
            File.Create($@"{todaysInputPath}\{currentDay}.txt");

            // create todays sourse files
            string todaysSourcePath = $@"{sourcePath}\{currentDay}.cs";
            if(!File.Exists(todaysSourcePath))
                File.Copy(templatePath, todaysSourcePath, false);

            string sourceCode = File.ReadAllText(todaysSourcePath);
            sourceCode = sourceCode.Replace("Template", currentDay);
            File.WriteAllText(todaysSourcePath, sourceCode);

            // update driver source file
            string driverCode = File.ReadAllText(driverPath);
            string pattern = @"(problem\.Run\()(.*)(\);)";
            string testFile = "$1@\"..\\..\\..\\Input\\" + currentDay + "\\test.txt\"$3";
            driverCode = Regex.Replace(driverCode, pattern, testFile);

            pattern = @"(var problem = new )([a-zA-Z0-9]+)(\(\);)";
            driverCode = Regex.Replace(driverCode, pattern, $@"$1{currentDay}$3");

            File.WriteAllText(driverPath, driverCode);
        }

    }
}
