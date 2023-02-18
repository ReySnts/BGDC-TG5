using UnityEngine;
public class Puzzle : MonoBehaviour
{
    public static Puzzle objInstance = null;
    public GameObject triggerGameObj = null;
    public GameObject uIGameObj = null;
    public bool isStarted = false;
    public bool isSolved = false;
    void Awake()
    {
        if (objInstance == null) 
        {
            objInstance = this;
            triggerGameObj = GameObject.Find("DoorTrigger");
            uIGameObj = GameObject.Find("PuzzleUI");
            triggerGameObj.SetActive(false);
            uIGameObj.SetActive(false);
        }
        else if (objInstance != this) Destroy(gameObject);
    }
    public void Activate()
    {
        Cursor.lockState = isStarted ? CursorLockMode.None : CursorLockMode.Locked;
        uIGameObj.SetActive(isStarted);
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
            {
                isStarted = false;
                Activate();
                isSolved = true;
            }
            else if 
            (
                Player.objInstance.isTriggeringPuzzle 
                && 
                Input.GetKeyDown(KeyCode.E)
            )
            {
                if (!isStarted) isStarted = true;
                else isStarted = false;
                Activate();
            }
        }
    }
}