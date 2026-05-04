using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Menu : MonoBehaviour
{
    [Header("Menu Settings")]
    [SerializeField] private GameObject _optionsMenu;
    [SerializeField] private GameObject _choiceMenu;

    public void ContinueClicked()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
        MenuState.MenuClosed();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Debug.Log("Continue Game");
    }

    public void RestartClicked()
    {

        Time.timeScale = 1f;
        MenuState.MenuClosed();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        string path = Application.persistentDataPath + "/save.json";
        if (System.IO.File.Exists(path))
        {
            System.IO.File.Delete(path);
        }
        Debug.Log("Restart Game");
        FadeScreen.Instance.FadeAndLoad("Level1");
    }

    public void ReturnClicked()
    {
        _choiceMenu.SetActive(true);
        Debug.Log("Open Choice");
    }

    public void OptionsClicked()
    {
        MenuState.OptionOpen();
        gameObject.SetActive(false);
        _optionsMenu.SetActive(true);
        Debug.Log("Open Options");
    }

}
