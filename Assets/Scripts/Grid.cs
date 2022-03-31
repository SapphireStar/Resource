using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Transform startPosition;
    public LayerMask wallMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    public float nodeDistance;

    Node[,] grid;
    public List<Node> finalPath;

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    private void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    
    }

    void CreateGrid()
    {

        grid = new Node[gridSizeX, gridSizeY];
        Vector2 bottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2;

        for(int x = 0; x< gridSizeX; x++)
        {
            for(int y = 0; y < gridSizeY; y++)
            {
                Vector2 worldPoint = bottomLeft + Vector2.right * (x * nodeDiameter + nodeRadius) + Vector2.up * (y * nodeDiameter + nodeRadius);
                bool Wall = true;
                RaycastHit2D rayUp = Physics2D.Raycast(worldPoint, Vector2.up, nodeRadius-0.01f, wallMask);
                RaycastHit2D rayDown = Physics2D.Raycast(worldPoint, -Vector2.up, nodeRadius - 0.01f, wallMask);
                RaycastHit2D rayRight = Physics2D.Raycast(worldPoint, Vector2.right, nodeRadius - 0.01f, wallMask);
                RaycastHit2D rayLeft = Physics2D.Raycast(worldPoint, -Vector2.right, nodeRadius - 0.01f, wallMask);

                if (rayUp||rayDown||rayRight||rayLeft)
                {
                    Wall = false;

                }
                grid[x, y] = new Node(Wall, worldPoint, x, y);

            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 0));
        if (grid != null)
        {
            foreach(Node node in grid)
            {
                if (node.isWall)
                {
                    Gizmos.color = Color.white;
                }
                else
                    Gizmos.color = Color.yellow;

                if (finalPath != null)
                {
                    if (finalPath.Contains(node))
                    {
                        Gizmos.color = Color.red;
                    }
                }
                Gizmos.DrawCube(node.position, Vector3.one * (nodeDiameter - nodeDistance)); 
            }
        }
    }
    public Node nodeFromWorldPosition(Vector3 a_WorldPosition)
    {
        float xPoint = ((a_WorldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x );
        float yPoint = ((a_WorldPosition.y + gridWorldSize.y / 2) / gridWorldSize.y );
        xPoint = Mathf.Clamp01(xPoint);
        yPoint = Mathf.Clamp01(yPoint);
        int x = Mathf.RoundToInt((gridSizeX - 1) * xPoint);
        int y = Mathf.FloorToInt((gridSizeY - 1 ) * yPoint );

        return grid[x, y];


    }
    public List<Node> getNeighboringNodes(Node a_Node)
    {
        List<Node> neighborNode = new List<Node>();
        int xCheck;
        int yCheck;
        xCheck = a_Node.GridX + 1;
        yCheck = a_Node.GridY;
        if (xCheck >= 0 && xCheck < gridSizeX)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                neighborNode.Add(grid[xCheck, yCheck]);
            }
        }

        xCheck = a_Node.GridX - 1;
        yCheck = a_Node.GridY;
        if (xCheck >= 0 && xCheck < gridSizeX)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                neighborNode.Add(grid[xCheck, yCheck]);
            }
        }
        xCheck = a_Node.GridX ;
        yCheck = a_Node.GridY+1;
        if (xCheck >= 0 && xCheck < gridSizeX)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                neighborNode.Add(grid[xCheck, yCheck]);
            }
        }
        xCheck = a_Node.GridX;
        yCheck = a_Node.GridY - 1;
        if (xCheck >= 0 && xCheck < gridSizeX)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                neighborNode.Add(grid[xCheck, yCheck]);
            }
        }
        return neighborNode;
    }
}
