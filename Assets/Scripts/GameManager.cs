using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //public playerSettings settings;

    public int lives;
    public int score;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void NextLevel(int lives)
    {
        this.lives = lives;
        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
        this.lives++;
    }

    public void NewGame()
    {
        lives = 3;
    }
}
