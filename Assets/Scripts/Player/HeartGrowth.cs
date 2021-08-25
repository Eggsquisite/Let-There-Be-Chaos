using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartGrowth : MonoBehaviour
{
    private HeartPulse hp;

    [SerializeField]
    private float growthSpeed;
    [SerializeField]
    private float minGrowthValue;
    [SerializeField]
    private float maxGrowthValue;

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
            Debug.Log("Growing");
            transform.localScale = new Vector2(baseGrowth, baseGrowth);
            baseGrowth = Mathf.Lerp(baseGrowth, growthUpdate, growthSpeed * Time.deltaTime);
        }
    }

    public void IncreaseGrowth(float newValue) {
        if (growthUpdate + newValue <= maxGrowthValue)
            growthUpdate += newValue;
        else if (growthUpdate + newValue > maxGrowthValue)
            growthUpdate = maxGrowthValue;
    }

    public void DecreaseGrowth(float newValue) {
        if (growthUpdate - newValue >= minGrowthValue)
            growthUpdate -= newValue;
        else if (growthUpdate - newValue < minGrowthValue)
            growthUpdate = minGrowthValue;
    }
}
