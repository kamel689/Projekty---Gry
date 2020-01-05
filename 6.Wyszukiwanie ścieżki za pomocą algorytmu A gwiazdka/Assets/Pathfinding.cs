using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    //A* bedzie przechowywał 3 głowne wartości - koszt G, koszt H i koszt F
    // GCOST = odległość od węzła początkowego
    // HCOST = odległość od węzła końcowego
    // FCOST = G Cost + H Cost
    Grid GridReference;//do odniesienia do grid class
    public Transform StartPosition;//Pozycja początkowa do znalezienia ścieżki
    public Transform TargetPosition;//Starting position to pathfind to

    private void Awake()//Kiedy program sie uruchomi
    {
        GridReference = GetComponent<Grid>();//Uzyskanie referencji dla game manager
    }

    private void Update()//Every frame
    {
        FindPath(StartPosition.position, TargetPosition.position);//Znajdz scieżke do celu
    }

    void FindPath(Vector3 a_StartPos, Vector3 a_TargetPos)
    {
        Node StartNode = GridReference.NodeFromWorldPoint(a_StartPos);//Pobieramy najbliższy węzeł pozycji początkowej 
        Node TargetNode = GridReference.NodeFromWorldPoint(a_TargetPos);//Pobiera węzeł najbliżej pozycji docelowej

        List<Node> OpenList = new List<Node>();//Lista węzłów dla openlist (do oceny)
        HashSet<Node> ClosedList = new HashSet<Node>();//Hashset węzłów dla ClosedList(juz ocenione)

        OpenList.Add(StartNode);//Dodaj węzeł początkowy do listy open, aby rozpocząć proces wyszukiwania ścieżki

        while (OpenList.Count > 0)//działa wtedy kiedy gdy liczba w liscie open jest wieksza od 0
        {
            Node CurrentNode = OpenList[0];//Utwórz węzeł o nazwie currentNode i ustaw go na pierwszym elemencie w liście otwartej
            for (int i = 1; i < OpenList.Count; i++)//Pętlę przez OpenList, zaczynając od drugiego obiektu
            {
                if (OpenList[i].FCost < CurrentNode.FCost || OpenList[i].FCost == CurrentNode.FCost && OpenList[i].ihCost < CurrentNode.ihCost)
                //Jeśli koszt f tego obiektu jest mniejszy lub równy kosztowi f bieżącego węzła
                {
                    CurrentNode = OpenList[i];//Ustaw bieżący węzeł dla tego obiektu (obiekt celu jest bliżej niż aktualny węzeł
                }
            }
                OpenList.Remove(CurrentNode);//Usuń biężący węzeł z otwartej listy po zakonczeniu pętli
                ClosedList.Add(CurrentNode);//I dodaj go do zamkniętej listy

                if (CurrentNode == TargetNode)//Sprawdamy czy bieżący węzeł jest węzłem docelowym
                {
                    GetFinalPath(StartNode, TargetNode);//Oblicz ostatnią ścieżkę
                }

                foreach (Node NeighborNode in GridReference.GetNeighboringNodes(CurrentNode))//pętle przechodzi przez każdego sąsiada bieżącego węzła
                {
                    if (!NeighborNode.bIsWall || ClosedList.Contains(NeighborNode))//Jeśli sąsiad jest ścianą lub został już sprawdzony
                    {
                        continue;//pomiń to
                    }
                    int MoveCost = CurrentNode.igCost + GetManhattenDistance(CurrentNode, NeighborNode);//Uzyskaj koszt F tego sąsiada (obliczamy koszt przeprowadzki do sąsiada)

                    if (MoveCost < NeighborNode.igCost || !OpenList.Contains(NeighborNode))//Jeśli koszt f jest większy niż koszt g lub nie ma go na liście otwartej
                    {
                        NeighborNode.igCost = MoveCost;//Ustaw koszt g na koszt F.
                        NeighborNode.ihCost = GetManhattenDistance(NeighborNode, TargetNode);//Ustaw koszt h
                        NeighborNode.ParentNode = CurrentNode;//Ustaw węzeł rodzica do śledzenia kroków

                        if (!OpenList.Contains(NeighborNode))//Jeśli sąsiada nie ma w OpenList
                        {
                            OpenList.Add(NeighborNode);//Dodaj go do listy
                        }
                    }
                }

            }
        }
    


    void GetFinalPath(Node a_StartingNode, Node a_EndNode)
    {
        List<Node> FinalPath = new List<Node>();//Lista utrzymująca ścieżkę sekwencyjnie(Lista przechowujaca scieżkę końcową)
        Node CurrentNode = a_EndNode;//Węzeł do przechowywania bieżącego sprawdzanego węzła (przechowujemy węzeł końcowy)

        while (CurrentNode != a_StartingNode)//działą gdy bieżący węzeł nie jest równy węzłowi początkowemu
        {
            FinalPath.Add(CurrentNode);//Dodaj bieżący węzeł do końcowej ścieżki
            CurrentNode = CurrentNode.ParentNode;//Przejdź do węzła nadrzędnego
        }

        FinalPath.Reverse();//Odwróć ścieżkę, aby uzyskać prawidłową kolejność

        GridReference.FinalPath = FinalPath;//Odwróć ścieżkę, aby uzyskać prawidłową kolejność

    }

    int GetManhattenDistance(Node a_nodeA, Node a_nodeB)
    {
        int ix = Mathf.Abs(a_nodeA.iGridX - a_nodeB.iGridX);//x1-x2
        int iy = Mathf.Abs(a_nodeA.iGridY - a_nodeB.iGridY);//y1-y2

        return ix + iy;//Zwróć sumę
    }
}
