using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class RoleTrigger : MonoBehaviour
{
    public delegate void GameOver();//委托  
    public static event GameOver gameOver;//事件  
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Guard")
        {
            if (gameOver != null)
            {
                gameOver();//发布事件  
            }
        }
    }
}

public class SceneControllor : MonoBehaviour, ISceneController, IUserAction { 

    AbstractActorFactory factory;

    public GameObject role;
    public Text FinalText;
    public int game = 1;
    int size = 3;

    void Awake()
    {
        Debug.Log("111");
        SSDirector director = SSDirector.getInstance();
        director.setFPS(60);
        director.currentScenceController = this;
        director.currentScenceController.LoadResources();
    }
    public void LoadResources()    
    {
        role = Instantiate(Resources.Load("Role")) as GameObject;
        int pos_Z = 0, pos_X = 0 ;
        int cnt = 0;
        for (int i = 0; i < size; i++)
        {
            pos_X = i * 6;
            for (int j = 0; j < size; j++)
            {
                pos_Z = j * 6;
                Instantiate(Resources.Load("Maze"), new Vector3(pos_X, 0, pos_Z), Quaternion.identity);
                Instantiate(Resources.Load("Guard"), new Vector3(pos_X - 2, 0.5f, pos_Z), Quaternion.identity);
                //Debug.Log(Resources.Load("Guard"));
                pos_Z += 4;
            }
        }
    }
    // Use this for initialization  
    void Start()
    {
        RoleTrigger.gameOver += GameOver;
    }

    // Update is called once per frame  
    void Update()
    {

    }
    void GameOver()
    {
        FinalText.text = "Game Over!!!";
        game = 0;
    }

    public void ReStart()
    {
        game = 1;
    }
}