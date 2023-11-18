using System;
using Art.Script;
using Character.Data.Character;
using Character.State;
using GPEs.Checkpoint;
using Kayak;
using SceneTransition;
using UI;
using UI.Menu;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Character
{
    [Serializable]
    public class PlayerStatsMultipliers
    {
        public float BreakingDistanceMultiplier = 1;
        public float MaximumSpeedMultiplier = 1;
        public float RotationSpeedMultiplier = 1;
        
        public float ChargeTimeReducingMultiplier = 1;
        public float ExperienceGainMultiplier = 1;
    }
    
    public class CharacterManager : MonoBehaviour
    {
        #region Properties

        [field: SerializeField] public CharacterStateBase CurrentStateBaseProperty { get; private set; }
        [field: SerializeField] public KayakController KayakControllerProperty { get; private set; }
        [field: SerializeField] public Animator PaddleAnimatorProperty { get; private set; }
        [field: SerializeField] public Animator CharacterAnimatorProperty { get; private set; }
        [field: SerializeField] public MonoBehaviour CharacterMonoBehaviour { get; private set; }
        [field: SerializeField] public IKControl IKPlayerControl { get; private set; }
        [field: SerializeField] public PlayerParameters Parameters { get; set; }
        [field: SerializeField] public PlayerAbilities Abilities { get; set; }
        
        
        [field: SerializeField] public bool PaddleLeft { get; private set; }
        [field: SerializeField] public bool PaddleRight { get; private set; }
        [field: SerializeField] public bool RotateLeft { get; private set; }
        [field: SerializeField] public bool RotateRight { get; private set; }

        #endregion

        [Header("Character Data")]
        public CharacterData Data;
        [Range(0, 360)] public float BaseOrientation;
        [Header("VFX")]
        public ParticleSystem SplashLeft;
        public ParticleSystem SplashRight;

        public UnityEvent OnPaddle;
        public UnityEvent OnEnterSprint;
        public UnityEvent OnStopSprint;
        
 
        
        
        [ReadOnly] public bool SprintInProgress = false;

        public PlayerStatsMultipliers PlayerStats;
 
        protected void Awake()
        {
            PlayerStats = new PlayerStatsMultipliers();
            
            Cursor.visible = false;
            CharacterMonoBehaviour = this;
        }

        private void Start()
        {
            CharacterNavigationState navigationState = new CharacterNavigationState(this);
            CurrentStateBaseProperty = navigationState;
            CurrentStateBaseProperty.Initialize(this);

            CurrentStateBaseProperty.EnterState(this);

            //rotate kayak
            Transform kayakTransform = KayakControllerProperty.transform;
            kayakTransform.eulerAngles = new Vector3(0, BaseOrientation, 0);
        }
        
        private void Update()
        {
            CurrentStateBaseProperty.UpdateState(this);
            
            //anim
            if (IKPlayerControl.CurrentType != IKType.Paddle || IKPlayerControl.Type == IKType.Paddle)
            {
                return;
            }
            CurrentStateBaseProperty.TimeBeforeSettingPaddleAnimator -= Time.deltaTime;
            if (CurrentStateBaseProperty.TimeBeforeSettingPaddleAnimator <= 0)
            {
                IKPlayerControl.SetPaddle();
            }
        }
        
        public void OnPaddleLeft(InputAction.CallbackContext context)
        {
            PaddleLeft = context.action.triggered;
        }
        public void OnPaddleRight(InputAction.CallbackContext context)
        {
            PaddleRight = context.action.triggered;
        }
        public void OnRotaLeft(InputAction.CallbackContext context)
        {
            RotateLeft = context.action.triggered;
        }
        public void OnRotaRight(InputAction.CallbackContext context)
        {
            RotateRight = context.action.triggered;
        }
        
        private void FixedUpdate()
        {
            CurrentStateBaseProperty.FixedUpdate(this);
        }
        
        public void SendDebugMessage(string message)
        {
            Debug.Log(message);
        }
    }

    [Serializable]
    public struct PlayerParameters
    {
        public bool InversedControls;
    }
    [Serializable]
    public struct PlayerAbilities
    {
        public bool SprintUnlock;
        public bool CanDestroyIceberg;
    }
}
