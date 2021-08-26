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

    [SerializeField] private float moveSpeedUpgrade;

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
        rb.AddForce(movementVect);

        if (rb.velocity.magnitude > maxVelocity) {
            rb.velocity = rb.velocity.normalized * maxVelocity;
        }
    }

    private void PlayerInput() {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        movementVect = new Vector3(h, v);
        movementVect = movementVect.normalized * moveSpeed * 100f * Time.deltaTime;
    }

    // Called during intro animation event to prevent player moevement
    public void SetMoveSpeed(int newValue) {
        moveSpeed = newValue;
    }

    public void UpdateMoveSpeed(int flag) {
        if (flag < 0)
            moveSpeed -= moveSpeedUpgrade;
        else if (flag > 0)
            moveSpeed += moveSpeedUpgrade;
    }
}
