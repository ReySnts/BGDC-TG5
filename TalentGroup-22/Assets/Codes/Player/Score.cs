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
    void Awake()
    {
        if (objInstance == null) objInstance = this;
        else if (objInstance != this) Destroy(gameObject);
    }

    public void AddScore()
    {
        score++;
        PlayerPrefs.SetInt("SavedScore", score);
        
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
        scoreText.text = score.ToString();
    }

    void OnDisable()
    {
        addScore -= AddScore;
    }
}