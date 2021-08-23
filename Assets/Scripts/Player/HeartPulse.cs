using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPulse : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer heartSprite;
    [SerializeField]
    private Vector3 firstPulseSize;
    [SerializeField]
    private float pulseSpeed;
    [SerializeField]
    private float pulseCooldown;
    [SerializeField]
    private bool canPulse;

    private Vector3 smoothedPulse;
    private Vector3 secondPulseSize;
    private GameObject heartGameobject;

    private int pulseIndex;

    private bool firstPulse;
    private bool secondPulse;
    
    // Start is called before the first frame update
    void Start()
    {
        if (heartSprite != null)
            heartGameobject = heartSprite.gameObject;

        secondPulseSize = new Vector3(firstPulseSize.x - 0.5f, firstPulseSize.y - 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (canPulse) {
            switch (pulseIndex)
            {
                case 1:
                    smoothedPulse = Vector3.Lerp(heartGameobject.transform.localScale, firstPulseSize, pulseSpeed * Time.deltaTime);
                    heartGameobject.transform.localScale = smoothedPulse;

                    if (heartGameobject.transform.localScale.x >= (firstPulseSize.x - 0.1f))
                        IncreasePulseIndex();
                    break;
                case 2:
                    smoothedPulse = Vector3.Lerp(heartGameobject.transform.localScale, secondPulseSize, pulseSpeed * Time.deltaTime);
                    heartGameobject.transform.localScale = smoothedPulse;

                    if (heartGameobject.transform.localScale.x <= (secondPulseSize.x + 0.1f))
                        IncreasePulseIndex();
                    break;
                case 3:
                    smoothedPulse = Vector3.Lerp(heartGameobject.transform.localScale, firstPulseSize, pulseSpeed * Time.deltaTime);
                    heartGameobject.transform.localScale = smoothedPulse;

                    if (heartGameobject.transform.localScale.x >= (firstPulseSize.x - 0.1f))
                        IncreasePulseIndex();
                    break;
                case 4:
                    smoothedPulse = Vector3.Lerp(heartGameobject.transform.localScale, Vector3.one, pulseSpeed * Time.deltaTime);
                    heartGameobject.transform.localScale = smoothedPulse;

                    // set pulse back to original size
                    if (heartGameobject.transform.localScale.x <= 1.1f)
                        IncreasePulseIndex();
                    break;
                case 5:
                    if (canPulse) {
                        pulseIndex = 0;
                        canPulse = false;
                        StartCoroutine(BeginPulseCooldown());
                    }
                    break;
                default:
                    IncreasePulseIndex();
                    break;
            }
        }
    }

    private void IncreasePulseIndex() {
        pulseIndex += 1;
    }

    IEnumerator BeginPulseCooldown() {
        yield return new WaitForSeconds(pulseCooldown);

        canPulse = true;
    }
}
