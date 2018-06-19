using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class FirstControllor : MonoBehaviour, SceneHandle, UserAction,AINext {
	SDirector MySD;
	public int boatState = 0;
	//0=start,1=end,2=move from start shore,3=move from end shore

	public int gameResult = 0;
	//1=win,2=lose

	GameObject[] onBoat=new GameObject[2];
	GameObject boat;

	float gap = 1.5f;

	Stack<GameObject> SPriest = new Stack<GameObject>();
	Stack<GameObject> EPriest = new Stack<GameObject>();
	Stack<GameObject> SDevil = new Stack<GameObject>();
	Stack<GameObject> EDevil = new Stack<GameObject>();

	Vector3 boatStart = new Vector3 (-2f, 0.5f, 0);
	Vector3 boatEnd = new Vector3 (2f, 0.5f, 0);

	float speed=10;

    public int boatsize()
    {
        int cnt = 0;
        if(onBoat[0].name== "priest(Clone)" || onBoat[0].name == "devil(Clone)")
        {
            cnt++;
        }
        if(onBoat[1].name == "priest(Clone)" || onBoat[1].name == "devil(Clone)")
        {
            cnt++;
        }
        return cnt;
    }

	public void Load(){
		GameObject myGO = Instantiate<GameObject> (Resources.Load<GameObject> ("prefabs/main"), Vector3.zero, Quaternion.identity);
		myGO.name = "main";
		boat = Instantiate (Resources.Load ("prefabs/boat"), boatStart, Quaternion.identity)as GameObject;
		for (int i = 0; i < 3; i++) {
			SPriest.Push (Instantiate (Resources.Load ("prefabs/priest")) as GameObject);
			SDevil.Push (Instantiate (Resources.Load ("prefabs/devil"))as GameObject);
		}
	}		

	void Awakemethod(){
		SDirector d = SDirector.getInstance ();
		d.currentSceneController = this;
		d.currentSceneController.Load ();
	}

	void Awake(){
		Awakemethod ();
	}

	int boatCapacity(){
		int cnt = 0;
		for(int i=0;i<2;i++){
			if (onBoat [i] == null)
				cnt++;
		}
		return cnt;
	}

	void setCharacterPositions(Stack<GameObject> que, Vector3 pos) {
		GameObject[] array = que.ToArray();
		for (int i = 0; i < que.Count; ++i) {  
			array[i].transform.position = new Vector3(pos.x + gap*i, pos.y, pos.z);  
		}  
	}

	public void getOn(GameObject obj){
		if (boatCapacity () != 0) {
			obj.transform.parent = boat.transform;
			if (onBoat [0] == null) {
				onBoat [0] = obj;
				obj.transform.localPosition = new Vector3 (-0.2f, 1f, 0);
			} else {
				onBoat [1] = obj;
				obj.transform.localPosition = new Vector3 (0.2f, 1f, 0);
			}
		}
	}

	public void priestGetOn(){
		if (SPriest.Count != 0 && boatCapacity () != 0 && this.boatState == 0) {
			getOn (SPriest.Pop ());
		} else if (EPriest.Count != 0 && boatCapacity () != 0 && this.boatState == 1) {
			getOn (EPriest.Pop ());
		}
	}

	public void devilGetOn(){
		if (SDevil.Count != 0 && boatCapacity () != 0 && this.boatState == 0) {
			getOn (SDevil.Pop ());
		} else if (EDevil.Count != 0 && boatCapacity () != 0 && this.boatState == 1) {
			getOn (EDevil.Pop ());
		}
	}

	public void priestGetOff(){
		for(int i=0;i<2;i++){
			if(onBoat[i]!=null){
				if(this.boatState==1){
					if(onBoat[i].name=="priest(Clone)"){
						EPriest.Push (onBoat [i]);
						onBoat[i].transform.parent=null;
						onBoat [i] = null;
						break;
					}
				}
				else if(this.boatState==0){
					if(onBoat[i].name=="priest(Clone)"){
						SPriest.Push (onBoat [i]);
						onBoat[i].transform.parent=null;
						onBoat[i]=null;
						break;
					}
				}
			}
		}
	}

	public void devilGetOff(){
		for(int i=0;i<2;i++){
			if(onBoat[i]!=null){
				if(this.boatState==1){
					if(onBoat[i].name=="devil(Clone)"){
						EDevil.Push (onBoat [i]);
						onBoat[i].transform.parent=null;
						onBoat [i] = null;
						break;
					}
				}
				else if(this.boatState==0){
					if(onBoat[i].name=="devil(Clone)"){
						SDevil.Push (onBoat [i]);
						onBoat[i].transform.parent=null;
						onBoat[i]=null;
						break;
					}
				}
			}
		}
	}	

	public void moveBoat(){
		if(boatCapacity()==2)return ;
		/*if (boatState == 0)
			boatState = 2;
		else if (boatState == 1)
			boatState = 3;*/
		this.boatState += 2;
		//Debug.Log (boatState);
	}

	public void restart(){
		//Awakemethod ();
		EditorSceneManager.LoadScene(EditorSceneManager.GetActiveScene().name);
		boatState = gameResult = 0;
        status = boatStatus.none;
	}

	public int getResult(){
		return gameResult;
	}

    enum boatStatus
    {
        p,d,pp,dd,pd,none
    }

    boatStatus status=boatStatus.none;

    public int getNext()
    {
        return status.GetHashCode();
    }

    //public int boatState = 0;
    //0=start,1=end,2=move from start shore,3=move from end shore

    struct nextPassenger
    {
        public boatStatus state;
        public int nextBoat;
    }

    private int getRandom()
    {
        float num = Random.Range(0f, 1f);
        if (num <= 0.5f) return 1;
        else return 2;
    }

    nextPassenger next;

    public void nextAction()
    {
        next.nextBoat = boatState;
        if (gameResult != 0 )
        {
            return ;
        }

        else
        {
            if (next.nextBoat == 0 &&
                SPriest.Count == 3 &&
                SDevil.Count == 3)
            {
                int turn = getRandom();
                if (turn == 1)
                    next.state = boatStatus.pd;
                else
                    next.state = boatStatus.dd;
            }

            else if (next.nextBoat == 1 &&
                 SPriest.Count == 2 &&
                 SDevil.Count == 2)
            {
                next.state = boatStatus.p;
            }

            else if (next.nextBoat == 1 &&
                 SPriest.Count == 3 &&
                 SDevil.Count == 2)
            {
                next.state = boatStatus.d;
            }

            else if (next.nextBoat == 1 &&
                 SPriest.Count == 3 &&
                 SDevil.Count == 1)
            {
                next.state = boatStatus.d;
            }

            else if (next.nextBoat == 0 &&
                 SPriest.Count == 3 &&
                 SDevil.Count == 2)
            {
                next.state = boatStatus.dd;
            }

            else if (next.nextBoat == 1 &&
                 SPriest.Count == 3 &&
                 SDevil.Count == 0)
            {
                next.state = boatStatus.d;
            }

            else if (next.nextBoat == 0 &&
                 SPriest.Count == 3 &&
                 SDevil.Count == 1)
            {
                next.state = boatStatus.pp;
            }

            else if (next.nextBoat == 1 &&
                SPriest.Count == 1 &&
                SDevil.Count == 1)
            {
                next.state = boatStatus.pd;
            }

            else if (next.nextBoat == 0 &&
                SPriest.Count == 2 &&
                SDevil.Count == 2)
            {
                next.state = boatStatus.pp;
            }

            else if (next.nextBoat == 1 &&
                SPriest.Count == 0 &&
                SDevil.Count == 2)
            {
                next.state = boatStatus.d;
            }

            else if (next.nextBoat == 0 &&
                SPriest.Count == 0 &&
                SDevil.Count == 3)
            {
                next.state = boatStatus.dd;
            }

            else if (next.nextBoat == 1 &&
                SPriest.Count == 0 &&
                SDevil.Count == 1)
            {
                int turn = getRandom();
                if (turn == 1) next.state = boatStatus.d;
                else next.state = boatStatus.p;
            }

            else if (next.nextBoat == 0 &&
                SPriest.Count == 2 &&
                SDevil.Count == 1)
            {
                next.state = boatStatus.p;
            }

            else if (next.nextBoat == 0 &&
                SPriest.Count == 0 &&
                SDevil.Count == 2)
            {
                next.state = boatStatus.dd;
            }

            else if (next.nextBoat == 0 &&
                SPriest.Count == 1 &&
                    SDevil.Count == 1)
            {
                next.state = boatStatus.pd;
            }

        }
    }

    bool getOff = false;

    public void nextGetOn()
    {
        nextAction();

        if (next.state == boatStatus.d)
        {
            devilGetOn();
        }
        else if (next.state == boatStatus.dd)
        {
            devilGetOn();
            devilGetOn();
        }
        else if (next.state == boatStatus.p)
        {
            priestGetOn();
        }
        else if (next.state == boatStatus.pp)
        {
            priestGetOn();
            priestGetOn();
        }
        else if (next.state == boatStatus.pd)
        {
            priestGetOn();
            devilGetOn();
        }
        else
        {
            return ;
        }
        getOff = true;
    }

    public void nextGetOff()
    {
        while (boatCapacity() != 2)
        {
            devilGetOff();
            priestGetOff();
        }
        getOff = false;
    }

    public int actNext()
    {
        if (gameResult != 0) return 2;
        nextGetOn();
        moveBoat();
        return 1;
    }

    public void clearBoat()
    {
        Debug.Log("11");
        devilGetOff();
        devilGetOff();
        priestGetOff();
        priestGetOff();
    }

	public int result(){
		if (EPriest.Count == 3 && EDevil.Count == 3) {
			gameResult = 1;
		}
		int cntPriest = 0, cntDevil = 0;
		for (int i = 0; i < 2; i++) {
			if (onBoat [i] != null) {
				if (onBoat [i].name == "priest(Clone)")
					cntPriest++;
				else
					cntDevil++;
			}
		}
		int cntPriestStart = 0, cntPriestEnd = 0;
		int cntDevilStart = 0, cntDevilEnd = 0;
		if (boatState == 0) {
			cntPriestStart = SPriest.Count + cntPriest;
			cntDevilStart = SDevil.Count + cntDevil;
			cntPriestEnd = EPriest.Count;
			cntDevilEnd = EDevil.Count;
		} else if (boatState == 1) {
			cntPriestStart = SPriest.Count;
			cntDevilStart = SDevil.Count;
			cntPriestEnd = EPriest.Count + cntPriest;
			cntDevilEnd = EDevil.Count + cntDevil;
		}
		/*cntPriestStart = SPriest.Count;
		cntDevilStart = SDevil.Count;
		cntPriestEnd = EPriest.Count;
		cntDevilEnd = EDevil.Count;*/
		if ((cntPriestStart != 0 && cntPriestStart < cntDevilStart) || (cntPriestEnd != 0 && cntPriestEnd < cntDevilEnd)) {
			gameResult = 2;
		}
		return gameResult;
	}

	// Use this for initialization
	void Start () {
		
	}

		
	// Update is called once per frame
	void Update () {
		setCharacterPositions (SPriest, new Vector3 (-13f, 3, 0));
		setCharacterPositions (EPriest, new Vector3 (9f, 3, 0));
		setCharacterPositions (SDevil, new Vector3 (-8f, 3, 0));
		setCharacterPositions (EDevil, new Vector3 (4f, 3, 0));
		if (boatState == 2) {
			boat.transform.position = Vector3.MoveTowards (boat.transform.position, boatEnd, speed * Time.fixedDeltaTime);
			if (boat.transform.position == boatEnd) {
				boatState = 1;
				result ();
			}
			
		} else if (boatState == 3) {
			boat.transform.position = Vector3.MoveTowards (boat.transform.position, boatStart, speed * Time.fixedDeltaTime);
			if (boat.transform.position == boatStart) {
				boatState = 0;
				result ();
			}
		} 
	}
}
