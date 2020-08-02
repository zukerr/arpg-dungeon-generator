using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SDSegmentManagement2 : MonoBehaviour
{
    private SDSegmentManagement parent;

    private GameObject pointTestPrefab;
    private GameObject dungeonContainer;

    //you have to call Constructor method immidiately after adding this component
    public void Constructor(SDSegmentManagement parent, GameObject pointTestPrefab, GameObject dungeonContainer)
    {
        this.parent = parent;
        this.pointTestPrefab = pointTestPrefab;
        this.dungeonContainer = dungeonContainer;
    }

    public void GeneratePointsInSegmentsAndSortThem()
    {
        GenerateRandomPoints();
        SortSegments();
    }
    private void GenerateRandomPoints()
    {
        foreach (Segment s in parent.EdgeSegments)
        {
            s.GenerateRandomPoint();
            Instantiate(pointTestPrefab, s.Point, pointTestPrefab.transform.rotation, dungeonContainer.transform);
        }
    }
    private void SortSegments()
    {
        List<Segment> leftSublist = SortSegmentHelper2(0, false);
        List<Segment> topSublist = SortSegmentHelper2(1, false);
        List<Segment> rightSublist = SortSegmentHelper2(2, true);
        List<Segment> bottomSublist = SortSegmentHelper2(3, true);

        for (int i = 0; i < 4; i++)
        {
            parent.EdgeSegments.RemoveAt(0);
        }
        parent.EdgeSegments.AddRange(leftSublist);
        parent.EdgeSegments.AddRange(topSublist);
        parent.EdgeSegments.AddRange(rightSublist);
        parent.EdgeSegments.AddRange(bottomSublist);
    }

    private void SortSegmentHelper(List<Segment> list)
    {
        for (int i = 0; i < parent.EdgeSegmentsAmount; i++)
        {
            list.Add(parent.EdgeSegments.ElementAt(4));
            parent.EdgeSegments.RemoveAt(4);
        }
    }
    
    private List<Segment> SortSegmentHelper2(int ii, bool reverse)
    {
        List<Segment> list = new List<Segment>();
        SortSegmentHelper(list);
        if(reverse)
        {
            list.Reverse();
        }
        list.Insert(0, parent.EdgeSegments.ElementAt(ii));
        return list;
    }
}
