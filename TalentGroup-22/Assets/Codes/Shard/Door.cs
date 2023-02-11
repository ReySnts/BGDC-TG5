using System.Collections;
using UnityEngine;
public class Door : MonoBehaviour
{
    bool isOpened = false;
    IEnumerator LoadCredits()
    {
        yield return new WaitForSeconds(2f);
        SceneLevel_1.objInstance.ViewCredits();
    }
    void Update()
    {
        if 
        (
            !isOpened
            &&
            Player.objInstance.isTriggeringPuzzle
            &&
            Puzzle.objInstance.isSolved 
            && 
            Input.GetKeyDown(KeyCode.E)
        )
        {
            isOpened = true;
            AudioManager.instance.DoorSFX();
            StartCoroutine
            (
                LoadCredits()
            );
        }
    }
}
