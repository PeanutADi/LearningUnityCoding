using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSActionManager : MonoBehaviour {

    private List<SSAction> Add = new List<SSAction>();
    private List<int> Delete = new List<int>();

    private Dictionary<int, SSAction> ActionList = new Dictionary<int, SSAction>();

	// Use this for initialization
	protected void Start () {
		
	}
	
	// Update is called once per frame
	protected void Update () {
		foreach(SSAction ac in Add)
        {
            ActionList[ac.GetInstanceID()] = ac;
        }
        Add.RemoveAll(it => true);

        foreach(KeyValuePair<int,SSAction> pair in ActionList)
        {
            SSAction ac = pair.Value;
            if (ac.destroy == true)
            {
                Delete.Add(ac.GetInstanceID());
            }
            else if (ac.enable == true)
            {
                ac.Update();
            }
        }

        foreach(int key in Delete)
        {
            SSAction ac = ActionList[key];
            ActionList.Remove(key);
            DestroyObject(ac);
        }
        Delete.RemoveAll(it => true);
	}

    public void Run(GameObject gameObject,SSAction action,ISSActionCallback callback) 
    {
        action.MyGameObject = gameObject;
        action.Callback = callback;
        action.MyTransform = gameObject.transform;
        Add.Add(action);
        action.Start();
    }
}
