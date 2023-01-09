using UnityEngine;

public class ScoreManager : MonoBehaviour {
    private static int playerScore;
    public int PlayersScore => playerScore;

    public void SetScore(int incomingScore) {
        playerScore += incomingScore;
    }

    public void ResetScore() {
        playerScore = 00000000;
    }
}