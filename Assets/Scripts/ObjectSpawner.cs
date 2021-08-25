using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
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

    [Header("Spread variables")]
    [SerializeField]
    private float objectXSpread;
    [SerializeField]
    private float objectYSpread;

    private Vector3 randPosition;


    // Start is called before the first frame update
    void Start()
    {
        // Spawn heart objects
        for (int i = 0; i < heartObjects.Count; i++) {
            SpreadObjects(true, i);
        }

        for (int i = 0; i < numberOfChaosObjectsToSpawn; i++) {
            SpreadObjects(false, 0);
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
            Instantiate(heartObjects[index], randPosition, Quaternion.identity, heartParent);
        } else
        {
            Instantiate(chaosObjects[Random.Range(0, chaosObjects.Count - 1)], randPosition, Quaternion.identity, chaosParent);
        }

    }
}
