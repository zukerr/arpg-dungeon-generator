using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ControlKeyboard();
    }

    private void ControlKeyboard()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            Player.player.GetComponent<Melee>().Exec();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Player.player.GetComponent<SpiritualBlow>().Exec();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (Player.player.GetComponent<Player>().Shiftable)
                Player.player.GetComponent<PlayerMovement>().enabled = false;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (Player.player.GetComponent<Player>().Shiftable)
                Player.player.GetComponent<PlayerMovement>().enabled = true;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Player.player.GetComponent<Player>().Shiftable)
                Player.player.GetComponent<PlayerMovement>().enabled = false;
        }
    }

}
