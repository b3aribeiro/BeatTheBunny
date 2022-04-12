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
    public int[] playersHealth;
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
        playersHealth[0] = 3;
        playersHealth[1] = 3;
        playersHealth[2] = 3;

    }

   
    public bool UpdateHealth(int playerNum, int damage)
    {
        playersHealth[playerNum] -= damage;
        //healthText[playerNum].text = playersHealthBars[playerNum].ToString();
        //healthBars[playerNum].localScale = new Vector3(playersHealthBars[playerNum] / 100f, 1, 1);

        if(playersHealth[playerNum]-1 > 0 )
        {
            return true;
        }  else
        {
            playersInGame--;
            CheckRoundOver();
            return false;
        }

    }

    void CheckRoundOver()
    {
        if(playersInGame != 3)
        {
            loseUI.SetActive(true);
            StartCoroutine(LoadLostScreen());
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
        StartCoroutine(LoadWinScreen());
    }

    public void PlayersLost(){

        loseUI.SetActive(true);
        StartCoroutine(LoadLostScreen());
    }

     IEnumerator LoadLostScreen()
    {
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("YouLost");
    }

    IEnumerator LoadWinScreen()
    {
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("Win");
    }

}
