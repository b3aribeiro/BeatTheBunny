using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    BunnyController _player;
    Rigidbody2D _rigidbody;
    AudioSource _audioSource;
    public GameObject explosion;
    public int scoreValueMin = 10;
    public int scoreValueMax = 25;
    public int enemyHealth = 5;
    public AudioClip hurtSound;
    
    

      void Start()
    {
        _player = FindObjectOfType<BunnyController>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
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

        if(other.gameObject.CompareTag("Bullet")){
           
           enemyHealth --;

           if(enemyHealth < 1){
               Destroy(gameObject);
           } 
           
       }

       if(other.gameObject.CompareTag("Player")){
           _audioSource.PlayOneShot(hurtSound);
       }
   }
}
