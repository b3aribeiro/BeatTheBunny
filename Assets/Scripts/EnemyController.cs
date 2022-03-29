using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    CharacterController _player;
    Rigidbody2D _rigidbody;
    public GameObject explosion;
    public int scoreValueMin = 10;
    public int scoreValueMax = 25;
    
    //AudioSource _audiosource;
    //public AudioClip deathSound;

      void Start()
    {
        _player = FindObjectOfType<CharacterController>();
        _rigidbody = GetComponent<Rigidbody2D>();
        //_audiosource = GetComponent<AudioSource>();
    }
   private void OnTriggerEnter2D(Collider2D other) {
       if(other.gameObject.CompareTag("Sword")){
           //_audiosource.PlayOneShot(deathSound);
           //Instantiate(explosion, transform.position, Quaternion.identity);
           if(_player.health < 3) {
            _player.AddScore(scoreValueMin);
           } else {
            _player.AddScore(scoreValueMax);
           }

           Destroy(gameObject);
           //Instantiate(explosion, transform.position, Quaternion.identity)
       }
   }
}
