using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RebPlayerCode : MonoBehaviour
{
    int health = 5;

    int speed = 5;
    int jumpForce = 600;
    int bulletForce = 600;

    bool alive = true;
    bool hurt = false;

    public GameObject bulletPrefab;

    public Transform spawnPoint;

    Rigidbody2D _rigidbody;

    Animator _animator;

    public bool grounded;
    public LayerMask groundLayer;
    public Transform feetPos;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }


    void Update()
    {
        if (!alive || hurt)
        {
            return;
        }

        float xSpeed = Input.GetAxis("Horizontal") * speed;
        _rigidbody.velocity = new Vector2(xSpeed, _rigidbody.velocity.y);
        _animator.SetFloat("Speed", Mathf.Abs(xSpeed));

        if (xSpeed > 0 && transform.localScale.x < 0 ||
            xSpeed < 0 && transform.localScale.x > 0)
        {
            transform.localScale *= new Vector2(-1, 1);
        }

        if (grounded && Input.GetButtonDown("Jump"))
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
            _rigidbody.AddForce(new Vector2(0, jumpForce));
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Vector2 bulletDir = new Vector2(transform.localScale.x, 0);
            bulletDir *= bulletForce;

            GameObject newBullet = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);
            newBullet.GetComponent<Rigidbody2D>().AddForce(bulletDir);

            _animator.SetTrigger("Shoot");
        }
    }

    private void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(feetPos.position, .3f, groundLayer);
        _animator.SetBool("Ground", grounded);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (alive && !hurt && other.gameObject.CompareTag("Enemy"))
        {
            
            
            health--;
            if (health < 1)
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
        _animator.SetTrigger("Hurt");
        _rigidbody.AddForce(new Vector2(-transform.localScale.x * 200, 200));
        yield return new WaitForSeconds(.2f);
        hurt = false;


    }
}