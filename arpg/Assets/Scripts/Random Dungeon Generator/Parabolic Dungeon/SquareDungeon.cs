using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SquareDungeon : MonoBehaviour
{
    [SerializeField]
    private GameObject squareSpritePrefab = null;
    [SerializeField]
    private GameObject dungeonContainer = null;
    [SerializeField]
    private Transform startingPosition = null;
    [SerializeField]
    private GameObject pointTestPrefab = null;
    [SerializeField]
    private GameObject edgePrefab = null;
    [SerializeField]
    private GameObject emptyPrefab = null;
    [SerializeField]
    private GameObject cameraBounds = null;

    [SerializeField]
    private float minEdgeLength = 50f;
    [SerializeField]
    private float maxEdgeLength = 70f;
    [SerializeField]
    private float minFrameWidthPercentage = 0.1f;
    [SerializeField]
    private float maxFrameWidthPercentage = 0.15f;
    [SerializeField]
    private int edgeSegmentsAmount = 5;
    [SerializeField]
    private float paraboleVertexFactor = 1.5f;
    [SerializeField]
    private float parabolePointFrequency = 0.1f;
    [SerializeField]
    private float spawnPointDensity = 0.1f;

    private SDMainSquares sdMainSquares;
    private SDSegmentManagement sdSegmentManagement;
    private SDEdges sdEdges;
    private SDParaboles sdParaboles;
    private SDSpawnPoints sdSpawnPoints;
    private SDBackground sdBackground;
    private MobSpawner mobSpawner;

    // Start is called before the first frame update
    void Start()
    {
        Setup();
        GenerateDungeon();
    }

    private void Setup()
    {
        sdMainSquares = gameObject.AddComponent<SDMainSquares>();
        sdMainSquares.Constructor(startingPosition, minEdgeLength, maxEdgeLength,
                                            minFrameWidthPercentage, maxFrameWidthPercentage,
                                            squareSpritePrefab, dungeonContainer);
        sdSegmentManagement = gameObject.AddComponent<SDSegmentManagement>();
        sdSegmentManagement.Constructor(sdMainSquares, edgeSegmentsAmount, pointTestPrefab, dungeonContainer);
        sdEdges = gameObject.AddComponent<SDEdges>();
        sdEdges.Constructor(sdSegmentManagement, dungeonContainer, edgePrefab);
        sdSpawnPoints = gameObject.AddComponent<SDSpawnPoints>();
        sdSpawnPoints.Constructor(sdMainSquares, emptyPrefab, spawnPointDensity, dungeonContainer);
        sdBackground = gameObject.AddComponent<SDBackground>();
        sdBackground.Constructor(sdMainSquares, startingPosition, cameraBounds);
        sdParaboles = gameObject.AddComponent<SDParaboles>();
        sdParaboles.Constructor(sdEdges, sdSpawnPoints, sdBackground, dungeonContainer, emptyPrefab, parabolePointFrequency, paraboleVertexFactor);
        mobSpawner = gameObject.GetComponent<MobSpawner>();
    }

    private void GenerateDungeon()
    {
        sdBackground.SetupResizeBigSquare();
        sdMainSquares.GenerateMainSquares();
        sdSegmentManagement.GenerateSegments();
        sdEdges.CreateEdges();
        sdParaboles.CreateParabolesOnEdges();
        sdBackground.AdjustBackground();
        sdSpawnPoints.CreateMobSpawnpointsInside();
        mobSpawner.Setup(sdSpawnPoints.MobSpawnpoints);
        mobSpawner.SpawnXMobsInRandomLocations(10);
    } 
}
