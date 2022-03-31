using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathFinding1 : MonoBehaviour
{
    Grid grid;
    public Transform startPosition;
    public Transform targetPosition;
    public void Awake()
    {
        grid = GetComponent<Grid>();

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        findPath(startPosition.position, targetPosition.position);
    }
    void findPath(Vector3 a_StartPosition, Vector3 a_TargetPosition)
    {
        Node startNode = grid.nodeFromWorldPosition(startPosition.position);
        Node targetNode = grid.nodeFromWorldPosition(targetPosition.position);
        List<Node> openList = new List<Node>();
        HashSet<Node> closedList = new HashSet<Node>();

        openList.Add(startNode);
        while (openList.Count > 0)
        {
            Node currentNode = openList[0];
            for(int i = 0; i < openList.Count; i++)
            {
                if (openList[i].FCost < currentNode.FCost || openList[i].FCost == currentNode.FCost && openList[i].HCost < currentNode.HCost)
                {
                    currentNode = openList[i];
                }
                openList.Remove(currentNode);
                closedList.Add(currentNode);

                if (currentNode == targetNode)
                {
                    getFinalPath(startNode, targetNode);
                }
                foreach (Node neighborNode in grid.getNeighboringNodes(currentNode))
                {
                    if (!neighborNode.isWall || closedList.Contains(neighborNode))
                    
                        {
                        continue;
                        }
                    int moveCost = currentNode.GCost + getManhattenDistance(currentNode,neighborNode);
                    if (moveCost < neighborNode.GCost || !openList.Contains(neighborNode))
                    {
                        neighborNode.GCost = moveCost;
                        neighborNode.HCost = getManhattenDistance(neighborNode, targetNode);
                        neighborNode.Parent = currentNode;

                        if (!openList.Contains(neighborNode))
                        {
                            openList.Add(neighborNode);
                        }
                    }
                }
            }
        }
    }
    void getFinalPath(Node a_StartNode,Node a_EndNode)
    {
        List<Node> FinalPath = new List<Node>();
        Node currenNode = a_EndNode;
        while (currenNode != a_StartNode)
        {
            FinalPath.Add(currenNode);
            currenNode = currenNode.Parent;
        }
        FinalPath.Reverse();
        grid.finalPath = FinalPath;
    }
    int getManhattenDistance(Node a_NodeA, Node a_NodeB)
    {
        int x = Mathf.Abs( a_NodeA.GridX - a_NodeB.GridX);
        int y = Mathf.Abs( a_NodeA.GridY - a_NodeB.GridY);
        return x + y;
    }
}
