using UnityEngine;
using System;

[Serializable]
public class Stat_OffenseGroup 
{
    public Stat attackSpeed;

    // physical damage
    public Stat damage;
    public Stat critPower;
    public Stat critChance;
    public Stat armorReduction;

    // elemental damage 
    public Stat fireDamage;
    public Stat iceDamage;
    public Stat lightningDamage;
}
