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
    bool isMaxed = false;
    void Awake()
    {
        if (objInstance == null) objInstance = this;
        else if (objInstance != this) Destroy(gameObject);
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
    }
    void OnEnable()
    {
        addScore += AddScore;
    }
    void AddScore()
    {
        score++;
        PlayerPrefs.SetInt
        (
            "SavedScore", 
            score
        );
    }
    void Update()
    {
        score = PlayerPrefs.GetInt
        (
            "SavedScore",
            0
        );
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
    void OnDisable()
    {
        addScore -= AddScore;
    }
}