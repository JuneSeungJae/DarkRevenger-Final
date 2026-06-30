using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Spike : MonoBehaviour
{
    Player player;
    public int damageAmount = 2; // 가시에서 주는 대미지 양

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 객체가 플레이어인지 확인
        if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어에게 대미지를 입힘
            player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damageAmount);
            }
        }
    }
}
