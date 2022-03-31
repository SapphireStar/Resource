using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node 
{
    public int GridX;
    public int GridY;

    public bool isWall;
    public Vector2 position;

    public Node Parent;

    public int GCost;
    public int HCost;
    public int FCost { get { return GCost + HCost; } }

    public Node(bool a_isWall, Vector2 a_Position, int a_igridX, int a_igridY)
    {
        isWall = a_isWall;
        position = a_Position;
        GridX = a_igridX;//X Position in the Node Array
        GridY = a_igridY;//Y Position in the Node Array
    }


}
