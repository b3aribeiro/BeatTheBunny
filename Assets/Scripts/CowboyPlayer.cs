using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CowboyPlayer : MonoBehaviour
{
    public int playerNum = 3;
    public int speed = 5;
    public int bulletForce = 400;
    public int jumpForce = 300;
    public GameObject bulletPrefab;
    

    public bool grounded;
    public LayerMask groundLayer;
    public Transform feetPos;
    public Transform bulletPos;
    //public GameObject youDie;

    bool alive = true;
    bool hurt = false;
    int health = 5;
    
    Rigidbody2D _rigidbody;
    Animator  _animator;
    string atkBtn;
    string jumpBtn;
    string xAxisBtn;

    AudioSource _audioSource;
    public AudioClip shootsound;
    public AudioClip jumpsound;

    void Start()
    {
        _rigidbody =  GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    
        
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
            

            if(health < 1)
            {
                _animator.SetTrigger("Die");
                alive = false; 
                //youDie.SetActive(true);
            }
            else {
                StartCoroutine(GotHurt());
            }
        }
    }

    IEnumerator GotHurt()
    {
        _animator.SetTrigger("Injured");
        hurt = true; 
        
        yield return new WaitForSeconds(.2f);
        hurt = false;
    }
}
