using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameConst;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _itemSprite = null;

    public int ID { get; private set; }
    public void Setup(int setID, eItemCategory category)
    {
        ID = setID;
        //Ç≤Ç∆Ç…å©ÇΩñ⁄ÇÃê›íË
        _itemSprite.sprite = Resources.LoadAll<Sprite>(ITEM_SPRITE_FILE_NAME)[(int)category];
    }

    public void Teardown()
    {
        ID = -1;
        _itemSprite.sprite = null;
    }

    //public void SetSquare(MapSquareData square)
    //{
    //    transform.position = square.GetCharacterRoot().position;
    //}

    //public void UnuseSelf()
    //{

    //}
}
