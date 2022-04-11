using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Water : MonoBehaviour
{
    
    AudioSource _audioSource;
    public AudioClip watersound;
    public GameObject loseUI;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

   

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player"))
        {
            _audioSource.PlayOneShot(watersound);

            StartCoroutine(LoadMainScreen());
            loseUI.SetActive(true);
            
        }
    }

    IEnumerator LoadMainScreen()
    {

        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("ProceduralMap");
    }

}
