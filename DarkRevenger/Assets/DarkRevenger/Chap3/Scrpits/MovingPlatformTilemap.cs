using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovingPlatformTilemap : MonoBehaviour
{
    public Tilemap tilemap;
    public Vector3 startCell; // 타일맵의 시작 셀 위치
    public Vector3 endCell; // 타일맵의 끝 셀 위치
    public float speed = 2f; // 이동 속도

    private Vector3 startPosition;
    private Vector3 endPosition;
    private Vector3 targetPosition;

    void Start()
    {
        // 타일맵 셀 위치를 월드 좌표로 변환
        startPosition = tilemap.CellToWorld(Vector3Int.FloorToInt(startCell)) + tilemap.tileAnchor;
        endPosition = tilemap.CellToWorld(Vector3Int.FloorToInt(endCell)) + tilemap.tileAnchor;
        startPosition.y = transform.position.y; // 시작 위치의 Y값을 현재 플랫폼의 Y값으로 설정
        endPosition.y = transform.position.y; // 끝 위치의 Y값을 현재 플랫폼의 Y값으로 설정
        targetPosition = endPosition;
    }

    void Update()
    {// 플랫폼을 목표 지점으로 이동
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // 목표 지점에 도달했는지 확인
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // 목표 지점을 반대로 전환
            targetPosition = (targetPosition == startPosition) ? endPosition : startPosition;
        }
    }
}
