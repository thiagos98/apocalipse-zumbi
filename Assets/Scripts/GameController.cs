using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameController : MonoBehaviour {
    [FormerlySerializedAs("PanelGameOver")] public GameObject panelGameOver;

    private void Start()
    {
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SetPanelGameOver(bool other)
    {
        panelGameOver.SetActive(other);
    }
}
