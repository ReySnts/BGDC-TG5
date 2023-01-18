using UnityEngine;
using TMPro;
public class Score : MonoBehaviour
{
    public static Score objInstance = null;
    public delegate void Delegate();
    public Delegate addScore = null;
    TextMeshProUGUI scoreText = null;
    public readonly int maxScore = 7;
    int score = 0;
<<<<<<<< HEAD:TalentGroup-22/Assets/Codes/Player/Score.cs
========
    bool isMaxed = false;
>>>>>>>> rey:TalentGroup-22/Assets/Codes/Shard/Score.cs
    void Awake()
    {
        if (objInstance == null) objInstance = this;
        else if (objInstance != this) Destroy(gameObject);
    }

    public void AddScore()
    {
        score++;
<<<<<<<< HEAD:TalentGroup-22/Assets/Codes/Player/Score.cs
        PlayerPrefs.SetInt("SavedScore", score);
        
========
        PlayerPrefs.SetInt
        (
            "SavedScore", 
            score
        );
>>>>>>>> rey:TalentGroup-22/Assets/Codes/Shard/Score.cs
    }
    void OnEnable()
    {
        addScore += AddScore;
    }
    void Start()
    {
<<<<<<<< HEAD:TalentGroup-22/Assets/Codes/Player/Score.cs
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
        scoreText.text = score.ToString();
    }

    void OnDisable()
    {
        addScore -= AddScore;
    }
========
        score = PlayerPrefs.GetInt
        (
            "SavedScore", 
            0
        );
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        if 
        (
            !isMaxed 
            && 
            score >= maxScore
        ) 
        {
            isMaxed = true;
            Puzzle.objInstance.triggerGameObj.SetActive(true);
        }
        else scoreText.text = score.ToString();
    }
>>>>>>>> rey:TalentGroup-22/Assets/Codes/Shard/Score.cs
}