using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public Transform AttackPoint;
    public Vector2 attackSize;
    float attackDelay;
    Enemy enemy;
    public Player player;
    public LayerMask enemyLayers;
    Animator enemyAnimator;
    public float attackDuration = 0.5f;
    public int face = -1;
    void Start()
    {
        enemy = GetComponent<Enemy>();
        enemyAnimator = enemy.enemyAnimator;
    }

    void Update()
    {
        attackDelay -= Time.deltaTime;
        if (attackDelay < 0) attackDelay = 0;

        float distance = Vector3.Distance(transform.position, target.position);

        if (attackDelay == 0 && distance <= enemy.status.fieldOfVision)
        {
            FaceTarget();

            if (distance <= enemy.status.atkRange)
            {
                AttackTarget();
            }
            else
            {
                if (!enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                {
                    MoveToTarget();
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
        transform.Translate(new Vector2(dir, 0) * enemy.status.moveSpeed * Time.deltaTime);
        enemyAnimator.SetBool("moving", true);
    }

    void FaceTarget()
    {
        if (target.position.x - transform.position.x < 0) // 타겟이 왼쪽에 있을 때
        {
            transform.localScale = new Vector3(face, 1, 1);
        }
        else // 타겟이 오른쪽에 있을 때
        {
            transform.localScale = new Vector3(-(face), 1, 1);
        }
    }

    void AttackTarget()
    {
        if (!target.GetComponent<Player>().isDashing) {
            enemyAnimator.SetTrigger("attack"); // 공격 애니메이션 실행
            attackDelay = enemy.status.atkSpeed; // 딜레이 충전
        }
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
                enemy.GetComponent<Player>().TakeDamage(this.enemy.status.atkDmg);
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