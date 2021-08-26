using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBlurb : MonoBehaviour
{
    [SerializeField]
    private float textDestroyDelay;
    [SerializeField]
    private List<Sprite> textSprite;
    
    private Animator anim;
    private SpriteRenderer sp;

    private void Start()
    {
        if (anim == null) anim = GetComponent<Animator>();
        if (sp == null) sp = GetComponent<SpriteRenderer>();

        sp.enabled = false;
        sp.sprite = textSprite[Random.Range(0, textSprite.Count)];
        sp.enabled = true;
    }

    public void BeginTextDelay() {
        StartCoroutine(DeleteText());
    }

    private IEnumerator DeleteText() {
        yield return new WaitForSeconds(textDestroyDelay);
        anim.SetBool("deleteText", true);
    }

    public void DestroyText() {
        Destroy(gameObject);
    }

}
