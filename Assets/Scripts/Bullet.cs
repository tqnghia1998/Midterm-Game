using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bullet : MonoBehaviour
{
    // Loại đạn này có thể phá sắt không
    public bool destroyIron = false;

    [SerializeField]
    public bool toBeDestroyed = false, isPlayerBullet;
    GameObject brickGameObject, ironGameObject;
    Tilemap tilemap;
    public int speed = 1;
    Rigidbody2D rb2d;

	void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        // Gán vận tốc cho đạn
        rb2d.velocity = transform.up * speed;

        // Lấy reference tường gạch và tường sắt
        brickGameObject = GameObject.FindGameObjectWithTag("Brick");
        ironGameObject = GameObject.FindGameObjectWithTag("Iron");
    }

    // Cho đạn hoạt động
    private void OnEnable()
    {
        if (rb2d != null)
        {
            rb2d.velocity = transform.up * speed;
        }
    }

    // Xử lý va chạm
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Dừng đạn
        if (rb2d != null)
        {
            rb2d.velocity = Vector2.zero;
        }

        // Xác định va chạm vào layer nào
        tilemap = collision.gameObject.GetComponent<Tilemap>();

        // Nếu va chạm vào tăng thì trừ máu nó
        if (collision.gameObject.GetComponent<Health>() != null)
        {
            Health health = collision.gameObject.GetComponent<Health>();

            if ((isPlayerBullet && !health.isPlayer) || (!isPlayerBullet && health.isPlayer))
            {
                collision.gameObject.GetComponent<Health>().TakeDamage();
            }
        }

        // Trường hợp có thể phá vỡ tường
        if ((collision.gameObject == brickGameObject) || (destroyIron && collision.gameObject == ironGameObject))
        {
            Vector3 hitPosition = Vector3.zero;

            // Ẩn tường đi (1 tile)
            foreach (ContactPoint2D hit in collision.contacts)
            {
                hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
                hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
                tilemap.SetTile(tilemap.WorldToCell(hitPosition), null);
            }
        }

        // Disable đạn chứ không xóa để tận dụng cho lần sau
        this.gameObject.SetActive(false);
    }

    // Trường hợp đạn bị phá vỡ
    private void OnDisable()
    {
        if (toBeDestroyed)
        {
            Destroy(this.gameObject);
        }
    }

    // Khi tăng bị chết thì cũng xóa luôn đạn của nó
    public void DestroyBullet()
    {
        // Nếu đạn đang ở trạng thái disable thì xóa luôn
        if (gameObject.activeSelf == false)
        {
            Destroy(this.gameObject);
        }
        
        // Nếu không thì gắn cờ để bị xóa trong OnDisable
        toBeDestroyed = true;
    }
}
