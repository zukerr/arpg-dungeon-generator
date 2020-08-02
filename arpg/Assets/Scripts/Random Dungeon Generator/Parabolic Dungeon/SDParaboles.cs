using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SDParaboles : MonoBehaviour
{
    private SDEdges sdEdges;
    private SDSpawnPoints sdSpawnPoints;
    private SDBackground sdBackground;
    private GameObject dungeonContainer;
    private GameObject emptyPrefab;
    private float parabolePointFrequency;
    private float paraboleVertexFactor;

    //you have to call Constructor method immidiately after adding this component
    public void Constructor(SDEdges sdEdges, SDSpawnPoints sdSpawnPoints, SDBackground sdBackground, GameObject dungeonContainer, GameObject emptyPrefab,
                            float parabolePointFrequency, float paraboleVertexFactor)
    {
        this.sdEdges = sdEdges;
        this.dungeonContainer = dungeonContainer;
        this.emptyPrefab = emptyPrefab;
        this.parabolePointFrequency = parabolePointFrequency;
        this.paraboleVertexFactor = paraboleVertexFactor;
        this.sdSpawnPoints = sdSpawnPoints;
        this.sdBackground = sdBackground;
    }

    public void CreateParabolesOnEdges()
    {
        foreach (Edge e in sdEdges.Edges)
        {
            CreateParaboleOnEdge(e);
        }
    }
    private void CreateParaboleOnEdge(Edge e)
    {
        GameObject paraboleContainer = Instantiate(emptyPrefab, e.NodeA, emptyPrefab.transform.rotation, dungeonContainer.transform);
        paraboleContainer.name = "Parabole";
        GameObject edgesContainer = Instantiate(emptyPrefab, e.NodeA, emptyPrefab.transform.rotation, paraboleContainer.transform);
        edgesContainer.name = "Edges of Parabole";
        List<Vector2> points = new List<Vector2>();
        
        float root2 = e.NodeA.x + e.VectorAtoB.magnitude;
        for (float i = e.NodeA.x; i < root2; i += parabolePointFrequency)
        {
            Vector2 point = new Vector2(i, e.NodeA.y + GlobalMath.SquareFunctionPositiveA(i, e.NodeA.x, root2, paraboleVertexFactor));
            points.Add(point);
        }
        sdSpawnPoints.CreateSpawnPointsInParabole(e, paraboleContainer, root2, paraboleVertexFactor);
        points.Add(new Vector2(root2, e.NodeA.y));
        for (int i = 1; i < points.Count(); i++)
        {
            sdEdges.CreateSingleEdge(new Edge(points.ElementAt(i), points.ElementAt(i - 1)), edgesContainer.transform);
        }
        paraboleContainer.transform.rotation = e.Rotation;
        for (int i = 0; i < points.Count() - 1; i++)
        {
            sdBackground.AdjustMinMaxForResize(edgesContainer.transform.GetChild(i).transform.position);
        }
    }
}
