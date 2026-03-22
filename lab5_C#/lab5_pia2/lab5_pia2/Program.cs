using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace lab5_pia2
{
    internal class Program
    {

        public static void print(List<(Vertex from, Vertex to, int weight)> rez)
        {
            foreach (var r in rez)
            {
                Console.WriteLine($"Grana od cvora: {r.from.id} do cvora: {r.to.id} sa tezinom: {r.weight}");
            }
            Console.WriteLine();
        }
        static void Main(string[] args)
        {
            int[] N_vrednosti = { 100, 1000, 10000, 100000 };
            int[] k_vrednosti = { 2, 5, 10, 20, 33, 50 };

            int N = N_vrednosti[0];//ostavila sam da se za svako N proveravaju svi slucajevi sa svim k vrednostima
            //int N = N_vrednosti[1];
            //int N = N_vrednosti[2];
            //int N = N_vrednosti[3];


            foreach (int k_vred in k_vrednosti)
            {
                int k = k_vred * N;
                Console.WriteLine($"_____ Test za N = {N}, k = {k} _____\n");
                var graf1 = Graf.GenerisiSlucaj1(N, k);
                var mst1 = graf1.PronadjiMST();
                var najlakse1 = graf1.Izjednaci();
                var najlakse1rez = najlakse1.PronadjiMST();
                Console.WriteLine("Slucaj 1 poredjenje stabala:");
                Console.WriteLine("_____ Minimalno sprezno stablo: _____");
                print(mst1);
                Console.WriteLine("_____ Najlakse sprezno stablo: _____");
                print(najlakse1rez);

                var graf2 = Graf.GenerisiSlucaj2(N, k);
                var mst2 = graf2.PronadjiMST();
                var najlakse2 = graf2.Izjednaci();
                var najlakse2rez = najlakse2.PronadjiMST();
                Console.WriteLine("Slucaj 2 poredjenje stabala:");
                Console.WriteLine("_____ Minimalno sprezno stablo: _____");
                print(mst2);
                Console.WriteLine("_____ Najlakse sprezno stablo: _____");
                print(najlakse2rez);
            }
        }
    }
}
