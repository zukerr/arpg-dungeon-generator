using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static GameObject player;

    public bool IsAttacking { get; set; }

    public bool Shiftable { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject;
        IsAttacking = false;
        Shiftable = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
