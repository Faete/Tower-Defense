using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class Critter : ScriptableObject
{
    public new string name;
    public bool catchable;

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

    public float AttackPower(){
        return baseAttack * Mathf.Pow(1.05f, level);
    }

    public float Health(){
        return health * Mathf.Pow(1.05f, level);
    }

    public void GainExperience(int exp){
        experience += exp;
        if(experience >= experienceToLevel){
            level++;
            experience -= experienceToLevel;
            experienceToLevel += 100;
        }
    }
}
