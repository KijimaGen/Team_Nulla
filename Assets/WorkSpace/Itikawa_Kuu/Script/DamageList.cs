using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageList : MonoBehaviour
{
    // リストのオリジナル
    [SerializeField]
    private GameObject damageOrigin = null;

    // 使用、未使用のリスト
    private List<GameObject> useList = null;
    private List<GameObject> unuseList = null;
    // Start is called before the first frame update
    void Start()
    {
        useList = new List<GameObject>();
        unuseList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
