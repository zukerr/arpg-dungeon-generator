using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalFunctions : MonoBehaviour
{
    public static GlobalFunctions globalFunctionsComponent;

    // Start is called before the first frame update
    void Start()
    {
        globalFunctionsComponent = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 GetFromCursorToPlayerVector()
    {
        Vector3 temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mouseWorldPosition = new Vector3(temp.x, temp.y, 0f);
        Vector3 fromPlayerToCursor = new Vector3(mouseWorldPosition.x - Player.player.transform.position.x,
                                                    mouseWorldPosition.y - Player.player.transform.position.y,
                                                    mouseWorldPosition.z - Player.player.transform.position.z);
        return fromPlayerToCursor;
    }
}
