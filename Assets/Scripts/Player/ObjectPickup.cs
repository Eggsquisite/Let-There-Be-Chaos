using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickup : MonoBehaviour
{
    private HeartGrowth hg;
    private List<HeartFollow> heartFollow = new List<HeartFollow>();

    private void Start()
    {
        if (hg == null) hg = GetComponent<HeartGrowth>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Hearts" && collision.gameObject.GetComponent<HeartFollow>().GetPickupReady())
        {
            var tmp = collision.gameObject.GetComponent<HeartFollow>();
            tmp.OnPickup(transform);
            IncreaseGrowth(tmp.GetGrowthTier());
            heartFollow.Add(tmp);

            // CODE FOR CHANGING MATERIAL COLOR INTENSITY
            //tmpMaterial.SetVector("_Color", Color.white * colorIntensity);

            //factor = Mathf.Pow(2, intensity);
            //var color = new Color(tmpSprite.color.r * factor, tmpSprite.color.g * factor, tmpSprite.color.b * factor);
        } 
        else if (collision.gameObject.tag == "Chaos")
        {
            var tmp = collision.gameObject.GetComponent<Chaos>();

            tmp.SpawnText();
            DecreaseGrowth(tmp.GetGrowthTier());
            Destroy(tmp.gameObject);
            // play a destruction sound
        }
    }

    private void IncreaseGrowth(int growthTier) {
        hg.IncreaseGrowth(growthTier);
    }
    public void DecreaseGrowth(int growthTier) {
        hg.DecreaseGrowth(growthTier);
    }
}
