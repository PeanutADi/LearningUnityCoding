using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetGuard : MonoBehaviour,GetActor {
    public GameObject guard;
    public void onCollision(Collision collider)
    {
        Debug.Log(collider.gameObject.tag);
        if (collider.gameObject.tag == "Guard")
        {
            Debug.Log(collider.gameObject.name);
            guard = collider.gameObject;
        }
    }
}