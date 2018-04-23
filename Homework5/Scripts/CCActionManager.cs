using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCActionManager : SSActionManager,ISSActionCallback{

    public FirstSceneController controller;
    public List<CCAction> cActions;
    public int diskCnt = 0;

    private List<SSAction> used = new List<SSAction>();
    private List<SSAction> free = new List<SSAction>();

    public void SSActionEvent
        (SSAction source,
        ActionType actionType = ActionType.COMPETE,
        int i = 0,
        string str = null,
        Object obj = null)
    {
        if (source is CCAction)
        {
            diskCnt--;
            DiskFactory factory = Singleton<DiskFactory>.Instance;
            factory.FreeDisk(source.MyGameObject);
            
        }
    }

    SSAction GetSSAction()
    {
        SSAction action = null;
        if (free.Count > 0)
        {
            action = free[0];
            free.RemoveAt(0);
        }
        else
        {
            action = ScriptableObject.Instantiate<CCAction>(cActions[0]);
        }
        used.Add(action);
        return action;
    }

    public void FreeSSAction(SSAction wait)
    {
        SSAction action = null;
        foreach(SSAction ac in used)
        {
            if (wait.GetInstanceID() == ac.GetInstanceID())
            {
                action = ac;
            }
        }
        if (action!=null)
        {
            action.Reset();
            free.Add(action);
            used.Remove(action);
        }
    }

    // Use this for initialization
    protected new void Start() {
        controller = (FirstSceneController)SSDirector.Director.currentController;
        controller.actionManager = this;
        cActions.Add(CCAction.GetSSAction());
    }
	
	public void Throw(Queue<GameObject> diskQue)
    {
        foreach(GameObject disk in diskQue)
        {
            Run(disk, GetSSAction(), (ISSActionCallback)this);
        }
    }


}
