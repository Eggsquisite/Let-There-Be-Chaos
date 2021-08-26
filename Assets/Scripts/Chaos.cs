using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaos : MonoBehaviour
{
    private SpawnText st;
    private Collider2D coll;

    [SerializeField]
    private int growthTier;

    // Start is called before the first frame update
    void Start()
    {
        if (st == null) st = GetComponent<SpawnText>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Chaos") { 
            Destroy(collision.gameObject);
            transform.localScale = new Vector3(4f, 4f);
        }
    }

    private void UpdateSize() { 
        
    }

    public void SpawnText() {
        st.SpawnBlurb(false);
    }

    // GETTERS/SETTERS ////////////////////////////////////////////////////////////////////////
    public int GetGrowthTier() {
        return growthTier;
    }
}
