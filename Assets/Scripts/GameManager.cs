using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private List<GameObject> loveBlurbPrefabs;
    [SerializeField]
    private List<GameObject> chaosBlurbPrefabs;

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
        return loveBlurbPrefabs[Random.Range(0, loveBlurbPrefabs.Count - 1)];
    }
}
