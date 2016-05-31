using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using Data.ADO;
using NUnit.Framework;

namespace Data.Tests.Data.ADO
{
    [TestFixture]
    class SqlBulkCopyTests
    {
        [Test]
        public void InsertTenThousandAlphabets1()
        {
            using (var entities = new Entities())
            {
                var insertTime = new Stopwatch();
                var alphabets  = Enumerable.Range(0, 10000).Select(i => new Alphabet { A = "A", B = "B", C = "C", D = "D", E = "E", F = "F", G = "G", H = "H", I = "I", J = "J", K = "K", L = "L", M = "M", N = "N", O = "O", P = "P", Q = "Q", R = "R", S = "S", T = "T", U = "U", V = "V", W = "W", X = "X", Y = "Y", Z = "Z" }).ToList();

                insertTime.Start();
                var sqlBulkCopy = new SqlBulkCopy(entities.SqlConnection)
                { 
                    DestinationTableName = "Alphabets",
                    ColumnMappings       = {{"A", "A"}, {"B", "B"}, {"C", "C"}, {"D", "D"}, {"E", "E"}, {"F", "F"}, {"G", "G"}, {"H", "H"}, {"I", "I"}, {"J", "J"}, {"K", "K"}, {"L", "L"}, {"M", "M"}, {"N", "N"}, {"O", "O"}, {"P", "P"}, {"Q", "Q"}, {"R", "R"}, {"S", "S"}, {"T", "T"}, {"U", "U"}, {"V", "V"}, {"W", "W"}, {"X", "X"}, {"Y", "Y"}, {"Z", "Z"}}
                };

                using (var dataReader = new ObjectDataReader<Alphabet>(alphabets))
                {
                    sqlBulkCopy.WriteToServer(dataReader);
                }
                
                sqlBulkCopy.Close();
                insertTime.Stop();

                double actualInsertTimePerAlphabet = (double)insertTime.ElapsedMilliseconds / alphabets.Count();

                Debug.WriteLine("Insert Time Milliseconds Per Row: " + actualInsertTimePerAlphabet);
            }
        }
    }
}
