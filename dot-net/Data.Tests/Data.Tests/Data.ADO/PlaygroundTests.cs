using System.Diagnostics;
using System.Linq;
using Data.ADO;
using NUnit.Framework;

namespace Data.Tests.Data.ADO
{
    [TestFixture]
    public class PlaygroundTests
    {
        [Test]
        public void InsertTenThousandAlphabets1()
        {
            using (var entities = new Entities())
            {
                var insertTime = new Stopwatch();
                var alphabetInserts = Enumerable.Range(0, 10000).Select(i => new Alphabet { A = "A", B = "B", C = "C", D = "D", E = "E", F = "F", G = "G", H = "H", I = "I", J = "J", K = "K", L = "L", M = "M", N = "N", O = "O", P = "P", Q = "Q", R = "R", S = "S", T = "T", U = "U", V = "V", W = "W", X = "X", Y = "Y", Z = "Z" }).Select(a => a.ToInsertStatement()).ToList();

                insertTime.Start();
                foreach (var alphabetInsert in alphabetInserts)
                {
                    entities.ExecuteNonQuery(alphabetInsert);
                }
                insertTime.Stop();

                double actualInsertTimePerAlphabet = (double)insertTime.ElapsedMilliseconds / alphabetInserts.Count();

                Debug.WriteLine("Insert Time Milliseconds Per Row: " + actualInsertTimePerAlphabet);
            }
        }

        [Test]
        public void InsertTenThousandAlphabets2()
        {
            using (var entities = new Entities())
            {
                var insertTime = new Stopwatch();
                var alphabetInserts = Enumerable.Range(0, 10000).Select(i => new Alphabet { A = "A", B = "B", C = "C", D = "D", E = "E", F = "F", G = "G", H = "H", I = "I", J = "J", K = "K", L = "L", M = "M", N = "N", O = "O", P = "P", Q = "Q", R = "R", S = "S", T = "T", U = "U", V = "V", W = "W", X = "X", Y = "Y", Z = "Z" }).Select(a => a.ToInsertStatement()).ToList();

                insertTime.Start();
                foreach (var alphabetInsert in alphabetInserts)
                {
                    entities.ExecuteQuery(alphabetInsert);
                }
                insertTime.Stop();

                double actualInsertTimePerAlphabet = (double)insertTime.ElapsedMilliseconds / alphabetInserts.Count();

                Debug.WriteLine("Insert Time Milliseconds Per Row: " + actualInsertTimePerAlphabet);
            }
        }

        [Test]
        public void InsertTenThousandAlphabets3()
        {
            using (var entities = new Entities())
            {
                var insertTime = new Stopwatch();
                var alphabetInserts = Enumerable.Range(0, 10000).Select(i => new Alphabet { A = "A", B = "B", C = "C", D = "D", E = "E", F = "F", G = "G", H = "H", I = "I", J = "J", K = "K", L = "L", M = "M", N = "N", O = "O", P = "P", Q = "Q", R = "R", S = "S", T = "T", U = "U", V = "V", W = "W", X = "X", Y = "Y", Z = "Z" }).Select(a => a.ToInsertStatement()).ToList();

                insertTime.Start();


                for (int i = 5; i < alphabetInserts.Count; i += 5)
                {
                    string insert = alphabetInserts[i - 5] + alphabetInserts[i - 4] + alphabetInserts[i - 3] + alphabetInserts[i - 2] + alphabetInserts[i - 1];
                    entities.ExecuteQuery(insert);
                }
                insertTime.Stop();

                double actualInsertTimePerAlphabet = (double)insertTime.ElapsedMilliseconds / alphabetInserts.Count();

                Debug.WriteLine("Insert Time Milliseconds Per Row: " + actualInsertTimePerAlphabet);
            }
        }
    }
}
