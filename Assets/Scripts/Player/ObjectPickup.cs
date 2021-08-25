using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickup : MonoBehaviour
{
    private List<HeartFollow> heartFollow = new List<HeartFollow>();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Hearts")
        {
            Debug.Log("Pickup heart: " + collision.gameObject.name);
            var tmp = collision.gameObject.GetComponent<HeartFollow>();

            if (heartFollow.Count == 0) {
                tmp.FollowPlayer(transform);
            }
            else if (heartFollow.Count > 0) { 
                tmp.FollowPlayer(heartFollow[heartFollow.Count - 1].transform);
            }

            heartFollow.Add(tmp);

            //tmpMaterial.SetVector("_Color", Color.white * colorIntensity);
            //factor = Mathf.Pow(2, intensity);
            //var color = new Color(tmpSprite.color.r * factor, tmpSprite.color.g * factor, tmpSprite.color.b * factor);
        } else if (collision.gameObject.tag == "Chaos")
        {
            DetachFromPlayer();
            Destroy(collision.gameObject);
            // play a destruction sound
        }
    }

    public void DetachFromPlayer() {
        if (heartFollow.Count > 0) {
            Debug.Log(heartFollow.Count);
            heartFollow[heartFollow.Count - 1].DetachFromPlayer();
            heartFollow.RemoveAt(heartFollow.Count - 1);
        }
        else
        {
            // shrink heart pulse and/or size
        }
    }
}
