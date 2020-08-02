using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SDMainSquares : MonoBehaviour
{
    //what we need to get from SquareDungeon
    private Transform startingPosition;
    private float minEdgeLength;
    private float maxEdgeLength;
    private float minFrameWidthPercentage;
    private float maxFrameWidthPercentage;
    private GameObject squareSpritePrefab;
    private GameObject dungeonContainer;

    //what is counted in this class
    public float EdgeLength { get; private set; }
    public float FrameWidth { get; private set; }
    public float SmallSquareEdgeLength { get; private set; }

    //what is counted in this class but is needed elsewhere
    public GameObject OutlineGO { get; private set; }
    public GameObject InsideGO { get; private set; }
    public Transform Center { get; private set; }

    //you have to call Constructor method immidiately after adding this component
    public void Constructor(Transform startingPosition, float minEdgeLength, float maxEdgeLength, 
                        float minFrameWidthPercentage, float maxFrameWidthPercentage, 
                        GameObject squareSpritePrefab, GameObject dungeonContainer)
    {
        this.startingPosition = startingPosition;
        this.minEdgeLength = minEdgeLength;
        this.maxEdgeLength = maxEdgeLength;
        this.minFrameWidthPercentage = minFrameWidthPercentage;
        this.maxFrameWidthPercentage = maxFrameWidthPercentage;
        this.squareSpritePrefab = squareSpritePrefab;
        this.dungeonContainer = dungeonContainer;
    }

    public void GenerateMainSquares()
    {
        GenerateEdgeLength();
        CreateOutline();
        GenerateFrameWidth();
        CreateInside();
    }
    private void GenerateEdgeLength()
    {
        EdgeLength = Random.Range(minEdgeLength, maxEdgeLength);
    }
    private void CreateOutline()
    {
        OutlineGO = Instantiate(squareSpritePrefab, startingPosition.position, dungeonContainer.transform.rotation, dungeonContainer.transform);
        OutlineGO.transform.localScale = new Vector3(EdgeLength, EdgeLength, 1f);
        Center = OutlineGO.transform;
    }
    private void GenerateFrameWidth()
    {
        float frameWidthPercentage = Random.Range(minFrameWidthPercentage, maxFrameWidthPercentage);
        FrameWidth = frameWidthPercentage * EdgeLength;
    }
    private void CreateInside()
    {
        SmallSquareEdgeLength = EdgeLength - (2 * FrameWidth);
        InsideGO = Instantiate(squareSpritePrefab, startingPosition.position, dungeonContainer.transform.rotation, dungeonContainer.transform);
        InsideGO.transform.localScale = new Vector3(SmallSquareEdgeLength, SmallSquareEdgeLength, 1f);
        InsideGO.GetComponent<SpriteRenderer>().color = Color.grey;
    }
}
