using UnityEngine;
public class Player : MonoBehaviour
{
    public static Player objInstance = null;
    public bool isCollidingEnemy = false;
    public bool isCollidingLocker = false;
    public bool isTriggeringEnemyGhost = false;
    public bool isTriggeringPuzzle = false;
    public readonly string lockerName = "Locker";
    public string lockerFullName = null;
    readonly string enemyName = "Enemy";
    readonly string enemyGhostName = "EnemyGhost";
    int enemyLen = 0;
    int enemyGhostLen = 0;
    int lockerLen = 0;
    void Awake()
    {
        if (objInstance == null) objInstance = this;
        else if (objInstance != this) Destroy(gameObject);
    }
    void Start()
    {
        enemyLen = enemyName.Length;
        enemyGhostLen = enemyGhostName.Length;
        lockerLen = lockerName.Length;
    }
    void OnCollisionStay2D(Collision2D other)
    {
        try
        {
            if 
            (
                other.gameObject.name.Substring
                (
                    0,
                    enemyLen
                )
                == enemyName
            )
            isCollidingEnemy = true;
            if 
            (
                other.gameObject.name.Substring
                (
                    0,
                    lockerLen
                )
                == lockerName
            )
            {
                isCollidingLocker = true;
                lockerFullName = other.gameObject.name;
            }
        }
        catch{}
    }
    void OnCollisionExit2D(Collision2D other)
    {
        try
        {
            if 
            (
                other.gameObject.name.Substring
                (
                    0,
                    enemyLen
                )
                == enemyName
            )
            isCollidingEnemy = false;
            if 
            (
                other.gameObject.name.Substring
                (
                    0,
                    lockerLen
                )
                == lockerName
            )
            isCollidingLocker = false;
        }
        catch{}
    }
    void OnTriggerStay2D(Collider2D other)
    {
        try 
        {
            if 
            (
                other.gameObject.name.Substring
                (
                    0,
                    enemyGhostLen
                )
                == enemyGhostName
            )
            isTriggeringEnemyGhost = true;
            if 
            (
                other.gameObject.name
                ==
                Puzzle.objInstance.triggerGameObj.name
            ) 
            isTriggeringPuzzle = true;
        }
        catch {}
    }
    void OnTriggerExit2D(Collider2D other)
    {
        try 
        {
            if 
            (
                other.gameObject.name.Substring
                (
                    0,
                    enemyGhostLen
                )
                == enemyGhostName
            )
            isTriggeringEnemyGhost = false;
            if 
            (
                other.gameObject.name
                ==
                Puzzle.objInstance.triggerGameObj.name
            ) 
            isTriggeringPuzzle = false;
        }
        catch {}
    }
    void Update() 
    {
        if 
        (
            !PlayerHealth.objInstance.isDie
            &&
            PlayerHide.objInstance.hasClicked
            &&
            isTriggeringEnemyGhost
        ) 
        PlayerHide.objInstance.fail?.Invoke();
        if (Splash.hasHitPlayer) 
        {
            Splash.hasHitPlayer = false;
            Enemy.isWaitingToHit = false;
        }
        else Enemy.isWaitingToHit = true;
    }
    void OnDisable()
    {
        PlayerData.objInstance.SaveData();
    }
}