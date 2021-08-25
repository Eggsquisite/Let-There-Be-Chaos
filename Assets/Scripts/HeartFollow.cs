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
    [SerializeField] [Range(0f, 1f)]
    private float followLightIntensity;

    private Color baseColor;
    private Color followColor;
    private Color lerpedColor;
    private Vector2 direction;
    private Transform followTarget;
    private GameObject playerObject;

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
        if (followPlayer)
        {
            // Calculate Direction to follow player
            direction = followTarget.GetComponent<Rigidbody2D>().position - rb.position;

            // slowly make the heart fully transparent
            if (sp.color != followColor)
            {
                lerpedColor = Color.Lerp(sp.color, followColor, transitionSpeed * Time.deltaTime);
                sp.color = lerpedColor;
            } else if (sp.color == followColor && followPlayer)
            {
                SetFollowPlayer(false);
                StartCoroutine(BeginHeartRespawn());
            }

            // slowly reduce the light of the hearts light
            if (heartLight.intensity > followLightIntensity)
                heartLight.intensity = Mathf.Lerp(heartLight.intensity, followLightIntensity, transitionSpeed * Time.deltaTime);
        }
        else if (!followPlayer && returnToBaseValues)
        {
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

    // Having Setters/Getters helps me visualize and organize ////////////////////////////////////////////////////////////////////////////////////////////////
    public bool GetPickupReady() {
        return pickupReady;
    }

    public int GetGrowthTier() {
        return growthTier;
    }

    public void SetPlayerObject(GameObject player) {
        playerObject = player;
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
