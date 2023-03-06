using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth objInstance = null;
    GameObject bloodObject = null;
    Image bloodImage = null;
    Animator bloodAnimator = null;
    public float currentHealth = 0f;
    public float minimumHealth = 0f;
    float maximumHealth = 100f;
    float regenAmount = 5f;
    public bool isDie = false;
    public bool isRegen = false;
    void Awake()
    {
        if (objInstance == null) 
        {
            objInstance = this;
            bloodObject = GameObject.Find("Blood");
            bloodImage = bloodObject.GetComponent<Image>();
            bloodAnimator = bloodObject.GetComponent<Animator>();
            currentHealth = maximumHealth;
        }
        else if (objInstance != this) Destroy(gameObject);
    }
    IEnumerator HoldRegen()
    {
        yield return new WaitForSeconds(1f);
        isRegen = false;
        currentHealth += regenAmount;
        if 
        (
            Player.objInstance.isCollidingEnemy
            ||
            Player.objInstance.isTriggeringEnemyGhost
        ) 
        currentHealth -= regenAmount;
    }
    IEnumerator HoldRestart()
    {
        yield return new WaitForSeconds(2f);
        SceneLevel_1.objInstance.Restart();
    }
    void Update()
    {
        #region Set Die
        if
        (
            !isDie
            &&
            currentHealth == minimumHealth
        )
        {
            isDie = true;
            PlayerHide.objInstance.success?.Invoke();
            AudioManager.instance.GameOver();
            StartCoroutine
            (
                HoldRestart()
            );
        }
        #endregion
        #region Regen
        if (!isRegen) 
        {
            isRegen = true;
            StartCoroutine
            (
                HoldRegen()
            );
        }
        #endregion
        #region Set Blood Anim
        bloodAnimator.SetFloat
        (
            "currentHealth",
            currentHealth = Mathf.Clamp
            (
                currentHealth,
                minimumHealth,
                maximumHealth
            )
        );
        bloodAnimator.SetBool
        (
            "isWaitingToHit",
            Enemy.isWaitingToHit
        );
        bloodAnimator.SetBool
        (
            "isDie",
            isDie
        );
        #endregion
    }
}