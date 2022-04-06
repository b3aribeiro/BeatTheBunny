using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    //This is the player manager. It monitors lives of the players.
    //It aslo handles the win and lose conditions, and the restarting of the game.

    int playersInGame = 3;
    int[] playersHealth;
    public bool roundOver = false;
    public int[] playersHealthBars;
    int score = 0;
    public TextMeshProUGUI[] healthText;
    public GameObject bunnyCoinUI;   
    public TextMeshProUGUI bunnyCoinTextUI;
    public GameObject winUI;   
    public GameObject loseUI;


    private void Start()
    {
        playersHealthBars = new int[playersInGame];

        for(int i = 0; i < playersHealth.Length; i++)
        {
            playersHealth[i] = 3;
        }
    }

   
    public bool UpdateHealth(int playerNum, int damage)
    {
        playersHealthBars[playerNum]--;
        //healthText[playerNum].text = playersHealthBars[playerNum].ToString();
        //healthBars[playerNum].localScale = new Vector3(playersHealthBars[playerNum] / 100f, 1, 1);

        if(playersHealthBars[playerNum] == 2 )
        {
            return true;
        } else if(playersHealthBars[playerNum] == 1 )
        {
            return true;
        } else
        {
            CheckRoundOver();
            playersInGame--;
            return false;
        }

    }

    void CheckRoundOver()
    {
        if(playersInGame != 3)
        {
            loseUI.SetActive(true);
            StartCoroutine(LoadMainScreen());
        }
    }

     public void AddScore(int addNum)
    {
        score += addNum;
        bunnyCoinTextUI.text = "" + score;
    }

    public void SubScore(int subNum)
    {
        score -= subNum;
        bunnyCoinTextUI.text = "" + score;
    }

    public void PlayersWon(){

        winUI.SetActive(true);
        StartCoroutine(LoadMainScreen());
    }

    public void PlayersLost(){

        loseUI.SetActive(true);
        StartCoroutine(LoadMainScreen());
    }

     IEnumerator LoadMainScreen()
    {
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("ProceduralMap");
    }
}
