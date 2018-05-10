using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUserAction
{
    void ReStart();//重新开始  
}

public class UserInterface : MonoBehaviour
{
    private IUserAction action;
    public GameObject role;
    public float speed = 1;
    public SceneControllor sceneController;
    // Use this for initialization  
    void Start()
    {
        action = SSDirector.getInstance().currentScenceController as IUserAction;
        sceneController = (SceneControllor)SSDirector.getInstance().currentScenceController;
        role = sceneController.role;
    }
    void OnGUI()
    {
        GUIStyle fontstyle1 = new GUIStyle();
        fontstyle1.fontSize = 50;
        fontstyle1.normal.textColor = new Color(255, 255, 255);
        if (GUI.Button(new Rect(0, 0, 120, 40), "RESTART"))
        {
            action.ReStart();
        }
    }
    // Update is called once per frame  
    void Update()
    {
        if (sceneController.game != 0)//游戏开始状态才可以接受用户输入移动主角  
        {
            float translationX = 3*Input.GetAxis("Horizontal") * speed;
            float translationZ = 3*Input.GetAxis("Vertical") * speed;
            translationX *= Time.deltaTime;
            translationZ *= Time.deltaTime;
            role.transform.Translate(translationX, 0, 0);
            role.transform.Translate(0, 0, translationZ);
        }
    }
}