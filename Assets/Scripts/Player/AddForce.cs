using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForce : MonoBehaviour
{
    [SerializeField]
    private float forceAdded;

    private bool isPlayer;

    private void Start()
    {
        if (transform.parent.tag == "Player")
            isPlayer = true;
    }

    public void UpdateForce(float addedValue) {
        forceAdded += addedValue;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Chaos")
        {
            Debug.Log("Adding force to chaos");
            Vector3 tmpDir = (collision.transform.position - transform.position).normalized;
            collision.GetComponent<Rigidbody2D>().AddForce(tmpDir * forceAdded);

        } else if (collision.tag == "Hearts" && isPlayer)
        {
            // Hearts caught in the pulse are attracted and will come towards the main heart
            Debug.Log("Adding force to hearts");
            Vector3 tmpDir = (transform.position - collision.transform.position).normalized;
            collision.GetComponent<Rigidbody2D>().AddForce(tmpDir * forceAdded);
        }
    }
}
