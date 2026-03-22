using lab5_pia2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5_pia2
{
    public class Vertex
    {
        public int id;
        public int key;
        public List<(Vertex sused, int tezina)> adjLista;

        public Vertex(int id)
        {
            this.id = id;
            key = int.MaxValue;
            adjLista = new List<(Vertex sused, int tezina)>();
        } 

    }
}
