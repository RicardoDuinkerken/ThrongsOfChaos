using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class HandController : MonoBehaviour
{
    [SerializeField] private InputActionAsset actionAsset;
    [SerializeField] private string controllerName;
    [SerializeField] private string actionNameTrigger;
    [SerializeField] private string actionNameGrip;

    private InputActionMap _actionMap;
    private InputAction _inputActionTrigger;
    private InputAction _inputActionGrip;

    private Animator _handAnimator;

    private void Awake()
    {
        //get all the actions
        _actionMap = actionAsset.FindActionMap(controllerName);
        Debug.Log(_actionMap);
        _inputActionGrip = _actionMap.FindAction(actionNameGrip);
        Debug.Log(_inputActionGrip);
        _inputActionTrigger = _actionMap.FindAction(actionNameTrigger);
        
        //get animator
        _handAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _inputActionGrip.Enable();
        _inputActionTrigger.Enable();
    }

    private void OnDisable()
    {
        _inputActionGrip.Disable();
        _inputActionTrigger.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        var gripValue = _inputActionGrip.ReadValue<float>();
        var triggerValue = _inputActionTrigger.ReadValue<float>();
        
        _handAnimator.SetFloat("Grip", gripValue);
        _handAnimator.SetFloat("Trigger", triggerValue);
    }
}
