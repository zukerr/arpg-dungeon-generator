using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SDSpawnPoints : MonoBehaviour
{
    public List<GameObject> MobSpawnpoints { get; private set; }

    private GameObject emptyPrefab;
    private float spawnPointDensity;
    private SDMainSquares sdMainSquares;
    private GameObject dungeonContainer;

    //you have to call Constructor method immidiately after adding this component
    public void Constructor(SDMainSquares sdMainSquares, GameObject emptyPrefab, float spawnPointDensity, GameObject dungeonContainer)
    {
        this.emptyPrefab = emptyPrefab;
        this.spawnPointDensity = spawnPointDensity;
        this.sdMainSquares = sdMainSquares;
        this.dungeonContainer = dungeonContainer;
    }

    public void CreateSpawnPointsInParabole(Edge e, GameObject paraboleContainer, float root2, float paraboleVertexFactor)
    {
        GameObject mobSpawnContainer = Instantiate(emptyPrefab, e.NodeA, emptyPrefab.transform.rotation, paraboleContainer.transform);
        mobSpawnContainer.name = "Mob Spawns";
        MobSpawnpoints = new List<GameObject>();

        //this loop is only for spawnpoints
        for (float i = e.NodeA.x; i < root2; i += 1f / spawnPointDensity)
        {
            Vector2 point = new Vector2(i, e.NodeA.y + GlobalMath.SquareFunctionPositiveA(i, e.NodeA.x, root2, paraboleVertexFactor));
            //create spawnpoints
            for (float j = e.NodeA.y; j > point.y; j -= 1f / spawnPointDensity)
            {

                GameObject go = Instantiate(emptyPrefab, new Vector2(point.x, j), emptyPrefab.transform.rotation, mobSpawnContainer.transform);
                MobSpawnpoints.Add(go);
            }
        }
    }

    public void CreateMobSpawnpointsInside()
    {
        Vector2 smallSquareCenter = sdMainSquares.InsideGO.transform.position;
        Vector2 smallSquareTopLeft = new Vector2(smallSquareCenter.x - (sdMainSquares.SmallSquareEdgeLength / 2), smallSquareCenter.y + (sdMainSquares.SmallSquareEdgeLength / 2));
        Vector2 smallSquareBottomRight = new Vector2(smallSquareCenter.x + (sdMainSquares.SmallSquareEdgeLength / 2), smallSquareCenter.y - (sdMainSquares.SmallSquareEdgeLength / 2));
        GameObject insideMobSpawnsContainer = Instantiate(emptyPrefab, smallSquareTopLeft, emptyPrefab.transform.rotation, dungeonContainer.transform);
        insideMobSpawnsContainer.name = "Small Square Spawns";
        for (float i = smallSquareTopLeft.y; i > smallSquareBottomRight.y; i -= 1f / spawnPointDensity)
        {
            for (float j = smallSquareTopLeft.x; j < smallSquareBottomRight.x; j += 1f / spawnPointDensity)
            {
                GameObject go = Instantiate(emptyPrefab, new Vector2(j, i), emptyPrefab.transform.rotation, insideMobSpawnsContainer.transform);
                MobSpawnpoints.Add(go);
            }
        }
    }
}
