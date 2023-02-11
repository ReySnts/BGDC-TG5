using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
    float restartTime = 5.0f;
    
    void Awake()
    {
        if (objInstance == null) objInstance = this;
        else if (objInstance != this) Destroy(gameObject);
    }

    public void AddScore()
    {
        score++;
        PlayerPrefs.SetInt("SavedScore", score);
        
        PlayerPrefs.SetInt
        (
            "SavedScore", 
            score
        );
    }
    void Start()
    {
        addScore += AddScore;
        score = PlayerPrefs.GetInt
        (
            "SavedScore", 
            0
        );
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
    }
    IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(restartTime);
        SceneLevel_1.objInstance.Restart();
    }
    void Update()
    {
        score = PlayerPrefs.GetInt
        (
            "SavedScore",
            0
        );
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        scoreText.text = score.ToString();
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