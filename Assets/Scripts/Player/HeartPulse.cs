using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPulse : MonoBehaviour
{
    [Header ("Pulse Variables")]
    [SerializeField]
    private GameObject pulseGameobject;

    [SerializeField]
    private Vector3 introPulseSize;
    [SerializeField]
    private Vector3 firstPulseSize;
    [SerializeField]
    private float pulseSpeed;
    [SerializeField]
    private float pulseCooldown;
    [SerializeField]
    private float forceUpgrade;
    [SerializeField]
    private bool canPulse;

    private bool introPulse;

    private AddForce pulseForce;
    private Collider2D pulseCollider;
    private SpriteRenderer pulseSprite;

    private Vector3 basePulseSize;
    private Vector3 smoothedPulse;
    private Vector3 secondPulseSize;

    private bool isDead;
    private int pulseIndex;

    [Header ("Light Variables")]
    [SerializeField]
    private UnityEngine.Experimental.Rendering.Universal.Light2D heartLight;
    [SerializeField]
    private float introLightIntensity;
    [SerializeField]
    private float maxLightIntensity;

    private float baseLightIntensity;

    
    // Start is called before the first frame update
    void Start()
    {
        if (pulseGameobject != null) {
            pulseForce = pulseGameobject.GetComponent<AddForce>();
            pulseCollider = pulseGameobject.GetComponent<Collider2D>();
            pulseSprite = pulseGameobject.GetComponent<SpriteRenderer>();
        }

        if (heartLight != null)
            baseLightIntensity = heartLight.intensity;

        secondPulseSize = new Vector3(firstPulseSize.x - 0.5f, firstPulseSize.y - 0.5f);
        basePulseSize = new Vector3(pulseGameobject.transform.localScale.x, pulseGameobject.transform.localScale.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) { 
            smoothedPulse = Vector3.Lerp(pulseGameobject.transform.localScale, Vector2.zero, 0.75f * Time.deltaTime);
            pulseGameobject.transform.localScale = smoothedPulse;

            heartLight.intensity = Mathf.Lerp(heartLight.intensity, 0f, 0.5f * Time.deltaTime);
        } 
        else if (canPulse && introPulse) {
            SetPulseCollider(true);
            smoothedPulse = Vector3.Lerp(pulseGameobject.transform.localScale, introPulseSize, pulseSpeed * Time.deltaTime);
            pulseGameobject.transform.localScale = smoothedPulse;

            heartLight.intensity = Mathf.Lerp(heartLight.intensity, introLightIntensity, pulseSpeed * Time.deltaTime);
            if (pulseGameobject.transform.localScale.x >= (introPulseSize.x - 0.1f))
                introPulse = false;
        }
        else if (canPulse && !introPulse) {
            switch (pulseIndex)
            {
                case 1:
                    SetPulseCollider(true);
                    smoothedPulse = Vector3.Lerp(pulseGameobject.transform.localScale, firstPulseSize, pulseSpeed * Time.deltaTime);
                    pulseGameobject.transform.localScale = smoothedPulse;

                    heartLight.intensity = Mathf.Lerp(heartLight.intensity, maxLightIntensity, pulseSpeed * Time.deltaTime);
                    if (pulseGameobject.transform.localScale.x >= (firstPulseSize.x - 0.1f))
                        IncreasePulseIndex();
                    break;
                case 2:
                    smoothedPulse = Vector3.Lerp(pulseGameobject.transform.localScale, secondPulseSize, pulseSpeed * Time.deltaTime);
                    pulseGameobject.transform.localScale = smoothedPulse;

                    if (pulseGameobject.transform.localScale.x <= (secondPulseSize.x + 0.1f))
                        IncreasePulseIndex();
                    break;
                case 3:
                    smoothedPulse = Vector3.Lerp(pulseGameobject.transform.localScale, firstPulseSize, pulseSpeed * Time.deltaTime);
                    pulseGameobject.transform.localScale = smoothedPulse;

                    if (pulseGameobject.transform.localScale.x >= (firstPulseSize.x - 0.1f))
                        IncreasePulseIndex();
                    break;
                case 4:
                    smoothedPulse = Vector3.Lerp(pulseGameobject.transform.localScale, basePulseSize, pulseSpeed * Time.deltaTime);
                    pulseGameobject.transform.localScale = smoothedPulse;

                    heartLight.intensity = Mathf.Lerp(heartLight.intensity, baseLightIntensity, pulseSpeed * Time.deltaTime);
                    // set pulse back to original size
                    if (pulseGameobject.transform.localScale.x <= basePulseSize.x + 0.1f)
                        IncreasePulseIndex();
                    break;
                case 5:
                    if (canPulse) {
                        pulseIndex = 0;
                        SetCanPulse(0);
                        SetPulseCollider(false);
                        StartCoroutine(BeginPulseCooldown());
                    }
                    break;
                default:
                    IncreasePulseIndex();
                    break;
            }
        }
    }

    public void IntroPulse() {
        introPulse = true;
    }

    private void IncreasePulseIndex() {
        pulseIndex += 1;
    }

    IEnumerator BeginPulseCooldown() {
        yield return new WaitForSeconds(pulseCooldown);

        SetCanPulse(1);
    }

    public void SetPulseSize(float newSize) {
        firstPulseSize = new Vector2(newSize, newSize);
        secondPulseSize = new Vector3(firstPulseSize.x - 0.5f, firstPulseSize.y - 0.5f);
    }

    public void UpdatePulseForce(int index) {
        if (pulseForce != null) { 
            if (index > 0)
                pulseForce.UpdateForce(forceUpgrade);
            else if (index < 0)
                pulseForce.UpdateForce(-forceUpgrade);
        }
    }

    private void SetPulseCollider(bool flag) {
        if (pulseCollider != null) 
            pulseCollider.enabled = flag;
    }

    public void SetCanPulse(int flag) {
        // Called during intro animation event
        if (flag == 0)
            canPulse = false;
        else if (flag == 1)
            canPulse = true;
    }

    public void SetIsDead(bool flag) {
        isDead = flag;
    }

    /// <summary>
    /// WHEN GROWTH REACHES A CERTAIN VALUE, INCREASE RESPECTIVE VALUES
    /// </summary>
    public void UpgradeHeart() { 
        
    }
}
