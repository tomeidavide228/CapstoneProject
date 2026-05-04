using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeScreen : MonoBehaviour
{
    [SerializeField] private Image _blackScreen;
    [SerializeField] private float _fadeDuration = 0.5f;

    public static FadeScreen Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void FadeIn()
    {
        StartCoroutine(Fade(1));
    }

    public void FadeOut()
    {
        StartCoroutine(Fade(0));
    }

    public void FadeAndLoad(string sceneName)
    {
        StartCoroutine(FadeLoad(sceneName));
    }

    private IEnumerator Fade(float targetAlpha)
    {
        _blackScreen.gameObject.SetActive(true);

        float startAlpha = _blackScreen.color.a;
        float timer = 0f;

        while (timer < _fadeDuration)
        {
            timer += Time.deltaTime;
            float t = timer / _fadeDuration;

            Color color = _blackScreen.color;
            color.a = Mathf.Lerp(startAlpha, targetAlpha, t);
            _blackScreen.color = color;

            yield return null;
        }

        if (targetAlpha == 0)
            _blackScreen.gameObject.SetActive(false);
    }

    private IEnumerator FadeLoad(string sceneName)
    {
        yield return Fade(1); // fade in
        SceneManager.LoadScene(sceneName);
        yield return Fade(0); // fade out
    }
}
