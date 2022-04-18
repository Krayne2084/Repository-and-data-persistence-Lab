using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuInterface : MonoBehaviour
{

    public TextMeshProUGUI HighScoreDisplay;
    public TMP_InputField NameInput;
    public GameObject NameWarning;
    private void Start()
    {
        MenuHandler.Instance.DisplayHighScore(HighScoreDisplay);
    }
    public void StartGame()
    {
        MenuHandler.Instance.StartGame(NameInput, NameWarning);
    }
    public void ExitGame()
    {
        MenuHandler.Instance.ExitGame();
    }
    public void ClearScore()
    {
        MenuHandler.Instance.ClearHighScore(HighScoreDisplay);
    }
}
