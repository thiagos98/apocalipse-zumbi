using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameController : MonoBehaviour
{
    public InterfaceController interfaceController;
    private void Awake()
    {
        SetTimeScale(1);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOver()
    {
        interfaceController.GameOver();
    }

    public void UpdateLifePlayer()
    {
        interfaceController.UpdateSliderLifePlayer();
    }

    public static void SetTimeScale(int time)
    {
        Time.timeScale = time;
    }
}
