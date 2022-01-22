using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJumpPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            EnemyBehavior enemy = collision.gameObject.GetComponent<EnemyBehavior>();
            if (enemy.isGrounded)
            {
                if (enemy.isLookRight)
                {
                    enemy.rb.AddForce(new Vector2(enemy.jumpForce/4, enemy.jumpForce));
                }
                else
                {
                    enemy.rb.AddForce(new Vector2(-enemy.jumpForce/4, enemy.jumpForce));
                }
            }
        } 
    }
}
