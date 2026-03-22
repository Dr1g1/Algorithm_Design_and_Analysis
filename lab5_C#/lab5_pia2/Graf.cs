using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace lab5_pia2
{

    public class Graf
    {

        public List<Vertex> Cvorovi { get; set; } = new List<Vertex>();
        public Vertex DodajCvor(int id)
        {
            Vertex v = new Vertex(id);
            Cvorovi.Add(v);
            return v;
        }
        public void DodajPoteg(Vertex a, Vertex b, int tezina)
        {
            a.adjLista.Add((b, tezina));
            b.adjLista.Add((a, tezina));
        }

        public Vertex NadjiCvor(int id)
        {
            return Cvorovi.FirstOrDefault(v => v.id == id);
        }

        public static Graf GenerisiSlucaj1(int N, int k)
        {
            var graf = new Graf();
            var rnd = new Random();
            var potezi = new HashSet<(int, int)>();

            for (int i = 0; i < N; i++)
                graf.DodajCvor(i);

            int centralni = rnd.Next(N);
            for (int i = 0; i < N; i++)
            {
                if (i != centralni)
                {
                    int tezina = rnd.Next(1, 101);
                    graf.DodajPoteg(graf.NadjiCvor(centralni), graf.NadjiCvor(i), tezina);
                    potezi.Add((Math.Min(centralni, i), Math.Max(centralni, i)));
                }
            }

            while (potezi.Count < N - 1 + k)
            {
                int a = rnd.Next(N);
                int b = rnd.Next(N);
                if (a == b) continue;
                var par = (Math.Min(a, b), Math.Max(a, b));
                if (potezi.Contains(par)) continue;

                int tezina = rnd.Next(1, 101);
                graf.DodajPoteg(graf.NadjiCvor(a), graf.NadjiCvor(b), tezina);
                potezi.Add(par);
            }

            return graf;
        }

        public static Graf GenerisiSlucaj2(int N, int k)
        {
            var graf = new Graf();
            var rnd = new Random();
            var potezi = new HashSet<(int, int)>();

            for (int i = 0; i < N; i++)
                graf.DodajCvor(i);

            for (int i = 0; i < N - 1; i++)
            {
                int tezina = rnd.Next(1, 101);
                graf.DodajPoteg(graf.NadjiCvor(i), graf.NadjiCvor(i + 1), tezina);
                potezi.Add((i, i + 1));
            }

            while (potezi.Count < N - 1 + k)
            {
                int a = rnd.Next(N);
                int b = rnd.Next(N);
                if (a == b) continue;
                var par = (Math.Min(a, b), Math.Max(a, b));
                if (potezi.Contains(par)) continue;

                int tezina = rnd.Next(1, 101);
                graf.DodajPoteg(graf.NadjiCvor(a), graf.NadjiCvor(b), tezina);
                potezi.Add(par);
            }

            return graf;
        }

        public List<(Vertex from, Vertex to, int weight)> PronadjiMST()
        {
            if (Cvorovi.Count == 0) return new List<(Vertex from, Vertex to, int weight)>();

            var mst = new List<(Vertex from, Vertex to, int weight)>();

            var key = new Dictionary<Vertex, int>();
            var parent = new Dictionary<Vertex, Vertex>();
            var uMST = new HashSet<Vertex>();

            foreach (var v in Cvorovi)
            {
                key[v] = int.MaxValue;
                parent[v] = null;
            }

            Vertex start = Cvorovi[0];
            key[start] = 0;

            var pq = new SortedSet<(int key, Vertex v)>(
                Comparer<(int, Vertex)>.Create((a, b) =>
                    a.Item1 == b.Item1 ? a.Item2.id.CompareTo(b.Item2.id) : a.Item1.CompareTo(b.Item1))
            );


            foreach (var v in Cvorovi)
                pq.Add((key[v], v));

            while (pq.Count > 0)
            {
                var (k, u) = pq.Min;
                pq.Remove(pq.Min);
                uMST.Add(u);

                if (parent[u] != null)
                    mst.Add((parent[u], u, key[u]));

                foreach (var (sused, tezina) in u.adjLista)
                {
                    if (!uMST.Contains(sused) && tezina < key[sused])
                    {
                        pq.Remove((key[sused], sused));
                        key[sused] = tezina;
                        parent[sused] = u;
                        pq.Add((key[sused], sused));
                    }
                }
            }

            return mst;
        }

        public Graf Izjednaci()
        {
            var rezG = new Graf();
            var mapa = new Dictionary<int, Vertex>();

            foreach (var stariCvor in Cvorovi)
            {
                var noviCvor = new Vertex(stariCvor.id);
                rezG.Cvorovi.Add(noviCvor);
                mapa[stariCvor.id] = noviCvor;
            }

            var dodateGrane = new HashSet<(int, int)>();

            foreach (var stariCvor in Cvorovi)
            {
                foreach (var (sused, _) in stariCvor.adjLista)
                {
                    int a = Math.Min(stariCvor.id, sused.id);
                    int b = Math.Max(stariCvor.id, sused.id);

                    if (!dodateGrane.Contains((a, b)))
                    {
                        rezG.DodajPoteg(mapa[a], mapa[b], 1);
                        dodateGrane.Add((a, b));
                    }
                }
            }

            return rezG;
        }


    }
}
