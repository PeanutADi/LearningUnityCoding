using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGUI : MonoBehaviour {

    private IUserAction action;

    bool first = true;

	// Use this for initialization
	void Start () {
        action = SSDirector.Director.currentController as IUserAction;
	}

    private void OnGUI()
    {
        if (Input.GetButtonDown("Fire1"))
        {

            Vector3 pos = Input.mousePosition;
            action.hit(pos);

        }

        //GUI.Label(new Rect(1000, 0, 400, 400), action.GetScore().ToString());

        Debug.Log("..?");

        if (first && GUI.Button(new Rect(700, 100, 90, 90), "Start"))
        {
            first = false;
            action.setGameState(GameState.ROUND_START);
        }

        Debug.Log("?..?");


        if (!first && action.GetGameState() == GameState.ROUND_FINISH && GUI.Button(new Rect(700, 100, 90, 90), "Next Round"))
        {
            Debug.Log("..??");
            action.setGameState(GameState.ROUND_START);
        }

    }
}
