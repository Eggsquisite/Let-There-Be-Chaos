using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartGrowth : MonoBehaviour
{
    private HeartPulse hp;
    private PlayerMovement pm;

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

        if (baseGrowth > minGrowthSize && baseGrowth <= thresholdOne && thresholdIndex != 1) {
            if (thresholdIndex > 1) {
                UpdateThreshold(-1);
            }

            thresholdIndex = 1;
        } 
        else if (baseGrowth > thresholdOne && baseGrowth <= thresholdTwo && thresholdIndex != 2)
        {
            if (thresholdIndex > 2) {
                UpdateThreshold(-1);
            }
            else {
                UpdateThreshold(1);
            }

            thresholdIndex = 2;
        }
        else if (baseGrowth > thresholdOne && baseGrowth <= thresholdThree && thresholdIndex != 3)
        {
            if (thresholdIndex > 3) {
                UpdateThreshold(-1);
            }
            else {
                UpdateThreshold(1);
            }

            thresholdIndex = 3;
        }
        else if (baseGrowth > thresholdOne && baseGrowth <= thresholdFour && thresholdIndex != 4)
        {
            if (thresholdIndex > 4) {
                UpdateThreshold(-1);
            }
            else {
                UpdateThreshold(1);
            }

            thresholdIndex = 4;
        }
        else if (baseGrowth > thresholdOne && baseGrowth <= thresholdFive && thresholdIndex != 5)
        {
            UpdateThreshold(1);

            thresholdIndex = 5;
        }
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

/*        if (growthUpdate + newValue <= maxGrowthValue)
            growthUpdate += newValue;
        else if (growthUpdate + newValue > maxGrowthValue)
            growthUpdate = maxGrowthValue;*/
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

        /*        if (growthUpdate - newValue >= minGrowthValue)
                    growthUpdate -= newValue;
                else if (growthUpdate - newValue < minGrowthValue)
                    growthUpdate = minGrowthValue;*/
    }

    private void UpdateThreshold(int flag) {
        pm.UpdateMoveSpeed(flag);
        hp.UpdatePulseForce(flag);
    }
}
