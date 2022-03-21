using System;
using System.Collections;
using CodeBase.Data;
using CodeBase.Enemy;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Hero
{
  [RequireComponent(typeof(HeroAnimator), typeof(CharacterController))]
  public class HeroAttack : MonoBehaviour, ISavedProgressReader
  {
    public HeroAnimator Animator;
    public CharacterController CharacterController;

    private static int _layerMask;
    private Collider[] _hits = new Collider[3];
    private Stats _stats;

    private void Awake()
    {
      _layerMask = 1 << LayerMask.NameToLayer("Hittable");
    }
    
    private void Update()
    {
      if (Input.GetButtonUp("Fire1") && !Animator.IsAttacking && !Animator.IsDiveRoll)
      {
        StartCoroutine(Attack());
        //Animator.PlayAttack();
      }
    }

    private void OnAttack()
    {
      PhysicsDebug.DrawDebug(StartPoint() + transform.forward, _stats.DamageRadius, 1.0f);
      for (int i = 0; i < Hit(); ++i)
      {
        Debug.Log("OnAttack");
        _hits[i].transform.parent.GetComponent<IHealth>().TakeDamage(_stats.Damage);
      }
    }

    private int Hit() => 
      Physics.OverlapSphereNonAlloc(StartPoint() + transform.forward, _stats.DamageRadius, _hits, _layerMask);

    private Vector3 StartPoint() =>
      new Vector3(transform.position.x, CharacterController.center.y / 2, transform.position.z);

    public void LoadProgress(PlayerProgress progress)
    {
      _stats = progress.HeroStats;
    }

    IEnumerator Attack()
    {
      CharacterController.enabled = false;
      Animator.PlayAttack();
      yield return new WaitForSeconds(1.5f);
      CharacterController.enabled = true;
    }
  }
}