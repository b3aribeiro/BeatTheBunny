using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyfishController : MonoBehaviour
{
    public int playerNum = 2;
    public int health = 3;

    public int jumpForce = 700;

    public int speed = 5;
    //int bulletForce = 600;

    bool alive = true;
    bool hurt = false;
    public bool grounded;
    public LayerMask groundLayer;
    public Transform feetPos;
    string jumpBtn;
    string xAxisBtn;
    //string atkBtn;
    Rigidbody2D _rigidbody;
    Animator _animator;
    AudioSource _audioSource;
    public AudioClip shootsound;
    public AudioClip jumpsound;  
    public GameObject life1UI; 
    public GameObject life2UI; 
    public GameObject life3UI; 

    //public GameObject bulletPrefab;
    //public Transform spawnPoint;
    GameManager _gameManager;

    void Start()
    {
         _gameManager = FindObjectOfType<GameManager>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        //player controller buttons
        //atkBtn = "Attack" + playerNum;
        jumpBtn = "Jump" + playerNum;
        xAxisBtn = "Horizontal" + playerNum;
    }


    void Update()
    {
        if (!alive || hurt)
        {
            return;
        }

        float xSpeed = Input.GetAxis(xAxisBtn) * speed;
        _rigidbody.velocity = new Vector2(xSpeed, _rigidbody.velocity.y);
        _animator.SetFloat("Speed", Mathf.Abs(xSpeed));

        if (xSpeed > 0 && transform.localScale.x < 0 ||
            xSpeed < 0 && transform.localScale.x > 0)
        {
            transform.localScale *= new Vector2(-1, 1);
        }

        if (grounded && Input.GetButtonDown(jumpBtn))
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
            _rigidbody.AddForce(new Vector2(0, jumpForce));
            _audioSource.PlayOneShot(jumpsound);
        }

        //if (Input.GetButtonDown(atkBtn))
        //{
        //    //Vector2 bulletDir = new Vector2(transform.localScale.x, 0);
        //    //bulletDir *= bulletForce;

        //    //GameObject newBullet = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);
        //    //newBullet.GetComponent<Rigidbody2D>().AddForce(bulletDir);

        //    //_animator.SetTrigger("Shoot");

        //    //_audioSource.PlayOneShot(shootsound);
        //}
    }

    private void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(feetPos.position, .3f, groundLayer);
        _animator.SetBool("Ground", grounded);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(alive && !hurt && other.gameObject.CompareTag("Enemy"))
        {
            health--;

            if(health == 2)
            { 
            life3UI.SetActive(false);
            //bunnyLifeTextUI.text = "♥♥";
            }

            if(health == 1)
            { 
            life2UI.SetActive(false);
            //bunnyLifeTextUI.text = "♥";
            }

            if (health < 1){
                life1UI.SetActive(false);
                _animator.SetTrigger("Die");
                alive = false;
                //StartCoroutine(LoadMainScreen());

            } else { StartCoroutine(GotHurt()); }
        }

        if(alive && !hurt && (other.gameObject.CompareTag("DangerZone") || other.gameObject.CompareTag("Water") ))
        {
            alive = false;
            health = 0;
            _animator.SetTrigger("Die");
            life3UI.SetActive(false);
            life2UI.SetActive(false);
            life1UI.SetActive(false);
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

            //winUI.SetActive(true);
            //StartCoroutine(LoadMainScreen());
        }

    }

    IEnumerator GotHurt()
    {
        hurt = true;
        _animator.SetTrigger("Hurt");
        _rigidbody.AddForce(new Vector2(-transform.localScale.x * 200, 200));
        yield return new WaitForSeconds(.2f);
        hurt = false;


    }
}