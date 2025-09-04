using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static EnemyCharacter;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    private Collider attackCollider;
    // Start is called before the first frame update
    void Start()
    {
        attackCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (currentState == stateMap[Action.Attack]) {
            attackCollider.enabled = true;
        }
        */
    }
}
