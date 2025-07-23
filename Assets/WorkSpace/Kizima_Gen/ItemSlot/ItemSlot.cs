/*
* @file ItemSlot.cs
* @brief アイテムスロットの管理者
* @author kijima
* @date 2025/7/23
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static UnityEngine.Input;

public class ItemSlot : SystemObject{
    [SerializeField]
    private List<ItemFrame> _slots;
    private int _slotIndex;

    /// <summary>
    /// 初期化
    /// </summary>
    public override void Initialize() {
        ExecuteAllItemSlot(action => action.Initialize());
    }

    /// <summary>
    /// 常に入力待ちをおこなう　
    /// </summary>
    private void Update() {
        if(GetKeyDown(KeyCode.RightArrow)) {
            _slotIndex++;
        }
        if (GetKeyDown(KeyCode.LeftArrow)) {
            _slotIndex--;
        }
    }

    /// <summary>
    /// 全アイテムフレームに処理を行わせる
    /// </summary>
    /// <param name="action"></param>
    private void ExecuteAllItemSlot(System.Action<ItemFrame> action) {
        if (action == null) return;
        for (int i = 0, max = _slots.Count; i < max; i++) {
            if (_slots[i] == null) continue;
            action(_slots[i]);
        }
    }

    public void ChangeItemIcon() {

    }

}
