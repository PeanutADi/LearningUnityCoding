using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolFactory : System.Object,AbstractActorFactory {

    private static PatrolFactory instance;

    public AbstractActorFactory getInstance()
    {
        if (instance == null)
        {
            instance = new PatrolFactory();
        }
        return instance;
    }

    public GameObject getActor()
    {
        GameObject patrol = GameObject.Instantiate<GameObject>(
                Resources.Load<GameObject>("Prefabs/Patrol")); ;
        return patrol;
    }

    public void freeActor(GameObject p)
    {
        p.SetActive(false);
    }
}
