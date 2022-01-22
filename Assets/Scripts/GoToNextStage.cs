using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNextStage : MonoBehaviour
{
    private void Update()
    {
        // ���߿�
        if (Input.GetKeyUp(KeyCode.Return))
        {
            ButtonPress();
        }
    }

    public void ButtonPress()
    {
        int sceneNo = GameManager.instance.currentStage;
        SceneManager.LoadScene("Scene0" + sceneNo);      
    }
}
