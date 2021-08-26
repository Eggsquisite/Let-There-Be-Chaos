using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartGrowth : MonoBehaviour
{
    private HeartPulse hp;
    private PlayerMovement pm;
    [SerializeField]
    private SpriteRenderer sp;

    [Header("Heart Darken Variables")]
    [SerializeField]
    private float transitionSpeed;

    private bool isDead;
    private int darkenIndex;

    private Color baseColor;
    private Color transitionColor;

    [SerializeField]
    private UnityEngine.Experimental.Rendering.Universal.Light2D heartLight;

    [Header("Growth Variables")]
    [SerializeField]
    private float growthSpeed;
    [SerializeField]
    private float minGrowthSize;
    [SerializeField]
    private float maxGrowthSize;
    [SerializeField]
    private float growthTierOne;
    [SerializeField]
    private float growthTierTwo;
    [SerializeField]
    private float growthTierThree;
    [SerializeField]
    private float growthTierFour;
    [SerializeField]
    private float growthTierFive;

    [Header("Threshold Variables")]
    [SerializeField]
    private float thresholdOne;
    [SerializeField]
    private float thresholdTwo;
    [SerializeField]
    private float thresholdThree;
    [SerializeField]
    private float thresholdFour;
    [SerializeField]
    private float thresholdFive;

    private int thresholdIndex;
    private float baseGrowth;
    private float growthUpdate;

    // Start is called before the first frame update
    void Start()
    {
        if (hp == null) hp = GetComponent<HeartPulse>();
        if (pm == null) pm = GetComponent<PlayerMovement>();

        baseGrowth = 1f;
        growthUpdate = 1f;

        baseColor = transitionColor = sp.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (baseGrowth != growthUpdate) {
            if (growthUpdate > maxGrowthSize) { 
                growthUpdate = maxGrowthSize;
            } else if (growthUpdate < minGrowthSize) {
                growthUpdate = minGrowthSize;
            }

            transform.localScale = new Vector2(baseGrowth, baseGrowth);
            baseGrowth = Mathf.Lerp(baseGrowth, growthUpdate, growthSpeed * Time.deltaTime);
        }

        if (baseColor != transitionColor) {
            sp.color = baseColor;
            
            if (!isDead)
                baseColor = Color.Lerp(baseColor, transitionColor, transitionSpeed * Time.deltaTime);
            else 
                baseColor = Color.Lerp(baseColor, transitionColor, 0.5f * Time.deltaTime);
        } 

        CalculateThreshold();
    }

    private void CalculateThreshold() { 
        if (baseGrowth > minGrowthSize && baseGrowth <= thresholdOne && thresholdIndex != 1) {
            if (thresholdIndex > 1) {
                UpdateThreshold(-1, 1);
            }

            thresholdIndex = 1;
        } 
        else if (baseGrowth > thresholdOne && baseGrowth <= thresholdTwo && thresholdIndex != 2)
        {
            if (thresholdIndex > 2) {
                UpdateThreshold(-1, 2);
            }
            else {
                UpdateThreshold(1, 2);
            }

            thresholdIndex = 2;
        }
        else if (baseGrowth > thresholdTwo && baseGrowth <= thresholdThree && thresholdIndex != 3)
        {
            if (thresholdIndex > 3) {
                UpdateThreshold(-1, 3);
            }
            else {
                UpdateThreshold(1, 3);
            }

            thresholdIndex = 3;
        }
        else if (baseGrowth > thresholdThree && baseGrowth <= thresholdFour && thresholdIndex != 4)
        {
            if (thresholdIndex > 4) {
                UpdateThreshold(-1, 4);
                heartLight.pointLightOuterRadius -= growthTierFour;
            }
            else {
                UpdateThreshold(1, 5);
            }

            thresholdIndex = 4;
        }
        else if (baseGrowth > thresholdFour && baseGrowth <= thresholdFive && thresholdIndex != 5)
        {
            UpdateThreshold(1, 5);

            thresholdIndex = 5;
        }
    }

    /// <summary>
    /// Takes 5 hits of chaos at smallest growth for heart to break
    /// </summary>
    public void DarkenHeart() {
        if (darkenIndex <= 0) { 
            transitionColor = new Color(transitionColor.r - 0.8f, transitionColor.g - 0.8f, transitionColor.b - 0.8f);
            darkenIndex = 1;
        } 
        else if (darkenIndex > 0 && darkenIndex < 4) {
            transitionColor = new Color(transitionColor.r - 0.05f, transitionColor.g - 0.05f, transitionColor.b - 0.05f);
            darkenIndex++;
        }
        else if (darkenIndex >= 4) {
            transitionColor = new Color(transitionColor.r - 0.05f, transitionColor.g - 0.05f, transitionColor.b - 0.05f, 0f);
            PlayerDead();
        }
    }

    private void PlayerDead() {
        isDead = true;
        pm.SetMoveSpeed(0);
        hp.SetIsDead(true);
        Debug.Log("Player dead");
    }

    public void IncreaseGrowth(int growthTier) {
        switch (growthTier) {
            case 1:
                growthUpdate += growthTierOne;
                break;
            case 2:
                growthUpdate += growthTierTwo;
                break;
            case 3:
                growthUpdate += growthTierThree;
                break;
            case 4:
                growthUpdate += growthTierFour;
                break;
            case 5:
                growthUpdate += growthTierFive;
                break;
            default:
                break;
        }
    }

    public void DecreaseGrowth(int growthTier) {
        switch (growthTier) {
            case 1:
                growthUpdate -= growthTierOne;
                break;
            case 2:
                growthUpdate -= growthTierTwo;
                break;
            case 3:
                growthUpdate -= growthTierThree;
                break;
            case 4:
                growthUpdate -= growthTierFour;
                break;
            case 5:
                growthUpdate -= growthTierFive;
                break;

            default:
                break;
        }
    }

    private void UpdateThreshold(int flag, int growthIndex) {
        pm.UpdateMoveSpeed(flag);
        hp.UpdatePulseForce(flag);

        if (flag < 0) {
            if (growthIndex == 1)
                heartLight.pointLightOuterRadius -= growthTierOne * 2;
            else if (growthIndex == 2)
                heartLight.pointLightOuterRadius -= growthTierTwo * 2;
            else if (growthIndex == 3)
                heartLight.pointLightOuterRadius -= growthTierThree * 2;
            else if (growthIndex == 4)
                heartLight.pointLightOuterRadius -= growthTierFour * 2;
            else if (growthIndex == 5)
                heartLight.pointLightOuterRadius -= growthTierFive * 2;
        } 
        else if (flag > 0) {
            if (growthIndex == 1)
                heartLight.pointLightOuterRadius += growthTierOne * 2;
            else if (growthIndex == 2)
                heartLight.pointLightOuterRadius += growthTierTwo * 2;
            else if (growthIndex == 3)
                heartLight.pointLightOuterRadius += growthTierThree * 2;
            else if (growthIndex == 4)
                heartLight.pointLightOuterRadius += growthTierFour * 2;
            else if (growthIndex == 5)
                heartLight.pointLightOuterRadius += growthTierFive * 2;
        }
    }

    public int GetGrowthThreshold() {
        return thresholdIndex;
    }
}
