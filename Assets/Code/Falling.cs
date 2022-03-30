using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : MonoBehaviour
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
        yield return new WaitForSeconds(.5f);
        GetComponent<Rigidbody2D>().isKinematic = false;
    }
}
