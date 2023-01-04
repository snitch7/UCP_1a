using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private static GameManager _instance;

    public static GameManager Instance => _instance;
    public static int CurrentScene = 0;
    public static int GameLevelScene = 3;

    private bool _died = false;

    public bool Died {
        get { return _died; }
        set { _died = value; }
    }

    private void Awake() {
        CheckGameManIsInScene();
        CurrentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        LightAndCameraSetup(CurrentScene);
    }

    private void LightAndCameraSetup(int sceneNumber) {
        switch (sceneNumber) {
            case 3:
            case 4:
            case 5:
            case 6: {
                LightSetup();
                CameraSetup();
                break;
            }
        }
    }

    void CameraSetup() {
        GameObject gameCamera = GameObject.FindGameObjectWithTag("MainCamera");

        gameCamera.transform.position = new Vector3(0, 0, -300);
        gameCamera.transform.eulerAngles = new Vector3(0, 0, 0);

        gameCamera.GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;
        gameCamera.GetComponent<Camera>().backgroundColor = new Color32(0, 0, 0, 255);
    }

    void LightSetup() {
        GameObject dirLight = GameObject.Find("Directional Light");
        dirLight.transform.eulerAngles = new Vector3(50, -30, 0);
        dirLight.GetComponent<Light>().color = new Color32(152, 204, 255, 255);
    }

    private void CheckGameManIsInScene() {
        if (_instance == null) {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else {
            Destroy(this.gameObject);
        }
    }

    void Start() {
    }


    void Update() {
    }
}