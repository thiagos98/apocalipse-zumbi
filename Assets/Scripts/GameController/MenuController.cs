using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject exitButton;

    private void Start()
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        exitButton.SetActive(true);
#endif
    }

    public void PlayGame()
    {
        StartCoroutine(ChangeScene(Scenes.Game));
    }

    private IEnumerator ChangeScene(string scene)
    {
        yield return new WaitForSecondsRealtime(0.2f);
        SceneManager.LoadScene(scene);
    }

    public void ExitGame()
    {
        StartCoroutine(Exit());
    }

    private IEnumerator Exit()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}