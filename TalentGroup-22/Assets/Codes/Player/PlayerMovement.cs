using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement objInstance = null;
    Rigidbody2D playerRigidbody = null;
    Animator playerAnimator = null;
    float speed = 0f;
    float runSpeed = 4f;
    float normalSpeed = 2f;
    bool isRunning = false;
    bool isWalking = false;
    Vector2 movement = Vector2.zero;
    void Awake()
    {
        if (objInstance == null) objInstance = this;
        else if (objInstance != this) Destroy(gameObject);
    }
    void Start()
    {
        playerRigidbody = Player.objInstance.GetComponent<Rigidbody2D>();
        playerAnimator = Player.objInstance.GetComponent<Animator>();
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
                    AudioManager.instance.PlayerWalk(false);
                    AudioManager.instance.PlayerRun(true);
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
                AudioManager.instance.PlayerRun(false);
                AudioManager.instance.PlayerWalk(true);
                #endregion
            }
        }
        else 
        {
            if (isWalking) 
            {
                isWalking = false;
                AudioManager.instance.PlayerWalk(false);
            }
            if (isRunning) 
            {
                isRunning = false;
                AudioManager.instance.PlayerRun(false);
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
    }
    void FixedUpdate()
    {
        if (playerRigidbody != null) 
        {
            playerRigidbody.MovePosition
            (
                playerRigidbody.position 
                + movement 
                * speed 
                * Time.fixedDeltaTime
            );
        }
    }
}