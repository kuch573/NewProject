using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.UI.Elements
{
  public class ActorUI : MonoBehaviour
  {
    public HpBar HpBar;

    private IHealth _health;

    public void Construct(IHealth health)
    {
      _health = health;
      _health.HealthChanged += UpdateHpBar;
    }
    
    private void OnDestroy()
    {
      _health.HealthChanged -= UpdateHpBar;
    }

    private void UpdateHpBar()
    {
      HpBar.SetValue(_health.Current, _health.Max);
    }

  }
}