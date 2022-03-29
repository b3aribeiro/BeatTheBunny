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

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            gametittleUI.SetActive(false);
            resetUI.SetActive(true);
            life1UI.SetActive(true);
            life2UI.SetActive(true);
            life3UI.SetActive(true);
            bunnyCoinUI.SetActive(true);
        }
    }
}
