using System;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class SpecialAttack : MonoBehaviour
    {
        public EnemyAnimator Animator;
        public AgentMoveToPlayer Follow;

        public float SpecialDamage = 10;
        public float MinDistance = 1;
        public float MaxDistance = 3;
        public float AttackCooldown = 2f;

        private Transform _heroTransform;
        private bool _isAttacking = false;
        private float _attackCooldown;

        public void Constract(Transform heroTransform) => 
            _heroTransform = heroTransform;
        
        private void Update()
        {
            UpdateCooldown();
            
            if (CanAttackSpecial())
            {
                StartAttack();
            }
        }

        private bool CanAttackSpecial()
        {
            return !_isAttacking && CooldownIsUp() && CheckRange();
        }

        private bool CheckRange()
        {
            return Vector3.Distance(transform.position, _heroTransform.position) > MinDistance &&
                   Vector3.Distance(transform.position, _heroTransform.position) < MaxDistance;
        }
        
        private void StartAttack()
        {
            DisableMove();
            transform.LookAt(_heroTransform);
            Animator.PlaySpecialAttack();
            _isAttacking = true;
        }
        
        private void OnSpecialAttack()
        {
            _heroTransform.GetComponent<IHealth>().TakeSpecialDamage(SpecialDamage);
        }

        private void OnSpecialAttackEnded()
        {
             EnableMove();
            _isAttacking = false;
            _attackCooldown = AttackCooldown;
        }
        
        private void UpdateCooldown()
        {
            if (!CooldownIsUp())
                _attackCooldown -= Time.deltaTime;
        }
        
        private bool CooldownIsUp()
        {
            return _attackCooldown <= 0f;
        }
        
        private void DisableMove()
        {
            Follow.enabled = false;
        }

        private void EnableMove()
        {
            Follow.enabled = true;
        }
    }
}