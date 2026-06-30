using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform AttackPoint;
    public Vector2 attackSize;
    public LayerMask enemyLayers;
    public float attackDuration = 0.5f;
    private Animator animator;
    private int attackCombo;
    public bool attacked;
    private float comboTimer;
    public float comboDelay = 1f; // 콤보가 초기화되는 시간
    public Status status;
    void Start()
    {
        animator = GetComponent<Animator>();
        attackCombo = 0;
        attacked = false;
        comboTimer = 0f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyManager.defaultKeys[1]) && !attacked)
        {
            Attack();
        }

        if (attackCombo > 0)
        {
            comboTimer += Time.deltaTime;
            if (comboTimer > comboDelay)
            {
                attackCombo = 0;
                comboTimer = 0f;
                animator.SetTrigger("attackend");
            }
        }
    }

    void Attack()
    {
        attackCombo++;
        comboTimer = 0f;

        if (attackCombo == 1)
        {
            StartCoroutine(AttackRoutine("Attack1"));
        }
        else if (attackCombo == 2)
        {
            StartCoroutine(AttackRoutine("Attack2"));
        }
        else if (attackCombo == 3)
        {
            StartCoroutine(AttackRoutine("Attack3"));
            attackCombo = 0;
        }
    }

    IEnumerator AttackRoutine(string trigger)
    {
        attacked = true;

        // 공격 애니메이션 실행
        animator.SetTrigger(trigger);

        // 공격 판정 생성
        CreateHitBox();

        // 공격 판정 지속 시간 동안 대기
        yield return new WaitForSeconds(attackDuration);

        // 공격 판정 제거
        RemoveHitBox();

        attacked = false;
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
}
