using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Enemy
{
  [RequireComponent(typeof(EnemyHealth), typeof(EnemyAnimator))]
  public class EnemyDeath : MonoBehaviour
  {
    public EnemyHealth Health;
    public EnemyAnimator Animator;

    public event Action Happaned;

    private void Start()
    {
      Health.HealthChanged += OnHealthChanged;
    }

    private void OnDestroy()
    {
      Health.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged()
    {
      if (Health.Current <= 0)
        Die();
    }

    private void Die()
    {
      Health.HealthChanged -= OnHealthChanged;

      Animator.PlayDeath();

      StartCoroutine(DestroyTimer());
      
      Happaned?.Invoke();
    }
    

    private IEnumerator DestroyTimer()
    {
      yield return new WaitForSeconds(3);
      Destroy(gameObject);
    }
  }
}