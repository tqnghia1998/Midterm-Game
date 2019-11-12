using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : Movement
{
    Rigidbody2D rb2d;
    float horizontal, vertical;
    BulletManager bullet;

    [SerializeField]
    LayerMask blockingLayer;
    enum Direction { Up, Down, Left, Right };

    public static bool freezing = false;

    public void ToFreezeTank()
    {
        CancelInvoke();
        StopAllCoroutines();
    }

    public void ToUnfreezeTank()
    {
        isMoving = false;
        RandomDirection();
        Invoke("AutoFire", Random.Range(0.5f, 1));     
    }

    void Start()
    {
        bullet = GetComponentInChildren<BulletManager>();
        rb2d = GetComponent<Rigidbody2D>();

        // Chọn ngẫu nhiên hướng cho Bot khi mới bắt đầu
        RandomDirection();

        // Đặt interval bắn đạn cho Bot
        Invoke("AutoFire", Random.Range(1, 3));
    }

    void AutoFire()
    {
        bullet.Fire();
        Invoke("AutoFire", Random.Range(1, 3));
    }

    public void RandomDirection()
    {
        // Hủy các interval random hướng hiện tại
        CancelInvoke("RandomDirection");
        
        // Tạo danh sách các hướng hợp lệ có thể chọn
        List<Direction> ranDirection = new List<Direction>();

        // Xác định xem liệu hướng hiện tại đang xét có hợp lệ không bằng cách thử di chuyển một bước
        if (!Physics2D.Linecast(transform.position, (Vector2)transform.position + new Vector2(1, 0), blockingLayer))
        {
            ranDirection.Add(Direction.Right);
        }
        if (!Physics2D.Linecast(transform.position, (Vector2)transform.position + new Vector2(-1, 0), blockingLayer))
        {
            ranDirection.Add(Direction.Left);
        }
        if (!Physics2D.Linecast(transform.position, (Vector2)transform.position + new Vector2(0, 1), blockingLayer))
        {
            ranDirection.Add(Direction.Up);
        }
        if (!Physics2D.Linecast(transform.position, (Vector2)transform.position + new Vector2(0, -1), blockingLayer))
        {
            ranDirection.Add(Direction.Down);
        }
        
        // Chọn ngẫu nhiên
        if (ranDirection.Count == 0)
        {
            return;
        }

        Direction selection = ranDirection[Random.Range(0, ranDirection.Count - 1)];
        
        if (selection == Direction.Up)
        {
            vertical = 1;
            horizontal = 0;
        }
        if (selection == Direction.Down)
        {
            vertical = -1;
            horizontal = 0;
        }
        if (selection == Direction.Right)
        {
            vertical = 0;
            horizontal = 1;
        }
        if (selection == Direction.Left)
        {
            vertical = 0;
            horizontal = -1;
        }

        // Cứ khoảng 3 đến 9s thì sẽ gọi lại hàm
        Invoke("RandomDirection", Random.Range(3, 5));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Khi gặp vật cản thì random hướng ngay lập tức (không cần chờ interval)
        RandomDirection();
        
        if (rb2d != null)
        {
            rb2d.velocity = Vector3.zero;
        }
    }

    // Dùng Fixed Update vì phải chờ tính toán xong
    private void FixedUpdate()
    {
        if (vertical != 0 && isMoving == false)
        {
            StartCoroutine(MoveVertical(vertical, rb2d));
        }
        else if (horizontal != 0 && isMoving == false)
        {
            StartCoroutine(MoveHorizontal(horizontal, rb2d));
        }
    }
}
