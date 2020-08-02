using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AWeaponScript : MonoBehaviour
{
    protected float damage = 10f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    protected abstract void CalculateDamage();

    private void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.GetComponent<Health>() != null) && (col.GetComponent<Player>() == null))
        {
            CalculateDamage();
            col.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
