using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreloadScene : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
