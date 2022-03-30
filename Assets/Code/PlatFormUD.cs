using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatFormUD : MonoBehaviour
{

        public float speed = .5f;
        public float distance = 5;
        private float startPosition;

        void Start()
        {
            startPosition = transform.position.y;

        }

        void Update()
        {
            Vector2 newPosition = transform.position;
            newPosition.y = Mathf.SmoothStep(startPosition, startPosition + distance, Mathf.PingPong(Time.time * speed, 1));
            transform.position = newPosition;
        }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}
