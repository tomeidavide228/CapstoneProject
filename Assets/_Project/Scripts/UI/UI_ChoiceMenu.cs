using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_ChoiceMenu : MonoBehaviour
{
    public void YesClicked()
    {
        FadeScreen.Instance.FadeAndLoad("MainMenu");
        Time.timeScale = 1f;
        string path = Application.persistentDataPath + "/save.json";
        if (System.IO.File.Exists(path))
        {
            System.IO.File.Delete(path);
        }
        Debug.Log("Yes: Return to Main Menu");
    }

    public void NoClicked()
    {
        gameObject.SetActive(false);
        Debug.Log("No: Return to Menu");
    }
}
