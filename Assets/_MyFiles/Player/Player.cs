using System;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SocketManager))]
[RequireComponent(typeof(InventoryComponent))]
public class Player : MonoBehaviour
{
    [SerializeField] private GameplayWidget gameplayWidgetPrefab;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float bodyTurnSpeed = 10f;
    [SerializeField] private ViewCamera viewCameraPrefab;
    [SerializeField] private float animTurnLerpScale = 5f;
    private GameplayWidget _gameplayWidget;
    
    private CharacterController _characterController;
    
    
    private ViewCamera _viewCamera;
    private Animator _animator;
    private float _animTurnSpeed;

    private Vector2 _moveInput;
    private Vector2 _aimInput;

    static int _animFwdId = Animator.StringToHash("ForwardAmount");
    static int _animRightId = Animator.StringToHash("RightAmount");
    static int _animTurnId = Animator.StringToHash("TurnAmount");

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _gameplayWidget = Instantiate(gameplayWidgetPrefab);
        _gameplayWidget.MoveStick.OnInputUpdated += MoveInputUpdated;
        _gameplayWidget.AimStick.OnInputUpdated += AimInputUpdated;
        _viewCamera = Instantiate(viewCameraPrefab);
        _viewCamera.SetFollowParent(transform);
    }

    private void MoveInputUpdated(Vector2 inputVal)
    {
        _moveInput = inputVal;
        Debug.Log(inputVal);
    }
    private void AimInputUpdated(Vector2 inputVal)
    {
        _aimInput = inputVal;
        Debug.Log(inputVal);
    }

    void Start()
    {
        //variable speed, ground movement
    }

    void Update()
    {
        Vector3 moveDir = _viewCamera.InputToWorldDir(_moveInput);
        _characterController.Move(moveDir * (speed * Time.deltaTime));

        Vector3 aimDir = _viewCamera.InputToWorldDir(_aimInput);
        if (aimDir == Vector3.zero)
        {
            aimDir = moveDir;
            _viewCamera.AddYawInput(_moveInput.x);
        }

        float angleDelta = 0;
        if (aimDir != Vector3.zero)
        {
            Vector3 prevDir = transform.forward;
            Quaternion goalRot = Quaternion.LookRotation(aimDir, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, goalRot, Time.deltaTime * bodyTurnSpeed);

            angleDelta = Vector3.SignedAngle(transform.forward, prevDir, Vector3.up);
            Debug.Log(angleDelta);
        }

        _animTurnSpeed = Mathf.Lerp(_animTurnSpeed, angleDelta/Time.deltaTime, Time.deltaTime * animTurnLerpScale);
        _animator.SetFloat(_animTurnId, _animTurnSpeed);

        float animFwdAmt = Vector3.Dot(moveDir, transform.forward);
        float animRightAmt = Vector3.Dot(moveDir, transform.right);


        _animator.SetFloat(_animFwdId, animFwdAmt);
        _animator.SetFloat(_animRightId, animRightAmt);


    }
}
