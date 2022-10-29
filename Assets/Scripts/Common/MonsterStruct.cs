using System;
using UnityEngine;

[Serializable]
public struct MonsterStruct
{
    [SerializeField] private GameObject monsterImage;
    [SerializeField] private GameObject monsterPrefab;
    
    public GameObject MonsterImage => monsterImage;
    public GameObject MonsterPrefab => monsterPrefab;
}
