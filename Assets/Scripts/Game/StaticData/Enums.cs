using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetFaction
{
    None = -1,
    Character = 0,
    Enemy = 1,
    AllyVehicleCore = 2,
    EnemyVehicle = 3,
}

public enum InputType
{
    Direction = 0,
    LeftClick = 1,
    Interact = 2,
    Reload = 3,
    ActiveSkill = 4,
}

public enum ActionType 
{
    None = -1,
    DefaultMove = 0,
    Fire = 1,
    Interact = 2,
    Reload = 3,
    ActiveSkill = 4,
}

public enum SpriteDirection
{
    Right = 0,
    Left = 1,
}