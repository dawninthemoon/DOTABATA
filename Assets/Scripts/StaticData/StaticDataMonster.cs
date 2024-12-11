using System;
using System.Collections;
using System.Collections.Generic;

public enum MonsterMoveType
{
    Ground,
    Air,
}

public class StaticDataMonster
{
    public int keyIndex;
    public int hp;
    public int atk;
    public float moveSpeed;
    public float attackSpeed;
    public MonsterMoveType moveType;
}
