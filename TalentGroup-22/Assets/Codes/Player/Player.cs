using UnityEngine;
public class Player : MonoBehaviour
{
    public static Player objInstance = null;
    public GameObject game_object = null;
    public bool isCollidingEnemy = false;
    public bool isCollidingLocker = false;
    public bool isTriggeringEnemyGhost = false;
    public readonly string lockerName = "Locker";
    public string lockerFullName = null;
    readonly string enemyName = "Enemy";
    readonly string enemyGhostName = "EnemyGhost";
    readonly string playerName = "Player";
    int enemyLen = 0;
    int enemyGhostLen = 0;
    int lockerLen = 0;
    void Awake()
    {
        if (objInstance == null) objInstance = this;
        else if (objInstance != this) Destroy(gameObject);
    }
    void OnEnable() 
    {
        game_object = GameObject.Find(playerName);
    }
    void Start()
    {
        enemyLen = enemyName.Length;
        enemyGhostLen = enemyGhostName.Length;
        lockerLen = lockerName.Length;
    }
    void OnCollisionStay2D(Collision2D other)
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
    void OnCollisionExit2D(Collision2D other)
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
        }
        catch {}
    }
    void Update() 
    {
        if (isTriggeringEnemyGhost) PlayerHide.objInstance.fail?.Invoke();
    }
}