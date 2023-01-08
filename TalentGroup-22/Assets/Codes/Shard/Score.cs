using UnityEngine;
using TMPro;
public class Score : MonoBehaviour
{
    public static Score objInstance = null;
    public delegate void Delegate();
    public Delegate addScore = null;
    TextMeshProUGUI scoreText = null;
    readonly string scoreTextObject = "ScoreText";
    public readonly int maxScore = 7;
    int score = 0;
    bool isMaxed = false;
    void Awake()
    {
        if (objInstance == null) objInstance = this;
        else if (objInstance != this) Destroy(gameObject);
    }
    void AddScore()
    {
        score++;
    }
    void OnEnable()
    {
        addScore += AddScore;
    }
    void Start()
    {
        scoreText = GameObject.Find(scoreTextObject).GetComponent<TextMeshProUGUI>();
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
    void OnDisable()
    {
        addScore -= AddScore;
    }
}