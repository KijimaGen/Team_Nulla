using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss_OZN : EnemyCharacter
{
    [SerializeField] private Transform hand;
    private bool actionCatch;
    private bool playerCatch;

    [SerializeField] private Transform playerMoveParent;
    [SerializeField] private Transform cameraMoveParent;


    [SerializeField] private ParticleSystem pressEffect;
    [SerializeField] private ParticleSystem CatchBleakEffect;
    [SerializeField] private ParticleSystem CatchSmokeEffect;

    float attackChanceTime;
    float attackChanceInterval;
    bool attackChance;
    bool success;


    public override void Setup()
    {
        attackStrategies = new Dictionary<AttackType, AttackStrategy>
        {
            { AttackType.Going, new GoingAttack() },
            { AttackType.LongRange, new LongRangeAttack() },
            { AttackType.TakeDistance, new TakeDistance() }
        };

        attackArea = 0.5f;
        speed = 1.5f;
        maxHP = 10000;
        HP = maxHP;
        rawAttack = 5;
        rawDefense = 0;
        base.Setup();
    }



    //private void Update()
    //{
    //    if(Input.GetKey(KeyCode.E))
    //    {
    //        animation.Play("çUåÇ1");
    //        animation.Play("îÚÇ—çûÇ›çUåÇ");
    //    }
    //}

    protected override void Update()
    {
        if (playerCatch) return;
        base.Update();

        if (actionCatch && Vector3.Distance(player.transform.position, hand.position) <= 0.3f)
        {
            PlayerCatch();
        }


    }

    public void PlayerCatch()
    {
        rb.isKinematic = true;

        CameraMove camera = Camera.main.GetComponent<CameraMove>();
        camera.enabled = false;
        camera.transform.SetParent(cameraMoveParent, true);
        camera.transform.localPosition = Vector3.zero;
        camera.transform.rotation = Quaternion.Euler(0, 0, 0);
        camera.transform.localRotation = Quaternion.Euler(0, 0, 0);

        player.transform.SetParent(playerMoveParent, true);
        player.GetComponent<Rigidbody>().isKinematic = true;
        player.transform.localPosition = Vector3.zero;
        player.transform.rotation = Quaternion.Euler(0, 0, 0);
        actionCatch = false;
        playerCatch = true;

        animation.Play("Ç¬Ç©Ç›ê¨å˜");
    }
    public void PlayerRelease()
    {
        rb.isKinematic = false;

        CameraMove camera = Camera.main.GetComponent<CameraMove>();
        camera.enabled = true;
        camera.transform.SetParent(player.transform, true);

        player.transform.SetParent(null);
        player.GetComponent<Rigidbody>().isKinematic = false;
        playerCatch = false;
    }

    protected override bool ViewAction()
    {
        //èÌÇ…ÉvÉåÉCÉÑÅ[Ç™ÇÌÇ©ÇÈ
        return true;
    }

    public override async UniTask GoingAttack()
    {
        Attack(player.transform.position);
        int rand = Random.Range(0, 1);

        if(rand == 0)
        {
            animation.Play("Ç¬Ç©Ç›îªíË");
        }
        if(rand == 1)
        {
           
        }

        return;
    }

    public override async UniTask LongRangeAttack()
    {
        int rand = Random.Range(0, 1);

        if (rand == 0)
        {
            animation.Play("îÚÇ—çûÇ›çUåÇ");
        }
        if (rand == 1)
        {
           
        }

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


    public void Attack1_Jump()
    {
        rb.velocity = transform.up * 5;
    }
    public void Attack1_Effect()
    {
        // é©ï™ÇÃå¸Ç´Ç…ëŒÇµÇƒï‚ê≥âÒì]Çâ¡Ç¶ÇÈ
        Quaternion rot = transform.rotation * Quaternion.Euler(0, -90, 0);
        ParticleSystem effect = Instantiate(pressEffect, transform.position, rot);


    }
    public void Attack2_Jump()
    {
        Vector3 targetDir = player.transform.position - transform.position;
        rb.velocity = targetDir.normalized * 2 + transform.up * 5;
    }
    public void Attack2_Effect()
    {

    }
    public void AttackCatch_Effect()
    {
        Vector3 effectPos = player.transform.position;
        Instantiate(CatchBleakEffect, effectPos, transform.rotation);
        Instantiate(CatchSmokeEffect, effectPos, transform.rotation);
    }

}
