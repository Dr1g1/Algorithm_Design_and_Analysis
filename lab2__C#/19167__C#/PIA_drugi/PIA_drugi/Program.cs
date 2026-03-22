using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace PIA_drugi
{
    internal class Program
    {
        // BUBBLE---------------------------------------------------------------------------------------------
        public static void BubbleFunkcija(int[] niz)
        {
            for (int i = 0; i < niz.Length; i++) 
            {
                for (int j = 0; j < niz.Length - i - 1; j++) 
                {
                    if (niz[j] > niz[j+1])
                    {
                        int pom = niz[j];
                        niz[j] = niz[j+1];
                        niz[j+1] = pom;
                    }
                }
            }
        }

        // MERGE----------------------------------------------------------------------------------------------
        public static void MergeFunkcija(int[] niz, int d, int g, int q)
        {
            int duzina1 = q - d + 1;
            int duzina2 = g - q;
            int[] l = new int[duzina1];
            int[] r = new int[duzina2];
            int i;
            for (i = 0; i < duzina1; i++)
                l[i] = niz[d + i];

            for (i = 0; i < duzina2; i++)
                r[i] = niz[q + i + 1];

            i = 0;
            int j = 0;
            int k = d;

            while (i < duzina1 && j < duzina2)
            {
                if (l[i] <= r[j])
                {
                    niz[k] = l[i];
                    i++;
                }
                else
                {
                    niz[k] = r[j];
                    j++;
                }
                k++;
            }
            while (i < duzina1)
            {
                niz[k] = l[i];
                k++; i++;
            }
            while (j < duzina2)
            {
                niz[k] = r[j];
                k++; j++;
            }
        }

        public static void MergeSortFunkcija(int[] niz, int d, int g)
        {
            if (d >= g)
                return;
            int q = d + (g - d) / 2;
            MergeSortFunkcija(niz, d, q);
            MergeSortFunkcija(niz, q + 1, g);
            MergeFunkcija(niz, d, g, q);
        }

        // COUNTING-------------------------------------------------------------------------------------------
        public static void CountingSortFunkcija(int[] niz, int k)
        {
            int n = niz.Length;
            int[] rez = new int[n];
            int[] count = new int[10];
            for (int i = 0; i < 10; i++)
                count[i] = 0;

            for (int i = 0; i < n; i++)
                count[(niz[i] / k) % 10]++;

            for (int i = 1; i < 10; i++)
                count[i] = count[i] + count[i - 1];

            for (int i = n - 1; i >= 0; i--)
            {
                rez[count[(niz[i] / k) % 10] - 1] = niz[i];
                count[(niz[i] / k) % 10]--;
            }

            for (int i = 0; i < n; i++)
                niz[i] = rez[i];
        }

        public static int maksElement(int[] niz)
        {
            int maks = niz[0];
            for (int i = 1; i < niz.Length; i++)
            {
                if (niz[i] > maks)
                    maks = niz[i];
            }
            return maks;
        }

        public static void radixsortFunkcija2(int[] niz)
        {
            int maks = maksElement(niz);
            for (int i = 1; maks / i > 0; i *= 10)
            {
                CountingSortFunkcija(niz, i);
            }
        }

        // GENERATOR------------------------------------------------------------------------------------------
        public static int[] GenerisiNiz(int n)
        {
            Random r = new Random();
            int[] niz = new int[n];

            for (int i = 0; i < n; i++)
                niz[i] = r.Next(0, 10001);

            return niz;
        }

        public static void nadjiNajmanjuCenuFunkcija(int[] niz, int K)
        {
            int cena = 0;
            int kupi = 0, gratis = niz.Length;

            Stopwatch sw = new Stopwatch();
            double elapsedMicroSeconds;

            sw.Reset();
            sw.Start();

            //BubbleFunkcija(niz);
            //MergeSortFunkcija(niz, 0, niz.Length - 1);
            radixsortFunkcija2(niz);

            sw.Stop();
            elapsedMicroSeconds = (sw.ElapsedTicks * 1000000) / Stopwatch.Frequency;
            Console.WriteLine("Izvrsenje sortiranja je trajalo: " + sw.ElapsedMilliseconds + "ms (" + elapsedMicroSeconds + " microsec.)");//microsec
            Console.WriteLine("\n");

            
            while ((gratis - kupi) > K) 
            {
                cena += niz[kupi];
                kupi++;
                gratis -= K;
            }
            if ((gratis - kupi) > 0) 
            {
                while (kupi < gratis) 
                {
                    cena += niz[kupi];
                    kupi++;
                }
            }

            Console.WriteLine("Cena: " + cena);
        }

        static void Main(string[] args)
        {
            int n1, n2, n3, n4, n5, n6;
            n1 = 100;
            n2 = 1000;
            n3 = 10000;
            n4 = 100000;
            n5 = 1000000;
            n6 = 10000000;

            int H = n6;//H razlicitih tipova slatkisa
            int[] slatkisi = GenerisiNiz(H);

            int K = (int)((double)H * 0.2);//K je 20% broja H

            Stopwatch sw1 = new Stopwatch();
            double elapsedMicroSeconds1;

            sw1.Reset();
            sw1.Start();
            nadjiNajmanjuCenuFunkcija(slatkisi, K);
            //NAPOMENA: U definiciji ove funkcije se bira algoritam sortiranja
            sw1.Stop();
            elapsedMicroSeconds1 = (sw1.ElapsedTicks * 1000000) / Stopwatch.Frequency;
            Console.WriteLine("Izvrsenje funkcije je trajalo: " + sw1.ElapsedMilliseconds + "ms (" + elapsedMicroSeconds1 + " microsec.)");//microsec
            Console.WriteLine("\n");

        }
    }
}
