using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Quit : MonoBehaviour
{
    public InputActionReference action;

    void Start()
    {
        action.action.Enable();
        action.action.performed += (ctx) =>
        {
#if UNITY_EDITOR
            // If in the Unity Editor, stop play mode
            UnityEditor.EditorApplication.isPlaying = false;
#else
           
            Application.Quit();
#endif
        };
    }
}
