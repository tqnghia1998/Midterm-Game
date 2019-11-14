using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : Movement
{
    float horizontal, vertical;
    Rigidbody2D rb2d;
    BulletManager bullet;
    public int level = 1;

    [SerializeField]
    Sprite crownSprite, crownGoldSprite, crownDiamondSprite;

    void Start ()
    {
        bullet = GetComponentInChildren<BulletManager>();
        rb2d = GetComponent<Rigidbody2D>();

        if (GameManager.playerLevel > 1)
        {
            transform.Find("Crown").gameObject.GetComponent<SpriteRenderer>().sprite = crownSprite;
            bullet.level = 2;

            if (GameManager.playerLevel > 2)
            {
                transform.Find("Crown").gameObject.GetComponent<SpriteRenderer>().sprite = crownGoldSprite;
                bullet.level = 3;
                
                if (GameManager.playerLevel > 3)
                {
                    transform.Find("Crown").gameObject.GetComponent<SpriteRenderer>().sprite = crownDiamondSprite;
                    bullet.level = 4;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (rb2d != null)
        {
            rb2d.velocity = Vector3.zero;
            horizontal = 0;
            vertical = 0;

            float currX = transform.position.x;
            float currY = transform.position.y;

            // Round to nearest .5
            transform.position = new Vector2(Mathf.Round(currX * 2) / 2, Mathf.Round(currY * 2) / 2);
        }
    }

    private void FixedUpdate()
    {
        // Nếu có độ dời ngang và chưa di chuyển
        if (horizontal != 0 && !isMoving)
        {
            StartCoroutine(MoveHorizontal(horizontal, rb2d));
        }

        // Tương tự cho độ dời dọc
        else if (vertical != 0 && !isMoving) 
        {
            StartCoroutine(MoveVertical(vertical, rb2d));
        }
        
        if (horizontal == 0 && vertical == 0)
        {
            rb2d.velocity = Vector3.zero;
        }
    }

    public void UpgradeTank()
    {
        if (level < 4)
        {
            level++;

            if (level == 2)
            {
                transform.Find("Crown").gameObject.GetComponent<SpriteRenderer>().sprite = crownSprite;
                bullet.UpgradeProjectileSpeed();
            }
            else if (level == 3)
            {	
                transform.Find("Crown").gameObject.GetComponent<SpriteRenderer>().sprite = crownGoldSprite;
                bullet.GenerateSecondCanonBall();
            }
            else if (level == 4)
            {
                transform.Find("Crown").gameObject.GetComponent<SpriteRenderer>().sprite = crownDiamondSprite;
                bullet.CanonBallPowerUpgrade();
            }
        }
    }

    void Update ()
    {
        // Liên tục cập nhật từ bàn phím
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        // Sự kiện bắn
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bullet.Fire();
        }
    }
}
