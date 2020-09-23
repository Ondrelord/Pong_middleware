using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IngameMenu : MonoBehaviour
{
    public GameObject escapeMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (escapeMenu.activeSelf)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            escapeMenu.SetActive(!escapeMenu.activeSelf);
        }
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

}
