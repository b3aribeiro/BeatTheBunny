using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(WaitToFall());
        }
    }

    IEnumerator WaitToFall()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<Rigidbody2D>().isKinematic = false;
        StartCoroutine(WaitToDestroy());
    }

    IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
