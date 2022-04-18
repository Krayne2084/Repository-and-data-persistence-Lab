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
    }
    public void StartGame(TMP_InputField nameInput, GameObject nameWarning)
    {
        if(nameInput.text == "")
        {
            nameWarning.SetActive(true);
            return;
        }
        UpdateName(nameInput);
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void UpdateName(TMP_InputField nameInput)
    {
        PlayerName = nameInput.text;
    }

    public class Score
    {
        public string PlayerName;
        public int HighScore;
    }

    public void SaveHighScore(int score)
    {
        Score past = LoadHighScore();
        if (past == null)
        {
            past = new Score();
        }
        if(past.HighScore < score) 
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

    public void DisplayHighScore(TextMeshProUGUI highScoreDisplay)
    {
        Score score = LoadHighScore();
        if (score == null)
        {
            highScoreDisplay.text = "High Score: 0";
            return;
        }
        highScoreDisplay.text = $"High Score: {score.PlayerName}: {score.HighScore}";
    }

    public void ClearHighScore(TextMeshProUGUI highScoreDisplay)
    {
        if (File.Exists(SavePath))
        {
            File.Delete(SavePath);
        }
        DisplayHighScore(highScoreDisplay);
    }
}
