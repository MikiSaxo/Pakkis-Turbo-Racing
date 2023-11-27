using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using Character.State;
using Kayak.Data;
using Sound;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using WaterAndFloating;
using Random = System.Random;

namespace Kayak
{
    [SelectionBase]
    [RequireComponent(typeof(Rigidbody))]
    public class KayakController : MonoBehaviour
    {
        public KayakData Data;

        public bool CanGo { get; set; }

        [field: SerializeField, Tooltip("Reference of the kayak rigidbody")]
        public Rigidbody Rb { get; private set; }

        [ReadOnly, Tooltip("If this value is <= 0, the drag reducing will be activated")]
        public float DragReducingTimer;

        [ReadOnly, Tooltip("= is the drag reducing method activated ?")]
        public bool CanReduceDrag = true;
        // [SerializeField, Tooltip("The floaters associated to the kayak's rigidbody")] public Floaters FloatersRef;

        [Header("VFX"), SerializeField] public ParticleSystem LeftPaddleParticle;
        [SerializeField] public ParticleSystem RightPaddleParticle;
        
        [Header("Materials"), SerializeField] private Material[] _kayakMatColor;
        [SerializeField] private MeshRenderer _kayakMat;

        [Header("Events")] public UnityEvent OnKayakCollision;
        public UnityEvent OnKayakSpeedHigh;
        [SerializeField] private float _magnitudeToLaunchEventSpeed;
        [SerializeField] private Vector2 _speedEventRecurrenceRandomBetween;

        //privates
        private float _speedEventCountDown;
        private float _particleTimer = -1;
        private CharacterNavigationState.Direction _particleSide;


        private void Start()
        {
            Rb = GetComponent<Rigidbody>();
            _kayakMat.material = _kayakMatColor[Manager.Instance.CurrentPlayerNumbers];
        }

        private void Update()
        {
            if (!CanGo)
                return;

            ClampVelocity();
            //ManageParticlePaddle();
            // ManageHighSpeedEvent();
        }

        private void FixedUpdate()
        {
            if (!CanGo)
                return;
            
            DragReducing();
        }

        private void OnCollisionEnter(Collision collision)
        {
            //CharacterManager characterManager = CharacterManager.Instance;
            // float value = collision.relativeVelocity.magnitude / Data.KayakValues.CollisionToBalanceMagnitudeDivider;
            //
            // if (collision.gameObject.GetComponent<Iceberg>() != null)
            // {
            //     Rb.velocity = Vector3.zero;
            // }

            //Debug.Log($"collision V.M. :{Math.Round(collision.relativeVelocity.magnitude)} -> {Math.Round(value,2)}");
            // characterManager.AddBalanceValueToCurrentSide(value);
            //OnKayakCollision.Invoke();
        }

        /// <summary>
        /// Clamp the kayak velocity x & z between -maximumFrontVelocity & maximumFrontVelocity
        /// </summary>
        private void ClampVelocity()
        {
            Vector3 velocity = Rb.velocity;
            KayakParameters kayakValues = Data.KayakValues;

            
            
            float velocityX = velocity.x;
            // float maxClamp = CharacterManager.Instance.SprintInProgress ? 
            //     kayakValues.MaximumFrontSprintVelocity :
            //     kayakValues.MaximumFrontVelocity * CharacterManager.Instance.PlayerStats.MaximumSpeedMultiplier;
            float maxClamp = kayakValues.MaximumFrontVelocity;

            velocityX = Mathf.Clamp(velocityX, -maxClamp, maxClamp);

            float velocityZ = velocity.z;
            velocityZ = Mathf.Clamp(velocityZ, -maxClamp, maxClamp);

            Rb.velocity = new Vector3(velocityX, velocity.y, velocityZ);
        }

        /// <summary>
        /// Artificially reduce the kayak drag to let it slide longer on water
        /// </summary>
        private void DragReducing()
        {
            if (DragReducingTimer > 0 || CanReduceDrag == false)
            {
                DragReducingTimer -= Time.deltaTime;
                return;
            }

            Vector3 velocity = Rb.velocity;
            float absX = Mathf.Abs(velocity.x);
            float absZ = Mathf.Abs(velocity.z);

            if (absX + absZ > 1)
            {
                Rb.velocity = new Vector3(
                    velocity.x * Data.DragReducingMultiplier * Time.deltaTime,
                    velocity.y,
                    velocity.z * Data.DragReducingMultiplier * Time.deltaTime);
            }
        }

        public void PlayPaddleParticle(CharacterNavigationState.Direction side)
        {
            _particleTimer = Data.TimeToPlayParticlesAfterPaddle;
            _particleSide = side;
        }

        private void ManageParticlePaddle()
        {
            if (_particleTimer > 0)
            {
                _particleTimer -= Time.deltaTime;
                if (_particleTimer <= 0)
                {
                    _particleTimer = -1;
                    switch (_particleSide)
                    {
                        case CharacterNavigationState.Direction.Left:
                            if (LeftPaddleParticle != null)
                            {
                                LeftPaddleParticle.Play();
                            }

                            break;
                        case CharacterNavigationState.Direction.Right:
                            if (RightPaddleParticle != null)
                            {
                                RightPaddleParticle.Play();
                            }

                            break;
                    }
                }
            }
        }

        private void ManageHighSpeedEvent()
        {
            _speedEventCountDown -= Time.deltaTime;
            if (_speedEventCountDown > 0 || Rb.velocity.magnitude < _magnitudeToLaunchEventSpeed)
            {
                return;
            }

            OnKayakSpeedHigh.Invoke();
            _speedEventCountDown = UnityEngine.Random.Range(_speedEventRecurrenceRandomBetween.x,
                _speedEventRecurrenceRandomBetween.y);
        }
    }
}