using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform playerTarget;
    [SerializeField]
    private bool canFollow;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float min_X;
    [SerializeField]
    private float max_X;
    [SerializeField]
    private float min_Y;
    [SerializeField]
    private float max_Y;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (canFollow) {
            //var tmpVector = new Vector3(playerTarget.position.x, playerTarget.position.y, transform.position.z);
            //transform.position = Vector3.MoveTowards(transform.position, tmpVector, speed * Time.deltaTime);

            Vector3 targetPosition = new Vector3(Mathf.Clamp(playerTarget.position.x, min_X, max_X),
                                        Mathf.Clamp(playerTarget.position.y, min_Y, max_Y),
                                        transform.position.z);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, speed * Time.fixedDeltaTime);
            transform.position = smoothedPosition;
        }
    }
}
