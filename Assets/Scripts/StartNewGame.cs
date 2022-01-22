using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartNewGame : MonoBehaviour
{
    public GameObject canvas;

    public void ButtonPressed()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.audioClick, 1f);
        Destroy(canvas);
        Destroy(GameManager.instance.gameObject);
        SceneManager.LoadScene("Scene01");
    }
}
