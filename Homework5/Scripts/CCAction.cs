using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCAction : SSAction {

    float G = -9.8f;

    float Vx;

    Vector3 Direction;

    float FlyTime;
    // Use this for initialization
   public override void Start () {
        enable = true;
        FlyTime = 0f;
        Vx = MyGameObject.GetComponent<DiskData>().speed;
        Direction = MyGameObject.GetComponent<DiskData>().direction;
	}
	
	// Update is called once per frame
	public override void Update () {
        if (MyGameObject.activeSelf == true)
        {
            FlyTime += Time.deltaTime;
            MyTransform.Translate(Direction * Vx * Time.deltaTime);
            //v*t
            MyTransform.Translate(Vector3.up * G * FlyTime * Time.deltaTime);
            //t*(0+at)
            if (this.MyTransform.position.y < -5f)
            {
                this.destroy = true;
                this.enable = false;
                this.Callback.SSActionEvent(this);
            }
        }
	}

    public static CCAction GetSSAction()
    {
        CCAction action = ScriptableObject.CreateInstance<CCAction>();
        return action;
    }
}
