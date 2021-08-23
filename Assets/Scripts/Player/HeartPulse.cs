using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPulse : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer heartSprite;
    [SerializeField]
    private Vector3 pulseSize;
    [SerializeField]
    private float firstPulseMaxTime;
    [SerializeField]
    private float secondPulseMaxTime;

    private GameObject heartGameobject;

    private float firstPulseTimer;
    private float secondPulseTimer;

    private bool canPulse;
    
    // Start is called before the first frame update
    void Start()
    {
        if (heartSprite != null)
            heartGameobject = heartSprite.gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 smoothedTransition = Vector3.Lerp(heartGameobject.transform.localScale, pulseSize, 0.5f * Time.deltaTime);
        heartGameobject.transform.localScale = smoothedTransition;

        if (heartGameobject.transform.localScale == pulseSize) {
            Debug.Log("Max sized reach");

        }
    }
}
