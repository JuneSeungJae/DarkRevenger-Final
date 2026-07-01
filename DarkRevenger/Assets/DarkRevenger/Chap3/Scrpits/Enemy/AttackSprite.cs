using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class AttackSprite : MonoBehaviour
{
    public int damage = 10; // 데미지 값
    private Animator animator;
    public Player player;
    void Start()
    {
        Player player = GetComponent<Player>();
        animator = GetComponent<Animator>();
        PlayAnimation();
    }

    void PlayAnimation()
    {
        animator.SetTrigger("play"); // 애니메이션 재생 트리거
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.status.nowHp -= damage;
        }
    }
}