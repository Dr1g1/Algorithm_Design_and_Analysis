#include <iostream>
#include <ctime>
#include <chrono>
#include "Node.h"
#include "BinomniHeap.h"

using namespace std;

BinomniHeap* GenerisiRandom(int n, int a, int b, int k);

int main()
{
    srand(time(NULL));

    int n;
    int x = 10, y = 1000;//default vrednosti
    int k = 100;

    cout << "Unesite n:" << endl;
    cin >> n;

    cout << "Unesite x:" << endl;
    cin >> x;

    cout << "Unesite y:" << endl;
    cin >> y;

    cout << "Unesite k:" << endl;
    cin >> k; 

    BinomniHeap *h = GenerisiRandom(n, x, y, k);

    cout << "Stampamo heap na pocetku:\n";
    (*h).PrintFunkcija();//stampamo heap
    cout << endl;

    BinomniHeap *h1 = new BinomniHeap();//max heap

    auto start = chrono::high_resolution_clock::now();
    (*h).SnimiMaxFunkcija((*h1));
    auto end = chrono::high_resolution_clock::now();

//NAPOMENA: vreme koje je mereno je predstavljeno u mikrosekundama

    cout << "Izvrsenje je trajalo: " << chrono::duration_cast<chrono::microseconds>(end - start).count() << " microsec.\n\n";

    cout << "Stampamo max heap:\n";
    (*h1).PrintFunkcija();

    start = chrono::high_resolution_clock::now();
    (*h1).TransformisiMinFunkcija();
    end = chrono::high_resolution_clock::now();

    cout << "Izvrsenje je trajalo: " << chrono::duration_cast<chrono::microseconds>(end - start).count() << " microsec.\n\n";

    cout << "Stampamo min heap (max heap nakon transformacije):\n";
    (*h1).PrintFunkcija();
}

BinomniHeap* GenerisiRandom(int n, int a, int b, int k)
{
    BinomniHeap* h = new BinomniHeap();
    int i = 0, dodatih = 0;

    for (int i = 0; i < n; i++)
    {
        int broj = rand() % (b - a + 1);
        (*h).InsertFunkcija(a + broj);
        if (i % k == 0)//svaki k-ti
            (*h).IzbaciMinimalniFunkcija();//izbacicemo minimalni element iz heap-a na svakih k
    }

    return h;
}