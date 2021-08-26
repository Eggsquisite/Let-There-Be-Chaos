using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnText : MonoBehaviour
{
    [SerializeField]
    private Transform spawnLocation;

    public void SpawnBlurb(bool flag)
    { 
        if (flag)
            Instantiate(GameManager.instance.GetLoveBlurb(), spawnLocation.position, Quaternion.identity);
        else {
            Debug.Log("Spawning chaos blurb");
            Instantiate(GameManager.instance.GetChaosBlurb(), spawnLocation.position, Quaternion.identity);
        }
    }
}
