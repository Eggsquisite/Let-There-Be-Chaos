using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rb;

    [Header("Movement Variables")]
    [SerializeField] private float moveSpeed;

    private Vector3 movementVect;

    private void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        //rb.MovePosition(transform.position + movementVect);
        rb.AddForce(movementVect);
    }

    private void PlayerInput() {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        movementVect = new Vector3(h, v);
        movementVect = movementVect.normalized * moveSpeed * 100f * Time.deltaTime;
    }
}
