using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SDSegmentManagement : MonoBehaviour
{
    private SDMainSquares sdMainSquares;
    private SDSegmentManagement2 part2;

    private float edgeLength;
    private float smallSquareEdgeLength;
    private Transform center;

    public List<Segment> EdgeSegments { get; private set; }
    public int EdgeSegmentsAmount { get; private set; }

    //you have to call Constructor method immidiately after adding this component
    public void Constructor(SDMainSquares sdMainSquares, int edgeSegmentsAmount, GameObject pointTestPrefab, GameObject dungeonContainer)
    {
        this.sdMainSquares = sdMainSquares;
        this.EdgeSegmentsAmount = edgeSegmentsAmount;
        part2 = gameObject.AddComponent<SDSegmentManagement2>();
        part2.Constructor(gameObject.GetComponent<SDSegmentManagement>(), pointTestPrefab, dungeonContainer);
    }

    public void GenerateSegments()
    {
        SetupVertexSegments();
        SetupAllEdgeSegments();
        part2.GeneratePointsInSegmentsAndSortThem();
    }
    private void SetupVertexSegments()
    {
        edgeLength = sdMainSquares.EdgeLength;
        smallSquareEdgeLength = sdMainSquares.SmallSquareEdgeLength;
        center = sdMainSquares.Center;

        EdgeSegments = new List<Segment>();
        Vector2 bigSquareBottomLeft = new Vector2(center.position.x - (edgeLength / 2), center.position.y - (edgeLength / 2));
        Vector2 smallSquareBottomLeft = new Vector2(center.position.x - (smallSquareEdgeLength / 2), center.position.y - (smallSquareEdgeLength / 2));
        Vector2 bigSquareTopRight = new Vector2(center.position.x + (edgeLength / 2), center.position.y + (edgeLength / 2));
        Vector2 smallSquareTopRight = new Vector2(center.position.x + (smallSquareEdgeLength / 2), center.position.y + (smallSquareEdgeLength / 2));
        Vector2 bigSquareTopLeft = new Vector2(center.position.x - (edgeLength / 2), center.position.y + (edgeLength / 2));
        Vector2 smallSquareTopLeft = new Vector2(center.position.x - (smallSquareEdgeLength / 2), center.position.y + (smallSquareEdgeLength / 2));
        Vector2 bigSquareBottomRight = new Vector2(center.position.x + (edgeLength / 2), center.position.y - (edgeLength / 2));
        Vector2 smallSquareBottomRight = new Vector2(center.position.x + (smallSquareEdgeLength / 2), center.position.y - (smallSquareEdgeLength / 2));

        EdgeSegments.Add(new Segment(bigSquareBottomLeft, smallSquareBottomLeft));
        EdgeSegments.Add(new Segment(new Vector2(smallSquareTopLeft.x - sdMainSquares.FrameWidth, smallSquareTopLeft.y), new Vector2(bigSquareTopLeft.x + sdMainSquares.FrameWidth, bigSquareTopLeft.y)));
        EdgeSegments.Add(new Segment(smallSquareTopRight, bigSquareTopRight));
        EdgeSegments.Add(new Segment(new Vector2(smallSquareBottomRight.x, smallSquareBottomRight.y - sdMainSquares.FrameWidth), new Vector2(smallSquareBottomRight.x + sdMainSquares.FrameWidth, smallSquareBottomRight.y)));
    }
    private void SetupAllEdgeSegments()
    {
        float edgeSegmentLength = smallSquareEdgeLength / EdgeSegmentsAmount;
        //left edge
        Vector2 smallSquareBottomLeft = new Vector2(center.position.x - (smallSquareEdgeLength / 2), center.position.y - (smallSquareEdgeLength / 2));
        Vector2 segBottomLeft = new Vector2(smallSquareBottomLeft.x - sdMainSquares.FrameWidth, smallSquareBottomLeft.y);
        SetupOneEdgeSegments(false, segBottomLeft, edgeSegmentLength);
        //top edge
        Vector2 smallSquareTopLeft = new Vector2(center.position.x - (smallSquareEdgeLength / 2), center.position.y + (smallSquareEdgeLength / 2));
        segBottomLeft = smallSquareTopLeft;
        SetupOneEdgeSegments(true, segBottomLeft, edgeSegmentLength);
        //right edge
        Vector2 smallSquareBottomRight = new Vector2(center.position.x + (smallSquareEdgeLength / 2), center.position.y - (smallSquareEdgeLength / 2));
        segBottomLeft = smallSquareBottomRight;
        SetupOneEdgeSegments(false, segBottomLeft, edgeSegmentLength);
        //bottom edge
        segBottomLeft = new Vector2(smallSquareBottomLeft.x, smallSquareBottomLeft.y - sdMainSquares.FrameWidth);
        SetupOneEdgeSegments(true, segBottomLeft, edgeSegmentLength);
    }
    private void SetupOneEdgeSegments(bool horizontal, Vector2 segBottomLeft, float edgeSegmentLength)
    {
        Vector2 segTopRight;
        for (int i = 0; i < EdgeSegmentsAmount; i++)
        {
            segTopRight = horizontal ?
                new Vector2(segBottomLeft.x + edgeSegmentLength, segBottomLeft.y + sdMainSquares.FrameWidth)
                : new Vector2(segBottomLeft.x + sdMainSquares.FrameWidth, segBottomLeft.y + edgeSegmentLength);
            EdgeSegments.Add(new Segment(segBottomLeft, segTopRight));
            segBottomLeft = horizontal ? new Vector2(segBottomLeft.x + edgeSegmentLength, segBottomLeft.y)
                : new Vector2(segBottomLeft.x, segBottomLeft.y + edgeSegmentLength);
        }
    }
}
