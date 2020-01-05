using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{

    public Transform StartPosition;//W tym miejscu program rozpocznie wyszukiwanie ścieżki.
    public LayerMask WallMask;//Jest to maska, której program będzie szukał podczas próby znalezienia przeszkód na ścieżce.
    public Vector2 vGridWorldSize;// vector2 do przechowywania szerokości i wysokości wykresu w jednostkach świata.
    public float fNodeRadius;//Przechowuje, jak duży będzie każdy kwadrat na wykresie
    public float fDistanceBetweenNodes;//Odległość, jaką kwadraty będą odradzały się od siebie

    Node[,] NodeArray;//Tablica węzłów używanych przez algorytm A*
    public List<Node> FinalPath;//Ukończona ścieżka, którą zostanie narysowana czerwona linia.


    float fNodeDiameter;//Dwukrotnie większy promień (Ustaw w funkcji start)
    int iGridSizeX, iGridSizeY;//Rozmiar siatki w jednostkach macierzy.


    private void Start()//Ran once the program starts
    {
        fNodeDiameter = fNodeRadius * 2;//Uruchomiony po uruchomieniu programu
        iGridSizeX = Mathf.RoundToInt(vGridWorldSize.x / fNodeDiameter);//Podziel współrzędne świata siatki przez średnicę, aby uzyskać rozmiar wykresu w jednostkach tablicowych.
        iGridSizeY = Mathf.RoundToInt(vGridWorldSize.y / fNodeDiameter);//Podziel współrzędne świata siatki przez średnicę, aby uzyskać rozmiar wykresu w jednostkach tablicowych.
        CreateGrid();//Draw the grid
    }

    void CreateGrid()
    {
        NodeArray = new Node[iGridSizeX, iGridSizeY];//Zadeklaruj tablicę węzłów
        Vector3 bottomLeft = transform.position - Vector3.right * vGridWorldSize.x / 2 - Vector3.forward * vGridWorldSize.y / 2;//Get the real world position of the bottom left of the grid.
        for (int x = 0; x < iGridSizeX; x++)//Pętla przez tablicę węzłów.
        {
            for (int y = 0; y < iGridSizeY; y++)//Pętla przez tablicę węzłów
            {
                Vector3 worldPoint = bottomLeft + Vector3.right * (x * fNodeDiameter + fNodeRadius) + Vector3.forward * (y * fNodeDiameter + fNodeRadius);//Get the world co ordinates of the bottom left of the graph
                bool Wall = true;//Zrób węzeł ścianą

                //Jeśli węzeł nie jest blokowany
                //Szybka kontrola kolizji z bieżącym węzłem i wszystkim na świecie w jego położeniu. Jeśli koliduje z obiektem z WallMask,
                //Instrukcja if zwróci false.
                if (Physics.CheckSphere(worldPoint, fNodeRadius, WallMask))
                {
                    Wall = false;//Obiekt nie jest ścianą
                }

                NodeArray[x, y] = new Node(Wall, worldPoint, x, y);//Utwórz nowy węzeł w tablicy.
            }
        }
    }

    //Funkcja, która pobiera sąsiednie węzły danego węzła.
    public List<Node> GetNeighboringNodes(Node a_NeighborNode)
    {
        List<Node> NeighborList = new List<Node>();//Stwórz nową listę wszystkich dostępnych sąsiadów.
        int icheckX;//Zmienna, aby sprawdzić, czy XPosition jest w zasięgu tablicy węzłów, aby uniknąć błędów poza zakresem.
        int icheckY;//Zmienna, aby sprawdzić, czy pozycja YPosition znajduje się w zasięgu tablicy węzłów, aby uniknąć błędów poza zakresem.

        //Sprawdź prawą stronę bieżącego węzła.
        icheckX = a_NeighborNode.iGridX + 1;
        icheckY = a_NeighborNode.iGridY;
        if (icheckX >= 0 && icheckX < iGridSizeX)//Jeśli XPosition znajduje się w zakresie tablicy
        {
            if (icheckY >= 0 && icheckY < iGridSizeY)//Jeśli pozycja YPosition znajduje się w zakresie tablicy
            {
                NeighborList.Add(NodeArray[icheckX, icheckY]);//Dodaj siatkę do listy dostępnych sąsiadów
            }
        }
        // Sprawdź lewą stronę bieżącego węzła.
        icheckX = a_NeighborNode.iGridX - 1;
        icheckY = a_NeighborNode.iGridY;
        if (icheckX >= 0 && icheckX < iGridSizeX)// Jeśli XPosition znajduje się w zakresie tablicy
        {
            if (icheckY >= 0 && icheckY < iGridSizeY)//Jeśli pozycja YPosition znajduje się w zakresie tablicy
            {
                NeighborList.Add(NodeArray[icheckX, icheckY]);//Dodaj siatkę do listy dostępnych sąsiadów
            }
        }
        //Sprawdź górną stronę bieżącego węzła.
        icheckX = a_NeighborNode.iGridX;
        icheckY = a_NeighborNode.iGridY + 1;
        if (icheckX >= 0 && icheckX < iGridSizeX)//Jeśli XPosition znajduje się w zakresie tablicy
        {
            if (icheckY >= 0 && icheckY < iGridSizeY)//Jeśli pozycja YPosition znajduje się w zakresie tablicy
            {
                NeighborList.Add(NodeArray[icheckX, icheckY]);//Dodaj siatkę do listy dostępnych sąsiadów
            }
        }
        //Sprawdź dolną strone bieżącego węzła
        icheckX = a_NeighborNode.iGridX;
        icheckY = a_NeighborNode.iGridY - 1;
        if (icheckX >= 0 && icheckX < iGridSizeX)//Sprawdź dolną stronę bieżącego węzła.
        {
            if (icheckY >= 0 && icheckY < iGridSizeY)//Jeśli pozycja YPosition znajduje się w zakresie tablicy
            {
                NeighborList.Add(NodeArray[icheckX, icheckY]);//Dodaj siatkę do listy dostępnych sąsiadów
            }
        }

        return NeighborList;//Zwróć listę sąsiadów.
    }

    //Pobiera węzeł najbliższy danej pozycji na świecie.
    public Node NodeFromWorldPoint(Vector3 a_vWorldPos)
    {
        float ixPos = ((a_vWorldPos.x + vGridWorldSize.x / 2) / vGridWorldSize.x);
        float iyPos = ((a_vWorldPos.z + vGridWorldSize.y / 2) / vGridWorldSize.y);

        ixPos = Mathf.Clamp01(ixPos);
        iyPos = Mathf.Clamp01(iyPos);

        int ix = Mathf.RoundToInt((iGridSizeX - 1) * ixPos);
        int iy = Mathf.RoundToInt((iGridSizeY - 1) * iyPos);

        return NodeArray[ix, iy];
    }


    //Funkcja rysująca model szkieletowy
    private void OnDrawGizmos()
    {

        Gizmos.DrawWireCube(transform.position, new Vector3(vGridWorldSize.x, 1, vGridWorldSize.y));//Draw a wire cube with the given dimensions from the Unity inspector

        if (NodeArray != null)//Jeśli siatka nie jest pusta
        {
            foreach (Node n in NodeArray)//Pętla przez każdy węzeł w siatce
            {
                if (n.bIsWall)//Jeśli bieżący węzeł jest węzłem ściennym
                {
                    Gizmos.color = Color.white;//Ustaw kolor węzła
                }
                else
                {
                    Gizmos.color = Color.yellow;//Ustaw kolor węzła
                }


                if (FinalPath != null)//Jeśli ostatnia ścieżka nie jest pusta
                {
                    if (FinalPath.Contains(n))//Jeśli bieżący węzeł znajduje się w końcowej ścieżce
                    {
                        Gizmos.color = Color.red;//Ustaw kolor tego węzł
                    }

                }


                Gizmos.DrawCube(n.vPosition, Vector3.one * (fNodeDiameter - fDistanceBetweenNodes));//Narysuj węzeł w pozycji węzła.
            }
        }
    }
}
