using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public GameObject groundForPlayer;

    PlayerControl player;
    bool isPlayerAtCenter;

    private void Start()
    {
        player = FindObjectOfType<PlayerControl>();
        isPlayerAtCenter = false;
    }

    private void Update()
    {
        if (player.isUsingLadder)
        {
            groundForPlayer.SetActive(false);
        }
        else
        {
            groundForPlayer.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            player.isAtLadder = true;
    
            if (player.isUsingLadder)
            {
                if (!isPlayerAtCenter)
                {
                    float distance = player.transform.position.x - transform.position.x;
                    if (player.isGoingUpLadder || player.isGoingDownLadder)
                    {
                        if (distance > 0)
                        {
                            player.transform.Translate(Vector2.left * Time.deltaTime * player.speed);
                            distance = player.transform.position.x - transform.position.x;
                            if (distance < 0) isPlayerAtCenter = true;
                        }
                        else
                        {
                            player.transform.Translate(Vector2.right * Time.deltaTime * player.speed);
                            distance = player.transform.position.x - transform.position.x;
                            if (distance > 0) isPlayerAtCenter = true;
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            player.NotUsingLadder();
            isPlayerAtCenter = false;
        }
    }
}
