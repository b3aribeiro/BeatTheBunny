using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DangerZoneController : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    GameManager _gameManager;

    void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        rigidbody = GetComponent<Rigidbody2D>();  
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (gameObject.tag == "Player")
        {      
            _gameManager.PlayersLost();
        }
    }
    void Update()
    {
        rigidbody.velocity = new Vector2(0.5f, 0f);
    }
}
