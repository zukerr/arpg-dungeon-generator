using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge
{
    public Vector2 NodeA { get; private set; }
    public Vector2 NodeB { get; private set; }
    public Vector2 VectorAtoB { get; private set; }
    public Quaternion Rotation { get; private set; }

    public Edge(Vector2 nodeA, Vector2 nodeB)
    {
        NodeA = nodeA;
        NodeB = nodeB;
        VectorAtoB = new Vector2(NodeB.x - NodeA.x, NodeB.y - NodeA.y);
        float sign = Mathf.Sign(VectorAtoB.y);
        Rotation = Quaternion.Euler(0, 0, sign * Vector2.Angle(Vector2.right, VectorAtoB));
    }
}
