using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rb;

    [Header("Movement Variables")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxVelocity;

    private Vector3 movementVect;

    private void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
        Debug.Log(rb.velocity);
    }

    private void FixedUpdate()
    {
        //rb.MovePosition(transform.position + movementVect);
        rb.AddForce(movementVect);

        if (rb.velocity.magnitude > maxVelocity) {
            rb.velocity = rb.velocity.normalized * maxVelocity;
        }

/*        if (Mathf.Abs(rb.velocity.x) >= maxVelocity) 
        {
            if (rb.velocity.x < 0) { 
                rb.velocity = new Vector2(-maxVelocity, rb.velocity.y);
            } else {
                rb.velocity = new Vector2(maxVelocity, rb.velocity.y);
            }
        } else if (Mathf.Abs(rb.velocity.y) >= maxVelocity) 
        {
            if (rb.velocity.y < 0) {
                rb.velocity = new Vector2(rb.velocity.x, -maxVelocity);
            } else { 
                rb.velocity = new Vector2(rb.velocity.x, maxVelocity);
            }

        } else if (Mathf.Abs(rb.velocity.x) >= maxVelocity && Mathf.Abs(rb.velocity.y) >= maxVelocity) 
        {
            if (rb.velocity.x < 0 && rb.velocity.y < 0)
                rb.velocity = new Vector2(-maxVelocity, -maxVelocity);
            else if (rb.velocity.x < 0 && rb.velocity.y >= 0)
                rb.velocity = new Vector2(-maxVelocity, maxVelocity);
            else if (rb.velocity.x >= 0 && rb.velocity.y < 0)
                rb.velocity = new Vector2(maxVelocity, -maxVelocity);
            else if (rb.velocity.x >= 0 && rb.velocity.y >= 0)
                rb.velocity = new Vector2(maxVelocity, maxVelocity);
        }
*/
    }

    private void PlayerInput() {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        movementVect = new Vector3(h, v);
        movementVect = movementVect.normalized * moveSpeed * 100f * Time.deltaTime;
    }
}
