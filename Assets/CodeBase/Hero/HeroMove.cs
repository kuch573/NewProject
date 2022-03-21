using System;
using CodeBase.Data;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Hero
{
    public class HeroMove : InputController, ISavedProgress
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private HeroAnimator _animator;
        [SerializeField] private float _movementSpeed;
        [SerializeField] private Transform _camera;
        private  Vector3 movement = Vector3.zero;

        public float rotSpeed = 15.0f;

        private float _vertSpeed;
        private float minFall = -1.5f;
        private bool hitGround = false;
        private float gravity = 9.8f;

        private void Start()
        {
            _vertSpeed = minFall;
        }

        private void FixedUpdate()
        {
            if (CheckGround(false))
            {
                _vertSpeed -= gravity * 1 * Time.deltaTime;
                movement.y = _vertSpeed;
                movement *= Time.deltaTime;
                _characterController.Move(movement);
            }
        }

        public void Move()
        {
            movement.x = horInput * _movementSpeed;
            movement.z = vertInput * _movementSpeed;
            movement = Vector3.ClampMagnitude(movement, _movementSpeed);

            Quaternion tmp = _camera.rotation;
            _camera.eulerAngles = new Vector3(0, _camera.eulerAngles.y, 0);
            movement = _camera.TransformDirection(movement);
            _camera.rotation = tmp;
            Quaternion direction = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation,
                direction, rotSpeed * Time.deltaTime);


            movement *= Time.deltaTime;
            _characterController.Move(movement);
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.WorldData.PositionOnLevel = new PositionOnLevel(CurrentLevel(), transform.position.AsVectorData());
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (CurrentLevel() != progress.WorldData.PositionOnLevel.Level) return;

            Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;
            if (savedPosition != null)
                Warp(to: savedPosition);
        }

        private static string CurrentLevel() =>
            SceneManager.GetActiveScene().name;

        private void Warp(Vector3Data to)
        {
            _characterController.enabled = false;
            transform.position = to.AsUnityVector().AddY(_characterController.height);
            _characterController.enabled = true;
        }

        private bool CheckGround(bool b)
        {
            RaycastHit hit;
            if (_vertSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit))
            {
                float check = (_characterController.height + _characterController.radius)/1.9f;
                hitGround = hit.distance <= check;  
            }
            return hitGround;
        }
        public void Dive()
        {
            if (CheckGround(true) && !_animator.IsDiveRoll)
            {
                _animator.PlayDiveRoll();
            }
        }
    }
}
