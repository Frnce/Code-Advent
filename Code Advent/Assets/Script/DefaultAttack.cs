﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Player;

[CreateAssetMenu(menuName ="Abilities/DefaultAttack")]
public class DefaultAttack : Ability
{
    public int damage = 1;
    private PlayerController player;

    public override void Initialize(GameObject obj)
    {
        player = obj.GetComponent<PlayerController>();
    }
    public override void TriggerAbility()
    {
        player.Attack();
    }
}
