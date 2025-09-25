using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPSave : MonoBehaviour
{
    public static float saveHP = 0;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        saveHP = HPGaugeUI.restHP;
    }
}
