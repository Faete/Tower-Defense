using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class Critter : ScriptableObject
{
    public new string name;
    public int level;
    public int experience;
    public int experienceGranted;
    public float health;
    public Sprite sprite;

    public float attackPower;
    public float reloadTime;
    public float projectileSpeed;
    public float attackRange;
    public Sprite projectileSprite;

    public float moveSpeed;
}
