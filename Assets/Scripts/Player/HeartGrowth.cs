using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartGrowth : MonoBehaviour
{
    private int growthIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseGrowth() {
        growthIndex += 1;
        Debug.Log("Growth: " + growthIndex);
    }
}
