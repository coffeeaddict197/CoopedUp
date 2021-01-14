using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
public class UIManager : MonoSingleton<UIManager>
{
    [Header("Game panel")]
    [SerializeField] GameObject[] PointBugs;
    [SerializeField] GameObject Panel_Frenzy;
    [SerializeField] TextMeshProUGUI text_Score;

    [Header("Intro panel")]
    [SerializeField] GameObject panelIntro;
    [SerializeField] Button b_TouchToPlay;

    //CHECKED nen chia ra thanh 1 file cs cho 1 panel rieng
    [Header("Setting panel")]
    [SerializeField] GameObject panelSetting;
    [SerializeField] Button b_Setting;
    [SerializeField] Button b_goHome;
    [SerializeField] Button b_Restart;
    [SerializeField] Button b_continueGame;

    [Header("High Score panel")]
    [SerializeField] GameObject panelHighScore;
    [SerializeField] Button b_playAgain;
    [SerializeField] TextMeshProUGUI textScore;
    [SerializeField] TextMeshProUGUI textHightScore;


    [Header("Credit panel")]
    [SerializeField] GameObject creditPanel;
    [SerializeField] Button credit_b_playAgain;
    [SerializeField] Toggle toggle_Music;
    [SerializeField] Toggle toggle_Sound;


    private void OnEnable()
    {
        SetEventButtonClick();

        GameManager.e_SetPoint += UpPointAnimation;
        GameManager.e_SetScore += SetScore;
        SetScore(0);
    }
    
    private void OnDisable()
    {
        GameManager.e_SetPoint -= UpPointAnimation;
        GameManager.e_SetScore -= SetScore;
    }

    private void Start()
    {
        ToggleMusicChangeValue(SoundManager.onMusic != 0);
        ToggleSoundChangeValue(SoundManager.onSound != 0);

        toggle_Music.isOn = (SoundManager.onMusic != 0);
        toggle_Sound.isOn = (SoundManager.onSound != 0);    
    }


    //SET BUTTON CLICK
    void SetEventButtonClick()
    {
        b_TouchToPlay.onClick.AddListener(TouchToPlayGame);
        b_Setting.onClick.AddListener(TouchSetting);
        b_Restart.onClick.AddListener(TouchToRestartGame);
        b_continueGame.onClick.AddListener(TouchPlayAgain);
        b_goHome.onClick.AddListener(TouchToBackHome);
        //High Score
        b_playAgain.onClick.AddListener(TouchToRestartGame);
        credit_b_playAgain.onClick.AddListener(TouchCombackHighScore);

        //Toggle music
        toggle_Music.onValueChanged.AddListener(ToggleMusicChangeValue);
        toggle_Sound.onValueChanged.AddListener(ToggleSoundChangeValue);
    }


    void TouchPlayAgain()
    {
        panelSetting.GetComponent<Tween_OutBack>().RollBack();
        GameManager.Instance.UnPauseGame();
        SoundManager.Instance.AwakeAllMusic();
    }


    void TouchSetting()
    {
        if (!GameManager.Instance.gameOver)
        {
            panelSetting.GetComponent<Tween_OutBack>().Execute();
            GameManager.Instance.PauseGame();
        }
        else
        {
            creditPanel.GetComponent<Credit_FadeOut>().FadeIn();
        }
        SoundManager.Instance.StopAllLoop();
    }

    void TouchToRestartGame()
    {
        if(GameManager.Instance.gameOver)
        {
            panelHighScore.GetComponent<Tween_EndGameFade>().FadeOut();
        }
        else
        {
            panelSetting.GetComponent<Tween_OutBack>().RollBack();
        }

        GameManager.Instance.ResetGameScene();
    }

    void TouchToBackHome()
    {
        panelSetting.GetComponent<Tween_OutBack>().RollBack();
        panelIntro.GetComponent<Tween_UIColorFadeOut>().FadeIn();
    }
    void TouchToPlayGame()
    {
        GameManager.Instance.ResetGameScene();
    }

    void TouchCombackHighScore()
    {
        creditPanel.GetComponent<Credit_FadeOut>().FadeOut();
    }

    public void ShowHightScorePanel()
    {
        panelHighScore.SetActive(true);
        textHightScore.text = GameManager.Instance.hightScore.ToString();
        textScore.text = GameManager.Instance.gameScore.ToString();
        panelHighScore.GetComponent<Tween_EndGameFade>().FadeIn();
    }


    void ToggleMusicChangeValue(bool check)
    {
        toggle_Music.isOn = check;
        SoundManager.Instance.CheckToggleMusic(check);
        if(toggle_Music.isOn)
        {
            toggle_Music.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/music_up");
        }
        else
        {
            toggle_Music.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/music_over");

        }
    }

    void ToggleSoundChangeValue(bool check)
    {
        toggle_Sound.isOn = check;
        SoundManager.Instance.CheckToggleSound(check);
        if (toggle_Sound.isOn)
        {
            toggle_Sound.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/sound_up");
        }
        else
        {
            toggle_Sound.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/sound_over");

        }
    }


    public void UpPointAnimation(int point)
    {
        if (point == 0)
        {
            for (int i = 0; i < PointBugs.Length; i++)
                PointBugs[i].SetActive(false);
            return;
        }
        for(int i = 0; i < point; i++)
            PointBugs[i].SetActive(true);

    }

    public void SetScore(int score)
    {
        text_Score.text = score.ToString();
    }

    public void FrenzyBackgroundToggle()
    {
        Panel_Frenzy.SetActive(!Panel_Frenzy.activeSelf);
    }
}
