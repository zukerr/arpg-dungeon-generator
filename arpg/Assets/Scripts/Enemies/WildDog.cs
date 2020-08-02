using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WildDog : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 1f;
    [SerializeField]
    private float dashStartRange = 3f;
    [SerializeField]
    private float dashLength = 5f;
    [SerializeField]
    private float dashCastTime = 1f;
    [SerializeField]
    private float dashingSpeed = 10f;
    [SerializeField]
    private float damage = 10f;
    [SerializeField]
    private float aggroRange = 15f;
    
    private bool dashing = false;

    private Rigidbody2D rbody;

	// Use this for initialization
	void Start ()
    {
        rbody = GetComponent<Rigidbody2D>();
        dashLength = Random.Range(dashLength - 0.5f, dashLength + 0.5f);
	}
	
	// Update is called once per frame
	void Update ()
    {
        FollowPlayerIfInRange();
	}

    private void FollowPlayerIfInRange()
    {
        Vector2 position2d = new Vector2(transform.position.x, transform.position.y);
        Vector2 player = Player.player.transform.position;
        if(Vector2.Distance(position2d, player) < aggroRange)
        {
            FollowPlayer();
        }
    }

    //As long as the distance between player and this wild dog is greater than dashStartRange, follow the player
    //If the distance is less, then start casting dash
    private void FollowPlayer()
    {
        Vector2 position2d = new Vector2(transform.position.x, transform.position.y);
        Vector2 player = Player.player.transform.position;
        Vector2 movementVector = new Vector2(transform.position.x - player.x, transform.position.y - player.y);
        movementVector = -movementVector;
        if ((Vector2.Distance(position2d, player) > dashStartRange)&&(!dashing))
        {
            //Follow player
            rbody.MovePosition(rbody.position + movementVector.normalized * Time.deltaTime * movementSpeed);
        }
        else if(!dashing)
        {
            //Start casting dash
            //Debug.Log("Dashing! " + movementVector);
            StartCoroutine(Dash(movementVector));
        }
    }

    private IEnumerator Dash(Vector2 playerDirection)
    {
        dashing = true;
        float time = 0;
        //Debug.Log("Starting casting dash");
        while(time < dashCastTime)
        {
            yield return null;
            time += Time.deltaTime;
        }
        //Debug.Log("Finished casting dash");
        //setup
        playerDirection.Normalize();

        //algorithm
        float distanceMade = 0f;
        Vector2 startingPosition = transform.position;
        //Debug.Log("Starting dash loop");
        while(distanceMade < dashLength)
        {
            
            rbody.MovePosition(rbody.position + playerDirection * Time.deltaTime * dashingSpeed);
            distanceMade = Vector2.Distance(transform.position, startingPosition);
            yield return null;
        }
        //Debug.Log("Finished dash loop");
        dashing = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.GetComponent<Player>() != null)
        {
            Health p = col.gameObject.GetComponent<Health>();
            p.TakeDamage(damage);
        }
    }
}
