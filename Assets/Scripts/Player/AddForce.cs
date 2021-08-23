using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForce : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Chaos")
        {
            Debug.Log("Adding force to chaos");
            Vector3 tmpDir = (transform.position - collision.transform.position).normalized;
            collision.GetComponent<Rigidbody2D>().AddForce(tmpDir * 0.5f);

        } else if (collision.tag == "Hearts")
        {

        }
    }
}
