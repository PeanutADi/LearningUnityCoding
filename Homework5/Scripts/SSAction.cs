using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSAction : ScriptableObject {

    public bool destroy = false;
    public bool enable = false;

    public GameObject MyGameObject { get; set; }
    public Transform MyTransform { get; set; }
    public ISSActionCallback Callback { get; set; }

    protected SSAction() { }

	// Use this for initialization
	public virtual void Start () {
		
	}
	
	// Update is called once per frame
	public virtual void Update () {
		
	}

    public void Reset()
    {
        destroy = false;
        enable = false;
        MyGameObject = null;
        MyTransform = null;
        Callback = null;
    }

}
