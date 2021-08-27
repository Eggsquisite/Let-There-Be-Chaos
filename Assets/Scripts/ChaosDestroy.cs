using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosDestroy : MonoBehaviour
{
    private void Start()
    {
        Invoke("DestroySelf", 5f);
    }

    private void DestroySelf() {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Chaos")
            Destroy(collision.gameObject);
    }
}
