using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement objInstance = null;
    readonly string objName = "Player";
    Rigidbody2D playerRigidbody = null;
    Animator playerAnimator = null;
    float speed = 0f;
    float runSpeed = 4f;
    float normalSpeed = 2f;
    bool isRunning = false;
    bool isWalking = false;
    Vector2 movement = Vector2.zero;
    PlayerManager playerPosData;
    void Awake()
    {
        if (objInstance == null) objInstance = this;
        else if (objInstance != this) Destroy(gameObject);
        //this playerprefs
        playerPosData = FindObjectOfType<PlayerManager>();
        playerPosData.LoadData();
    }
    void Start()
    {
        playerRigidbody = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        playerAnimator = GameObject.Find("Player").GetComponent<Animator>();
        //  playerRigidbody = GameObject.Find(objName).GetComponent<Rigidbody2D>();
        //  playerAnimator = GameObject.Find(objName).GetComponent<Animator>();
       // playerRigidbody = Player.objInstance.gameObject.GetComponent<Rigidbody2D>();
       // playerAnimator = Player.objInstance.gameObject.GetComponent<Animator>();
    }
    void Update()
    {
        if 
        (
            Input.GetKey(KeyCode.W) 
            ||
            Input.GetKey(KeyCode.A) 
            ||
            Input.GetKey(KeyCode.S) 
            ||
            Input.GetKey(KeyCode.D) 
        )
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            if 
            (
                Input.GetKey(KeyCode.LeftShift) 
            )
            {
                if (!isRunning)
                {
                    #region Run
                    isRunning = true;
                    speed = runSpeed;
                 //   SoundManager.objInstance.walk.enabled = false;
                   // SoundManager.objInstance.run.enabled = true;
                    #endregion
                }
            }
            else 
            {
                isWalking = false;
                isRunning = false;
            }
            if (!isWalking) 
            {
                #region Walk
                isWalking = true;
                speed = normalSpeed;
           //     SoundManager.objInstance.run.enabled = false;
           //     SoundManager.objInstance.walk.enabled = true;
                #endregion
            }
        }
        else 
        {
            if (isWalking) 
            {
                isWalking = false;
             //   SoundManager.objInstance.walk.enabled = false;
            }
            if (isRunning) 
            {
                isRunning = false;
             //   SoundManager.objInstance.run.enabled = false;
            }
            movement = Vector2.zero;
        }
        #region Set Anim
        playerAnimator.SetFloat
        (
            "Horizontal", 
            movement.x
        );
        playerAnimator.SetFloat
        (
            "Vertical", 
            movement.y
        );
        playerAnimator.SetFloat
        (
            "Speed", 
            movement.sqrMagnitude
        );
        #endregion

        #region Set Audio
        AudioManager.instance.PlayerSFX();
        #endregion
    }
    void FixedUpdate()
    {
        try
        {
            playerRigidbody.MovePosition
            (
                playerRigidbody.position 
                + movement 
                * speed 
                * Time.fixedDeltaTime
            );
        }
        catch{}
    }
}