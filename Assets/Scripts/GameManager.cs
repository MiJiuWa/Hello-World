using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager Inst;
    public static GameManager Instance
    {
        get
        {
            return Inst;
        }
    }

    private void Awake()
    {
        Inst = this;
        DontDestroyOnLoad(Inst);


        IconLoad();//获取Icon资源

        MainCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();

        BagDragUI = MainCanvas.transform.Find("BagDrapUI").gameObject.AddComponent<BagDragUI>();
        BagDragUI.Inst();
    }

    string _ResourcesIconPath = "UI-ICON/Icon_b/Icon_";
    public List<Sprite> _iconGroup = new List<Sprite>();
    Sprite _tempSprite;
    void IconLoad()
    {
        _iconGroup.Clear();
        for (int i = 1; i <= 17; i++)//界面不够大，就放少一些吧
        {
            _tempSprite = Resources.Load<Sprite>(string.Format("{0}{1}", _ResourcesIconPath, i));
            _iconGroup.Add(_tempSprite);
        }
    }

    public const string _groove = "Groove";//把标签维护起来
    public const string _itemIcon = "ItemIcon";
    public const string _bagPart = "BagPart";

    public BagDragUI BagDragUI
    {
        get; private set;
    }
    public Canvas MainCanvas
    {
        get;private set;
    }
}
