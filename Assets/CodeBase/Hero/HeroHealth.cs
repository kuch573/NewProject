using System;
using System.Collections;
using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Hero
{
  public class HeroHealth : MonoBehaviour, ISavedProgress, IHealth
  {
    public HeroAnimator Animator;
    public HeroMove Move;
    
    private State _state;

    public event Action HealthChanged;

    public float Current
    {
      get => _state.CurrentHP;
      set
      {
        if (value != _state.CurrentHP)
        {
          _state.CurrentHP = value;
          
          HealthChanged?.Invoke();
        }
      }
    }

    public float Max
    {
      get => _state.MaxHP;
      set => _state.MaxHP = value;
    }


    public void LoadProgress(PlayerProgress progress)
    {
      _state = progress.HeroState;
      HealthChanged?.Invoke();
    }

    public void UpdateProgress(PlayerProgress progress)
    {
      progress.HeroState.CurrentHP = Current;
      progress.HeroState.MaxHP = Max;
    }

    public void TakeDamage(float damage)
    {
      if(Current <= 0)
        return;
      
      Current -= damage;
      Animator.PlayHit();
    }

    public void TakeSpecialDamage(float specialDamage)
    {
      if(Current <= 0)
        return;

      Current -= specialDamage;
      StartCoroutine(Hit());
    }

    IEnumerator Hit()
    {
      Move.enabled = false;
      Animator.PlaySpecialHit();
      yield return new WaitForSeconds(2f);
      Move.enabled = true;
    }
  }
}