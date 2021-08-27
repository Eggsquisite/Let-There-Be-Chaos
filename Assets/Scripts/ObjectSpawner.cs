using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public static ObjectSpawner instance;

    [Header("Chaos variables")]
    [SerializeField]
    private Transform chaosParent;
    [SerializeField]
    private float numberOfChaosObjectsToSpawn;
    [SerializeField]
    private List<GameObject> chaosObjects;

    [Header("Heart variables")]
    [SerializeField]
    private Transform heartParent;
    [SerializeField]
    private List<GameObject> heartObjects;
    [SerializeField]
    private int numberOfHeartsToSpawn;

    [Header("Spread variables")]
    [SerializeField]
    private float objectXSpread;
    [SerializeField]
    private float objectYSpread;

    private bool canSpawn;
    private Vector3 randPosition;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void SpawnObjects() { 
            // Spawn heart objects
        for (int i = 0; i < numberOfHeartsToSpawn; i++) {
            SpreadObjects(true, i);
        }

        for (int i = 0; i < numberOfChaosObjectsToSpawn; i++) {
            SpreadObjects(false, 0);
        }

        canSpawn = true;
        StartCoroutine(SpawnChaos());
    }

    private IEnumerator SpawnChaos() { 
        while (canSpawn)
        {
            yield return new WaitForSeconds(5f);
            randPosition = new Vector3(Random.Range(-objectXSpread, objectXSpread), Random.Range(-objectYSpread, objectYSpread));
            Instantiate(chaosObjects[Random.Range(0, chaosObjects.Count)], randPosition, Quaternion.identity, chaosParent);
        }
    }

    /// <summary>
    /// If flag is true, spawn heart objects, else is flag is false, spawn chaos objects
    /// </summary>
    /// <param name="index"></param>
    private void SpreadObjects(bool flag, int index) {
        randPosition = new Vector3(Random.Range(-objectXSpread, objectXSpread), Random.Range(-objectYSpread, objectYSpread));
        if (flag)
        {
            Instantiate(heartObjects[Random.Range(0, heartObjects.Count)], randPosition, Quaternion.identity, heartParent);
        } else
        {
            Instantiate(chaosObjects[Random.Range(0, chaosObjects.Count)], randPosition, Quaternion.identity, chaosParent);
        }
    }

    public void TeleportObject(Transform objectTransform) {
        randPosition = new Vector3(Random.Range(-objectXSpread, objectXSpread), Random.Range(-objectYSpread, objectYSpread));

        objectTransform.position = randPosition;
    }
}
