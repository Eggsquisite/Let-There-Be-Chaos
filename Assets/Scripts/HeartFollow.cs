using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartFollow : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D coll;
    private SpriteRenderer sp;

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float minDetachSpeed;
    [SerializeField]
    private float maxDetachSpeed;
    [SerializeField]
    private float colorSpeed;
    [SerializeField]
    private float baseDelay;

    private bool returnToBase;
    private float baseDetachSpeed;

    [Header("Light Variables")]
    [SerializeField]
    private UnityEngine.Experimental.Rendering.Universal.Light2D heartLight;
    [SerializeField] [Range(0f, 1f)]
    private float followLightIntensity;

    private Color baseColor;
    private Color followColor;
    private Color lerpedColor;
    private Vector2 direction;
    private Transform playerTarget;

    private float baseLightIntensity;

    private bool followPlayer;

    private void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (coll == null) coll = GetComponent<Collider2D>();
        if (sp == null) sp = GetComponent<SpriteRenderer>();

        baseColor = sp.color;
        followColor = new Color(sp.color.r, sp.color.g, sp.color.b, 0.5f);
        baseLightIntensity = heartLight.intensity;
        baseDetachSpeed = Random.Range(minDetachSpeed, maxDetachSpeed);
    }

    private void Update()
    {
        if (followPlayer)
        {
            // Calculate Direction to follow player
            direction = playerTarget.GetComponent<Rigidbody2D>().position - rb.position;

            // slowly make the heart fully transparent
            if (sp.color != followColor)
            {
                lerpedColor = Color.Lerp(sp.color, followColor, colorSpeed * Time.deltaTime);
                sp.color = lerpedColor;
            }

            // slowly reduce the light of the hearts light
            if (heartLight.intensity > followLightIntensity)
                heartLight.intensity = Mathf.Lerp(heartLight.intensity, followLightIntensity, colorSpeed * Time.deltaTime);
        }
        else if (!followPlayer && returnToBase)
        {
            // slowly return the heart to full opacity, and restore collider when that happens
            if (sp.color != baseColor)
            {
                lerpedColor = Color.Lerp(sp.color, baseColor, colorSpeed / 2 * Time.deltaTime);
                sp.color = lerpedColor;
            }
            else
                returnToBase = false;

            if (heartLight.intensity < baseLightIntensity)
                heartLight.intensity = Mathf.Lerp(heartLight.intensity, baseLightIntensity, colorSpeed * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        if (followPlayer)
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
    }

    public void DetachFromPlayer() {
        followPlayer = false;
        rb.velocity *= baseDetachSpeed;
        StartCoroutine(ReturnToBase());
    }

    public void FollowPlayer(Transform target) {
        followPlayer = true;
        playerTarget = target;
        coll.enabled = false;
    }

    private IEnumerator ReturnToBase() {
        yield return new WaitForSeconds(0.1f);
        coll.enabled = true;
        yield return new WaitForSeconds(baseDelay);
        returnToBase = true;
    }
}
