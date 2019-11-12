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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb2d.velocity = Vector3.zero;
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
    }

    public void UpgradeTank()
    {
        if (level < 4)
        {
            level++;

            if (level == 2)
            {
                transform.Find("Crown").gameObject.GetComponent<SpriteRenderer>().enabled = true;
                transform.Find("Crown").gameObject.GetComponent<SpriteRenderer>().sprite = crownSprite;
                bullet.UpgradeProjectileSpeed();
            }
            else if (level == 3)
            {	
                transform.Find("Crown").gameObject.GetComponent<SpriteRenderer>().enabled = true;
                transform.Find("Crown").gameObject.GetComponent<SpriteRenderer>().sprite = crownGoldSprite;
                bullet.GenerateSecondCanonBall();
            }
            else if (level == 4)
            {
                transform.Find("Crown").gameObject.GetComponent<SpriteRenderer>().enabled = true;
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
