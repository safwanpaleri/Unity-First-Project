//Headers.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameSession : MonoBehaviour
{
    //Variables.
    [SerializeField] int Health = 3;
    [SerializeField] Text LivesText;
    [SerializeField] Text ScoreText;
    int Score = 0;
    
    //The first Function to activate, before start() function.
    private void Awake()
    {
        //if already a script exist, keep old one and delete newly created script.
        //the old one have have score, health etc, while the new one is just created which will have score = 0 for example.
        int noOfgamesession = FindObjectsOfType<GameSession>().Length;
        if(noOfgamesession>1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }    
    }
    
    void Start()
    {
        //for UI purpose. Converting Number variable into String.
        LivesText.text = Health.ToString();
        ScoreText.text = Score.ToString();
    }
    
    //Taking Damage and changing in UI
    public void DeathControl()
    {
        Health--;
        LivesText.text = Health.ToString();
        //if the player dies.
        if(Health<=0)
        {
            //Load Game Over Scene and reset score and Health
            SceneManager.LoadScene(0);
            FindObjectOfType<GameSession>().ResetEve();
        }
        else
        {
            //if the player is still alive,
            //then start from beginning of level.
            int index = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(index);
        }
    }

    //Adding score and changing in UI
    public void Addscore(int points)
    {
        Score += points;
        ScoreText.text = Score.ToString();
    }

    //reseting the health and Score to default one
    //Called after death of player.
    public void ResetEve()
    {
        Health = 3;
        Score = 0;
    }
}
