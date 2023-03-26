using System;
using UnityEngine;

[System.Serializable]
public class Score
{
    public Action OnScoreChanged;

    private int currentValue;
    [SerializeField]
    private int targetValue;

    public int CurrentValue 
    { 
        get => currentValue; 
        set 
        { 
            if (currentValue != value)
            {
                currentValue = value;
                OnScoreChanged?.Invoke();
            }
        } 
    }
    public int TargetValue { get => targetValue; }


    public Score(int targetValue)
    {
        this.currentValue = 0;
        this.targetValue = targetValue;
    }

    public bool IsScoreAchieved()
    {
        if (targetValue == -1)
        {
            return false;
        }

        if (currentValue >= targetValue)
        {
            return true;
        }
        return false;
    }
}
