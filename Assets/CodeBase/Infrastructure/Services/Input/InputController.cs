using CodeBase.Hero;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Input
{
  public class InputController : MonoBehaviour
  {
      [SerializeField] private HeroMove heroMove;
      [SerializeField] private HeroAttack heroAttack;
      
      protected float horInput;
      protected float vertInput;
      

      private void Update()
      {
          horInput = UnityEngine.Input.GetAxis("Horizontal");
          vertInput = UnityEngine.Input.GetAxis("Vertical");

          if (horInput != 0 || vertInput != 0)
          {
              heroMove.Move();
          }
          

          if (UnityEngine.Input.GetButtonDown("Dive"))
          {
              heroMove.Dive();
          }

          /*if (UnityEngine.Input.GetButtonUp("Fire1"))
          {
              heroAttack.Attack();
          }*/

      }
      
  }
}