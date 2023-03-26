using System;
using UnityEngine;

[System.Serializable]
public class InputButton
{
    [SerializeField]
    private KeyCode key;


    public KeyCode Key { get => key; }
    public bool IsHold { get; set; }
    public Action OnPress { get; set; }
    public Action OnRelease { get; set; }
    public float HoldTime { get; set; }
}
