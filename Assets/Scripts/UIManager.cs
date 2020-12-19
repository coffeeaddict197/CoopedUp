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
    }


    void TouchPlayAgain()
    {
        panelSetting.GetComponent<Tween_OutBack>().RollBack();
        GameManager.Instance.UnPauseGame();
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


    public void UpPointAnimation(int point)
    {
        if (point == 0)
        {
            for (int i = 0; i < PointBugs.Length; i++)
                PointBugs[i].SetActive(false);
            return;
        }
        PointBugs[point - 1].SetActive(true);

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
