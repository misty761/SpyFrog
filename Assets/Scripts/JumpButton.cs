using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpButton : MonoBehaviour
{
    public void ButtonDown()
    {
        if (GameManager.instance.state == GameManager.State.Playing)
        {
            PlayerControl player = FindObjectOfType<PlayerControl>();
            if (player.isGrounded || player.isUsingLadder)
            {
                if (player.isUsingLadder)
                {
                    player.NotUsingLadder();
                }
                SoundManager.instance.PlaySound(SoundManager.instance.audioJump, 2f);
                player.rb.AddForce(new Vector2(0, player.forceJump));
            }
        }
    }

    public void ButtonUp()
    {
        PlayerControl player = FindObjectOfType<PlayerControl>();
        player.bitJumpButtonUp = true;
    }
}
