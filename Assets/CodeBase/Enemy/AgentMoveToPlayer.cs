using System;
using CodeBase.Data;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
  public class AgentMoveToPlayer : Follow
  {
    private const float MinimalDistance = 1;
    
    public NavMeshAgent Agent;
    public EnemyDeath Death;
    
    private Transform _heroTransform;

    public void Constract(Transform heroTransform) => 
      _heroTransform = heroTransform;

    private void Start()
    {
      Death.Happaned += Die;
    }

    private void OnDestroy()
    {
      Death.Happaned -= Die;
    }

    private void Die()
    {
      _heroTransform = this.transform;
    }

    private void Update()
    {
      if(IsInitialized() && IsHeroNotReached())
        Agent.destination = _heroTransform.position;
    }
    

    private bool IsInitialized() => 
      _heroTransform != null;
    

    private bool IsHeroNotReached() => 
      Agent.transform.position.SqrMagnitudeTo(_heroTransform.position) >= MinimalDistance;
  }
}