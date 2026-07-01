using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class BossAI3 : MonoBehaviour
{
    public Vector3 tarforom;
    public Transform target;
    float attackDelay;
    float castDelay; // 캐스트 공격 지연 시간
    Boss3 boss3;
    Animator enemyAnimator;
    private bool isHit = false; // 공격받았는지 여부
    private SpriteRenderer spriteRenderer;
    public Player player;
    public GameObject attackSpritePrefab; // Attack sprite prefab
    public float attackSpriteDuration = 1f; // Duration for the attack sprite on the ground
    private bool isCasting = false; // 캐스트 중인지 여부
    public Transform AttackPoint;
    public Vector2 attackSize;
    public LayerMask enemyLayers;
    public float attackDuration = 0.5f;
    void Start()
    {
        boss3 = GetComponent<Boss3>();
        enemyAnimator = boss3.enemyAnimator;
        spriteRenderer = GetComponent<SpriteRenderer>(); // 스프라이트 렌더러 가져오기
    }

    void Update()
    {
        if (isHit) return; // Hit 애니메이션 중에는 아무 것도 하지 않음
        attackDelay -= Time.deltaTime;
        if (attackDelay < 0) attackDelay = 0;

        castDelay -= Time.deltaTime; // 캐스트 지연 시간 감소
        if (castDelay < 0) castDelay = 0; // 캐스트 지연 시간이 0 이하로 떨어지지 않게 함


        float distance = Vector3.Distance(transform.position, target.position);

        if (isCasting) return;

        if (attackDelay == 0 && distance <= boss3.status.fieldOfVision) // 공격 지연 시간이 0이고 타겟이 인지 범위 내에 있을 때
        {
            FaceTarget(); // 타겟 방향으로 회전

            if (distance <= boss3.status.atkRange) // 타겟이 공격 범위 내에 있을 때
            {
                AttackTarget(true); // 공격 범위 내에서 타겟 공격
            }
            else
            {
                MoveToTarget(); // 타겟으로 이동
            }
            if (castDelay == 0 && distance <= boss3.status.fieldOfVision) // 캐스트 지연 시간이 0이고 타겟이 인지 범위 내에 있을 때
            {
                StartCoroutine(CastAttack());
            }
        }
        else
        {
            enemyAnimator.SetBool("moving", false);
        }
    }

    void MoveToTarget()
    {
        if (!isCasting) // 캐스트 중이 아닐 때만 움직임
        {
            float dir = target.position.x - transform.position.x;
            dir = (dir < 0) ? -1 : 1;
            transform.Translate(new Vector2(dir, 0) * boss3.status.moveSpeed * Time.deltaTime);
            enemyAnimator.SetBool("moving", true);
        }
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

    void AttackTarget(bool isInAtkRange)
    {
        if (!target.GetComponent<Player>().isDashing) // 타겟이 대쉬 중이 아닐 때만 공격
        {
            int num = Random.Range(1, 3); // 1, 2 중 랜덤 숫자 생성
            if (num == 1 && isInAtkRange) // 1이 나왔고 공격 범위 내에 있을 때
            {
                enemyAnimator.SetTrigger("attack"); // 공격 애니메이션 실행
            }
            else if (num == 2 && isInAtkRange) // 2가 나왔고 공격 범위 내에 있을 때
            {
                enemyAnimator.SetTrigger("attack2"); // 공격2 애니메이션 실행
            }
            attackDelay = boss3.status.atkSpeed; // 공격 지연 시간 설정
        }
    }
    IEnumerator CastAttack()
    {
        isCasting = true;
        enemyAnimator.SetTrigger("cast");
        tarforom = target.position;
        enemyAnimator.SetBool("moving", false);

        // 캐스트 애니메이션의 길이만큼 대기
        yield return new WaitForSeconds(enemyAnimator.GetCurrentAnimatorStateInfo(0).length);
        
        isCasting = false;
        castDelay = 3f;
        // 바로 걷는 애니메이션 재생
        GameObject attackSprite = Instantiate(attackSpritePrefab, tarforom, Quaternion.identity);
        Destroy(attackSprite, attackSpriteDuration);
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
                enemy.GetComponent<Player>().TakeDamage(boss3.status.atkDmg);
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
        if(!target.GetComponent<Player>().isDashing){
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