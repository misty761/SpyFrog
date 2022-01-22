using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject[] playerLives;
    public GameObject playing_UI;
    public GameObject title_UI;
    public GameObject gameover_UI;
    public GameObject cleared_UI;
    public GameObject[] goCountFruits;
    public Text textTimeRemain;
    public Text textScore;
    public Text textBestScore;
    public Text textStage;
    public AudioClip audioFanfare;

    public int countFruit;
    public int maxFruit = 8;
    public int playerLifeMax = 3;
    public int playerLifeLimit = 15;
    public int playerLife;
    public float timeMax = 120f;

    int score;
    int bestScore;
    public float timeRemain;
    float preAlertTime = -100f;
    public int currentStage;
    public int maxStage = 8;
    public float offsetEnemySpeed;

    AudioSource audioSource;
    
    public enum State
    {
        Title,
        Playing,
        Cleared,
        GameOver
    }

    public State state;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("GameManager : GameObject already exist!");
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        state = State.Title;
        currentStage = 1;
        FirstInitilize();
        Initialize();  
    }

    void FirstInitilize()
    {
        score = 0;
        textScore.text = "" + score;
        playerLife = playerLifeMax;
        textStage.text = "Stage " + currentStage;
        offsetEnemySpeed = 0f;
    }

    public void Initialize()
    {
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        textBestScore.text = "Top : " + bestScore;
        countFruit = 0;
        timeRemain = timeMax;

        // Title, Playing, Game over UI
        SetUI();

        // 먹은 과일 수 표시
        DisplayFruit();

        // 플레이어 라이프 표시
        DisplayPlayerLife();
    }

    

    // Update is called once per frame
    void Update()
    {
        // 남은 시간
        SetRemainTime();

        // stage clear
        CheckStageClear();

        // 다음 스테이지로 이동(개발용)
        if (Input.GetKeyUp(KeyCode.N)) countFruit = maxFruit;
    }

    void CheckStageClear()
    {
        if (state == State.Playing && countFruit >= maxFruit)
        {
            PlaySound(SoundManager.instance.audioFanfare, 0.1f);
            state = State.Cleared;
            if (currentStage < maxStage) currentStage++;
            else offsetEnemySpeed += 0.1f;
            SetUI();
        }
    }

    public void PlaySound(AudioClip audioClip, float volume)
    {
        if (SoundManager.instance.isSoundOn) audioSource.PlayOneShot(audioClip, volume);
    }

    void SetRemainTime()
    {
        if (state == State.Playing)
        {
            if (timeRemain > 0)
            {
                timeRemain -= Time.deltaTime;
                if (timeRemain > 10f)
                {
                    textTimeRemain.text = "Time : " + (int)timeRemain;
                }
                else
                {
                    textTimeRemain.text = "Time : " + "<color=#FF0000>" + (int)timeRemain + "</color>";
                }
                
            }
            else
            {
                ReducePlayerLife();
                if (playerLife > 0)
                {
                    timeRemain += 10f;
                }
            }

            // Alert
            if (timeRemain < 10f && Time.time > preAlertTime + Mathf.Lerp(0.1f, 2f, (float)timeRemain / 10f))
            {
                SoundManager.instance.PlaySound(SoundManager.instance.audioAlert, 1f);
                preAlertTime = Time.time;
            }
        }
    }

    void SetUI()
    {
        if (state == State.Title)
        {
            title_UI.SetActive(true);
            playing_UI.SetActive(false);
            gameover_UI.SetActive(false);
            cleared_UI.SetActive(false);
        }
        else if (state == State.Playing)
        {
            title_UI.SetActive(false);
            playing_UI.SetActive(true);
            gameover_UI.SetActive(false);
            cleared_UI.SetActive(false);
        }
        else if (state == State.GameOver)
        {
            title_UI.SetActive(false);
            playing_UI.SetActive(false);
            gameover_UI.SetActive(true);
            cleared_UI.SetActive(false);
        }
        else if (state == State.Cleared)
        {
            title_UI.SetActive(false);
            playing_UI.SetActive(false);
            gameover_UI.SetActive(false);
            cleared_UI.SetActive(true);
        }
    }

    public void StartGame()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.audioClick, 1f);
        state = State.Playing;
        SetUI();
    }

    public void StartStage()
    {
        Initialize();
        state = State.Playing;
        textStage.text = "Stage " + currentStage;
        SetUI();
        DisplayFruit();
    }

    public void ContinueGame()
    {
        if (currentStage == 1)
        {
            StartNewGame startNewGame = FindObjectOfType<StartNewGame>();
            startNewGame.ButtonPressed();
        }
        else
        {
            FirstInitilize();
            StartStage();
            SceneManager.LoadScene("Scene0" + currentStage);
        }
    }

    public void GameOver()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.audioGameOver, 1f);

        // best score
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
            textBestScore.text = "Top : " + bestScore;
        }

        state = State.GameOver;
        
        SetUI();
    }

    void DisplayFruit()
    {
        // 먹은 과일 개수 표시
        if (countFruit == 0)
        {
            goCountFruits[0].SetActive(false);
            goCountFruits[1].SetActive(false);
            goCountFruits[2].SetActive(false);
            goCountFruits[3].SetActive(false);
            goCountFruits[4].SetActive(false);
            goCountFruits[5].SetActive(false);
            goCountFruits[6].SetActive(false);
            goCountFruits[7].SetActive(false);
        }
        else if (countFruit == 1)
        {
            goCountFruits[0].SetActive(true);
            goCountFruits[1].SetActive(false);
            goCountFruits[2].SetActive(false);
            goCountFruits[3].SetActive(false);
            goCountFruits[4].SetActive(false);
            goCountFruits[5].SetActive(false);
            goCountFruits[6].SetActive(false);
            goCountFruits[7].SetActive(false);
        }
        else if (countFruit == 2)
        {
            goCountFruits[0].SetActive(true);
            goCountFruits[1].SetActive(true);
            goCountFruits[2].SetActive(false);
            goCountFruits[3].SetActive(false);
            goCountFruits[4].SetActive(false);
            goCountFruits[5].SetActive(false);
            goCountFruits[6].SetActive(false);
            goCountFruits[7].SetActive(false);
        }
        else if (countFruit == 3)
        {
            goCountFruits[0].SetActive(true);
            goCountFruits[1].SetActive(true);
            goCountFruits[2].SetActive(true);
            goCountFruits[3].SetActive(false);
            goCountFruits[4].SetActive(false);
            goCountFruits[5].SetActive(false);
            goCountFruits[6].SetActive(false);
            goCountFruits[7].SetActive(false);
        }
        else if (countFruit == 4)
        {
            goCountFruits[0].SetActive(true);
            goCountFruits[1].SetActive(true);
            goCountFruits[2].SetActive(true);
            goCountFruits[3].SetActive(true);
            goCountFruits[4].SetActive(false);
            goCountFruits[5].SetActive(false);
            goCountFruits[6].SetActive(false);
            goCountFruits[7].SetActive(false);
        }
        else if (countFruit == 5)
        {
            goCountFruits[0].SetActive(true);
            goCountFruits[1].SetActive(true);
            goCountFruits[2].SetActive(true);
            goCountFruits[3].SetActive(true);
            goCountFruits[4].SetActive(true);
            goCountFruits[5].SetActive(false);
            goCountFruits[6].SetActive(false);
            goCountFruits[7].SetActive(false);
        }
        else if (countFruit == 6)
        {
            goCountFruits[0].SetActive(true);
            goCountFruits[1].SetActive(true);
            goCountFruits[2].SetActive(true);
            goCountFruits[3].SetActive(true);
            goCountFruits[4].SetActive(true);
            goCountFruits[5].SetActive(true);
            goCountFruits[6].SetActive(false);
            goCountFruits[7].SetActive(false);
        }
        else if (countFruit == 7)
        {
            goCountFruits[0].SetActive(true);
            goCountFruits[1].SetActive(true);
            goCountFruits[2].SetActive(true);
            goCountFruits[3].SetActive(true);
            goCountFruits[4].SetActive(true);
            goCountFruits[5].SetActive(true);
            goCountFruits[6].SetActive(true);
            goCountFruits[7].SetActive(false);
        }
        else if (countFruit == 8)
        {
            goCountFruits[0].SetActive(true);
            goCountFruits[1].SetActive(true);
            goCountFruits[2].SetActive(true);
            goCountFruits[3].SetActive(true);
            goCountFruits[4].SetActive(true);
            goCountFruits[5].SetActive(true);
            goCountFruits[6].SetActive(true);
            goCountFruits[7].SetActive(true);
        }
    }

    void DisplayPlayerLife()
    {
        for (int i = 0; i < playerLives.Length; i++)
        {
            if (playerLife == i)
            {
                for (int j = 0; j < playerLives.Length; j++)
                {
                    if (i > j)
                    {
                        playerLives[j].SetActive(true);
                    }
                    else
                    {
                        playerLives[j].SetActive(false);
                    }
                }
            }
        }
    }

    public void AddScore(int point)
    {
        SoundManager.instance.PlaySound(SoundManager.instance.audioScore, 1f);
        score += point;
        textScore.text = "" + score;
        DisplayFruit();
    }

    public void ReducePlayerLife()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.audioDamaged, 1f);
        playerLife--;
        DisplayPlayerLife();
        if (playerLife <= 0)
        {
            GameOver();
        }
    }

    public void AddLife()
    {
        if (playerLife <= playerLifeLimit)
        {
            SoundManager.instance.PlaySound(SoundManager.instance.audioLifeUp, 1f);
            playerLife++;
            DisplayPlayerLife();
        }
        else
        {
            AddScore(10);
        }
    }
}
