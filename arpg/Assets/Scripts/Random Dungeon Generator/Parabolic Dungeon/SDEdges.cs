using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SDEdges : MonoBehaviour
{
    public List<Edge> Edges { get; private set; }
    private SDSegmentManagement sdSegmentManagement;
    private GameObject dungeonContainer;
    private GameObject edgePrefab;

    //you have to call Constructor method immidiately after adding this component
    public void Constructor(SDSegmentManagement sdSegmentManagement, GameObject dungeonContainer, GameObject edgePrefab)
    {
        this.sdSegmentManagement = sdSegmentManagement;
        this.dungeonContainer = dungeonContainer;
        this.edgePrefab = edgePrefab;
    }

    //create edges
    public void CreateEdges()
    {
        Edges = new List<Edge>();
        Segment first = sdSegmentManagement.EdgeSegments.ElementAt(0);
        Segment last = sdSegmentManagement.EdgeSegments.Last();
        Edge firstEdge = new Edge(first.Point, last.Point);
        //CreateSingleEdge(firstEdge, dungeonsContainer.transform);
        Edges.Add(firstEdge);
        for (int i = 1; i < sdSegmentManagement.EdgeSegments.Count(); i++)
        {
            first = sdSegmentManagement.EdgeSegments.ElementAt(i);
            last = sdSegmentManagement.EdgeSegments.ElementAt(i - 1);
            Edge e = new Edge(first.Point, last.Point);
            //CreateSingleEdge(e, dungeonsContainer.transform);
            Edges.Add(e);
        }
    }
    public void CreateSingleEdge(Edge e, Transform container)
    {
        GameObject go = Instantiate(edgePrefab, e.NodeA, e.Rotation, container);
        go.transform.localScale = new Vector3(e.VectorAtoB.magnitude, go.transform.localScale.y, 1f);
    }
}
