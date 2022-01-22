using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float speed = 0.5f;
    //public float distanceMoveX = 30f;
    //public float originalX;
    //public float originalY;
    public float timeMove = 0f;
    public float jumpForce = 130f;

    public bool isStopped;

    public bool isLookRight;
    public bool isGrounded;
    float timeAfterStart;

    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isLookRight = false;
        //originalX = transform.position.x;
        //originalY = transform.position.y;
        timeAfterStart = 0f;
        isStopped = true;
        isGrounded = false;
        speed += GameManager.instance.offsetEnemySpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.state != GameManager.State.Playing) return;

        if (GameManager.instance.state == GameManager.State.Cleared) return;

        // timeMove 지나고 나서 움직임
        if (isStopped) timeAfterStart += Time.deltaTime;
        if (timeAfterStart > timeMove) isStopped = false;
        if (isStopped) return;

        float currentX = transform.position.x;
        if (!isLookRight)
        {
            transform.Translate(Vector2.left * Time.deltaTime * speed);
            /*
            if (currentX > originalX - distanceMoveX)
            {
                transform.Translate(Vector2.left * Time.deltaTime * speed);
            }
            else
            {
                isLookRight = true;
            }
            */
        }
        else
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed);
            /*
            if (currentX < originalX + distanceMoveX)
            {
                transform.Translate(Vector2.right * Time.deltaTime * speed);
            }
            else
            {
                isLookRight = false;
            }
            */
        }

        // 적이 바라 보는 방향에 따라 스프라이트 회전
        if (!isLookRight) transform.localScale = new Vector2(1, 1);
        else transform.localScale = new Vector2(-1, 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.x > 0.9f || collision.contacts[0].normal.x < -0.9f)
        {
            // 충돌시 반대 방향으로 감
            isLookRight = !isLookRight;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0) 
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}
