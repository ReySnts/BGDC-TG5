using System.Collections;
using UnityEngine;
using TMPro;
public class Score : MonoBehaviour
{
    public static Score objInstance = null;
    public delegate void Delegate();
    public Delegate addScore = null;
    TextMeshProUGUI scoreText = null;
    readonly string scoreTextObject = "ScoreText";
    float restartTime = 2f;
    int score = 0;
    readonly int maxScore = 7;
    bool isMaxed = false;
    void Awake()
    {
        if (objInstance == null) objInstance = this;
        else if (objInstance != this) Destroy(gameObject);
    }
    void AddScore()
    {
        score++;
        SoundManager.objInstance.Crystal_get.Play();
    }
    void OnEnable()
    {
        addScore += AddScore;
    }
    void Start()
    {
        scoreText = GameObject.Find(scoreTextObject).GetComponent<TextMeshProUGUI>();
    }
    IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(restartTime);
        Scene.objInstance.Restart();
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
            StartCoroutine(RestartLevel());
        }
        else scoreText.text = score.ToString();
    }
    void OnDisable()
    {
        addScore -= AddScore;
    }
}