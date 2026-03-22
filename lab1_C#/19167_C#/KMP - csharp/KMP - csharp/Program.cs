using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Schema;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
using System.Threading;
using System.Linq.Expressions;

namespace KMP___csharp
{
    internal class Program
    {

        public static int[] ComputePrefixFunkcija(string pattern)
        {
            int m = pattern.Length;
            int[] pi = new int[m];
            int k = 0;
            for (int q = 1; q < m; q++)
            {
                while (k > 0 && pattern[k] != pattern[q])
                    k = pi[k - 1];
                if (pattern[k] == pattern[q])
                    k++;
                pi[q] = k;
            }
            return pi;
        }

        public static void KmpMatcherFunkcija(string tekst, string pattern)
        {
            int m = pattern.Length;
            int n = tekst.Length;
            int q = 0;
            int[] pi = new int[m];
            int br = 0;

            pi = ComputePrefixFunkcija(pattern);

            for (int i = 0; i < n; i++)
            {
                //ispostavilo se da ova dva fora moraju da idu pre while-a zato sto ce inace doci do exceptiona jer ce da pokusa da procita pattern[m]
                if (q == pattern.Length)
                {
                    Console.WriteLine("Zadati uzorak se u tekstu pojavljuje od " + (i - pattern.Length + 1) + ". indeksa.");
                    q = pi[q - 1];
                    br++;
                }

                while (q > 0 && pattern[q] != tekst[i])
                    q = pi[q - 1];

                if (pattern[q] == tekst[i])
                {
                    q++;
                }
            }
            Console.WriteLine("---Zadati uzorak se pojavio " + br + " puta---");
        }

        static void Main(string[] args)
        {
            string tekst;
            Stopwatch sw = new Stopwatch();
            double elapsedMicroSeconds;

            //podrazumevane vrednosti
            string path = "mojtekst100.txt";

            string patern1 = "match";
            string patern2 = "characters";
            string patern3 = "matched successfully";
            string patern4 = "shifts in the naive pattern-matching algorithm and";

            int param1 = 1, param2 = 100;

            //parametar1 = { 1 - ASCII, 2 - Hexadecimal }
            //parametar2 = { 100, 1000, 10000, 100000 }
            Console.WriteLine("Unesite parametre:");
            param1 = int.Parse(Console.ReadLine());
            param2 = int.Parse(Console.ReadLine());

            IzaberiFunkcija(param1, param2, out patern1, out patern2, out patern3, out patern4, out path);

            using (StreamReader sr = new StreamReader(path))
            {
                tekst = sr.ReadToEnd();
            }

            Console.WriteLine("1. podstring od 5 karaktera");

            sw.Reset();
            sw.Start();
            KmpMatcherFunkcija(tekst, patern1);
            sw.Stop();
            elapsedMicroSeconds = (sw.ElapsedTicks * 1000000) / Stopwatch.Frequency;
            Console.WriteLine("Izvrsenje je trajalo: " + elapsedMicroSeconds);//microsec
            Console.WriteLine("\n");

            Console.WriteLine("2. podstring od 10 karaktera");
            sw.Reset();
            sw.Start();
            KmpMatcherFunkcija(tekst, patern2);
            sw.Stop();
            elapsedMicroSeconds = (sw.ElapsedTicks * 1000000) / Stopwatch.Frequency;
            Console.WriteLine("Izvrsenje je trajalo: " + elapsedMicroSeconds);//microsec
            Console.WriteLine("\n");

            Console.WriteLine("3. podstring od 20 karaktera");
            sw.Reset();
            sw.Start();
            KmpMatcherFunkcija(tekst, patern3);
            sw.Stop();
            elapsedMicroSeconds = (sw.ElapsedTicks * 1000000) / Stopwatch.Frequency;
            Console.WriteLine("Izvrsenje je trajalo: " + elapsedMicroSeconds);//microsec
            Console.WriteLine("\n");

            Console.WriteLine("4. podstring od 50 karaktera");
            sw.Reset();
            sw.Start();
            KmpMatcherFunkcija(tekst, patern4);
            sw.Stop();
            elapsedMicroSeconds = (sw.ElapsedTicks * 1000000) / Stopwatch.Frequency;
            Console.WriteLine("Izvrsenje je trajalo: " + elapsedMicroSeconds);//microsec
            Console.WriteLine("\n");

        }

        public static void IzaberiFunkcija(int parametar1, int parametar2, out string patern1, out string patern2, out string patern3, out string patern4, out string path)
        //parametar1 = { 1 - ASCII, 2 - Hexadecimal }
        //parametar2 = { 100, 1000, 10000, 100000 }
        {
            path = "";
            patern1 = "";
            patern2 = "";
            patern3 = "";
            patern4 = "";

            switch (parametar1)
            {
                case 1:
                    switch (parametar2)
                    {
                        case 100:
                            path = "mojtekst100.txt";

                            patern1 = "match";//5 karatera
                            patern2 = "characters";//10 karaktera
                            patern3 = "matched successfully";//20 karaktera
                            patern4 = "shifts in the naive pattern-matching algorithm and";//50 karaktera

                            Console.WriteLine("Isvrsenje algoritma nad tekstom od priblizno 100 reci: \n");
                            break;
                        case 1000:
                            path = "mojtekst1000.txt";

                            patern1 = "apply";//5 karatera
                            patern2 = "algorithms";//10 karaktera
                            patern3 = "asymptotic notations";//20 karaktera
                            patern4 = "the running time may differ on different inputs of";//50 karaktera

                            Console.WriteLine("Isvrsenje algoritma nad tekstom od priblizno 1000 reci: \n");
                            break;
                        case 10000:
                            path = "mojtekst10000.txt";

                            patern1 = "Samsa";//5 karatera
                            patern2 = "remembered";//10 karaktera
                            patern3 = "sister was screaming";//20 karaktera
                            patern4 = "Eventually, though, Gregor realised that he had no";//50 karaktera

                            Console.WriteLine("Isvrsenje algoritma nad tekstom od priblizno 10 000 reci: \n");
                            break;
                        case 100000:
                            path = "mojtekst100000.txt";

                            patern1 = "dread";//5 karatera   
                            patern2 = "human life";//10 karaktera  
                            patern3 = "this fabulous entity";//20 karaktera
                            patern4 = "—If virtue goes to sleep, it will be more vigorous";//50 karaktera


                            Console.WriteLine("Isvrsenje algoritma nad tekstom od priblizno 100 000 reci: \n");
                            break;
                    }
                    break;
                case 2:
                    switch (parametar2)
                    {
                        case 100:
                            path = "hex~100.txt";

                            patern1 = "030FA";//5 karatera
                            patern2 = "BD0D4A 1B2";//10 karaktera
                            patern3 = "AC2F 39796C 92 DD143";//20 karaktera
                            patern4 = "3D E8 D876 F5F 4E1 2666 F1 73 BA7 46 05 2E01 5CD0D";//50 karaktera

                            Console.WriteLine("Isvrsenje algoritma nad tekstom od priblizno 100 reci: \n");
                            break;
                        case 1000:
                            path = "hex~1000.txt";

                            patern1 = "F6429";//5 karatera
                            patern2 = "19 A8 F56E";//10 karaktera
                            patern3 = "DDB 7B3FE 21453 B07B";//20 karaktera
                            patern4 = "48576 1254 F2316 30 18F5 BF3 3BB4D7 A4 F2F72 DCA 1";//50 karaktera

                            Console.WriteLine("Isvrsenje algoritma nad tekstom od priblizno 1000 reci: \n");
                            break;
                        case 10000:
                            path = "hex~10000.txt";

                            patern1 = "59FBF";//5 karatera
                            patern2 = "502 8528BB";//10 karaktera
                            patern3 = "9E0DF 4B612 E8A089 5";//20 karaktera
                            patern4 = "6AD E4 C901C DA1E3C 422 CD7 2C57 4741 A3A24D AE142";//50 karaktera

                            Console.WriteLine("Isvrsenje algoritma nad tekstom od priblizno 10 000 reci: \n");
                            break;
                        case 100000:
                            path = "hex~100000.txt";

                            patern1 = "D1E0A";//5 karatera   
                            patern2 = "072 07548B";//10 karaktera  
                            patern3 = "197A FE051E 141 6B 4";//20 karaktera
                            patern4 = "E432D1 8723 0A DF826 7028 2E0 A488 5A8FA 90AB D449";//50 karaktera


                            Console.WriteLine("Isvrsenje algoritma nad tekstom od priblizno 100 000 reci: \n");
                            break;
                    }
                    break;
                default:
                    path = "mojtekst100.txt";

                    patern1 = "match";
                    patern2 = "characters";
                    patern3 = "matched successfully";
                    patern4 = "shifts in the naive pattern-matching algorithm and";
                    break;

            }

        }
    }
}
