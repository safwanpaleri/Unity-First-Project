using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameSession : MonoBehaviour
{
    [SerializeField] int Health = 3;
    [SerializeField] Text LivesText;
    [SerializeField] Text ScoreText;
    int Score = 0;
    private void Awake()
    {
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
        LivesText.text = Health.ToString();
        ScoreText.text = Score.ToString();
    }
    public void DeathControl()
    {
        Health--;
        LivesText.text = Health.ToString();
        if(Health<=0)
        {
            SceneManager.LoadScene(0);
            FindObjectOfType<GameSession>().ResetEve();
        }
        else
        {
            int index = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(index);
        }
    }

    public void Addscore(int points)
    {
        Score += points;
        ScoreText.text = Score.ToString();
    }

    public void ResetEve()
    {
        Health = 3;
        Score = 0;
    }
}
