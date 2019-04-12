using System;
using System.IO;

namespace ReSync
{
    class Program
    {
        static void Main(string[] args)
        {
            var directories = Directory.GetDirectories("D:\\resync");
            foreach(var directory in directories)
            {
                var files = Directory.GetFiles(directory, "*.dat", 
                    SearchOption.AllDirectories);
                var sqlFile = @"D:\\resync\sql\" + directory.Replace(@"\","").Replace("D:","")
                    .Replace("resync","") + ".sql";
                Console.WriteLine(":r " + sqlFile.Replace("\\\\","\\"));
                Console.WriteLine("print \"" + sqlFile +"\";");
                if (File.Exists(sqlFile))
                    continue;
                string sql = "";
                foreach (var file in files)
                {
                    sql += @"BULK INSERT staging
    FROM '" + file + "'" +
        @"WITH
    (
    FIRSTROW = 2,
    FIELDTERMINATOR = ';', --CSV field delimiter
    ROWTERMINATOR = '\n', --Use to shift the control to next row
    ERRORFILE = '"+ sqlFile.Replace(".sql",".csv") + "'," + 
    @"TABLOCK
    );";
                    File.WriteAllText(sqlFile, sql);                    
                }
                
            }
            Console.ReadKey();


        }

    }
}
