using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss_OZN : EnemyCharacter
{
    [SerializeField] private Transform hand;
    private bool actionCatch;
    private bool playerCatch;
    public override void Setup()
    {
        attackStrategies = new Dictionary<AttackType, AttackStrategy>
        {
            { AttackType.Going, new GoingAttack() },
            { AttackType.TakeDistance, new TakeDistance() }
        };

        attackArea = 0.5f;
        speed = 1.5f;
        maxHP = 15;
        HP = maxHP;
        rawAttack = 5;
        rawDefense = 0;
        base.Setup();
    }
    public void PlayerCatch()
    {
        player.transform.Find("Camera").GetComponent<CameraMove>().enabled = false;
        rb.isKinematic = true;
        player.transform.SetParent(transform, true);
        player.GetComponent<Rigidbody>().isKinematic = true;
        player.transform.localPosition = Vector3.zero;
        player.transform.localRotation = Quaternion.identity;

        animation.Play("Ç¬Ç©Ç›ê¨å˜");
    }
    public void PlayerRelease()
    {
        player.transform.SetParent(null);
        rb.isKinematic = false;
        player.GetComponent<Rigidbody>().isKinematic = false;
        player.transform.Find("Camera").GetComponent<CameraMove>().enabled = true;
    }

    public override async UniTask GoingAttack()
    {
        animation.Play("Ç¬Ç©Ç›îªíË");
        return;
    }

    public override async UniTask LongRangeAttack()
    {
        return;
    }

    public void OnActionCatch()
    {
        actionCatch = true;
    }
    public void OffActionCatch()
    {
        actionCatch = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            //if (!actionCatch) return;
            if (playerCatch) return;

            PlayerCatch();
            playerCatch = true;
        }
    }
}
