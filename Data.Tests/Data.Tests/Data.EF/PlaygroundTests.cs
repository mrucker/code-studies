using System.Diagnostics;
using System.Linq;
using Data.EF;
using NUnit.Framework;

namespace Data.Tests.Data.EF
{
    [TestFixture]
    public class PlaygroundTests
    {
        [Test, Ignore("This one takes a long time (about 13 miliseconds per row).")]
        public void InsertTenThousandAlphabets1()
        {
            using (var entities = new Entities())
            {
                var insertTime = new Stopwatch();
                var alphabets  = Enumerable.Range(0, 10000).Select(i => new Alphabet { A = "A", B = "B", C = "C", D = "D", E = "E", F = "F", G = "G", H = "H", I = "I", J = "J", K = "K", L = "L", M = "M", N = "N", O = "O", P = "P", Q = "Q", R = "R", S = "S", T = "T", U = "U", V = "V", W = "W", X = "X", Y = "Y", Z = "Z" }).ToList();

                insertTime.Start();

                foreach (var alphabet in alphabets)
                {
                    entities.Alphabets.Add(alphabet);
                }
                entities.SaveChanges();

                insertTime.Stop();

                double actualInsertTimePerAlphabet = (double)insertTime.ElapsedMilliseconds / alphabets.Count();

                Debug.WriteLine("Insert Time Milliseconds Per Row: " + actualInsertTimePerAlphabet);
            }
        }

        [Test, Ignore("This one takes a long time (about 13 miliseconds per row).")]
        public void InsertTenThousandAlphabets2()
        {
            using (var entities = new Entities())
            {
                var insertTime = new Stopwatch();
                var alphabets  = Enumerable.Range(0, 10000).Select(i => new Alphabet { A = "A", B = "B", C = "C", D = "D", E = "E", F = "F", G = "G", H = "H", I = "I", J = "J", K = "K", L = "L", M = "M", N = "N", O = "O", P = "P", Q = "Q", R = "R", S = "S", T = "T", U = "U", V = "V", W = "W", X = "X", Y = "Y", Z = "Z" }).ToList();

                entities.Configuration.ProxyCreationEnabled  = false;
                entities.Configuration.ValidateOnSaveEnabled = false;
                entities.Configuration.LazyLoadingEnabled    = false;

                insertTime.Start();

                foreach (var alphabet in alphabets)
                {
                    entities.Alphabets.Add(alphabet);
                }
                entities.SaveChanges();

                insertTime.Stop();

                double actualInsertTimePerAlphabet = (double)insertTime.ElapsedMilliseconds / alphabets.Count();

                Debug.WriteLine("Insert Time Milliseconds Per Row: " + actualInsertTimePerAlphabet);
            }
        }

        [Test]
        public void InsertTenThousandAlphabets3()
        {
            var entities   = new Entities();
            var insertTime = new Stopwatch();
            var alphabets  = Enumerable.Range(0, 10000).Select(i => new Alphabet { A = "A", B = "B", C = "C", D = "D", E = "E", F = "F", G = "G", H = "H", I = "I", J = "J", K = "K", L = "L", M = "M", N = "N", O = "O", P = "P", Q = "Q", R = "R", S = "S", T = "T", U = "U", V = "V", W = "W", X = "X", Y = "Y", Z = "Z" }).ToList();

            entities.Configuration.ProxyCreationEnabled = false;
            entities.Configuration.ValidateOnSaveEnabled = false;
            entities.Configuration.LazyLoadingEnabled = false;

            insertTime.Start();

            for (int i = 0; i < alphabets.Count; i++)
            {
                if (i % 500 == 0)
                {
                    entities.SaveChanges();
                    entities.Dispose();
                    entities = new Entities();
                }

                entities.Alphabets.Add(alphabets[i]);
            }
            entities.SaveChanges();

            insertTime.Stop();

            double actualInsertTimePerAlphabet = (double)insertTime.ElapsedMilliseconds / alphabets.Count();

            Debug.WriteLine("Insert Time Milliseconds Per Row: " + actualInsertTimePerAlphabet);

            entities.Dispose();
        }
    }
}
