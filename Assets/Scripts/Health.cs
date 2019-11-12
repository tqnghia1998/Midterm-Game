using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    int totalHealth, currentHealth;

    [SerializeField]
    public bool isPlayer;

    Animator anime;
    Rigidbody2D rb2d;

    bool divineIntervention;

    void Start()
    {
        SetHealth();
        anime = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }	

    public void TakeDamage(int damage = 1, bool destroyedByPowerUp = false)
    {
        divineIntervention = destroyedByPowerUp;
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            rb2d.velocity = Vector2.zero;
            anime.SetTrigger("Killed");
            StartCoroutine(Death());
        }
    }

    public void SetHealth()
    {
        currentHealth = totalHealth;
    }

    public void SetInvincible()
    {
        currentHealth = 500;
    }

    IEnumerator Death()
    {
        GameplayManager GPM = GameObject.Find("Canvas").GetComponent<GameplayManager>();

        // Khi user chết thì sinh ra mới nếu còn mạng
        if (gameObject.CompareTag("UserTank"))
        {
            GPM.SpawnUser();
        }

        // Nếu Bot chết thì tăng biến đếm để cộng điểm
        else
        {
            if (gameObject.CompareTag("FastTank"))
            {
                GameManager.fastTankDestroyed++;
            }
            else if (gameObject.CompareTag("BigTank"))
            {
                GameManager.bigTankDestroyed++;
            }
            else if (gameObject.CompareTag("ArmoredTank"))
            {
                GameManager.armoredTankDestroyed++;
            }

            if (!divineIntervention)
            {
                // Create generate bonus
                if (gameObject.GetComponent<BonusTank>().IsBonusTankCheck()) GPM.GenerateBonusCrate();
            }
        }

        yield return new WaitForSeconds(0.5F);

        Destroy(gameObject);
    }

}
