using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{

    private Image mouseImage;
    private int mouseType = 0;
    private int zPosition;

    public Sprite none;
    public Sprite hat;
    public Sprite hand;
    public Sprite shoes;
    public Camera cam;

    void Start()
    {
        mouseImage = GetComponent<Image>();
        zPosition = -400;
    }

    public int getMouseType() { return mouseType; }
    public void setMouseType(int m) { mouseType = m; }

    void Update()
    {
        Debug.Log(mouseType);
        if (mouseType == 0)
        {
            mouseImage.sprite = none;
        }
        else if (mouseType == 1)
        {
            mouseImage.sprite = hat;
        }
        else if (mouseType == 2)
        {
            mouseImage.sprite = hand;
        }
        else if (mouseType == 3)
        {
            mouseImage.sprite = shoes;
        }
        transform.position = new Vector3(Input.mousePosition.x - 450, Input.mousePosition.y - 300, zPosition);

        //Debug.Log (Input.mousePosition);  
        if (mouseType != 0)
        {
            zPosition = 100;
        }
        else
        {
            zPosition = -400;
        }
    }
}