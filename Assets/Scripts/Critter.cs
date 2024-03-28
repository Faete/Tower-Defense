using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class Critter : ScriptableObject
{
    public bool catchable;
    public bool canSpawnOnWater;

    public new string name;
    public int level;
    public int experience;
    public int experienceToLevel;
    public int baseExperienceGranted;
    public float health;
    public Sprite sprite;

    public float baseAttack;
    public float reloadTime;
    public float projectileSpeed;
    public float attackRange;
    public Sprite projectileSprite;

    public float moveSpeed;
}
