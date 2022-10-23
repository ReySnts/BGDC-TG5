using System.Collections;
using UnityEngine;
public class Door : MonoBehaviour
{
    Animator animator = null;
    GameObject portal = null;
    bool isUnlocked = false;
    bool onClick = false;
    bool isOpened = false;
    void Start()
    {
        animator = GetComponent<Animator>();
        portal = GameObject.Find("Portal");
        portal.SetActive(false);
    }
    IEnumerator HoldDoor()
    {
        onClick = true;
        yield return new WaitForSeconds(1f);
        onClick = false;
    }
    void Update()
    {
        if 
        (
            !isUnlocked 
            && 
            Puzzle.objInstance.isSolved
        )
        {
            isUnlocked = true;
            portal.SetActive(true);
        }
        if 
        (
            isUnlocked 
            && 
            Player.objInstance.isTriggeringDoor 
            && 
            !onClick 
            && 
            Input.GetKeyDown(KeyCode.E)
        )
        {
            StartCoroutine
            (
                HoldDoor()
            );
            isOpened = isOpened ? false : true;
            animator.SetBool
            (
                "IsOpened",
                isOpened
            );
            AudioManager.instance.DoorOpen();
        }
    }
}
