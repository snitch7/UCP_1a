using System;
using System.Collections;
using System.Timers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour {
    private Scenes scenes;
    private float _gameTimer = 0;
    private float[] _endLevelTimer = { 30, 30, 45 };
    private int _currentSceneNum = 0;
    private bool _gameEnding = false;

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
        _gameTimer = 0;
        SceneManager.LoadScene(GameManager.currentScene);
    }

    public void GameOver() {
        Debug.Log("ENDSCORE: " + GameManager.Instance.GetComponent<ScoreManager>().PlayersScore);
        SceneManager.LoadScene("gameOver");
    }

    public void BeginGame(int gameLevel) {
        SceneManager.LoadScene(gameLevel);
    }

    private void Update() {
        if (_currentSceneNum != SceneManager.GetActiveScene().buildIndex) {
            _currentSceneNum = SceneManager.GetActiveScene().buildIndex;
            GetScene();
        }

        GamerTimer();
    }

    private void GamerTimer() {
        switch (scenes) {
            case Scenes.level1:
            case Scenes.level2:
            case Scenes.level3: {
                if (_gameTimer < _endLevelTimer[_currentSceneNum - 3]) {
                    _gameTimer += Time.deltaTime;
                }
                else {
                    if (!_gameEnding) {
                        _gameEnding = true;
                        if (SceneManager.GetActiveScene().name != "level3") {
                            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerTransition>().LevelEnds =
                                true;
                        }
                        else {
                            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerTransition>().GameCompleted =
                                true;
                        }

                        Invoke("NextLevel", 4);
                    }
                }

                break;
            }
        }
    }

    private void  NextLevel() {
        _gameEnding = false;
        _gameTimer = 0;
        SceneManager.LoadScene(GameManager.currentScene + 1);
    }

    private void GetScene() {
        scenes = (Scenes)_currentSceneNum;
    }

}