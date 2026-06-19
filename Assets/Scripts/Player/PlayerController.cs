using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
// using static InputHelpers;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInput input;
    InputHelpers helper = new();
    private InputAction move;
    private InputAction look;

    public Vector2 MovementInput { get; private set;}
    public Vector2 RotationInput { get; private set;}

    // public event Action<bool> inputAttack, inputReload, inputChangeWeapon;
    // movement accesories lol
    // public event Action<bool> inputJump, inputCrounch, inputSprint;

    // public event Action<bool> inputInteraction;

    private void Awake() {
        move =      input.actions.FindAction("Move", true);
        look =      input.actions.FindAction("Look", true);

        // attack =    input.actions.FindAction("Attack", true);
        // reload =    input.actions.FindAction("Reload", true);
        // changeWeapon = input.actions.FindAction("ChangeWeapon", true);

        // jump =      input.actions.FindAction("Jump", true);
        // sprint =    input.actions.FindAction("Sprint", true);
        // crounch =   input.actions.FindAction("Crounch", true);

        // interaction = input.actions.FindAction("Interaction", true);
    }
    private void OnEnable() {
        // helper.BindBoolAction(attack, InvokeAttack);
        // helper.BindBoolAction(reload, InvokeReload);
        // helper.BindBoolAction(changeWeapon, InvokeChangeWeapon);

        // helper.BindBoolAction(jump, InvokeJump);
        // helper.BindBoolAction(sprint, InvokeSprint);
        // helper.BindBoolAction(crounch, InvokeCrounch);

        // helper.BindBoolAction(interaction, InvokeInteraction);
    }
    private void OnDisable() {
        helper.UnbindAll();
    }
    private void Update() {
        MovementInput = move.ReadValue<Vector2>();
        RotationInput = look.ReadValue<Vector2>();
    }
    // private void InvokeAttack(bool value) => inputAttack?.Invoke(value);
    // private void InvokeReload(bool value) => inputReload?.Invoke(value);
    // private void InvokeChangeWeapon(bool value) => inputChangeWeapon?.Invoke(value);

    // private void InvokeJump(bool value) => inputJump?.Invoke(value);
    // private void InvokeSprint(bool value) => inputSprint?.Invoke(value);
    // private void InvokeCrounch(bool value) => inputCrounch?.Invoke(value);

    // private void InvokeInteraction(bool value) => inputInteraction?.Invoke(value);
}
public class InputHelpers
{
    private Dictionary<(InputAction, Action<bool>), Action<InputAction.CallbackContext>> _bindings = new();

    public void BindBoolAction(InputAction action, Action<bool> boolHandler) {
        Action<InputAction.CallbackContext> contextHandler = (ctx) => 
            boolHandler.Invoke(ctx.ReadValue<float>() == 1);
        
        _bindings[(action, boolHandler)] = contextHandler;
        
        action.started += contextHandler;
        action.canceled += contextHandler;
    }

    public void UnbindBoolAction(InputAction action, Action<bool> boolHandler) {
        if (_bindings.TryGetValue((action, boolHandler), out var contextHandler)) {
            action.started -= contextHandler;
            action.canceled -= contextHandler;
            _bindings.Remove((action, boolHandler));
        }
    }
    public void UnbindAll() {
        foreach (var ((action, _), handler) in _bindings) {
            action.started -= handler;
            action.canceled -= handler;
        }
        _bindings.Clear();
    }
}