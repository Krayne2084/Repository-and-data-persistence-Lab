using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;

public class MenuHandler : MonoBehaviour
{
    public static MenuHandler Instance;

    public string PlayerName;
    public static string SavePath;
    public TextMeshProUGUI HighScoreDisplay;
    public TMP_InputField NameInput;
    public GameObject NameWarning;
    private void Awake()
    {
        SavePath = Application.persistentDataPath + "/saveFile.json";
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        DisplayHighScore();
    }
    public void StartGame()
    {
        if(NameInput.text == null)
        {
            NameWarning.SetActive(true);
            return;
        }
        SceneManager.LoadScene(1);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void UpdateName()
    {
        PlayerName = NameInput.text;
    }

    public class Score
    {
        public string PlayerName;
        public int HighScore;
    }

    public void SaveHighScore(int score)
    {
        Score past = LoadHighScore();
        if (past.HighScore < score)
        {
            past.HighScore = score;
            past.PlayerName = PlayerName;
            string json = JsonUtility.ToJson(past);
            File.WriteAllText(SavePath, json);
        }
    }

    public Score LoadHighScore()
    {
        if (File.Exists(SavePath))
        {
            string json = File.ReadAllText(SavePath);
            Score data =  JsonUtility.FromJson<Score>(json);
            return data;
        }
        else
        {
            return null;
        }
    }

    void DisplayHighScore()
    {
        Score score = LoadHighScore();
        if (score == null)
        {
            return;
        }
        HighScoreDisplay.text = $"High Score: {score.PlayerName} - {score.HighScore}";
    }
}
