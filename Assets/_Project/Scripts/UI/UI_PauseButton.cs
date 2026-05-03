using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class UI_PauseButton : MonoBehaviour
{
    [Header("Pause Menu Settings")]
    [SerializeField] private GameObject _pauseMenu;

    private void Update()
    {
        if (MenuState.IsOptionShowing == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        MenuState.MenuOpen();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _pauseMenu.SetActive(true);
        Debug.Log("Pause Game");
    }
}
