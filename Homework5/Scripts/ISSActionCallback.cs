using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionType { START, COMPETE }

public interface ISSActionCallback  {
    void SSActionEvent
        (SSAction source,
        ActionType actionType = ActionType.COMPETE,
        int i = 0,
        string str = null,
        Object obj = null);
}
