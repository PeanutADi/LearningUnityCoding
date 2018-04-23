using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreRecorder : MonoBehaviour
{
    private int score;

    public ScoreRecorder()
    {
        score = 0;
    }

    public void record(DiskData disk)
    {
        score += disk.score;
    }
    
    public void Reset()
    {
        score = 0;
    }

    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
        }
    }

} 

public class FirstSceneController : MonoBehaviour,ISceneController,IUserAction {

    public CCActionManager actionManager { get; set; }

    public ScoreRecorder Recorder { get; set; }

    public Queue<GameObject> diskQue = new Queue<GameObject>();

    public int diskCnt;

    private float Gap = 0f;

    private GameState gameState = GameState.START;

    public int round = -1;

    private void NextRound()
    {
        round = (round + 1) % 3;
        DiskFactory factory = Singleton<DiskFactory>.Instance;
        for(int i = 0; i < diskCnt; i++)
        {
            diskQue.Enqueue(factory.GetDisk(round));
        }

        actionManager.Throw(diskQue);
    }

    void Awake()
    {
        SSDirector director = SSDirector.Director;
        director.currentController = this;
        diskCnt = 10;
        this.gameObject.AddComponent<ScoreRecorder>();
        this.gameObject.AddComponent<DiskFactory>();
        Recorder = Singleton<ScoreRecorder>.Instance;
        director.currentController.Load();
    }

	// Use this for initialization
	void Start () {
        Awake();
	}
	
	// Update is called once per frame
	private void Update () {
		if(actionManager.diskCnt == 0 && gameState == GameState.RUNNING)
        {
            gameState = GameState.ROUND_FINISH;
        }

        if (actionManager.diskCnt == 0 && gameState == GameState.ROUND_START)
        {
            NextRound();
            actionManager.diskCnt = 10;
            gameState = GameState.RUNNING;
        }

        if (Gap > 1)
        {
            ThrowDisk();
            Gap = 0;
        }
        else
        {
            Gap += Time.deltaTime;
        }
	}

    private void ThrowDisk()
    {
        if (diskQue.Count > 0)
        {
            GameObject disk = diskQue.Dequeue();

            float dy = UnityEngine.Random.Range(0f, 4f);
            disk.transform.position = new Vector3(-disk.GetComponent<DiskData>().direction.x * 7, dy, 0);

            disk.SetActive(true);
        }
    }

    public void Load()
    {
        //载入背景
    }

    public void GameOver()
    {
        GUIStyle fontStyle = new GUIStyle();
        fontStyle.normal.textColor = Color.red;
        fontStyle.fontSize = 40;
        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 20, 300, 200), "GAME OVER" , fontStyle);

    }

    public int GetScore()
    {
        return Recorder.Score;
    }

    public GameState GetGameState()
    {
        return gameState;
    }

    public void setGameState(GameState gs)
    {
        gameState = gs;
    }

    public void hit(Vector3 pos)
    {
        Ray ray = Camera.main.ScreenPointToRay(pos);

        RaycastHit[] hit;
        hit = Physics.RaycastAll(ray);
        for(int i = 0; i < hit.Length; i++)
        {
            RaycastHit oneHit = hit[i];
            if (oneHit.collider.gameObject.GetComponent<DiskData>() != null)
            {
                Recorder.record(oneHit.collider.gameObject.GetComponent<DiskData>());
                oneHit.collider.gameObject.transform.position = new Vector3(0, -5, 0);
            }
        }

    }

}
