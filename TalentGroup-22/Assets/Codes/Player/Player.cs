using System.Collections;
using UnityEngine;
public class Player : MonoBehaviour
{
    public static Player objInstance = null;
    public bool isCollidingEnemy = false;
    public bool isCollidingLocker = false;
    public bool isTriggeringEnemyGhost = false;
    public bool isTriggeringDoor = false;
    public bool isTriggeringPortal = false;
    bool hasLoadedChoices = false;
    public readonly string lockerName = "Locker";
    public string lockerFullName = null;
    readonly string enemyName = "Enemy";
    readonly string enemyGhostName = "EnemyGhost";
    readonly string portalName = "Portal";
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
            #region Enemy AI
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
            #endregion
            #region Locker
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
                PlayerHide.objInstance.lockerSpriteRenderer = other.gameObject.GetComponent<SpriteRenderer>();
            }
            #endregion
        }
        catch{}
    }
    void OnCollisionExit2D(Collision2D other)
    {
        try
        {
            #region Enemy AI
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
            #endregion
            #region Locker
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
                isCollidingLocker = false;
                PlayerHide.objInstance.lockerSpriteRenderer = null;
            }
            #endregion
        }
        catch{}
    }
    void OnTriggerStay2D(Collider2D other)
    {
        try 
        {
            #region Portal
            if 
            (
                other.gameObject.name 
                == 
                portalName
            ) 
            isTriggeringPortal = true;
            #endregion
            #region EnemyGhost
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
            #endregion
            #region DoorTrigger
            if 
            (
                other.gameObject.name
                ==
                Puzzle.objInstance.triggerGameObj.name
            ) 
            isTriggeringDoor = true;
            #endregion
        }
        catch {}
    }
    void OnTriggerExit2D(Collider2D other)
    {
        try 
        {
            #region Portal
            if 
            (
                other.gameObject.name 
                == 
                portalName
            ) 
            isTriggeringPortal = false;
            #endregion
            #region EnemyGhost
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
            #endregion
            #region DoorTrigger
            if 
            (
                other.gameObject.name
                ==
                Puzzle.objInstance.triggerGameObj.name
            ) 
            isTriggeringDoor = false;
            #endregion
        }
        catch {}
    }
    IEnumerator LoadChoices()
    {
        yield return new WaitForSeconds(2f);
        SceneLevel_1.objInstance.NextScene();
    }
    void Update() 
    {
        #region Portal
        if 
        (
            isTriggeringPortal 
            && 
            !hasLoadedChoices
        ) 
        {
            hasLoadedChoices = true;
            AudioManager.instance.NotifDoor();
            StartCoroutine
            (
                LoadChoices()
            );
        }
        #endregion
        #region EnemyGhost
        if 
        (
            !PlayerHealth.objInstance.isDie
            &&
            PlayerHide.objInstance.hasClicked
            &&
            isTriggeringEnemyGhost
        ) 
        PlayerHide.objInstance.fail?.Invoke();
        #endregion
        #region EnemyGhostBoss
        if (Splash.hasHitPlayer) 
        {
            Splash.hasHitPlayer = false;
            Enemy.isWaitingToHit = false;
        }
        else Enemy.isWaitingToHit = true;
        #endregion
    }
    void OnDisable()
    {
        PlayerData.objInstance.SaveData();
    }
}