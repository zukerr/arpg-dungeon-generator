using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SDBackground : MonoBehaviour
{
    private Transform startingPosition;
    private SDMainSquares sdMainSquares;
    private GameObject cameraBounds;

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    //you have to call Constructor method immidiately after adding this component
    public void Constructor(SDMainSquares sdMainSquares, Transform startingPosition, GameObject cameraBounds)
    {
        this.startingPosition = startingPosition;
        this.sdMainSquares = sdMainSquares;
        this.cameraBounds = cameraBounds;
    }

    public void AdjustBackground()
    {
        ResizeOutline();
        AdjustCameraBounds();
    }
    public void AdjustMinMaxForResize(Vector2 vector)
    {
        if (vector.x < minX)
        {
            minX = vector.x;
        }
        if (vector.x > maxX)
        {
            maxX = vector.x;
        }
        if (vector.y < minY)
        {
            minY = vector.y;
        }
        if (vector.y > maxY)
        {
            maxY = vector.y;
        }
    }
    public void SetupResizeBigSquare()
    {
        minX = startingPosition.position.x;
        maxX = minX;
        minY = startingPosition.position.y;
        maxY = minY;
    }

    private void ResizeOutline()
    {
        Debug.Log("minX: " + minX + " maxX: " + maxX);
        Debug.Log("minY: " + minY + " maxY: " + maxY);
        float width = maxX - minX;
        float height = maxY - minY;
        sdMainSquares.OutlineGO.transform.localScale = new Vector3(width, height, 1f);
        sdMainSquares.OutlineGO.transform.position = new Vector3(maxX - (width / 2f), maxY - (height / 2f), sdMainSquares.OutlineGO.transform.position.z);
    }
    private void AdjustCameraBounds()
    {
        cameraBounds.transform.position = sdMainSquares.OutlineGO.transform.position;
        cameraBounds.GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
        cameraBounds.transform.localScale = sdMainSquares.OutlineGO.transform.localScale;
    }
}
