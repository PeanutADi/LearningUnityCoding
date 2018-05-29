using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGUI : MonoBehaviour {

    UserAction action;
    ObjectFactory factory;
    int currentCnt;
    GameObject[] myObj;
    int flag;
    StageDirector SD;

    GameObject obj1, obj2;

    int scene;
    int stage;

    int []stageFlag;

    //背景
    public Texture2D img;

    // Use this for initialization
    void Start () {
        factory = new ParticleFactory();
        action = new UserAction();
        SD = new StageDirector();
        currentCnt = 0;
        myObj = new GameObject[10];
        flag = -1;
        scene = 0;
        stage = 0;

        stageFlag = new int[3] { 0,0,0};
	}
	
	// Update is called once per frame
	void Update () {
        float movez = Input.GetAxis("Horizontal");
        float movex = Input.GetAxis("Vertical");
        action.moveObject(movez, movex);
    }

    private void OnGUI()
    {
        //最后的提示字体颜色
        GUIStyle fontStyle = new GUIStyle();
        fontStyle.normal.background = null;
        fontStyle.normal.textColor = new Color(1, 0, 0);
        fontStyle.fontSize = 20;

        if (scene == 0)
        {
            if (GUI.Button(new Rect(0, 0, 150, 50), "Get a magic ball"))
            {
                GameObject tmp = Instantiate(factory.GetGameObject());
                myObj[currentCnt] = tmp;
                currentCnt++;
            }
            if (GUI.Button(new Rect(0, 50, 150, 50), "Make the ball lager"))
            {
                action.largerObjcet();
            }
            if (GUI.Button(new Rect(0, 100, 150, 50), "Make the ball smaller"))
            {
                action.smallerObjcet();
            }
            if (GUI.Button(new Rect(0, 150, 150, 50), "Shine in different places"))
            {
                action.shine();
            }
            if (GUI.Button(new Rect(Screen.width - 100, 0, 100, 50), "Reset the ball"))
            {
                action.resetObject();
            }
            if (GUI.Button(new Rect(Screen.width /2 -50 , 0, 150, 50), "View some scenes"))
            {
                this.scene = 1;
            }
            for (int i = 0; i < currentCnt; i++)
            {
                if (GUI.Button(new Rect(10 + 120 * i, Screen.height - 30, 110, 30), "MagicBall" + (i + 1)))
                {
                    action.setObj(myObj[i]);
                    flag = i;
                }
            }
            if (flag != -1) GUI.Label(new Rect(10 + 120 * flag, Screen.height - 50, 110, 30), "Current control ball");

        }
        else if (scene == 1)
        {
            for (int i = 0; i < currentCnt; i++)
            {
                myObj[i].SetActive(false);
            }
            currentCnt = 0;
            if(stage == 0)
            {
                GUI.Label(new Rect(Screen.width / 2 - 200, Screen.height - 50, 400, 50), "Imagine a car drives at you,and barely stops before you ...");
                if (stageFlag[0] == 0)
                {
                    obj1 = Instantiate(factory.GetGameObject());
                    obj2 = Instantiate(factory.GetGameObject());
                    SD.carSituation(obj1, obj2);
                }
                stageFlag[0] = 1;
                if (GUI.Button(new Rect(Screen.width - 100, 0, 100, 50), "Next scene"))
                {
                    obj1.SetActive(false);
                    obj2.SetActive(false);
                    stage += 1;
                }
                if (GUI.Button(new Rect(Screen.width - 100, 50, 100, 50), "View again"))
                {
                    obj1.SetActive(false);
                    obj2.SetActive(false);
                    stageFlag[0] = 0;
                }
            }
            if(stage == 1)
            {
                GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height - 50, 400, 50), "Imagine you staring at a sky full of fireflies...");
                if (stageFlag[1] == 0)
                {
                    obj1 = Instantiate(factory.GetGameObject());
                    action.setObj(obj1);
                    action.skyShine();
                }
                stageFlag[1] = 1;
                if (GUI.Button(new Rect(Screen.width - 100, 0, 100, 50), "Next scene"))
                {
                    obj1.SetActive(false);
                    stage += 1;
                }
                
            }
            if(stage == 2)
            {
                GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height /2 -25, 400, 50), "No more scenes...",fontStyle);
                if(GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 +40, 150, 50), "Back to main menu"))
                {
                    stage = 0;
                    stageFlag[0] = stageFlag[1] = 0;
                    scene = 0;
                }

            }
        }
    }
}
