using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BunnyController : MonoBehaviour
{
    public int playerNum = 1;
    public int speed = 5;
    public int health = 3;
    public int jumpForce = 300;
    public bool alive = true;
    public bool hurt = false;
    public bool grounded;
    public LayerMask groundLayer;
    public Transform feetPos;
    Rigidbody2D _rigidbody;
    Animator _animator;
    AudioSource _audioSource;
    public AudioClip swooshsound;
    public AudioClip jumpsound;
    public GameObject life1UI; 
    public GameObject life2UI; 
    public GameObject life3UI;  
    public GameObject bunnyCoinUI;   
    public TextMeshProUGUI bunnyCoinTextUI;


    //if creating a bullet, make it a trigger 
    //add rigid body, set gravity to 0
    //public GameObject bulletPrefab;
    //int bulletForce = 600;

    public GameObject swordRight;

    private float timeBetweenAtk;
    public float beginAtk = 0.3f;
    string atkBtn;
    string jumpBtn;
    string xAxisBtn;
    GameManager _gameManager;

    void Start()
    {
      _gameManager = FindObjectOfType<GameManager>();
      _rigidbody = GetComponent<Rigidbody2D>();
      _animator = GetComponent<Animator>(); 
      _audioSource = GetComponent<AudioSource>(); 

        //player controller buttons
        atkBtn = "Attack" + playerNum;
        jumpBtn = "Jump" + playerNum;
        xAxisBtn = "Horizontal" + playerNum;
    }

    async void Update()
    {
        if(!alive || hurt){ return; }

        //walking right and left
        float xSpeed = Input.GetAxis(xAxisBtn) * speed;
         _rigidbody.velocity = new Vector2 (xSpeed, _rigidbody.velocity.y);
        _animator.SetFloat("Speed", Mathf.Abs(xSpeed));
        
       if (xSpeed > 0 && transform.localScale.x < 0 || xSpeed < 0 && transform.localScale.x > 0)
        {
            transform.localScale *= new Vector2 (-1,1);
        }   


        //jumping
        if (grounded && Input.GetButtonDown(jumpBtn))
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
            _rigidbody.AddForce(new Vector2(0,jumpForce));
            _audioSource.PlayOneShot(jumpsound);
        }

       
        if(timeBetweenAtk <= 0)
        {
            if(Input.GetButtonDown(atkBtn))
            {
                _animator.SetTrigger("Fight");
                timeBetweenAtk = beginAtk;
                _audioSource.PlayOneShot(swooshsound);
            }

        } else {
            timeBetweenAtk -= Time.deltaTime;
        }
            //Vector2 bulletDirection = new Vector2(bulletForce * transform.localScale.x, 0)
            //GameObject newBullet = Instatiate(bulletPrefab,transform.position, Quaternion.identity);
            //newBullet.GetComponent<Rigidbody2D>().AddForce(bulletDirection); 
    }
    
    void SwordOn(){
        swordRight.SetActive(true);
    }
    void SwordOff(){
        swordRight.SetActive(false);
    }

    private void FixedUpdate() 
    {
        //overlapcircle draws a circular raycast around a given point.
        //point, radius, mask
        grounded = Physics2D.OverlapCircle(feetPos.position, .3f, groundLayer);
        _animator.SetBool("Grounded", grounded);
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

            _gameManager.PlayersWon();
        }

    }

    IEnumerator GotHurt()
    {
        hurt = true;
        _animator.SetTrigger("Hurt");

        //knockback
        _rigidbody.AddForce(new Vector2(-transform.localScale.x * 250, 250));
        yield return new WaitForSeconds(.4f);
        hurt = false;
    }

    IEnumerator LoadMainScreen()
    {
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("ProceduralMap");
    }
}
