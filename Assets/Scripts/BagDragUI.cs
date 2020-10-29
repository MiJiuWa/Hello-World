using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagDragUI : MonoBehaviour
{
    public GameObject _bagGrid//背包的格子
    {
        get; private set;
    }
    GameObject _itemSeed;
    public void Inst()
    {
        _bagGrid = transform.Find("BagPart/Grid").gameObject;
        _itemSeed = transform.Find("ItemSeed").gameObject;

        BuildItem();
    }

    GameObject _tempObj;
    ItemDarg _tempItemDarg;
    void BuildItem()
    {
        for (int i = 0; i < GameManager.Instance._iconGroup.Count; i++)
        {
            _tempObj = Instantiate(_itemSeed, _bagGrid.transform);
            _tempObj.transform.localPosition = Vector3.zero;
            _tempObj.transform.localRotation = Quaternion.identity;
            _tempObj.transform.localScale = Vector3.one;

            _tempItemDarg = _tempObj.AddComponent<ItemDarg>();
            _tempItemDarg.Inst();
            _tempItemDarg.ShowSprite(GameManager.Instance._iconGroup[i]);
        }
    }
}
