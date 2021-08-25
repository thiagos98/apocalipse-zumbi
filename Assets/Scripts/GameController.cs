using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameController : MonoBehaviour
{
    private InterfaceController _interfaceController;
    private void Awake()
    {
        SetTimeScale(1);
        _interfaceController = GameObject.FindWithTag(Tags.Canvas).GetComponent<InterfaceController>();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOver()
    {
        _interfaceController.GameOver();
    }

    public void UpdateLifePlayer()
    {
        _interfaceController.UpdateSliderLifePlayer();
    }

    public static void SetTimeScale(int time)
    {
        Time.timeScale = time;
    }

    public void UpdateAmountDeadZombies()
    {
        _interfaceController.UpdateAmountDeadZombies();
    }

    public void ShowUpWarningBossCreated()
    {
        _interfaceController.ShowUpWarningBossCreated();
    }
}
