using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDirector : MonoBehaviour {

    public StageDirector()
    {
    }

    public void carSituation(GameObject obj1,GameObject obj2)
    {
        obj1.transform.position = new Vector3(4, 0, 0);
        obj2.transform.position = new Vector3(-4, 0, 0);
        obj1.transform.localScale = new Vector3(0, 0, 0);
        obj2.transform.localScale = new Vector3(0, 0, 0);
        float cnt = 0f;
        while (cnt < 2f)
        {
            cnt += Time.deltaTime;
        }
        float x = 0f;
        while (x < 15f)
        {
            x += Time.deltaTime;
            Debug.Log(x);
            obj1.transform.localScale = new Vector3(x/5, x/5, x/5);
            obj2.transform.localScale = new Vector3(x/5, x/5, x/5);
        }
    }

}
