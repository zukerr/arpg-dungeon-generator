using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField]
    private Image healthImage = null;

    [SerializeField]
    private float maxHealth = 100f;

    private float health = 100f;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        if(healthImage == null)
        {
            Debug.LogError("healthImage reference in " + gameObject.name + " object on Health script needs to be assigned!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
        healthImage.fillAmount = health / maxHealth;
    }

    public void Restore(float value)
    {
        health += value;
        healthImage.fillAmount = health / maxHealth;
    }

    private void Die()
    {
        //do something
        if(GetComponent<Player>() == null)
        {
            Destroy(gameObject);
        }
    }
}
