using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartFollow : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D coll;
    private SpriteRenderer sp;
    private SpawnText st;

    [Header("Follow Variables")]
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float transitionSpeed;

    private bool pickupReady;
    private bool followPlayer;
    private bool returnToBaseValues;

    [Header("Growth Variables")]
    private int growthTier;

    [Header("Respawn Variables")]
    [SerializeField]
    private float respawnDelay;


    [Header("Light Variables")]
    [SerializeField]
    private UnityEngine.Experimental.Rendering.Universal.Light2D heartLight;
    [SerializeField]
    private float alphaTransitionDelay;

    private Color baseColor;
    private Color followColor;
    private Color lerpedColor;
    private Vector2 direction;
    private Transform followTarget;

    private bool alphaTransition;
    private float baseLightIntensity;


    private void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (coll == null) coll = GetComponent<Collider2D>();
        if (sp == null) sp = GetComponent<SpriteRenderer>();
        if (st == null) st = GetComponent<SpawnText>();

        SetPickupReady(true);

        baseColor = sp.color;
        baseLightIntensity = heartLight.intensity;
        followColor = new Color(sp.color.r, sp.color.g, sp.color.b, 0f);

        RandomizeGrowthTier();
        UpdateSize();
    }

    private void Update()
    {
        if (alphaTransition)
            FadeAway();

        if (followPlayer) {
            // Calculate Direction to follow player
            direction = followTarget.GetComponent<Rigidbody2D>().position - rb.position;
        }
        else if (!followPlayer && returnToBaseValues) {
            // slowly return the heart to full opacity, and restore collider when that happens
            if (sp.color != baseColor)
            {
                lerpedColor = Color.Lerp(sp.color, baseColor, transitionSpeed / 2 * Time.deltaTime);
                sp.color = lerpedColor;
            }
            else {
                SetColliderEnabled(true);
                SetReturnToBaseValues(false);
            }

            if (heartLight.intensity < baseLightIntensity)
                heartLight.intensity = Mathf.Lerp(heartLight.intensity, baseLightIntensity, transitionSpeed * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        if (followPlayer)
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Chaos")
        {
            var tmp = collision.gameObject.GetComponent<Chaos>();
            tmp.SetGrowthTier(tmp.GetGrowthTier() - 1);
            Vector3 tmpDir = (collision.transform.position - transform.position).normalized;
            tmp.GetComponent<Rigidbody2D>().AddForce(tmpDir * 15f * growthTier);

            if (growthTier > 1) {
                growthTier -= 1;
                UpdateSize();
            } else
            {
                SetPickupReady(false);
                SetColliderEnabled(false);
                SetAlphaTransition(true);
            }
        }
    }

    public void OnPickup(Transform target) {
        followTarget = target;

        st.SpawnBlurb(true);
        SetPickupReady(false);
        SetFollowPlayer(true);
        SetColliderEnabled(false);
        StartCoroutine(AlphaTransition());
    }

    private IEnumerator AlphaTransition() {
        yield return new WaitForSeconds(alphaTransitionDelay);

        SetAlphaTransition(true);
    }

    private IEnumerator BeginHeartRespawn() {
        yield return new WaitForSeconds(respawnDelay);

        ObjectSpawner.instance.TeleportObject(transform);
        ReturnToBase();
    }

    private void ReturnToBase() {
        SetPickupReady(true);
        SetColliderEnabled(true);
        SetReturnToBaseValues(true);
        RandomizeGrowthTier();
        UpdateSize();
    }

    private void RandomizeGrowthTier() {
        growthTier = Random.Range(1, 6);
    }

    private void UpdateSize() {
        switch (growthTier)
        {
            case 1:
                transform.localScale = new Vector2(1f, 1f);
                break;
            case 2:
                transform.localScale = new Vector2(1.5f, 1.5f);
                break;
            case 3:
                transform.localScale = new Vector2(2f, 2f);
                break;
            case 4:
                transform.localScale = new Vector2(2.5f, 2.5f);
                break;
            case 5:
                transform.localScale = new Vector2(3f, 3f);
                break;
            default:
                break;
        }
    }

    private void FadeAway() {
        // slowly make the heart fully transparent
        if (sp.color != followColor)
        {
            lerpedColor = Color.Lerp(sp.color, followColor, transitionSpeed * Time.deltaTime);
            sp.color = lerpedColor;
        }
        else if (sp.color == followColor)
        {
            SetFollowPlayer(false);
            SetAlphaTransition(false);
            StartCoroutine(BeginHeartRespawn());
        }

        // slowly reduce the intensity of the hearts light
        if (heartLight.intensity > 0f)
            heartLight.intensity = Mathf.Lerp(heartLight.intensity, 0f, transitionSpeed * Time.deltaTime);
    }

    // Having Setters/Getters helps me visualize and organize ////////////////////////////////////////////////////////////////////////////////////////////////
    public bool GetPickupReady() {
        return pickupReady;
    }

    public int GetGrowthTier() {
        return growthTier;
    }

    private void SetAlphaTransition(bool flag) {
        alphaTransition = flag;
    }

    private void SetReturnToBaseValues(bool flag) {
        returnToBaseValues = flag;
    }

    private void SetPickupReady(bool flag) {
        pickupReady = flag;
    }

    private void SetFollowPlayer(bool flag) {
        followPlayer = flag;
    }

    private void SetColliderEnabled(bool flag) {
        coll.enabled = flag;
    }
}
