#pragma once
#include "Node.h"
#include <iostream>

using namespace std;

//klasa koja implementira binomni heap
class BinomniHeap
{

public:

	Node* head;//lista korena

	BinomniHeap();//konstruktor

	void InsertFunkcija(int n);
	void MergeFunkcija(BinomniHeap& b);
	int IzbaciMinimalniFunkcija();
	void DecreaseKeyFunkcija(Node* x, int k);
	void ObrisiNode(Node* x);
	void TransformisiMinFunkcija();
	void SnimiMaxFunkcija(BinomniHeap& h);
	void InsertMinFunkcija(int n, bool indikator = true);
	void MergeMinFunkcija(BinomniHeap& h, bool indikator = true);
	Node* LinkFunkcija(Node* hn1, Node* n2, bool indikator = true);
	void KonsolidacijaFunkcija(bool indikator = true);
	void PrintPomocna(Node* n);
	void SnimiMaxPomocna(BinomniHeap& b, Node* n);
	void HeapifyTreeFunkcija(Node* n);
	void HeapifyFunkcija(Node* n);
	void PrintFunkcija();
};