using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPulse : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer heartSprite;
    [SerializeField]
    private float firstPulseMaxTime;
    [SerializeField]
    private float secondPulseMaxTime;

    private float firstPulseTimer;
    private float secondPulseTimer;

    private bool canPulse;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
