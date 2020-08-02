using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment
{
    public Vector2 BottomLeft { get; private set; }
    public Vector2 TopLeft { get; private set; }
    public Vector2 TopRight { get; private set; }
    public Vector2 BottomRight { get; private set; }

    public Vector2 Point { get; private set; }

    public Segment(Vector2 bottomLeft, Vector2 topRight)
    {
        BottomLeft = bottomLeft;
        TopRight = topRight;
        TopLeft = new Vector2(bottomLeft.x, topRight.y);
        BottomRight = new Vector2(topRight.x, bottomLeft.y);
    }

    public void GenerateRandomPoint()
    {
        float x = Random.Range(BottomLeft.x, TopRight.x);
        float y = Random.Range(BottomLeft.y, TopRight.y);
        Point = new Vector2(x, y);
    }
}
