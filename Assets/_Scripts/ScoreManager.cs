using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
    private int _playerScore;

    public int PlayerScore {
        get { return _playerScore; }
    }

    public void SetScore(int incomingScore) {
        _playerScore += incomingScore;
    }

    public void ResetScore() {
        _playerScore = 0000000;
    }
}
