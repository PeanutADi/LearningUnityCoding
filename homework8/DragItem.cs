﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform myTransform;
    private RectTransform myRectTransform;
    // 用于event trigger对自身检测的开关
    private CanvasGroup canvasGroup;
    // 拖拽操作前的有效位置，拖拽到有效位置时更新
    public Vector3 originalPosition;
    // 记录上一帧所在物品格子
    private GameObject lastEnter = null;
    // 记录上一帧所在物品格子的正常颜色
    private Color lastEnterNormalColor;
    // 拖拽至新的物品格子时，该物品格子的高亮颜色
    private Color highLightColor = Color.cyan;
    void Start()
    {
        myTransform = this.transform;
        myRectTransform = this.transform as RectTransform;
        canvasGroup = GetComponent<CanvasGroup>();
        originalPosition = myTransform.position;
    }
    void Update()
    {
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;//让event trigger忽略自身，这样才可以让event trigger检测到它下面一层的对象,如包裹或物品格子等
        lastEnter = eventData.pointerEnter;
        lastEnterNormalColor = lastEnter.GetComponent<Image>().color;
        originalPosition = myTransform.position;//拖拽前记录起始位置
        gameObject.transform.SetAsLastSibling();//保证当前操作的对象能够优先渲染，即不会被其它对象遮挡住
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(myRectTransform, eventData.position, eventData.pressEventCamera, out globalMousePos))
        {
            myRectTransform.position = globalMousePos;
        }
        GameObject curEnter = eventData.pointerEnter;
        bool inItemGrid = EnterItemGrid(curEnter);
        if (inItemGrid)
        {
            Image img = curEnter.GetComponent<Image>();
            lastEnter.GetComponent<Image>().color = lastEnterNormalColor;
            if (lastEnter != curEnter)
            {
                lastEnter.GetComponent<Image>().color = lastEnterNormalColor;
                lastEnter = curEnter;//记录当前物品格子以供下一帧调用
            }
            //当前格子设置高亮
            img.color = highLightColor;
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject curEnter = eventData.pointerEnter;
        //拖拽到的空区域中（如包裹外），恢复原位
        if (curEnter == null)
        {
            myTransform.position = originalPosition;
        }
        else
        {
            //移动至物品格子上
            if (curEnter.name == "Item")
            {
                myTransform.position = curEnter.transform.position;
                originalPosition = myTransform.position;
                curEnter.GetComponent<Image>().color = lastEnterNormalColor;//当前格子恢复正常颜色
            }
            else
            {
                //移动至包裹中的其它物品上
                if (curEnter.name == eventData.pointerDrag.name && curEnter != eventData.pointerDrag)
                {
                    Vector3 targetPostion = curEnter.transform.position;
                    curEnter.transform.position = originalPosition;
                    myTransform.position = targetPostion;
                    originalPosition = myTransform.position;
                }
                else//拖拽至其它对象上面（包裹上的其它区域）
                {
                    myTransform.position = originalPosition;
                }
            }
        }
        lastEnter.GetComponent<Image>().color = lastEnterNormalColor;//上一帧的格子恢复正常颜色
        canvasGroup.blocksRaycasts = true;//确保event trigger下次能检测到当前对象
    }
    // 判断鼠标指针是否指向包裹中的物品格子
    // <param name="go">鼠标指向的对象</param>
    bool EnterItemGrid(GameObject go)
    {
        if (go == null)
        {
            return false;
        }
        return go.name == "Item";
    }
}