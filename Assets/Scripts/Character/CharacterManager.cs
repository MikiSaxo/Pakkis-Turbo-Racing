using System;
using System.Collections;
using Art.Script;
using Character.Data.Character;
using Character.State;
using DG.Tweening;
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


        [field: SerializeField] public bool Click { get; private set; }
        [field: SerializeField] public bool PaddleLeft { get; private set; }
        [field: SerializeField] public bool PaddleRight { get; private set; }
        [field: SerializeField] public bool RotateLeft { get; private set; }
        [field: SerializeField] public bool RotateRight { get; private set; }

        #endregion

        [Header("------------------- New")] [SerializeField]
        private bool _isTesting;

        [SerializeField] private UIPlayer _uiPlayer;

        [Header("-------------------")] [Space(20f)] [Header("Character Data")]
        public CharacterData Data;

        [Range(0, 360)] public float BaseOrientation;
        [Header("VFX")] public ParticleSystem SplashLeft;
        public ParticleSystem SplashRight;

        public UnityEvent OnPaddle;
        public UnityEvent OnEnterSprint;
        public UnityEvent OnStopSprint;

        private Vector3 _startPos;
        private bool _isReady;
        private float _cooldownResetPos;


        [ReadOnly] public bool SprintInProgress = false;

        public bool CanGo { get; set; }
        public bool CanPaddle { get; set; }
        public bool IsDead { get; set; }

        public ColorKayak KayakColor { get; set; }

        public PlayerStatsMultipliers PlayerStats;

        private bool _hasInit;

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

            int color = Manager.Instance.CurrentPlayerNumbers;
            KayakColor = (ColorKayak)color;
            //rotate kayak
            Transform kayakTransform = KayakControllerProperty.transform;
            kayakTransform.eulerAngles = new Vector3(0, BaseOrientation, 0);

            _startPos = Manager.Instance.GetStartPos();
            Manager.Instance.UIPlayers.Add(_uiPlayer);
            Manager.Instance.Players.Add(this);

            // CanPaddle = true;

            if (_isTesting)
            {
                KayakControllerProperty.CanGo = true;
                CanGo = true;
            }

            ResetPos();
            StartCoroutine(WaitToDecal());
        }

        IEnumerator WaitToDecal()
        {
            yield return new WaitForSeconds(.5f);
            ResetPos();
            _hasInit = true;
        }


        private void Update()
        {
            if (IsDead || Manager.Instance.IsGameEnded)
                return;

            if (!CanGo && !_isTesting)
            {
                _uiPlayer.HasClicked = Click;

                transform.DORotate(Vector3.zero, 0);
                KayakControllerProperty.transform.DORotate(Vector3.zero, 0);
            }

            // print(Vector3.Distance(KayakControllerProperty.transform.position, Vector3.zero));
            // if (!Manager.Instance.IsGameStarted)
            // {
            //     _cooldownResetPos += Time.deltaTime;
            //     
            //     if (_hasInit && Vector3.Distance(KayakControllerProperty.transform.position, Vector3.zero) < 4f &&
            //         _cooldownResetPos > .5f)
            //     {
            //         _cooldownResetPos = 0;
            //         ResetPos();
            //     }
            // }

            if (!CanPaddle)
                return;

            // Update State
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
            if (IsDead || Manager.Instance.IsGameEnded)
                return;

            if (!CanPaddle)
                return;

            CurrentStateBaseProperty.FixedUpdate(this);
        }

        public void ResetPos()
        {
            KayakControllerProperty.transform.position = _startPos;
        }

        public void SendDebugMessage(string message)
        {
            Debug.Log(message);
        }

        public void OnJoining(InputAction.CallbackContext context)
        {
            Click = context.action.triggered;
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