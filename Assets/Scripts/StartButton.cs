using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.instance.StartGame();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return)) StartGame();
    }
}
