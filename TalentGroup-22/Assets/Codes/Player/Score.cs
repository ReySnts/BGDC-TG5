using System.Collections;
using UnityEngine;
using TMPro;
public class Score : MonoBehaviour
{
    public static Score objInstance = null;
    public delegate void Delegate();
    public Delegate addScore = null;
    TextMeshProUGUI scoreText = null;
    float restartTime = 2f;
    int score = 0;
    readonly int maxScore = 7;
    bool isMaxed = false;

   PlayerManager playerScore;

    void Awake()
    {
        if (objInstance == null) objInstance = this;
        else if (objInstance != this) Destroy(gameObject);
       
    }


    public void AddScore()
    {
        score++;
    }

    void OnEnable()
    {
        addScore += AddScore;
    }
    void Start()
    {
        score = PlayerPrefs.GetInt("SavedScore", 0);
        
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
    }

    IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(restartTime);
        SceneCodes.objInstance.Restart();
    }

    void Update()
    {
        if (!isMaxed && score >= maxScore)
        {
            isMaxed = true;
          //  StartCoroutine(RestartLevel());
        }
        else
        {
            PlayerPrefs.SetInt("SavedScore", score);
            scoreText.text = score.ToString();
        }

        
    }

    void OnDisable()
    {
        addScore -= AddScore;
    }

}