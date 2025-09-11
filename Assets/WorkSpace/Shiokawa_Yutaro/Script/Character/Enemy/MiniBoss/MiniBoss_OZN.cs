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
    Animator animator;

    public override void Setup()
    {
        animator = GetComponent<Animator>();
        attackStrategies = new Dictionary<AttackType, AttackStrategy>
        {
            { AttackType.Going, new GoingAttack() },
            { AttackType.LongRange, new LongRangeAttack() },
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

        if (actionCatch && Vector3.Distance(player.transform.position, transform.position) <= 1)
        {
            PlayerCatch();
        }

    }

    public void PlayerCatch()
    {
        //player.transform.Find("Camera").GetComponent<CameraMove>().enabled = false;
        //rb.isKinematic = true;
        player.transform.SetParent(transform, true);
        player.GetComponent<Rigidbody>().isKinematic = true;
        //player.GetComponent<Rigidbody>().detectCollisions = false;
        //player.transform.localPosition = Vector3.zero;
        //player.transform.rotation = Quaternion.Euler(0,0,0);
        //actionCatch = false;
        //playerCatch = true;

        animator.SetTrigger("Ç¬Ç©Ç›ê¨å˜");
    }
    public void PlayerRelease()
    {
        //player.transform.SetParent(null);
        //rb.isKinematic = false;
        //player.GetComponent<Rigidbody>().isKinematic = false;
        //player.GetComponent<Rigidbody>().detectCollisions = true;
        //player.transform.Find("Camera").GetComponent<CameraMove>().enabled = true;
        //playerCatch = false;
    }

    protected override bool ViewAction()
    {
        //èÌÇ…ÉvÉåÉCÉÑÅ[Ç™ÇÌÇ©ÇÈ
        return true;
    }

    public override async UniTask GoingAttack()
    {
        int rand = Random.Range(0, 1);

        if(rand == 0)
        {
            
            animator.SetTrigger("Ç¬Ç©Ç›îªíË");
        }
        if(rand == 1)
        {
            animation.Play("çUåÇ1");
        }

        return;
    }

    public override async UniTask LongRangeAttack()
    {
        return;
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
    public void Attack2_Jump()
    {
        Vector3 targetDir = player.transform.position - transform.position;
        rb.velocity = targetDir.normalized * 2 + transform.up * 5;
    }
}
