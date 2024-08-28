using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private GameplayWidget gameplayWidgetPrefab;
    private GameplayWidget _gameplayWidget;
    private CharacterController _characterController;
    private Animator _animator;

    private Vector2 _moveInput;
    [SerializeField] private float _speed = 10f;
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _gameplayWidget = Instantiate(gameplayWidgetPrefab);
        _gameplayWidget.MoveStick.OnInputUpdated += InputUpdated;
    }

    private void InputUpdated(Vector2 inputVal)
    {
        _moveInput = inputVal;
        Debug.Log(inputVal);
    }

    void Start()
    {
        //variable speed, ground movement
    }

    void Update()
    {
        _characterController.Move(new Vector3(_moveInput.x, 0, _moveInput.y) * (_speed * Time.deltaTime));
    }
}
