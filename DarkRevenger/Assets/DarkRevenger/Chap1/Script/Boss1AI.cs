using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Boss1AI : MonoBehaviour
{
    public Transform target;
    public Transform AttackPoint;
    public Vector2 attackSize;
    float attackDelay;
    Boss1 boss1;
    Animator enemyAnimator;
    public LayerMask enemyLayers;
    public bool isAttacking = false;
    public float attackDuration = 0.5f;
    void Start()
    {
        boss1 = GetComponent<Boss1>();
        enemyAnimator = boss1.enemyAnimator;
    }

    void Update()
    {
        attackDelay -= Time.deltaTime;
        if (attackDelay < 0) attackDelay = 0;

        float distance = Vector3.Distance(transform.position, target.position);

        if (attackDelay == 0 && distance <= boss1.status.fieldOfVision)
        {
            FaceTarget();

            if (distance <= boss1.status.atkRange)
            {
                if (!isAttacking)
                {
                    StartCoroutine(AttackTarget());
                }
            }
            else
            {
                if (!enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                {
                    if (!isAttacking) {
                        MoveToTarget();
                    }
                }
            }
        }
        else
        {
            enemyAnimator.SetBool("moving", false);
        }
    }

    void MoveToTarget()
    {
        float dir = target.position.x - transform.position.x;
        dir = (dir < 0) ? -1 : 1;
        transform.Translate(new Vector2(dir, 0) * boss1.status.moveSpeed * Time.deltaTime);
        enemyAnimator.SetBool("moving", true);
    }

    void FaceTarget()
    {
        if (target.position.x - transform.position.x < 0) // 타겟이 왼쪽에 있을 때
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else // 타겟이 오른쪽에 있을 때
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    IEnumerator AttackRoutine(string[] combo)
    {
        foreach (string attack in combo)
        {
            if(!boss1.dead){
                enemyAnimator.SetTrigger(attack);
                yield return new WaitForSeconds(enemyAnimator.GetCurrentAnimatorStateInfo(0).length);
            }
        }
    }
    IEnumerator AttackTarget()
    {
        isAttacking = true;

        if (!target.GetComponent<Player>().isDashing) {
            int comboIndex = Random.Range(1, 4);
            string[] combo;

            switch (comboIndex)
            {
                case 1:
                    combo = new string[] { "attack" };
                    break;
                case 2:
                    combo = new string[] { "attack", "attack2" };
                    break;
                case 3:
                    combo = new string[] { "attack", "attack2", "attack3" };
                    break;
                default:
                    combo = new string[] { "attack" };
                    break;
            }
            yield return StartCoroutine(AttackRoutine(combo));
        }
        attackDelay = boss1.status.atkSpeed; // 딜레이 충전
        isAttacking = false;
    }
    void CreateHitBox()
    {
        // BoxCollider2D를 임시로 생성하여 공격 판정을 수행
        BoxCollider2D hitBox = AttackPoint.gameObject.AddComponent<BoxCollider2D>();
        hitBox.isTrigger = true;
        hitBox.size = attackSize;

        // 적과의 충돌 감지
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(AttackPoint.position, attackSize, 0, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Player"))
            {
                enemy.GetComponent<Player>().TakeDamage(boss1.status.atkDmg);
            }
        }
    }

    void RemoveHitBox()
    {
        // 임시로 생성한 BoxCollider2D 제거
        BoxCollider2D hitBox = AttackPoint.GetComponent<BoxCollider2D>();
        if (hitBox != null)
        {
            Destroy(hitBox);
        }
    }
    void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
            return;

        Gizmos.DrawWireCube(AttackPoint.position, attackSize);
    }
    public void ApplyDamage()
    {
        if (!target.GetComponent<Player>().isDashing) {
            CreateHitBox();
            StartCoroutine(WaitAndPrint(attackDuration));
            RemoveHitBox();
        }
    }

    IEnumerator WaitAndPrint(float waitTime)
    {
        // waitTime 초 동안 대기
        yield return new WaitForSeconds(waitTime);
    }

}
