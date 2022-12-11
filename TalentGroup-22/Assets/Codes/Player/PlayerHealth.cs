using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth objInstance = null;
    Image bloodImage = null;
    readonly string bloodObject = "Blood";
    public float currentHealth = 0f;
    float minimumHealth = 0f;
    float maximumHealth = 100f;
    float regenAmount = 5f;
    bool isDie = false;
    bool isRegen = false;
    void Awake()
    {
        if (objInstance == null) objInstance = this;
        else if (objInstance != this) Destroy(gameObject);
    }
    void Start()
    {
        bloodImage = GameObject.Find(bloodObject).GetComponent<Image>();
        currentHealth = maximumHealth;
    }
    IEnumerator HoldRegen()
    {
        yield return new WaitForSeconds(1f);
        isRegen = false;
        currentHealth += regenAmount;
    }
    void Update()
    {
        currentHealth = Mathf.Clamp
        (
            currentHealth,
            minimumHealth,
            maximumHealth
        );
        if
        (
            !isDie
            &&
            currentHealth == minimumHealth
        )
        {
            isDie = true;
            Scene.objInstance.Restart();
        }
        if
        (
            !Enemy.isHittingPlayer
            &&
            !isRegen
        )
        {
            isRegen = true;
            StartCoroutine
            (
                HoldRegen()
            );
        }
    }
}