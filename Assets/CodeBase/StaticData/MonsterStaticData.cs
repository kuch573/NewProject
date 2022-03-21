using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "MonsterData", menuName = "StaticData/Monster")]
    public class MonsterStaticData : ScriptableObject
    {
        public MonsterTypeId MonsterTypeId;
        
        [Range(1, 100)]
        public int Hp = 100;
        
        [Range(1f,100f)]
        public float Damage = 20;

        [Range(0.5f,5f)]
        public float EffectiveDistance = 0.5f;

        [Range(0.5f,3f)]
        public float Cleavage = 0.5f;

        [Range(0, 10)] 
        public float MoveSpeed = 3;

        public GameObject Prefab;
    }
}