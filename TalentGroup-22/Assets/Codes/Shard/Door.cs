using System.Collections;
using UnityEngine;
public class Door : MonoBehaviour
{
    bool isOpened = false;
    IEnumerator LoadChoices()
    {
        yield return new WaitForSeconds(2f);
        SceneLevel_1.objInstance.NextScene();
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
            AudioManager.instance.DoorOpen();
            StartCoroutine
            (
                LoadChoices()
            );
        }
    }
}
