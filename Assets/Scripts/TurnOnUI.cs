using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnOnUI : MonoBehaviour
{
    //public TextMeshProUGUI 
    public GameObject gametittleUI;
    //public GameObject bunnyLifeUI;
    public GameObject bunnyCoinUI;
    public GameObject resetUI; 
    
    public GameObject life1UI; 
    public GameObject life2UI; 
    public GameObject life3UI;

    public GameObject jellylife1UI;
    public GameObject jellylife2UI;
    public GameObject jellylife3UI;

    public GameObject ghostlife1UI;
    public GameObject ghostlife2UI;
    public GameObject ghostlife3UI;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            gametittleUI.SetActive(false);
            resetUI.SetActive(true);
            life1UI.SetActive(true);
            life2UI.SetActive(true);
            life3UI.SetActive(true);
            jellylife1UI.SetActive(true);
            jellylife2UI.SetActive(true);
            jellylife3UI.SetActive(true);
            ghostlife1UI.SetActive(true);
            ghostlife2UI.SetActive(true);
            ghostlife3UI.SetActive(true);
            bunnyCoinUI.SetActive(true);
        }
    }
}
