using UnityEngine;
public class Puzzle : MonoBehaviour
{
    public static Puzzle objInstance = null;
    public GameObject triggerGameObj = null;
    public GameObject uIGameObj = null;
    public bool isStarted = false;
    public bool isSolved = false;
    const string PuzzleData = "IsPuzzleSolved";
    void Awake()
    {
        if (objInstance == null) objInstance = this;
        else if (objInstance != this) Destroy(gameObject);
    }
    void Start()
    {
        triggerGameObj = GameObject.Find("PuzzleTrigger");
        uIGameObj = GameObject.Find("PuzzleUI");
        triggerGameObj.SetActive(false);
        uIGameObj.SetActive(false);
    }
    void Update() 
    {
        if (!isSolved)
        {
            if 
            (
                ShardSlot.correctCoordinates.Count 
                == 
                Score.objInstance.maxScore
            )
            isSolved = true;
            else if 
            (
                Player.objInstance.isTriggeringPuzzle 
                && 
                Input.GetKeyDown(KeyCode.E)
            )
            {
                if (!isStarted) isStarted = true;
                else isStarted = false;
                uIGameObj.SetActive(isStarted);
            }
        }
    }
}