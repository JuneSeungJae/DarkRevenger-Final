using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status
{
    public UnitCode unitCode { get; } // 바꿀 수 없게 get만
    public string name { get; set; }
    public int maxHp { get; set; }
    public int nowHp { get; set; }
    public int atkDmg { get; set; }
    public float atkSpeed { get; set; }
    public float moveSpeed { get; set; }
    public float atkRange { get; set; }
    public float fieldOfVision { get; set; }

    public Status()
    {
    }

    public Status(UnitCode unitCode, string name, int maxHp, int atkDmg, float atkSpeed, float moveSpeed, float atkRange, float fieldOfVision)
    {
        this.unitCode = unitCode;
        this.name = name;
        this.maxHp = maxHp;
        nowHp = maxHp;
        this.atkDmg = atkDmg;
        this.atkSpeed = atkSpeed;
        this.moveSpeed = moveSpeed;
        this.atkRange = atkRange;
        this.fieldOfVision = fieldOfVision;
    }

    public Status SetUnitStatus(UnitCode unitCode)
    {
        Status status = null;

        switch (unitCode)
        {
            case UnitCode.player:
                status = new Status(unitCode, "Player", 100, 10, 1f, 8f, 0, 0);
                break;
            case UnitCode.player1:
                status = new Status(unitCode, "Player1", 150, 20, 1f, 8f, 0, 0);
                break;
            case UnitCode.player2:
                status = new Status(unitCode, "Player2", 200, 30, 1f, 8f, 0, 0);
                break;
            case UnitCode.enemy1:
                status = new Status(unitCode, "Enemy1", 30, 5, 1.5f, 1.5f, 2f, 5f);
                break;
            case UnitCode.boss1:
                status = new Status(unitCode, "Boss1", 300, 10, 1f, 2f, 3f, 20f);
                break;
            case UnitCode.enemy2:
                status = new Status(unitCode, "Enemy2", 80, 10, 1.5f, 1.5f, 2f, 5f);
                break;
            case UnitCode.boss2:
                status = new Status(unitCode, "Boss2", 600, 15, 1f, 2f, 3f, 20f);
                break;
            case UnitCode.enemy3:
                status = new Status(unitCode, "Enemy3", 150, 20, 1.5f, 1.5f, 2f, 5f);
                break;
            case UnitCode.boss3:
                status = new Status(unitCode, "Boss3", 900, 20, 1f, 2f, 3f, 20f);
                break;
        }
        return status;
    }
}
