using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA220111_2
{
    internal class Program
    {
        static Dictionary<string, Dictionary<DateTime, int>> sportagak
            = new Dictionary<string, Dictionary<DateTime,int>>();
        static void Main()
        {
            F01();
            F02();
            F03();
            F04();
            F05();
            F06();
            Console.ReadKey(true);
        }

        private static void F06()
        {
            var keresettNap = new DateTime(2012, 07, 29);
            int dsz = sportagak.Sum(x => x.Value[keresettNap]);
            Console.WriteLine($"6. feladat:\n\t{keresettNap.Day}.-án/én {dsz} db döntő volt");
        }

        private static void F05()
        {
            int ermekSzama = sportagak.Sum(x => x.Value.Sum(y => y.Value));
            #region nem - LINQ
            //int sum = 0;
            //foreach (var olimpiaiIdoszak in sportagak.Values)
            //{
            //    foreach (var eremSzam in olimpiaiIdoszak.Values)
            //    {
            //        sum += eremSzam;
            //    }
            //}
            //Console.WriteLine($"output: {sum}");
            #endregion
            Console.WriteLine($"5. feladat:\n\t{ermekSzama} db aranyérmet osztottak ki az olimpián");
        }

        private static void F04()
        {
            var model = new Dictionary<DateTime, int>();

            foreach (var sportag in sportagak)
            {
                foreach (var nap in sportag.Value)
                {
                    if (!model.ContainsKey(nap.Key))
                    {
                        model.Add(nap.Key, nap.Value);
                    }
                    else model[nap.Key] += nap.Value;
                }
            }
            #region kiiratás
            //foreach (var nap in model)
            //{
            //    Console.WriteLine($"{nap.Key.ToString("MM-dd")} - {nap.Value} db");
            //}
            #endregion
            var ldn = model.OrderBy(x => x.Value).Last();

            Console.WriteLine($"4. feladat:\n\tA legtöbb döntő ({ldn.Value} db) {ldn.Key.Day}.-án/én volt");
        }

        private static void F03()
        {
            int ua = sportagak["Úszás"].Sum(x => x.Value);
            Console.WriteLine($"3. feladat:\n\taranyérmek száma úszásban: {ua} db");
        }

        private static void F02()
        {
            int db = sportagak["Atlétika"].Count(x => x.Value > 0);
            Console.WriteLine($"2. feladat\n\tDöntős napok száma atlétika sportágban {db} db");
        }

        private static void F01()
        {
            using (var sr = new StreamReader(@"..\..\res\London2012.txt", Encoding.UTF8))
            {
                while (!sr.EndOfStream)
                {
                    var v = sr.ReadLine().Split(';');

                    sportagak.Add(v[0], new Dictionary<DateTime, int>());

                    var datum = new DateTime(2012, 07, 28);
                    for (int i = 1; i <= 16; i++)
                    {
                        sportagak[v[0]].Add(datum, int.Parse(v[i]));
                        datum = datum.AddDays(1);
                    }
                }
            }
        }
    }
}
