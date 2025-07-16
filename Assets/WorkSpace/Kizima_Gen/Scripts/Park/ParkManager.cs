/*
* @file ParkManager.cs
* @brief パークの管理者
* @author kijima
* @date 2025/7/16
*/
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkManager : SystemObject {

    //パークの最大数
    private int _parkSelectMax = 3;
    
    //パーク管理リスト
    private List<Park> _parks = new List<Park>();

    [SerializeField]
    private Park _parkOrigin = null;

    [SerializeField]
    private GameObject _parkRoot = null;

    //パーク列の指している先
    private int Index = 1;

    

    /// <summary>
    /// 初期化
    /// </summary>
    public override void Initialize() {
        for(int i = 0,max = _parkSelectMax; i< max; i++) {
            //パークを置くキャンバスにパークのアイテムを生成
            Park park = Instantiate(_parkOrigin, _parkRoot.transform);
            //初期化
            
            //リストに追加
            _parks.Add(park);
        }
        _parkRoot.SetActive(false);
    }

    private void Update() {
        //デバッグ用の確認
        if(Input.GetKeyDown(KeyCode.Z)) {
            //もしすでにパークが開かれていた場合選んでいるパークを使用
            if (_parkRoot.activeSelf) {
                _parks[Index].SelectPark();
                //使用して終了
                _parkRoot.SetActive(false);
                ExecuteAllPark(park => park.TearDown());
                return;
            }
            _parkRoot.SetActive(true);
            ChangeParkID();
        }

        if (Input.GetKeyDown(KeyCode.X)) {
            if (!_parkRoot.activeSelf) return;
            _parkRoot.SetActive(false);
            ExecuteAllPark(park => park.TearDown());
        }

        //キーの入力によってインデックスを変更
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            Index++;
            if(Index >= _parks.Count) {
                Index = 0;
            }
            
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            Index--;
            if (Index < 0) {
                Index = _parks.Count -1;
            }
            
        }
        //全アウトラインの機能を停止
        ExecuteAllPark(park => park.ChangeOutline(false));
        //インデックスが向いている者だけoutlineをつける
        _parks[Index].ChangeOutline(true);
    }

    /// <summary>
    /// パークの最大数を変える
    /// </summary>
    /// <param name="parkMax"></param>
    public void ChangeParkMax(int parkMax) {
        _parkSelectMax = parkMax;


    }

    /// <summary>
    /// パークアイテムの再生性
    /// </summary>
    public void ChangeParkID() {
        for (int i = 0, max = _parks.Count; i < max; i++) {
            int randomMasterID = Random.Range(0, MasterdataManager.parkData[0].Count);
            //かぶらなくなるまでループ
            while (CheckSomeParkID(randomMasterID)) {
                randomMasterID = Random.Range(0, MasterdataManager.parkData[0].Count);
            }
            _parks[i].Initialize(randomMasterID);
        }
    }

    /// <summary>
    /// 渡された引数が既にパークのリストにないかをチェック
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    private bool CheckSomeParkID(int ID) {
        for(int i = 0,max = _parks.Count;i < max;i++) {
            if(ID == _parks[i].GetID()) return true;
        }
        return false;
    }

    private void ExecuteAllPark(System.Action<Park> action) {
        if (action == null ) return;
        for (int i = 0, max = _parks.Count; i < max; i++) {
            if (_parks[i] == null) continue;
            action(_parks[i]);
        }
    }
   
}
