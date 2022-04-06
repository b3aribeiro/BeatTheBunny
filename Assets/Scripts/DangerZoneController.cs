using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DangerZoneController : MonoBehaviour
{
    public Rigidbody2D rigidbody;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();  
    }

    void Update()
    {
        rigidbody.velocity = new Vector2(0.5f, 0f);

        if (gameObject.tag == "Player")
        {       
            ResetScene();
        }
    }

     IEnumerator ResetScene()
    {

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("ProceduralMap");
    }
}
