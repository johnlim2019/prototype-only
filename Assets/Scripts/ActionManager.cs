using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using Unity.VisualScripting;


public class ActionManager : MonoBehaviour
{
  public PlayerInput playerInput;
  public UnityEvent<Vector2> movement;
  public UnityEvent melee;
  public UnityEvent range;

  void Start()
  {
    playerInput = new PlayerInput();
    playerInput.ActionMap.Enable();
    playerInput.ActionMap.Movement.performed += OnMovementAction;
    playerInput.ActionMap.Movement.canceled += OnMovementAction;
    playerInput.ActionMap.Melee.performed += OnMeleeAction;
    playerInput.ActionMap.Range.performed += OnRangeAction;
  }

  public void OnMovementAction(InputAction.CallbackContext context)
  {
    if (context.performed)
    {
      Vector2 vector = context.ReadValue<Vector2>();
      // Debug.Log(vector);
      movement.Invoke(vector);
    }
    else if (context.canceled)
    {
      Vector2 noMove = new Vector2(0, 0);
      // Debug.Log(noMove);
      movement.Invoke(noMove);
    }
  }


  public void OnMeleeAction(InputAction.CallbackContext context)
  {
    if (context.performed)
    {
      melee.Invoke();
    }
  }
  public void OnRangeAction(InputAction.CallbackContext context)
  {
    if (context.performed)
    {
      range.Invoke();
    }
  }


}

