using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideToSidePlatform : MonoBehaviour
{
    public float speed = 0.5f;
    public float distance = 5;
    private float startPosition;

    void Start()
    {
        startPosition = transform.position.x;
    }

    void Update()
    {
        Vector2 newPosition = transform.position;
        //Mathf has most math functions in it
        //SmoothStep eases in and out like a lerp
        //it is doing(start, how far it will go, how much it will change)
        //PingPong makes the result alternate between 0 and 1
        newPosition.x = Mathf.SmoothStep(startPosition, startPosition + distance, Mathf.PingPong(Time.time * speed, 1));
        transform.position = newPosition;
    }   

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }

}
