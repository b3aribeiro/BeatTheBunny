using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CowboyPlayer : MonoBehaviour
{
    public int playerNum = 3;
    public int health = 3;
    public int jumpForce = 400;
    public int bulletForce = 400;
    public int speed = 5;
    bool alive = true;
    bool hurt = false;
    public bool grounded;
    public LayerMask groundLayer;
    public Transform feetPos;
    public Transform bulletPos;
    string atkBtn;
    string jumpBtn;
    string xAxisBtn;
    Rigidbody2D _rigidbody;
    Animator  _animator;
    AudioSource _audioSource;
    public AudioClip shootsound;
    public AudioClip jumpsound;
    public GameObject life1UI; 
    public GameObject life2UI; 
    public GameObject life3UI; 
    public GameObject bulletPrefab;
    //public GameObject youDie;
    GameManager _gameManager;
    public Collider2D _collider;

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _rigidbody =  GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _collider = GetComponent<Collider2D>();
    
        
        //player controller buttons
        atkBtn = "Attack" + playerNum;
        jumpBtn = "Jump" + playerNum;
        xAxisBtn = "Horizontal" + playerNum;
    }

    
    void Update()
    {
        
        if(!alive || hurt){
            return;
        }
        
        
        //Move horizontally
        float xSpeed = Input.GetAxis(xAxisBtn) * speed; 
        _rigidbody.velocity = new Vector2(xSpeed, _rigidbody.velocity.y);
        _animator.SetFloat("Speed", Mathf.Abs(xSpeed));

        if (xSpeed > 0 && transform.localScale.x < 0 ||
         xSpeed < 0 && transform.localScale.x > 0)
        {
            transform.localScale *= new Vector2(-1, 1);
        }
        

        //Fire
        if (Input.GetButtonDown(atkBtn))
        {
            
            //bullet 
            
            Vector2 bulletDir = new Vector2(transform.localScale.x, 0);
            bulletDir *= bulletForce;
            GameObject newBullet = Instantiate(bulletPrefab, bulletPos.position, Quaternion.identity);
            newBullet.GetComponent<Rigidbody2D>().AddForce(bulletDir);
            _animator.SetTrigger("Shoot");

            _audioSource.PlayOneShot(shootsound);
            
            
        }
        

        //Jump
        
        if (grounded && Input.GetButtonDown(jumpBtn))

        {
            _animator.SetBool("Jump",true);
            _rigidbody.AddForce(new Vector2(0,jumpForce));
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
            
            _audioSource.PlayOneShot(jumpsound);
        }
    }


    private void FixedUpdate() {
            grounded = Physics2D.OverlapCircle(feetPos.position, .1f, groundLayer);
            _animator.SetBool("Grounded",grounded); 

            if(feetPos.position.y < -12)
            {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

        }

    private void OnCollisionEnter2D(Collision2D other) {

         if(alive && !hurt && other.gameObject.CompareTag("Enemy"))
        { 
            
            health--;

            if(health == 2)
            { 
            life3UI.SetActive(false);
            }

            if(health == 1)
            { 
            life2UI.SetActive(false);
            }

            if (health < 1){
                life1UI.SetActive(false);
                _animator.SetTrigger("Die");
                alive = false;
                _collider.enabled = false;

            } else { StartCoroutine(GotHurt()); }
        }

        if(alive && !hurt && (other.gameObject.CompareTag("DangerZone") || other.gameObject.CompareTag("Water") ))
        {
            _rigidbody.AddForce(new Vector2(-transform.localScale.x * 250, 250));
            alive = false;
            health = 0;
            _animator.SetTrigger("Die");
            life3UI.SetActive(false);
            life2UI.SetActive(false);
            life1UI.SetActive(false);
            _gameManager.PlayersLost();
            //StartCoroutine(LoadMainScreen());
        }

        if(alive && !hurt && other.gameObject.CompareTag("FinishLine"))
        { 
           hurt = true;
           
           if(health < 3) {
            //add score at GameManager
            _gameManager.AddScore(50);
           } else {
            _gameManager.AddScore(100);
           }

            _gameManager.PlayersWon();
        }
    }

    IEnumerator GotHurt()
    {
        _rigidbody.AddForce(new Vector2(-transform.localScale.x * 250, 250));
        _animator.SetTrigger("Injured");
        hurt = true; 
        
        yield return new WaitForSeconds(.2f);
        hurt = false;
    }
}
