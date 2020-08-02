using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : AAttackAbstract
{
    public GameObject weaponPrefab;

    [SerializeField]
    private float speed = 1f;

    [SerializeField]
    private float degree = 90f;

    private bool side = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    protected override IEnumerator ExecCoroutine()
    {
        Player.player.GetComponent<Player>().IsAttacking = true;

        //Spawn a weapon
        Vector3 fromPlayerToCursor = GlobalFunctions.globalFunctionsComponent.GetFromCursorToPlayerVector();

        float sign = Mathf.Sign(fromPlayerToCursor.y);

        //starting degree calculation
        float startingAngle = Vector3.Angle(Vector3.right, fromPlayerToCursor);
        float degreeOffset = side ? -(degree / 2f) : (degree / 2f);
        Quaternion startingRotation = Quaternion.Euler(0, 0, (sign * startingAngle) + degreeOffset);

        GameObject weapon = Instantiate(weaponPrefab, transform.position, startingRotation, transform);
        weapon.AddComponent<WSMelee>();

        float loopAngle = 0f;
        //Perform an animation
        while (loopAngle < degree)
        {
            float absMultiplier = speed * Time.deltaTime * 100f;
            float multiplier = side ? absMultiplier : -absMultiplier;
            weapon.transform.rotation = Quaternion.Euler(0, 0, weapon.transform.rotation.eulerAngles.z + multiplier);
            loopAngle += absMultiplier;
            yield return null;
        }

        Destroy(weapon);

        //internal cooldown
        yield return InternalCooldownCoroutine(0.5f);

        side = !side;
        Player.player.GetComponent<Player>().IsAttacking = false;
    }
}
