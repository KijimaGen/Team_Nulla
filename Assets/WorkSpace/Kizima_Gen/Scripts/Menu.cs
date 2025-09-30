/**
 * @file Menu.cs
 * @brief メニューに付くスクリプト
 * @author kijima
 * @date 2025/9/18
 */
using Cysharp.Threading.Tasks;
using MaykerStudio;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static ItemUtility;

public class Menu : MonoBehaviour{
    //アイテムアイコンのデータ
    private List<Sprite> ItemIcons = new List<Sprite>(5);

    //実際に目に見えるアイコン
    [SerializeField]
    private List<Image> itemIcon = new List<Image>(5);

    //メニューの親
    [SerializeField]
    private GameObject menuParent;

    //インデックス(リストの矢印)
    private int index;

    //枠
    [SerializeField]
    List <Image> ImageList = new List<Image>(5);

    //メニューを開いているかどうか
    public bool isOpenMenu;

    //自身のインスタンス(他で参照したいものとかあるので)
    public static Menu instance { get; private set; }

    //入力されたかどうか
    private InputAction decideAction;

    //各種ステータステキスト
    [SerializeField]
    private TextMeshProUGUI AttackText;
    [SerializeField]
    private TextMeshProUGUI LifeText;
    [SerializeField]
    private TextMeshProUGUI DefenceText;
    [SerializeField]
    private TextMeshProUGUI SpeedText;

    int? selectedIndex = null;

    //ぷれいやー
    GameObject player;

    InputActions inputActions;//インプットシステム

    void Start(){
        instance = this;
       menuParent.SetActive(false);
       index = 0;

        //プレイヤーを探す
        player = GameObject.FindWithTag("Player");
        var playerInput = player.GetComponent<PlayerInput>();
        decideAction = playerInput.actions["Decide"];

        inputActions = new InputActions();
    }

    void Update(){
        //一回全リスト白
        for(int i = 0; i < ImageList.Count; i++) {
            ImageList[i].color = Color.white;
        }
        if(index < ImageList.Count)
            ImageList[index].color = Color.red;

        if(isOpenMenu) {
            //プレイヤーを探す
            player = GameObject.FindWithTag("Player");
            //アイテムリストをキャッシュして受け取る
            player.GetComponent<PlayerCharacter>().SendItemList();

            //アイテムリストを初期化して開く
            List<ItemBase> itemList = GetPlayerItems();


            for(int i = 0;i < itemList.Count;i++) {
                if (itemList[i] == null) continue;

                //インデックスしているところのアクティブを消す
                itemList[i].gameObject.SetActive(false);
                //アイテムボックスのアクティブも変える
                itemList[i].SetVisibleStatusBox(false);
            }


            if (itemList[index] != null) {
                //インデックスしているところのアクティブをつける
                itemList[index].gameObject.SetActive(true);
                //アイテムボックスのアクティブも変える
                itemList[index].SetVisibleStatusBox(true);
            }

        }
    }


    /// <summary>
    /// メニューを開く
    /// </summary>
    public void OpenMenu() {

        

        //プレイヤーを探す
        player = GameObject.FindWithTag("Player");
        //アイテムリストをキャッシュして受け取る
        player.GetComponent<PlayerCharacter>().SendItemList();

        //アイテムリストを初期化して開く
        List<ItemBase> itemList = GetPlayerItems();

        // 空の要素を確保
        ItemIcons = new List<Sprite>(new Sprite[itemList.Count]);

        for (int i = 0; i < itemList.Count; i++) {
            if (itemList[i] == null) {
                //一応nullを入れておく
                itemIcon[i].sprite = null;
                continue;
            }

            //受け取ったアイコンの情報をもらう
            ItemIcons[i] = itemList[i].myIcon;
            //情報を反映
            itemIcon[i].sprite = ItemIcons[i];
        }

        //テキストを変更
        int Attack = (int)player.GetComponent<PlayerCharacter>().GetAttack();
        for(int i =0; i < itemList.Count;i++) {
            //このis演算子はpossessItemList[i]がPowerUpItem型かどうかを検知してくれる
            if (itemList[i] is PowerUpItem)
                Attack += (int) ((PowerUpItem) itemList[i]).GetAttackValue();
        }
        Attack += (int)player.GetComponent<PlayerCharacter>().GetWeaponAttack();

        AttackText.text = "Attack : " + Attack.ToString();


        //防御の値
        int Defence = (int) player.GetComponent<PlayerCharacter>().GetDefense();
        DefenceText.text = "Defence : " + Defence.ToString();

        //HPの値
        int HP = (int) player.GetComponent<PlayerCharacter>().GetHP();
        for (int i = 0; i < itemList.Count; i++) {
            //このis演算子はpossessItemList[i]がPowerUpItem型かどうかを検知してくれる
            if (itemList[i] is LifeUpItem)
                HP += (int) ((LifeUpItem) itemList[i]).GetLifeValue();
        }
        LifeText.text = "HP : " + HP.ToString();


        int speed = (int) player.GetComponent<PlayerCharacter>().GetSpeed();
        SpeedText.text = "speed : " + speed.ToString();


        //メニュー画面を開く
        menuParent.SetActive(true);

        //変数をtrue
        isOpenMenu = true;

        // 時間を止める
        Time.timeScale = 0f;


    }

    /// <summary>
    /// メニュー画面を閉じる
    /// </summary>
    /// <param name="context"></param>
    public void CloseMenu() {
       menuParent.SetActive(false);

        //プレイヤーを探す
        player = GameObject.FindWithTag("Player");
        //アイテムリストをキャッシュして受け取る
        player.GetComponent<PlayerCharacter>().SendItemList();

        //アイテムリストを初期化して開く
        List<ItemBase> itemList = GetPlayerItems();


        for (int i = 0; i < itemList.Count; i++) {
            if (itemList[i] == null) continue;

            //インデックスしているところのアクティブを消す
            itemList[i].gameObject.SetActive(false);
            //アイテムボックスのアクティブも変える
            itemList[i].SetVisibleStatusBox(false);
        }

        // 時間を動かす
        Time.timeScale = 1f;
        //変数をfalse
        isOpenMenu = false;
    }

    /// <summary>
    /// パークリストのインデックスを増やす
    /// </summary>
    public void IncreaceIndex(InputAction.CallbackContext context) {
        if (context.performed) {
            if (!isOpenMenu) return;
            index++;
            if (index >= ImageList.Count) {
                index = 0;
            }
        }
    }
    /// <summary>
    /// パークリストのインデックスを減らす
    /// </summary>
    public void DecreaseIndex(InputAction.CallbackContext context) {
        if (context.performed) {
            if (!isOpenMenu) return;
            index--;
            if (index < 0) {
                index = ImageList.Count - 1;
            }
        }
    }

    /// <summary>
    /// 決定処理
    /// </summary>
    public void Decide(InputAction.CallbackContext context) {
        if (context.performed) {
           
            if (isOpenMenu) { 
                RemoveItemInMenu();
            }

            //インデックスを指定
            selectedIndex = index;
        }
    }
    

    /// <summary>
    /// インデックスを返す
    /// </summary>
    /// <returns></returns>
    public int GetIndex() {
        return index;
    }

    public async UniTask SwapItemInMenu(ItemBase getItem) {

        //インデックスを指定するための物を作る
        selectedIndex = null;

        //メニューを開く
        OpenMenu();
        
        //決定ボタンまち
        while (selectedIndex == null) {
            
            //1触れ待ち
            await UniTask.DelayFrame(1);
        }

        //拾おうとしたアイテムを拾う
        //プレイヤーを探す
        GameObject player = GameObject.FindWithTag("Player");
        RemoveItemInMenu();
        player.GetComponent<PlayerCharacter>().GetItem(getItem, index);
        CloseMenu();

    }

    /// <summary>
    /// こちらでアイテムを捨てる
    /// </summary>
    private void RemoveItemInMenu() {
        //プレイヤーを探す
        player = GameObject.FindWithTag("Player");
        //アイテムリストをキャッシュして受け取る
        player.GetComponent<PlayerCharacter>().SendItemList();
        player.GetComponent<PlayerCharacter>().RemoveItemFromList(index);
        //アイコンを更新
        itemIcon[index].sprite = null;
    }

    public void Plus(InputAction.CallbackContext context) {
        if (isOpenMenu) {
            CloseMenu();
        }
        else {
            OpenMenu();
        }
    }


}
