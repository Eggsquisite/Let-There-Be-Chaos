using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPulse : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer heartSprite;
    [SerializeField]
    private AddForce pulseForce;
    [SerializeField]
    private float forceUpgrade;

    [Header ("Light Variables")]
    [SerializeField]
    private UnityEngine.Experimental.Rendering.Universal.Light2D pulseLight;
    [SerializeField]
    private float minLightIntensity;
    [SerializeField]
    private float maxLightIntensity;

    private float baseLightIntensity;

    [Header ("Pulse Variables")]
    [SerializeField]
    private Vector3 firstPulseSize;
    [SerializeField]
    private float pulseSpeed;
    [SerializeField]
    private float pulseCooldown;
    [SerializeField]
    private bool canPulse;

    private Vector3 basePulseSize;
    private Vector3 smoothedPulse;
    private Vector3 secondPulseSize;
    private GameObject heartGameobject;

    private int pulseIndex;
    
    // Start is called before the first frame update
    void Start()
    {
        if (heartSprite != null)
            heartGameobject = heartSprite.gameObject;

        baseLightIntensity = pulseLight.intensity;
        secondPulseSize = new Vector3(firstPulseSize.x - 0.5f, firstPulseSize.y - 0.5f);
        basePulseSize = new Vector3(heartGameobject.transform.localScale.x, heartGameobject.transform.localScale.y);
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

                    pulseLight.intensity = Mathf.Lerp(pulseLight.intensity, maxLightIntensity, pulseSpeed * Time.deltaTime);
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
                    smoothedPulse = Vector3.Lerp(heartGameobject.transform.localScale, basePulseSize, pulseSpeed * Time.deltaTime);
                    heartGameobject.transform.localScale = smoothedPulse;

                    pulseLight.intensity = Mathf.Lerp(pulseLight.intensity, baseLightIntensity, pulseSpeed * Time.deltaTime);
                    // set pulse back to original size
                    if (heartGameobject.transform.localScale.x <= basePulseSize.x + 0.1f)
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

    public void SetPulseSize(float newSize) {
        firstPulseSize = new Vector2(newSize, newSize);
        secondPulseSize = new Vector3(firstPulseSize.x - 0.5f, firstPulseSize.y - 0.5f);
    }

    private void SetPulseForce() {
        pulseForce.UpdateForce(forceUpgrade);
    }

    public void SetCanPulse(int flag) {
        if (flag == 0)
            canPulse = false;
        else if (flag == 1)
            canPulse = true;
    }
}
