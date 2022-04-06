using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    BunnyController _player;
    Rigidbody2D _rigidbody;
    AudioSource _audioSource;
    Animator _animator;
    public GameObject explosion;
    public int scoreValueMin = 10;
    public int scoreValueMax = 25;
    public int enemyHealth = 2;
    public AudioClip hurtSound;

    public bool alive = true;
    public bool hurt = false;

    public Animation starhurt;
    GameManager _gameManager;


    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _player = FindObjectOfType<BunnyController>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
    }


   private void OnTriggerEnter2D(Collider2D other) {
       if(other.gameObject.CompareTag("Sword")){
           //_audiosource.PlayOneShot(deathSound);
           //Instantiate(explosion, transform.position, Quaternion.identity);
           if(_player.health < 3) {
           _gameManager.AddScore(scoreValueMin);
           } else {
            _gameManager.AddScore(scoreValueMax);
           }

            enemyHealth--;
            starhurt.Play();

            if (enemyHealth < 1)
            {
                Destroy(gameObject);
            }

            //Destroy(gameObject);
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (alive && !hurt && other.gameObject.CompareTag("Sword"))
        {

            hurt = true;
            enemyHealth--;
            _animator.SetTrigger("Hurt");

            if (enemyHealth < 1)
            {
                _animator.SetTrigger("Die");
                alive = false;
            }
            else
            {
                StartCoroutine(GotHurt());
            }
        }

    }

    IEnumerator GotHurt()
    {
        hurt = true;
        starhurt.Play();
        _animator.SetTrigger("Hurt");
        _rigidbody.AddForce(new Vector2(-transform.localScale.x * 200, 200));
        yield return new WaitForSeconds(.2f);
        hurt = false;


    }
}
