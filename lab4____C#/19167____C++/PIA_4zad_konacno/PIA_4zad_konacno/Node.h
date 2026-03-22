#pragma once
#include <iostream>

using namespace std;

//klasa koja implementira cvorove binomnog heap-a
class Node 
{
public:

	int data;
	int degree;
	Node* parent;
	Node* child;
	Node* sibling;

	Node(int data);
};

inline Node::Node(int data)
{
	this->data = data;
	degree = 0;
	parent = nullptr;
	child = nullptr;
	sibling = nullptr;
}