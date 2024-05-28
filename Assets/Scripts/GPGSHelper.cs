using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using TMPro;
using UnityEngine.SocialPlatforms;

public class GPGSHelper : MonoBehaviour
{
    public TextMeshProUGUI txtLoginResult;

    public static GPGSHelper Instance => instance;
    private static GPGSHelper instance;
    GPGSHelper() { instance = this; }

    void Start()
    {
        var config = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        Login();
    }

    public void Login()
    {
        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptAlways, (success) =>
        {
            if (success == SignInStatus.Success)
                txtLoginResult.text = PlayGamesPlatform.Instance.localUser.userName;
            else
                txtLoginResult.text = "Failed";
        });
    }

    public void Logout()
    {
        ((PlayGamesPlatform)Social.Active).SignOut();
        txtLoginResult.text = "Logout";
    }

    public void ShowLeaderBoard()
    {
        PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard);
    }

    public void AddLeaderBoard(int stage)
    {
        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptAlways, (bsuccess) =>
        {
            if (bsuccess == SignInStatus.Success)
            {
                PlayGamesPlatform.Instance.ReportScore(stage, GPGSIds.leaderboard, (bool success) =>
                {
                    if (success)
                    {
                        GameManager.Instance.ShowAndroidToastMessage("랭킹 등록 성공!");
                    }
                });
            }
        });
    }
}