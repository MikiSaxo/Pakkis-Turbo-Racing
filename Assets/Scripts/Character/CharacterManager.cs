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
        [field: SerializeField] public InputManagement InputManagementProperty { get; private set; }
        [field: SerializeField] public Animator PaddleAnimatorProperty { get; private set; }
        [field: SerializeField] public Animator CharacterAnimatorProperty { get; private set; }
        [field: SerializeField] public TransitionManager TransitionManagerProperty { get; private set; }
        [field: SerializeField] public MonoBehaviour CharacterMonoBehaviour { get; private set; }
        [field: SerializeField] public IKControl IKPlayerControl { get; private set; }
        [field: SerializeField] public PlayerParameters Parameters { get; set; }
        [field: SerializeField] public PlayerAbilities Abilities { get; set; }
        [field: SerializeField] public ScriptForDebug ScriptDebug { get; private set; }

        #endregion

        [Header("Character Data")]
        public CharacterData Data;
        [Range(0, 360)] public float BaseOrientation;
        [Header("VFX")]
        public ParticleSystem SplashLeft;
        public ParticleSystem SplashRight;

        [Header("Events")] public UnityEvent StartGame;
        public UnityEvent OnPaddle;
        public UnityEvent OnEnterSprint;
        public UnityEvent OnStopSprint;
        
        [HideInInspector] public bool IsGameLaunched;

        [ReadOnly] public bool SprintInProgress = false;
        [ReadOnly] public bool InWaterFlow = false;

        public PlayerStatsMultipliers PlayerStats;
 
        protected void Awake()
        {
            PlayerStats = new PlayerStatsMultipliers();
            
            // base.Awake();
            Cursor.visible = false;
            CharacterMonoBehaviour = this;
        }

        private void Start()
        {
            CharacterNavigationState navigationState = new CharacterNavigationState(this);
            CurrentStateBaseProperty = navigationState;
            CurrentStateBaseProperty.Initialize(this);
            print(CurrentStateBaseProperty);

            CurrentStateBaseProperty.EnterState(this);

            //rotate kayak
            Transform kayakTransform = KayakControllerProperty.transform;
            kayakTransform.eulerAngles = new Vector3(0, BaseOrientation, 0);

            //CameraManagerProperty.InitializeCams(kayakTransform);
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
        private void FixedUpdate()
        {
            CurrentStateBaseProperty.FixedUpdate(this);
        }
        
        public void SwitchState(CharacterStateBase stateBaseCharacter)
        {
            CurrentStateBaseProperty.ExitState(this);
            CurrentStateBaseProperty = stateBaseCharacter;
            stateBaseCharacter.EnterState(this);
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
