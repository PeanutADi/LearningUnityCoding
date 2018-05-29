using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFactory : MonoBehaviour,ObjectFactory {

    public GameObject GetGameObject()
    {
        return (GameObject)Resources.Load("prefabs/MagicBall");
    }

}
