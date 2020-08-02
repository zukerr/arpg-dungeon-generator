using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritualBlow : AAttackAbstract
{
    public GameObject weaponPrefab;

    [SerializeField]
    private float speed = 1f;

    [SerializeField]
    private float degree = 220f;

    [SerializeField]
    private float chargingTime = 1f;


    protected override IEnumerator ExecCoroutine()
    {
        Player.player.GetComponent<Player>().IsAttacking = true;

        //Spawn a weapon
        Vector3 fromPlayerToCursor = GlobalFunctions.globalFunctionsComponent.GetFromCursorToPlayerVector();

        float sign = Mathf.Sign(fromPlayerToCursor.y);

        //starting degree calculation
        float startingAngle = Vector3.Angle(Vector3.right, fromPlayerToCursor);
        float degreeOffset = degree/2f;
        Quaternion startingRotation = Quaternion.Euler(0, 0, (sign * startingAngle) + degreeOffset);

        GameObject weapon = Instantiate(weaponPrefab, transform.position, startingRotation, transform);
        weapon.AddComponent<WSSpiritualBlow>();

        //disable movement while charging up
        Player.player.GetComponent<Player>().Shiftable = false;
        Player.player.GetComponent<PlayerMovement>().enabled = false;

        //charging up
        yield return InternalCooldownCoroutine(chargingTime);

        //enable movement back
        Player.player.GetComponent<PlayerMovement>().enabled = true;
        Player.player.GetComponent<Player>().Shiftable = true;

        //set starting weapon length
        float startingWeaponLength = weapon.transform.localScale.x;
        float weaponSizeIncreaseFactor;

        float loopAngle = 0f;
        //Perform an animation
        while (loopAngle < degree)
        {
            float absMultiplier = speed * Time.deltaTime * 100f;
            weapon.transform.rotation = Quaternion.Euler(0, 0, weapon.transform.rotation.eulerAngles.z - absMultiplier);
            //increase the size of the weapon
            if(loopAngle < degree / 2)
            {
                weaponSizeIncreaseFactor = (loopAngle / degree);
            }
            else
            {
                weaponSizeIncreaseFactor = ((degree - loopAngle) / degree);
            }
            weapon.transform.localScale = new Vector3(startingWeaponLength * (1 + weaponSizeIncreaseFactor), weapon.transform.localScale.y, weapon.transform.localScale.z);
            loopAngle += absMultiplier;
            yield return null;
        }

        Destroy(weapon);

        //internal cooldown
        yield return InternalCooldownCoroutine(0.5f);

        Player.player.GetComponent<Player>().IsAttacking = false;
    }
}
