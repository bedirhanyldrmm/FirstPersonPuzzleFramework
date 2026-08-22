using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PuzzleCondition : MonoBehaviour
{
    public enum LogicMode
    {
        All,
        Any
    }

    [SerializeField]
    private LogicMode logicMode = LogicMode.All;

    [SerializeField]
    private List<bool> conditions = new List<bool>
    {
        false,
        false
    };

    [SerializeField]
    private UnityEvent onSolved;
    [SerializeField]
 
    private UnityEvent onUnsolved;

    private bool isSolved;

    public void SetCondition(int index, bool value)
    {
        if (index < 0 || index >= conditions.Count)
            return;

        conditions[index] = value;
        Evaluate();
    }

    public void ActivateCondition(int index)
    {
        SetCondition(index, true);
    }

    public void DeactivateCondition(int index)
    {
        SetCondition(index, false);
    }

    private void Evaluate()
    {
        bool solved = logicMode == LogicMode.All
            ? AreAllConditionsActive()
            : IsAnyConditionActive();

        if (solved && !isSolved)
        {
            isSolved = true;
            onSolved?.Invoke();
        }
        else if (!solved && isSolved)
        {
            isSolved = false;
            onUnsolved?.Invoke();
        }
    }

    private bool AreAllConditionsActive()
    {
        foreach (bool condition in conditions)
        {
            if (!condition)
                return false;
        }

        return true;
    }

    private bool IsAnyConditionActive()
    {
        foreach (bool condition in conditions)
        {
            if (condition)
                return true;
        }

        return false;
    }
}