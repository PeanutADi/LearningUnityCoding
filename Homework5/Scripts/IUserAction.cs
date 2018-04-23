using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { START,RUNNING,ROUND_START,ROUND_FINISH,PAUSE}

public interface IUserAction {

    void GameOver();
    GameState GetGameState();
    void setGameState(GameState gameState);
    int GetScore();
    void hit(Vector3 position);

}
