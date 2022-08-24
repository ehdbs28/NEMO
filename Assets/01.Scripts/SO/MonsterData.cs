using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterData", menuName = "SO/MonsterData")]
public class MonsterData : ScriptableObject
{
    public float health;
    public float damage;
    public float speed;
    public Vector3 size;
}
