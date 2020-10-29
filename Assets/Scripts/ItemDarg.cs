using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemDarg : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    Image _icon;
    Image _side;
    GameObject _saveParent;
    CanvasGroup _canvasGroup;
    public void Inst()
    {
        _side = transform.GetComponent<Image>();
        _icon = transform.Find("Icon").GetComponent<Image>();
        _icon.raycastTarget = false;//关闭Icon上面的射线检测，只留下边框的

        //这个组件上的BlocksRaycasts属性可以让射线传过去，这样就能检测到下面的东西了
        _canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    public void ShowSprite(Sprite index)
    {
        _icon.sprite = index;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = false;//关闭后，不阻挡向下检测
        _saveParent = transform.parent.gameObject;
        //为了让Item拖动的时候不会被别的界面所阻挡，所以直接放到Canvas下面，成为最下面的物体
        transform.SetParent(GameManager.Instance.MainCanvas.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        gameObject.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = true;//把他的射线检测再开回来

        GameObject pointEventObj = eventData.pointerEnter;
        if (pointEventObj == null)
        {
            transform.SetParent(_saveParent.transform);
            transform.localPosition = Vector3.zero;
        }
        else if (pointEventObj.CompareTag(GameManager._groove))//发现是凹槽
        {
            transform.SetParent(pointEventObj.transform);
            transform.localPosition = Vector3.zero;
        }
        else if (pointEventObj.CompareTag(GameManager._itemIcon))//发现是道具
        {
            transform.SetParent(pointEventObj.transform.parent);
            transform.localPosition = Vector3.zero;
            pointEventObj.transform.SetParent(_saveParent.transform);
            pointEventObj.transform.localPosition = Vector3.zero;
        }
        else if (pointEventObj.CompareTag(GameManager._bagPart))//发现是背包
        {
            transform.SetParent(GameManager.Instance.BagDragUI._bagGrid.transform);
            transform.localPosition = Vector3.zero;
        }
        else
        {
            transform.SetParent(_saveParent.transform);
            transform.localPosition = Vector3.zero;
        }
    }
}
