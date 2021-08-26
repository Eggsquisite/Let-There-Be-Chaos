using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaos : MonoBehaviour
{
    private SpawnText st;
    private Collider2D coll;

    private int growthTier;

    // Start is called before the first frame update
    void Start()
    {
        if (st == null) st = GetComponent<SpawnText>();

        RandomizeGrowthTier();
        UpdateSize();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Chaos") { 
            if (collision.gameObject.GetComponent<Chaos>().GetGrowthTier() < growthTier)
            {
                Destroy(collision.gameObject);
                if (growthTier < 5)
                    growthTier += 1;
                transform.localScale = new Vector3(transform.localScale.x + 0.5f, transform.localScale.y + 0.5f);
            } 
        }
    }

    private void RandomizeGrowthTier() {
        growthTier = Random.Range(1, 6);
    }

    private void UpdateSize() {
        switch (growthTier)
        {
            case 0:
                Destroy(gameObject);
                break;
            case 1:
                transform.localScale = new Vector2(1f, 1f);
                break;
            case 2:
                transform.localScale = new Vector2(1.5f, 1.5f);
                break;
            case 3:
                transform.localScale = new Vector2(2f, 2f);
                break;
            case 4:
                transform.localScale = new Vector2(2.5f, 2.5f);
                break;
            case 5:
                transform.localScale = new Vector2(3f, 3f);
                break;
            default:
                break;
        }
    }

    public void SpawnText() {
        if (st != null)
            st.SpawnBlurb(false);
    }

    // GETTERS/SETTERS ////////////////////////////////////////////////////////////////////////
    public int GetGrowthTier() {
        return growthTier;
    }

    public void SetGrowthTier(int newTier) {
        growthTier = newTier;
        UpdateSize();
    }
}
