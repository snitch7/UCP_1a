using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour {
    private Scenes scenes;

    public enum Scenes {
        bootUp,
        title,
        shop,
        level1,
        level2,
        level3,
        gameOver
    }

    public void ResetScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOver() {
        Debug.Log("ENDSCORE: " + GameManager.Instance.GetComponent<ScoreManager>().PlayersScore);
        SceneManager.LoadScene("gameOver");
    }

    public void BeginGame() {
        SceneManager.LoadScene("testLevel");
    }
}