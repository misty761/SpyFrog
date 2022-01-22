using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContinueButton : MonoBehaviour
{
    GoogleMobileAdsReward googleAD;
    bool isPressed;
    
    // Start is called before the first frame update
    void Start()
    {
        googleAD = FindObjectOfType<GoogleMobileAdsReward>();

        isPressed = false;
    }

    public void TouchUp()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.audioClick, 1f);
        isPressed = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPressed)
        {
            if (googleAD.isAdFailedToLoad)
            {
                isPressed = false;
                GameManager.instance.ContinueGame();
                googleAD.MyLoadAD();
            }
            else
            {
                if (googleAD.rewardedAd.IsLoaded())
                {
                    isPressed = false;
                    googleAD.rewardedAd.Show();
                }
            }
        }

        if (googleAD.isRewarded && googleAD.bCloseAD)
        {
            GameManager.instance.ContinueGame();
            googleAD.MyLoadAD();
        }
    }
}
