using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Advent.Player;
using Advent.Abilities;

[CreateAssetMenu(menuName ="Abilities/DefaultAttack")]
public class DefaultAttack : Ability
{
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