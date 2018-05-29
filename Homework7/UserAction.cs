using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserAction : MonoBehaviour {

    GameObject obj;
    public float speed = 5;

    public void moveObject(float movex,float movez)
    {
        
        if (movex != 0 || movez != 0)
        {
            if (obj == null)
            {
                Debug.Log("No selected object!--move");
                return;
            }
            obj.transform.Translate(movex * speed * Time.deltaTime, movez * speed * Time.deltaTime , 0);
        }
    }

    public void largerObjcet()
    {
        if(obj == null)
        {
            Debug.Log("No selected object!--larger");
            return;
        }
        Vector3 myVec = obj.transform.localScale;
        obj.transform.localScale = new Vector3(myVec.x * 2, myVec.y * 2, myVec.z * 2);
    }

    public void shine()
    {
        if (obj == null)
        {
            Debug.Log("No selected object!--shine");
            return;
        }
        ParticleSystem ps = obj.GetComponent<ParticleSystem>();
        if (ps == null) return;
        var sh = ps.shape;
        sh.enabled = true;
        sh.scale = new Vector3(5, 0, 0);
    }

    public void skyShine()
    {
        if (obj == null)
        {
            Debug.Log("No selected object!--shine");
            return;
        }
        ParticleSystem ps = obj.GetComponent<ParticleSystem>();
        if (ps == null) return;
        var sh = ps.shape;
        sh.enabled = true;
        sh.scale = new Vector3(18, 8, 0);
        ps.emissionRate = 10f;
        ps.maxParticles = 10;
    }

    public void resetObject()
    {
        if (obj == null)
        {
            Debug.Log("No selected object!--reset");
            return;
        }
        ParticleSystem ps = obj.GetComponent<ParticleSystem>();
        if (ps == null) return;
        var sh = ps.shape;
        sh.enabled = true;
        sh.scale = new Vector3(0, 0, 0);
        obj.transform.localScale = new Vector3(1, 1, 1);
    }

    public void smallerObjcet()
    {
        if (obj == null)
        {
            Debug.Log("No selected object!");
            return;
        }
        Vector3 myVec= obj.transform.localScale;
        obj.transform.localScale = new Vector3(myVec.x/2, myVec.y/2, myVec.z/2);
    }

    public void setObj(GameObject gameObject)
    {
        this.obj = gameObject;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        
	}
}
