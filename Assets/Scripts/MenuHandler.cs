using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    public static MenuHandler Instance;
    private void Awake()
    {
        if (Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void StartGame()
    {
        print("Load main Scene");
        SceneManager.LoadScene(1);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        print("Exit Game");
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    
}
