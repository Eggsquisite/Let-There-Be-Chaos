using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private GameObject loveBlurbPrefabs;
    [SerializeField]
    private GameObject chaosBlurbPrefabs;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetLoveBlurb() {
        return loveBlurbPrefabs;
    }

    public GameObject GetChaosBlurb() {
        return chaosBlurbPrefabs;
    }
}
