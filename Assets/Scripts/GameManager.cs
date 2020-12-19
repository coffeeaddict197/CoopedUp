using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameManager : MonoSingleton<GameManager>
{
    // Start is called before the first frame update
    [Header("Refenrece gameplay")]
    public BirdController bird;
    public Camera camera;
    [HideInInspector] public bool startPlay = false;

    [Header("Bug Point")]
    public int MaxBugPoints;
    [HideInInspector] public bool gameOver;

    //Setup role ID
    public static int ROPE_ID;
    [Header("Game Score")]
    const string KEY_HIGHTSCORE = "HightScore";
    int _bugPoints;
    int _highScore;
    int _gameScore;

    [Header("Game state")]
    public bool isPause;
    public int gameScore
    {
        get => _gameScore;
        set
        {
            _gameScore = value;
            e_SetScore?.Invoke(_gameScore);
        }
    }

    public int hightScore
    {
        get => _highScore;
        set
        {
            if (value > _highScore)
            {
                _highScore = value;
                PlayerPrefs.SetInt(KEY_HIGHTSCORE, value);
            }
        }
    }

    public int bugPoints
    {
        get => _bugPoints;
        set
        {
            _bugPoints = value;
            e_SetPoint?.Invoke(value);
        }
    }


    public static Action<int> e_SetPoint;
    public static Action<int> e_SetScore;

    private new void Awake()
    {
        hightScore = PlayerPrefs.GetInt(KEY_HIGHTSCORE, 0);
    }


    public void GameOverState()
    {
        hightScore = _gameScore;
        UIManager.Instance.ShowHightScorePanel();
        this.gameOver = true;
    }

    void ResetPointGame()
    {
        this.startPlay = false;
        this.gameOver = false;
        this.gameScore = 0;
        this.bugPoints = 0;
    }
    public void ResetGameScene()
    {
        bird.ResetState();
        camera.transform.position = new Vector3(0f, 0f, camera.transform.position.z);
        bird.transform.position = new Vector2(0 , -2f);
        GenerateMap.Instance.ResetAllObstacle();
        RespawnDecor.Instance.ResetDeccor();
        Hostile.instance.ResetPosition();
        this.ResetPointGame();
        UnPauseGame();
    }



    public void PauseGame() { isPause = true; Time.timeScale = 0f; }

    public void UnPauseGame() { isPause = false; Time.timeScale = 1f; }







}
