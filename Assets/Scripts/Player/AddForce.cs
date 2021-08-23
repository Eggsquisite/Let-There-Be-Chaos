using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForce : MonoBehaviour
{
    [SerializeField]
    private float forceAdded;

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

        } else if (collision.tag == "Hearts")
        {

        }
    }
}
