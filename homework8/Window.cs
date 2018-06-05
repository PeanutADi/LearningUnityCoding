using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window : MonoBehaviour
{
    public Vector2 range = new Vector2(5f, 3f);
    Transform mTrans;
    Quaternion mStart;
    Image thisImage;
    Vector2 mRot = Vector2.zero;
    void Start()
    {
        mTrans = transform;
        mStart = mTrans.localRotation;
    }
    void Update()
    {
        if (thisImage != null) Debug.Log(thisImage.name);
        Vector3 pos = Input.mousePosition;
        //pos.x = -pos.x;
        //
        pos.y = -pos.y;
        float halfWidth = Screen.width * 0.5f;
        float halfHeight = Screen.height * 0.5f;
        float x = Mathf.Clamp((pos.x - halfWidth) / halfWidth, -1f, 1f);
        float y = Mathf.Clamp((pos.y - halfHeight) / halfHeight, -1f, 1f);
        mRot = Vector2.Lerp(mRot, new Vector2(x, y), Time.deltaTime * 5f);
        mTrans.localRotation = mStart * Quaternion.Euler(-mRot.y * range.y, mRot.x * range.x, 0f);
        
    }
}