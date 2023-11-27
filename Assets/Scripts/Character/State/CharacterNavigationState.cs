using System;
using System.Collections;
using Character.Camera;
using Kayak;
using Kayak.Data;
using Sound;
using UnityEngine;
using UnityEngine.Events;

namespace Character.State
{
    public class CharacterNavigationState : CharacterStateBase
    {
        //enum
        public enum Direction
        {
            Left = 0,
            Right = 1
        }

        private enum RotationType
        {
            Static = 0,
            Paddle = 1
        }

        //inputs
        private InputManagement _inputs;
        private float _staticInputTimer;

        //kayak
        private float _leftPaddleCooldown, _rightPaddleCooldown;
        private Direction _lastPaddleSide;

        //reference
        private KayakController _kayakController;
        private KayakParameters _kayakValues;

        private Rigidbody _kayakRigidbody;

        //priority
        private RotationType _lastInputType;
        private float _staticTime;
        private float _paddleTime;
        private bool _paddleWasReleased, _staticWasReleased;

        private float _timerLastInputTrigger = 0;
        private Direction _lastInputPaddle;
        private int _paddleCount = 0;
        private float _paddleCooldownCurrent = 0;
        private float _paddleCooldownAddPaddle = 0;
        private float _paddleCooldown = 0;
        private float _paddleForceSprint = 0;
        private int _paddleNumber = 0;
        private bool _hasReleasePaddle = false;

        #region Constructor

        public CharacterNavigationState(CharacterManager characterManager) : base()
        {
            CharacterManagerRef = characterManager;
            _kayakController = CharacterManagerRef.KayakControllerProperty;
            _kayakRigidbody = CharacterManagerRef.KayakControllerProperty.Rb;
            _kayakValues = CharacterManagerRef.KayakControllerProperty.Data.KayakValues;

            _lastInputType = RotationType.Static;
        }

        #endregion

        #region CharacterBaseState overrided function

        public override void EnterState(CharacterManager character)
        {
            //values
            _rightPaddleCooldown = _kayakValues.PaddleCooldown;
            _leftPaddleCooldown = _kayakValues.PaddleCooldown;
            _staticInputTimer = _kayakValues.StaticRotationCooldownAfterPaddle;

            _paddleCooldown = CharacterManagerRef.KayakControllerProperty.ResetPaddleTurbo;
            _paddleNumber = CharacterManagerRef.KayakControllerProperty.NbPaddleToSprint;
            _paddleForceSprint = CharacterManagerRef.KayakControllerProperty.SprintPower;
            _hasReleasePaddle = true;
                
            //booleans
            CanBeMoved = true;
            CanCharacterMakeActions = true;

            //anim
            TimeBeforeSettingPaddleAnimator = 1f;
        }

        public override void UpdateState(CharacterManager character)
        {
            PaddleCooldownManagement();

            // if (_timerLastInputTrigger > _kayakValues.TimerMaxForSprint)
            // {
            //     CharacterManagerRef.SprintInProgress = false;
            // }

            if (_paddleCount > 0)
            {
                _paddleCooldownCurrent += Time.deltaTime;

                if (_paddleCooldownCurrent >= _paddleCooldown)
                {
                    _paddleCooldownCurrent = 0;
                    _paddleCount = 0;
                    Debug.Log("reduce to 0");
                }
            }
        }

        public override void FixedUpdate(CharacterManager character)
        {
            SetBrakeAnimationToFalse();

            if (CanCharacterMove == false)
            {
                StopCharacter();
                return;
            }

            ManageKayakMovementInputs();

            KayakRotationManager(RotationType.Paddle);
            KayakRotationManager(RotationType.Static);

            VelocityToward();
        }

        public override void SwitchState(CharacterManager character)
        {
        }

        public override void ExitState(CharacterManager character)
        {
            //DisableSprint();
        }

        #endregion

        #region Methods

        /// <summary>
        /// manages the rotation of the kayak based on the input rotation type and updates the relevant rotation force values.
        /// </summary>
        private void KayakRotationManager(RotationType rotationType)
        {
            //get rotation
            float rotationForceY = rotationType == RotationType.Paddle ? RotationPaddleForceY : RotationStaticForceY;

            //calculate rotation
            if (Mathf.Abs(rotationForceY) > 0.001f)
            {
                rotationForceY = Mathf.Lerp(rotationForceY, 0,
                    rotationType == RotationType.Paddle
                        ? _kayakValues.PaddleRotationDeceleration
                        : _kayakValues.StaticRotationDeceleration);
            }
            else
            {
                rotationForceY = 0;
            }

            //apply transform
            Transform kayakTransform = _kayakController.transform;
            kayakTransform.Rotate(Vector3.up, rotationForceY);

            //changes values
            switch (rotationType)
            {
                case RotationType.Paddle:
                    RotationPaddleForceY = rotationForceY;
                    break;
                case RotationType.Static:
                    RotationStaticForceY = rotationForceY;
                    break;
            }
        }

        /// <summary>
        /// Lerp the character velocity to 0
        /// </summary>
        private void StopCharacter()
        {
            //_kayakRigidbody.velocity = Vector3.Lerp(_kayakRigidbody.velocity, Vector3.zero, 0.01f);
        }

        /// <summary>
        /// Set the animators brake booleans to false
        /// </summary>
        private void SetBrakeAnimationToFalse()
        {
            CharacterManagerRef.PaddleAnimatorProperty.SetBool("BrakeLeft", false);
            CharacterManagerRef.PaddleAnimatorProperty.SetBool("BrakeRight", false);
            CharacterManagerRef.CharacterAnimatorProperty.SetBool("BrakeLeft", false);
            CharacterManagerRef.CharacterAnimatorProperty.SetBool("BrakeRight", false);
        }

        /// <summary>
        /// Manage the kayak static/paddle movement method choice
        /// </summary>
        private void ManageKayakMovementInputs()
        {
            const float timeToSetLastInput = 1.5f;
            bool staticInput = CharacterManagerRef.RotateLeft != false || CharacterManagerRef.RotateRight != false;
            bool paddleInput = CharacterManagerRef.PaddleLeft || CharacterManagerRef.PaddleRight;

            if (((paddleInput == false) ||
                 (_lastInputType == RotationType.Paddle) ||
                 (_paddleWasReleased) == false && _lastInputType == RotationType.Paddle) &&
                _staticInputTimer <= 0 && staticInput)
            {
                HandleStaticRotation();

                _staticTime += Time.deltaTime;
                if (_staticTime >= timeToSetLastInput)
                {
                    _paddleTime = 0f;
                    _lastInputType = RotationType.Static;
                }
            }

            // Debug.Log($"Left {CharacterManagerRef.PaddleLeft} / Right {CharacterManagerRef.PaddleRight}");
            if (CharacterManagerRef.PaddleLeft == false && CharacterManagerRef.PaddleRight == false)
            {
                // Debug.Log($"Release");
                _hasReleasePaddle = true;
            }
            
            if (((staticInput == false) ||
                 (_lastInputType == RotationType.Static)) &&
                paddleInput)
            {
                HandlePaddleMovement();

                _paddleTime += Time.deltaTime;
                if (_paddleTime >= timeToSetLastInput)
                {
                    _staticTime = 0f;
                    _paddleWasReleased = false;
                    if (staticInput == false)
                    {
                        _lastInputType = RotationType.Paddle;
                    }
                }
            }

            if (paddleInput == false)
            {
                _paddleWasReleased = true;
                if (_paddleTime >= timeToSetLastInput)
                {
                    _lastInputType = RotationType.Paddle;
                }
            }
        }

        #endregion

        #region Paddle Movement

        /// <summary>
        /// Handle the paddling of the kayak, add rotation force, launch the paddleForceCurve method to move the rigidbody, play paddle sound, set animation paddle trigger
        /// </summary>
        private void Paddle(Direction direction)
        {
            //timers
            _kayakController.DragReducingTimer = 0.5f;
            _staticInputTimer = _kayakValues.StaticRotationCooldownAfterPaddle;

            //rotation
            if (direction == _lastPaddleSide)
            {
                float rotation = _kayakValues.PaddleSideRotationForce *
                                 CharacterManagerRef.PlayerStats.RotationSpeedMultiplier;
                RotationPaddleForceY += direction == Direction.Right ? -rotation : rotation;
            }

            _lastPaddleSide = direction;

            //force
            MonoBehaviourRef.StartCoroutine(PaddleForceCurve());

            //animation & particles
            CharacterManagerRef.PaddleAnimatorProperty.SetTrigger(direction == Direction.Left
                ? "PaddleLeft"
                : "PaddleRight");
            CharacterManagerRef.CharacterAnimatorProperty.SetTrigger(direction == Direction.Left
                ? "PaddleLeft"
                : "PaddleRight");
            _kayakController.PlayPaddleParticle(direction);

            //events
            switch (direction)
            {
                case Direction.Left:
                    OnPaddleLeft.Invoke();
                    break;
                case Direction.Right:
                    OnPaddleRight.Invoke();
                    break;
            }

            CharacterManagerRef.OnPaddle.Invoke();
        }

        /// <summary>
        /// Detect & verify paddle input and launch paddle method 
        /// </summary>
        private void HandlePaddleMovement()
        {
            //input -> paddleMovement
            // if (CharacterManagerRef.PaddleRight && CharacterManagerRef.PaddleLeft)
            // {
            //     HandleBothPress();
            // }

            // _paddleCooldownAddPaddle += Time.deltaTime;
            
            if (CharacterManagerRef.PaddleLeft && CharacterManagerRef.PaddleRight == false
                || CharacterManagerRef.PaddleRight && CharacterManagerRef.PaddleLeft == false)
            {
                if (Manager.Instance.IsGameStarted && _hasReleasePaddle)
                {
                    _paddleCooldownAddPaddle = 0;
                    _paddleCount++;
                    _paddleCooldownCurrent = 0;
                    _hasReleasePaddle = false;
                    Debug.Log($"Count {_paddleCount}");
                }
            }

            
            
            if (CharacterManagerRef.PaddleLeft && _leftPaddleCooldown <= 0 && CharacterManagerRef.PaddleRight == false)
            {
                Direction direction =
                    CharacterManagerRef.Parameters.InversedControls ? Direction.Right : Direction.Left;
                _leftPaddleCooldown = _kayakValues.PaddleCooldown;
                _rightPaddleCooldown = _kayakValues.PaddleCooldown / 2;
                CheckIfSprint(direction);
                Paddle(direction);
            }

            if (CharacterManagerRef.PaddleRight && _rightPaddleCooldown <= 0 && CharacterManagerRef.PaddleLeft == false)
            {
                Direction direction =
                    CharacterManagerRef.Parameters.InversedControls ? Direction.Left : Direction.Right;
                _rightPaddleCooldown = _kayakValues.PaddleCooldown;
                _leftPaddleCooldown = _kayakValues.PaddleCooldown / 2;
                CheckIfSprint(direction);
                Paddle(direction);
           
            }
        }

        private void HandleBothPress()
        {
            if (_leftPaddleCooldown < 0)
            {
                Direction direction =
                    CharacterManagerRef.Parameters.InversedControls ? Direction.Right : Direction.Left;
                _leftPaddleCooldown = _kayakValues.PaddleCooldown;
                _rightPaddleCooldown = _kayakValues.PaddleCooldown / 2;
                Paddle(direction);
            }

            if (_rightPaddleCooldown < 0)
            {
                Direction direction =
                    CharacterManagerRef.Parameters.InversedControls ? Direction.Left : Direction.Right;
                _rightPaddleCooldown = _kayakValues.PaddleCooldown;
                _leftPaddleCooldown = _kayakValues.PaddleCooldown / 2;
                Paddle(direction);
            }

            //change the rotation inertia to 0
            RotationPaddleForceY = Mathf.Lerp(RotationPaddleForceY, 0, 0.1f);
            RotationStaticForceY = Mathf.Lerp(RotationStaticForceY, 0, 0.1f);
        }

        private bool _isSprinting;
        private void CheckIfSprint(Direction direction)
        {
            // if (_lastInputPaddle == direction || CharacterManagerRef.Abilities.SprintUnlock == false)
            // {
            //     return;
            // }

            if (_paddleCount >= _paddleNumber)
            {
                Debug.Log("im sprinting");
                CharacterManagerRef.KayakControllerProperty.IsSprinting(true);
                CharacterManagerRef.OnEnterSprint.Invoke();
                _isSprinting = true;
                _paddleCount =_paddleNumber + 1;
            }
            else
            {
                DisableSprint();
            }

            _timerLastInputTrigger = 0;
            _lastInputPaddle = direction;
        }

        public void DisableSprint()
        {
            _isSprinting = false;
            CharacterManagerRef.KayakControllerProperty.IsSprinting(false);
            CharacterManagerRef.OnStopSprint.Invoke();
        }

        /// <summary>
        /// Add paddle force to the kayak a certain number of times
        /// </summary>
        private IEnumerator PaddleForceCurve()
        {
            float sprintMultiply = CharacterManagerRef.SprintInProgress ? _paddleForceSprint : 1;
            for (int i = 0; i <= _kayakValues.NumberOfForceAppliance; i++)
            {
                float x = 1f / _kayakValues.NumberOfForceAppliance * i;
                float force = (_kayakValues.ForceCurve.Evaluate(x) * _kayakValues.PaddleForce) * sprintMultiply;
                Vector3 forceToApply = _kayakController.transform.forward * force;
                _kayakRigidbody.AddForce(forceToApply);

                yield return new WaitForSeconds(_kayakValues.TimeBetweenEveryAppliance);
            }
        }

        /// <summary>
        /// Count the different cooldowns
        /// </summary>
        private void PaddleCooldownManagement()
        {
            _leftPaddleCooldown -= Time.deltaTime;
            _rightPaddleCooldown -= Time.deltaTime;

            _staticInputTimer -= Time.deltaTime;

            _timerLastInputTrigger += Time.deltaTime;
        }

        #endregion

        #region Rotate Movement

        /// <summary>
        ///detect static rotation input and apply static rotation by adding rotation force & setting animator booleans
        /// </summary>
        private void HandleStaticRotation()
        {
            if (_isSprinting)
                return;
            
            bool isFast = Mathf.Abs(_kayakRigidbody.velocity.x + _kayakRigidbody.velocity.z) >= 0.1f;

            //left
            if (CharacterManagerRef.RotateLeft) // > _inputs.Inputs.Deadzone)
            {
                if (isFast)
                {
                    DecelerationAndRotate(Direction.Right);
                }

                RotationStaticForceY += _kayakValues.StaticRotationForce;

                CharacterManagerRef.PaddleAnimatorProperty.SetBool("BrakeLeft", true);
                CharacterManagerRef.CharacterAnimatorProperty.SetBool("BrakeLeft", true);
            }
            else
            {
                CharacterManagerRef.PaddleAnimatorProperty.SetBool("BrakeLeft", false);
                CharacterManagerRef.CharacterAnimatorProperty.SetBool("BrakeLeft", false);
            }

            //right
            if (CharacterManagerRef.RotateRight) // > _inputs.Inputs.Deadzone)
            {
                if (isFast)
                {
                    DecelerationAndRotate(Direction.Left);
                }

                RotationStaticForceY -= _kayakValues.StaticRotationForce;

                CharacterManagerRef.PaddleAnimatorProperty.SetBool("BrakeRight", true);
                CharacterManagerRef.CharacterAnimatorProperty.SetBool("BrakeRight", true);
            }
            else
            {
                CharacterManagerRef.PaddleAnimatorProperty.SetBool("BrakeRight", false);
                CharacterManagerRef.CharacterAnimatorProperty.SetBool("BrakeRight", false);
            }
        }

        /// <summary>
        /// Lerp the kayak velocity to 0 and make add rotation force
        /// </summary>
        private void DecelerationAndRotate(Direction direction)
        {
            if (_isSprinting)
                return;
            
            Vector3 targetVelocity = new Vector3(0, _kayakRigidbody.velocity.y, 0);

            float lerp = _kayakValues.VelocityDecelerationLerp *
                         CharacterManagerRef.PlayerStats.BreakingDistanceMultiplier;
            _kayakRigidbody.velocity = Vector3.Lerp(_kayakRigidbody.velocity, targetVelocity, lerp);

            float force = _kayakValues.VelocityDecelerationRotationForce;

            RotationStaticForceY += direction == Direction.Left ? -force : force;
        }

        #endregion
    }
}