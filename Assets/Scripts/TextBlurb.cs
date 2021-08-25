using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBlurb : MonoBehaviour
{
    [SerializeField]
    private float textDestroyDelay;

    private Animator anim;

    private void Start()
    {
        if (anim == null) anim = GetComponent<Animator>();
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
