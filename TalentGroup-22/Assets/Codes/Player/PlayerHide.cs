using UnityEngine;
public class PlayerHide : MonoBehaviour
{
    public static PlayerHide objInstance = null;
    public delegate void Delegate();
    public Delegate fail = null;
    SpriteRenderer playerSprite = null;
    BoxCollider2D playerBoxCollider = null;
    GameObject playerMovementObject = null;
    readonly string playerMovementName = "PlayerMovement";
    public bool hasClicked = false;
    void Awake()
    {
        if (objInstance == null) objInstance = this;
        else if (objInstance != this) Destroy(gameObject);
    }
    void Fail()
    {
        hasClicked = false;
        playerSprite.enabled = true;
        playerBoxCollider.isTrigger = false;
        playerMovementObject.SetActive(true);
    }
    void OnEnable()
    {
        fail += Fail;
    }
    void Start()
    {
        playerMovementObject = GameObject.Find(playerMovementName);
        playerSprite = Player.objInstance.game_object.GetComponent<SpriteRenderer>();
        playerBoxCollider = Player.objInstance.game_object.GetComponent<BoxCollider2D>();
        Fail();
    }
    void Update()
    {
        if 
        (
            Input.GetKeyDown(KeyCode.E)
        )
        {
            if (!hasClicked) 
            {
                if (Player.objInstance.isCollidingLocker) 
                {
                    #region Success
                    hasClicked = true;
                    playerSprite.enabled = false;
                    playerBoxCollider.isTrigger = true;
                    playerMovementObject.SetActive(false);
                    #endregion
                }
            }
            else Fail();
        }
    }
    void OnDisable()
    {
        fail -= Fail;
    }
}