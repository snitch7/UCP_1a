using System;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadSceneComponent : MonoBehaviour {
    float _timer = 0;
    public string _loadThisScene;

    private void Start() {
        GameManager.Instance.GetComponentInChildren<ScoreManager>().ResetScore();
    }

    void Update() {
        _timer += Time.deltaTime;

        if (_timer > 3) {
            SceneManager.LoadScene(_loadThisScene);
        }
    }
}