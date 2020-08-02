using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Dont make instances of this behaviour
public abstract class AAttackAbstract : MonoBehaviour
{
    protected bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected abstract IEnumerator ExecCoroutine();

    public void Exec()
    {
        if (!Player.player.GetComponent<Player>().IsAttacking)
        {
            StartCoroutine(ExecCoroutine());
        }
    }

    protected IEnumerator InternalCooldownCoroutine(float value)
    {
        float time = 0f;
        while (time < value)
        {
            time += Time.deltaTime;
            yield return null;
        }
    }
}
