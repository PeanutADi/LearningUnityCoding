using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
public interface AbstractActorFactory {
    AbstractActorFactory getInstance();
    GameObject getActor();
    void freeActor(GameObject p);
}
