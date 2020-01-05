using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {

    public int iGridX;//Pozycja X w tablicy węzłów
    public int iGridY;//Pozycja Y w tablicy węzłów

    public bool bIsWall;//Informuje program, czy ten węzeł jest blokowany.
    public Vector3 vPosition;// pozycja węzła świata

    public Node ParentNode;//Dla algorytmu A* zapisze, z którego węzła wcześniej pochodził, aby mógł śledzić najkrótszą ścieżkę.

    public int igCost;//Koszt przejścia do następnego kwadratu.
    public int ihCost;//Odległość do celu od tego węzła.

    public int FCost { get { return igCost + ihCost; } }//Funkcja szybkiego pobierania w celu dodania kosztu G i kosztu H, a ponieważ nigdy nie będziemy musieli edytować FCost, nie potrzebujemy ustawionej funkcji

    public Node(bool a_bIsWall, Vector3 a_vPos, int a_igridX, int a_igridY)//Constructor
    {
        bIsWall = a_bIsWall;//Informuje program, czy ten węzeł jest blokowany.
        vPosition = a_vPos;//pozycja węzła świata
        iGridX = a_igridX;//X Position in the Node Array
        iGridY = a_igridY;//Pozycja Y w tablicy węzłów
    }

}
