using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAction
{

    //public void AcceptInput()
    //{
    //    while (true)
    //    {
    //        // 移動の受付
    //        if (AcceptMove()) break;
    //        // 攻撃の受付
    //        if (await AcceptAttack()) break;
    //        //方向転換入力の受付処理
    //        await AcceptDirChange();
    //    }
    //}

    /// <summary>
    /// 移動の受付、内部処理
    /// </summary>
    /// <returns>移動したらTrue</returns>
    public bool AcceptMove()
    {
        // 8方向の入力を受け付ける
        Vector3 inputDir = AcceptDirInput().normalized;
        if (inputDir.magnitude <= 0.0f) return false;
        // 移動可否の判定
        CharacterBase player = GetPlayer();

        // 受け付けた入力に応じて移動
        player.SetPosition();
        return true;
    }

    private Vector3 AcceptDirInput()
    {
        if (Input.GetKey(KeyCode.W))
        {

        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {

        }
        else
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {

            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {

            }
        }
        return Vector3.zero;
    }
    /// <summary>
    /// 通常攻撃入力受付、処理
    /// </summary>
    /// <returns></returns>
    private bool AcceptAttack()
    {
        if (!Input.GetMouseButton(0)) return false;

        ExecuteAction(GetPlayer(), NORMAL_ATTACK_ACTION_ID);
        return true;
    }

    private void AcceptDirChange()
    {
        if (!Input.GetKey(KeyCode.Space)) return;

        CharacterBase player = GetPlayer();
        //方向転換入力のトリガーを受け付け、隣接エネミーの方向を自動敵に向く
        //ジャンプ
    }

    private void ChangeDirToEnemy(CharacterBase character)
    {
        //スタートの向き
        int startIndex = (int)character.direction + 1;
        int basePosX = character.posX, basePosY = character.posY;
        //８方向の隣接マスで走査、エネミーを探し、向きを変えマスの色を変える
        Vector3.Dot()
    }
}
