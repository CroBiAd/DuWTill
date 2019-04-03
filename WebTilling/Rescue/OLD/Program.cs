using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;
using System.Data;
using System.Data.OleDb;
using System.Data.SQLite;

namespace YourDB {
    class Program {
        static void Main(string[] args) {
            // defaults
            string csv = "TillingDB";
            bool drop = true;
            string dir = Environment.CurrentDirectory;
            // check args
            Regex rx = new Regex(@"^/|^-{1,2}(?<param>[^=:]+)[=:]{0,1}(?<val>.*$)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            args.ToList().ForEach(x => {
                Match m = rx.Match(x);
                if (m.Groups["param"] != null && m.Groups["param"].Success) {
                    switch (m.Groups["param"].Value) {
                        case "csvfilename":
                        case "f":
                            csv = m.Groups["val"].Value + ".csv";
                            break;
                        case "workingdirectory":
                        case "d":
                            dir = m.Groups["val"].Value.TrimEnd('\\');
                            break;
                        case "addrecords":
                        case "r":
                            drop = false;
                            break;
                        default:
                        case "usage":
                        case "help":
                        case "h":
                            usage();
                            break;
                    }
                }
            });
            if (File.Exists(dir + "\\" + csv)) {
                try {
                    var sw = new Stopwatch();
                    sw.Start();
                    // read data from .csv file
                    DataSet ds = new DataSet("csvfile");
                    using (OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OleDb.4.0; Data Source=" + dir + ";Extended Properties=\"Text;HDR=YES;FMT=Delimited\"")) {
                        conn.Open();
                        using (OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM [" + csv + "]", conn)) {
                            adapter.AcceptChangesDuringFill = false;
                            int n = adapter.Fill(ds);
                            sw.Stop();
                            Console.WriteLine();
                            Console.WriteLine(n.ToString() + " records read from " + csv + " file in " + sw.ElapsedMilliseconds + "ms");
                        }
                    }
                    // create a new database or connect to existing one:
                    using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + dir + "\\Tilling.sqlite;Version=3;")) {
                        conn.Open();
                        using (SQLiteCommand cmd = conn.CreateCommand()) {
                            // (re)create a table
                            if (drop) {
                                cmd.CommandText = @"DROP TABLE IF EXISTS [Mutations]";
                                cmd.ExecuteNonQuery();
                            }
                            cmd.CommandText = @"CREATE TABLE IF NOT EXISTS [Mutations] (
                                [mutID] INTEGER NOT NULL PRIMARY KEY,
                                [mutant] INTEGER NULL,
                                [ctg] NVARCHAR(50) NULL,
                                [position] INTEGER NULL,
                                [mutType] NVARCHAR(10) NULL,
                                [parentAllele] NARCHAR(2) NULL,
                                [mutAllele] NARCHAR(2) NULL,
                                [depth] INTEGER NULL,
                                [zygosity] NVARCHAR(10) NULL,
                                [flankSeq] NVARCHAR(1024) NULL,
                                [assemblyID] NVARCHAR(128) NULL,
                                [chr] NARCHAR(4) NULL,
                                [flankStart] INTEGER NULL,
                                [flankEnd] INTEGER NULL
                            )";
                            cmd.ExecuteNonQuery();
                            Console.WriteLine("DB structure has been (re)created. Transferring records...");
                            // fill the table 
                            sw.Restart();
                            using (SQLiteTransaction trans = conn.BeginTransaction()) {
                                cmd.CommandText = "SELECT * FROM Mutations";
                                cmd.Transaction = trans;
                                using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd)) {
                                    using (SQLiteCommandBuilder cb = new SQLiteCommandBuilder(da)) {
                                        da.InsertCommand = cb.GetInsertCommand();
                                        int nrec = da.Update(ds.Tables[0]);
                                        trans.Commit();
                                        sw.Stop();
                                        Console.WriteLine(nrec.ToString() + " records transferred to DB in " + sw.ElapsedMilliseconds + "ms");
                                    }
                                }
                            }
                            // check
                            cmd.CommandText = "SELECT * FROM Mutations ORDER BY mutID DESC LIMIT 5";
                            SQLiteDataReader reader = cmd.ExecuteReader();
                            Console.WriteLine();
                            Console.WriteLine("Bottom 5 rows from " + reader.GetTableName(0) + " table (desc):");
                            int n = reader.FieldCount;
                            for (int i = 0; i < n; i++)
                                Console.Write(reader.GetName(i) + "\t");
                            Console.WriteLine();
                            while (reader.Read()) {
                                for (int i = 0; i < n; i++)
                                    Console.Write(reader.GetValue(i).ToString() + "\t");
                                Console.WriteLine();
                                Console.WriteLine();
                                Console.WriteLine("That's all.");
                            }
                        }
                    }
                } catch (Exception ex) {
                    Console.WriteLine();
                    Console.WriteLine("Error: " + ex.Message);
                    usage();
                    Console.WriteLine();
                }
            } else {
                usage();
                Console.WriteLine();
            }
            Console.WriteLine("Press any key to quit.");
            System.Console.ReadKey();
        }
        static void usage() {
            Console.WriteLine("Usage:");
            Console.WriteLine("------");
            Console.WriteLine("It's better to run from vithin Visual studio.");
            Console.WriteLine("Set command line arguments and/or working directory on YourDB->Properties->Debug page if required.");
            Console.WriteLine("Alternatively, publish, install and execute... ");
            Console.WriteLine("");
            Console.WriteLine("Program accepts short/long Linux/Windows styles:");
            Console.WriteLine("--csvfilename=TillingDB OR -f=TillingDB OR /csvfilename:TillingDB OR /f:TillingDB");
            Console.WriteLine("");
            Console.WriteLine("Only three parameters:");
            Console.WriteLine("--csvfilename, -f comma separated file with mutations table to read in; default: 'TillingDB'");
            Console.WriteLine("--workingdirectory, -d path to csv file, DB will be created in the same directory; default: Environment.CurrentDirectory");
            Console.WriteLine("--addrecords, -r flag (!) whether to add records to existing table; default: create a new table");
            Console.WriteLine("");
            Console.WriteLine("Example:");
            Console.WriteLine(@"YourDB.exe --workingdirectory=C:\VS2015\WebTilling\DuWTill\App_Data\ --csvfilename=TillingDB");
            //https://addons.mozilla.org/en-US/firefox/addon/sqlite-manager-webext/
        }
    }
}
