using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartFollow : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D coll;
    private SpriteRenderer sp;

    [Header("Follow Variables")]
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float transitionSpeed;

    private bool pickupReady;
    private bool followPlayer;
    private bool returnToBaseValues;

    [Header("Text Variables")]
    [SerializeField]
    private Transform textTransform;
    /*    [SerializeField]
        private float minTextDelay;
        [SerializeField]
        private float maxTextDelay;*/

    //private float baseTextDelay;
    //private bool textBubblesReady;

    //private Coroutine textRoutine;

    [Header("Growth Variables")]
    [SerializeField] [Range (1, 5)]
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

        SetPickupReady(true);

        baseColor = sp.color;
        baseLightIntensity = heartLight.intensity;
        followColor = new Color(sp.color.r, sp.color.g, sp.color.b, 0f);
    }

    private void Update()
    {
        if (followPlayer) {
            // Calculate Direction to follow player
            direction = followTarget.GetComponent<Rigidbody2D>().position - rb.position;
            FadeAway();
        }
        else if (!followPlayer && returnToBaseValues) {
            // slowly return the heart to full opacity, and restore collider when that happens
            if (sp.color != baseColor)
            {
                lerpedColor = Color.Lerp(sp.color, baseColor, transitionSpeed / 2 * Time.deltaTime);
                sp.color = lerpedColor;
            }
            else
                SetReturnToBaseValues(false);

            if (heartLight.intensity < baseLightIntensity)
                heartLight.intensity = Mathf.Lerp(heartLight.intensity, baseLightIntensity, transitionSpeed * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        if (followPlayer)
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
    }

    public void OnPickup(Transform target) {
        followTarget = target;

        SpawnText();
        SetPickupReady(false);
        SetFollowPlayer(true);
        SetColliderEnabled(false);
        StartCoroutine(AlphaTransition());
    }

    private IEnumerator AlphaTransition() {
        yield return new WaitForSeconds(alphaTransitionDelay);

        SetAlphaTransition(true);
    }

    private void SpawnText() {
        Instantiate(GameManager.instance.GetLoveBlurb(), textTransform.position, Quaternion.identity, transform);
    }

    private IEnumerator BeginHeartRespawn() {
        yield return new WaitForSeconds(respawnDelay);

        ObjectSpawner.instance.TeleportObject(transform);
        ReturnToBase();
    }

    private void ReturnToBase() {
        SetPickupReady(true);
        SetReturnToBaseValues(true);
    }

    private void FadeAway() {
        // slowly make the heart fully transparent
        if (sp.color != followColor && alphaTransition)
        {
            lerpedColor = Color.Lerp(sp.color, followColor, transitionSpeed * Time.deltaTime);
            sp.color = lerpedColor;
        }
        else if (sp.color == followColor && alphaTransition)
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
