using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MobSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject mob = null;
    [SerializeField]
    private GameObject dungeonContainer = null;
    [SerializeField]
    private GameObject emptyPrefab = null;

    private List<GameObject> mobSpawnPoints;
    private GameObject mobContainer;

    public void Setup(List<GameObject> mobSpawnPoints)
    {
        this.mobSpawnPoints = mobSpawnPoints;
        SetupMobContainer();
    }
    public void SpawnXMobsInRandomLocations(int x)
    {
        int rngLocation;
        for (int i = 0; i < x; i++)
        {
            rngLocation = Random.Range(0, mobSpawnPoints.Count());
            SpawnMob(mobSpawnPoints.ElementAt(rngLocation).transform);
        }
    }
    private void SetupMobContainer()
    {
        mobContainer = Instantiate(emptyPrefab, dungeonContainer.transform.position, dungeonContainer.transform.rotation, dungeonContainer.transform);
        mobContainer.name = "MobContainer";
    }
    private void SpawnMob(Transform location)
    {
        Instantiate(mob, location.position, location.rotation, mobContainer.transform);
    }
}
