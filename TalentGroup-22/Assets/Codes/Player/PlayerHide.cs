using UnityEngine;
public class PlayerHide : MonoBehaviour
{
    public static PlayerHide objInstance = null;
    public delegate void Hide();
    public Hide fail = null;
    public Hide success = null;
    SpriteRenderer playerSprite = null;
    BoxCollider2D playerBoxCollider = null;
    GameObject playerMovementObject = null;
    public bool hasClicked = false;
    void Awake()
    {
        if (objInstance == null) objInstance = this;
        else if (objInstance != this) Destroy(gameObject);
    }
    void Success()
    {
        hasClicked = true;
        playerSprite.enabled = false;
        playerBoxCollider.isTrigger = true;
        playerMovementObject.SetActive(false);
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
        success += Success;
    }
    void Start()
    {
        playerMovementObject = GameObject.Find("PlayerMovement");
        playerSprite = Player.objInstance.GetComponent<SpriteRenderer>();
        playerBoxCollider = Player.objInstance.GetComponent<BoxCollider2D>();
        Fail();
    }
    void Update()
    {
        if 
        (
            Input.GetKeyDown(KeyCode.E)
        )
        {
            if (hasClicked) Fail();
            else if (Player.objInstance.isCollidingLocker) Success();
        }
    }
    void OnDisable()
    {
        fail -= Fail;
        success -= Success;
    }
}