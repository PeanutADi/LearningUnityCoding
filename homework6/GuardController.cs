using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardController : MonoBehaviour
{
    private float pos_X, pos_Z;
    public float speed = 0.5f;
    private float dis = 0;
    private bool flag = true;
    public int state = 0;
    public GameObject role;//追捕的主角  
    int n = 0;
    public SceneControllor sceneController;
    bool isOver = false;
    // Use this for initialization  
    void Start()
    {
        sceneController = (SceneControllor)SSDirector.getInstance().currentScenceController;
        role = sceneController.role;//获得主角对象  
        pos_X = this.transform.position.x;
        pos_Z = this.transform.position.z;
    }

    void FixedUpdate()
    {     
        //Debug.Log(Vector3.Distance(this.transform.position, role.transform.position));
            if (state == 0)
        {
            patrol();//巡逻  
        }
        else if (state == 1)
        {
            chase(role);//追捕  
        }
        if (Vector3.Distance(this.transform.position, role.transform.position) <= 1)
        {
            isOver = true;
        }

    }

    private void OnGUI()
    {
        if (isOver)
        {
            GUIStyle fontStyle = new GUIStyle();
            fontStyle.normal.textColor = Color.red;   //设置字体颜色  
            fontStyle.fontSize = 40;       //字体大小  
            GUI.Label(new Rect(Screen.width/2-100, Screen.height/2, 200, 40), "GameOver",fontStyle);

        }
    }

    void patrol()
    {
        if (flag)
        {
            switch (n)
            {
                case 0:
                    pos_X += 2;
                    break;
                case 1:
                    pos_Z += 2;
                    break;
                case 2:
                    pos_X -= 2;
                    break;
                case 3:
                    pos_Z -= 2;
                    break;
            }
            flag = false;
        }
        dis = Vector3.Distance(transform.position, new Vector3(pos_X, 0.5f, pos_Z));
        if (dis > 0.7)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(pos_X, 0.5f, pos_Z), speed * Time.deltaTime);
            this.state = 0;
        }

        else
        {
            n++;
            n %= 4;
            flag = true;
            this.state = 0;
            Debug.Log("@@");
        }

        if (Vector3.Distance(this.transform.position, role.transform.position) < 3)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(pos_X, 0.5f, pos_Z), speed * Time.deltaTime);
            Debug.Log(this.transform.position);
            this.state = 1;
        }
    }

    void chase(GameObject role)
    {
        Debug.Log("Chancing...");
        transform.position = Vector3.MoveTowards(this.transform.position, role.transform.position,  speed * Time.deltaTime);
    }
}