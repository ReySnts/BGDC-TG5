using UnityEngine;
public class SceneLevel_1 : Scene
{
    protected override void RegisterGameObject()
    {
        gameObjects.Add
        (
            GameObject.Find("PlayerMovement")  
        );
        gameObjects.Add
        (
            GameObject.Find("PlayerHealth")  
        );
        gameObjects.Add
        (
            GameObject.Find("PlayerHide")  
        );
        gameObjects.Add
        (
            GameObject.Find("Score")  
        );
        gameObjects.Add
        (
            GameObject.Find("Puzzle")  
        );
        gameObjects.Add
        (
            GameObject.Find("ScoreUI")
        );
        gameObjects.Add
        (
            GameObject.Find("Blood")
        );
        gameObjects.Add
        (
            GameObject.Find("PuzzleUI")  
        );
    }
}