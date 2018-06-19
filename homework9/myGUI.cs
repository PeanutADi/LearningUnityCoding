using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myGUI : MonoBehaviour {
	private UserAction action;

	// Use this for initialization
	void Start () {
		action = SDirector.getInstance ().currentSceneController as UserAction;
	}

    // Update is called once per frame
    void OnGUI()
    {
        GUIStyle fontStyle = new GUIStyle();
        fontStyle.normal.background = null;
        fontStyle.normal.textColor = new Color(1, 0, 0);
        fontStyle.fontSize = 20;

        float width = Screen.width / 6;
        float height = Screen.height / 12;

        action.result();

		if (action.getResult()==1)
        { //赢了的话
			GUI.Label(new Rect(Screen.width/2, 15, 100, 100), "Win!", fontStyle);
            if (GUI.Button(new Rect(0, Screen.height - height, Screen.width, height), "Restart!"))
            {
                action.restart();
            }
        }
        else if (action.getResult()==2)
        { //输了的话
			GUI.Label(new Rect(Screen.width/2,15, 100, 100), "Lose!", fontStyle);
            if (GUI.Button(new Rect(0, Screen.height - height, Screen.width, height), "Restart!"))
            {
                action.restart();
            }
        }
        else
        { 
            if (GUI.Button(new Rect(0, 0, width, height), "PriestOnBoat"))
            {
				
                action.priestGetOn();
            }

            if (GUI.Button(new Rect( width, 0, width, height), "PriestOffBoat"))
            {
				action.result ();
                action.priestGetOff();
            }

            if (GUI.Button(new Rect(0, height, width, height), "DevilOnBoat"))
            {
                action.devilGetOn();
            }

            if (GUI.Button(new Rect(width, height, width, height), "DevilOffBoat"))
            {
				action.result ();
                action.devilGetOff();
            }

            if (GUI.Button(new Rect(2 * width, 0, width, height), "GO"))
            {
                action.moveBoat();
            }
			if (GUI.Button (new Rect (2 * width, height, width, height), "restart")) {
				action.restart ();
			}

            if (GUI.Button(new Rect(0, height * 2, width, height), "Next"))
            {
                action.result();
                action.clearBoat();
                action.actNext();
                //action.clearBoat();
            }
        }
    }
}
