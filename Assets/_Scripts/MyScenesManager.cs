using UnityEngine;
using UnityEngine.SceneManagement;

public class MyScenesManager : MonoBehaviour {
    private Scenes _scenes;

    public enum Scenes {
        Bootup,
        Title,
        Shop,
        Level01,
        Level02,
        Level03,
        Gameover
    }

    public void ResetScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOver() {
        SceneManager.LoadScene("Gameover");
    }

    public void BeginGame() {
        SceneManager.LoadScene("testLevel");
    }
}