using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    public int speed = 4;
    protected bool isMoving = false;

    protected virtual void MoveByPath() {}

    protected IEnumerator MoveHorizontal(float movementHorizontal, Rigidbody2D rb2d)
    {
        isMoving = true;

        Quaternion rotation = Quaternion.Euler(0, 0, -movementHorizontal * 90f);
        transform.rotation = rotation;

        var backUp = transform.position.x;
        
        float movementProgress = 0f;
        Vector2 movement, endPos;

        while (movementProgress < Mathf.Abs(movementHorizontal))
        {
            movementProgress += speed * Time.deltaTime;
            movementProgress = Mathf.Clamp(movementProgress, 0f, 1f);
            movement = new Vector2(speed * Time.deltaTime * movementHorizontal, 0f);
            endPos = rb2d.position + movement;

            if (movementProgress == 1)
            {
                endPos = new Vector2(backUp + movementHorizontal, endPos.y);
            }

            rb2d.MovePosition(endPos);

            yield return new WaitForFixedUpdate();
        }

        isMoving = false;
        MoveByPath();
    }

    protected IEnumerator MoveVertical(float movementVertical, Rigidbody2D rb2d)
    {
        isMoving = true;

        Quaternion rotation;

        if (movementVertical < 0)
        {
            rotation = Quaternion.Euler(0, 0, movementVertical * 180f);
        }
        else
        {
            rotation = Quaternion.Euler(0, 0, 0);
        }

        transform.rotation = rotation;

        var backUp = transform.position.y;

        float movementProgress = 0f;
        Vector2 endPos, movement;
 
        while (movementProgress < Mathf.Abs(movementVertical))
        {
            movementProgress += speed * Time.deltaTime;
            movementProgress = Mathf.Clamp(movementProgress, 0f, 1f);

            movement = new Vector2(0f, speed * Time.deltaTime * movementVertical);
            endPos = rb2d.position + movement;
            
            if (movementProgress == 1)
            {
                endPos = new Vector2(endPos.x, backUp + movementVertical);
            }

            rb2d.MovePosition(endPos);

            yield return new WaitForFixedUpdate();
        }

        isMoving = false;
        MoveByPath();
    }
}
