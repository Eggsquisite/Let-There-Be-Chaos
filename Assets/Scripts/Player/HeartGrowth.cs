using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartGrowth : MonoBehaviour
{
    private HeartPulse hp;

    [Header("Growth Variables")]
    [SerializeField]
    private float growthSpeed;
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


    private float baseGrowth;
    private float growthUpdate;

    // Start is called before the first frame update
    void Start()
    {
        if (hp == null) hp = GetComponent<HeartPulse>();
        baseGrowth = 1f;
        growthUpdate = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (baseGrowth != growthUpdate) {
            transform.localScale = new Vector2(baseGrowth, baseGrowth);
            baseGrowth = Mathf.Lerp(baseGrowth, growthUpdate, growthSpeed * Time.deltaTime);
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
}
