using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform playerTarget;
    [SerializeField]
    private float speed;
    [SerializeField]
    private bool canFollow;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canFollow) {
            var tmpVector = new Vector3(playerTarget.position.x, playerTarget.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, tmpVector, speed * Time.deltaTime);
        }
    }
}
