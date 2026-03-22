#include "BinomniHeap.h"

//konstruktor
BinomniHeap::BinomniHeap()
{
    head = nullptr;//inicijalno nullptr
}

void BinomniHeap::MergeFunkcija(BinomniHeap& b)
//f-ja koja spaja 2 binomna heap-a
{
    MergeMinFunkcija(b);//poziva pomocnu f-ju
}

void BinomniHeap::MergeMinFunkcija(BinomniHeap& b, bool indikator)
//pomocna f-ja koju MergeFunkcija poziva; spaja 2 binomna heap-a u jedan
//indikator kaze da li je u pitanju min ili max heap
{
    Node* novi = nullptr;
    Node* ptr1 = head;//pokazivac na prvi heap
    Node* ptr2 = b.head;//pokazivac na drugi heap
    Node** pom = &novi;

    while (ptr1 != nullptr && ptr2 != nullptr) //spajanje dva heap-a na osnovu stepena
    {
        if (ptr1->degree < ptr2->degree) 
        {
            *pom = ptr1;
            ptr1 = ptr1->sibling;
        }
        else 
        {
            *pom = ptr2;
            ptr2 = ptr2->sibling;
        }
        pom = &(*pom)->sibling;
    }

    if (ptr1 == nullptr)
        *pom = ptr2;
    if (ptr2 == nullptr)
        *pom = ptr1;

    head = novi;

    KonsolidacijaFunkcija(indikator);//konsolidacija jer je potrebno da spojimo cvorove sa istim stepenom
}

void BinomniHeap::InsertMinFunkcija(int n, bool indikator)
//funkcija za unos novog elementa u min heap 
{
    Node* novi = new Node(n);
    BinomniHeap h;
    h.head = novi;

    MergeMinFunkcija(h, indikator);//spajanje novog heap-a sa postojecim
}

void BinomniHeap::InsertFunkcija(int n)
//upravlja unosom u heap, po default-u koristi min heap
{
    InsertMinFunkcija(n);
}

//f-ja koja spaja 2 cvora sa razlicitim stepenima
Node* BinomniHeap::LinkFunkcija(Node* n1, Node* n2, bool indikator) {
    if ((indikator && n1->data < n2->data) || (!indikator && n1->data > n2->data))
    {
        //prelancavanje
        n2->parent = n1;
        n2->sibling = n1->child;
        n1->child = n2;
        n1->degree++;

        return n1;
    }
    else
    {
        n1->parent = n2;
        n1->sibling = n2->child;
        n2->child = n1;
        n2->degree++;

        return n2;
    }
}


void BinomniHeap::KonsolidacijaFunkcija(bool indikator)
{
    //f-ja koja spaja cvorove sa istim stepenima
    Node* tmp = head;
    Node** tmps = new Node * [30];

    for (int i = 0; i < 30; i++) {
        tmps[i] = nullptr;
    }

    head = nullptr;

    //spajanje cvorova sa istim stepenom
    while (tmp != nullptr)
    {
        Node* tmpsl = tmp->sibling;
        tmp->sibling = nullptr;
        int degree = tmp->degree;

        while (tmps[degree])
        {
            tmp = LinkFunkcija(tmp, tmps[degree], indikator);
            tmps[degree++] = nullptr;
        }

        tmps[degree] = tmp;
        tmp = tmpsl;
    }

    for (int i = 0; i < 30; i++)
    {
        if (tmps[i] != nullptr)
        {
            if (head == nullptr)
            {
                head = tmps[i];
                tmp = tmps[i];
            }
            else
            {
                tmp->sibling = tmps[i];
                tmp = tmp->sibling;
            }
        }
    }
}

int BinomniHeap::IzbaciMinimalniFunkcija()
{
//izdvajanje minimalnog elementa iz heap-a
    Node* n = head;
    Node* pret = nullptr;
    Node* nmin = head;
    Node* nminpret = nullptr;

    if (!n)//heap je prazan
        return -1;
    //prolazimo kroz sve cvorove kako bi se nasao minimalni
    while (n)
    {
        if (nmin->data > n->data)
        {
            nmin = n;
            nminpret = pret;
        }

        pret = n;
        n = n->sibling;
    }
    //uklanjanje najmanjeg cvora iz liste
    if (!nminpret)
        head = head->sibling;

    else
        nminpret->sibling = nmin->sibling;

    int mindata = nmin->data;
    //obrstanje grananja i dodavanje u novi heap
    n = nmin->child;

    BinomniHeap b1;

    while (n != nullptr)
    {
        Node* pomsl = n->sibling;
        n->parent = nullptr;
        n->sibling = b1.head;
        b1.head = n;
        n = pomsl;
    }

    delete nmin;//brisanje najmanjeg cvora
    MergeFunkcija(b1);

    return mindata;//vracamo najmanji element koji smo izbacili
}

void BinomniHeap::PrintFunkcija()
{
    Node* pom = head;
    while (pom)
    {
        cout << "Stablo stepena: " << pom->degree << " ";
        PrintPomocna(pom);
        cout << endl;
        pom = pom->sibling;
    }
}

void BinomniHeap::PrintPomocna(Node* x)
//pomocna f-ja za f-ju koja stampa heap
{
    cout << x->data << " ";
    Node* pom = x->child;

    while (pom)
    {
        PrintPomocna(pom);
        pom = pom->sibling;
    }
}

void BinomniHeap::SnimiMaxFunkcija(BinomniHeap& h)
//cuva binomni heap kao mac
{
    Node* pom = head;
    while (pom != nullptr) {
        SnimiMaxPomocna(h, pom);
        pom = pom->sibling;
    }
}

void BinomniHeap::SnimiMaxPomocna(BinomniHeap& h, Node* n)
{
    h.InsertMinFunkcija(n->data, false);
    Node* pom = n->child;
    while (pom != nullptr) {
        SnimiMaxPomocna(h, pom);
        pom = pom->sibling;
    }
}


void BinomniHeap::TransformisiMinFunkcija()
{
    Node* pom = head;
    while (pom) 
    {
        HeapifyTreeFunkcija(pom);
        pom = pom->sibling;
    }
}

void BinomniHeap::HeapifyFunkcija(Node* n)
//fja koja ima logiku za ispravljanje min heap svojstva
{
    if (n->child == nullptr)
        return;

    Node* p = n->child->sibling, * pmin = n->child;

    while (p)
    {
        if (p->data < pmin->data)
            pmin = p;

        p = p->sibling;
    }
    if (n->data > pmin->data) {
        swap(n->data, pmin->data);
        //rekurzivno zove samu sebe
        HeapifyFunkcija(pmin);
    }
}

void BinomniHeap::HeapifyTreeFunkcija(Node* n)
{
    //zadovoljavanje min heap svojstava
    if (n->child == nullptr)
        return;

    Node* pom = n->child;
    while (pom) 
    {
        if (pom->child == nullptr)
            HeapifyFunkcija(pom);
        else
            HeapifyTreeFunkcija(pom);
        pom = pom->sibling;
    }
    HeapifyFunkcija(n);
}

void BinomniHeap::ObrisiNode(Node* n)//brise cvor iz heap-a
{
    //prvo smanji vrednost tog cvora 
    auto max_int = std::numeric_limits<int>::max;
    int broj = (int)max_int;//iz nekog razloga ovo nije htelo da radi dok nisam stavila max_int u drugu promenljivu
    DecreaseKeyFunkcija(n, broj);//nije htela ni direktna konverzija
    IzbaciMinimalniFunkcija();
}

void BinomniHeap::DecreaseKeyFunkcija(Node* n, int datanova)//smanjuje vrednost data kod cvorova
{
    if (datanova > n->data)
    {
        cout << "Nova vrednost ključa je veca od trenutne!" << endl;
        return;
    }
    n->data = datanova;

    Node* pom1 = n;
    Node* pom2 = pom1->parent;

    while (pom2 != nullptr && pom1->data < pom2->data)
    {
        swap(pom1->data, pom2->data);

        pom1 = pom2;
        pom2 = pom1->parent;
    }
}

