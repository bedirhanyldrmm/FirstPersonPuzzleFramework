using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PuzzleCondition : MonoBehaviour
{
    [SerializeField]
    private List<bool> conditions = new List<bool>
    {
        false,
        false
    };

    [SerializeField]
    private UnityEvent onSolved;

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
        bool solved = true;

        foreach (bool condition in conditions)
        {
            if (!condition)
            {
                solved = false;
                break;
            }
        }

        if (solved && !isSolved)
        {
            isSolved = true;

            Debug.Log("PUZZLE SOLVED!");

            onSolved?.Invoke();
        }
        else if (!solved)
        {
            isSolved = false;
        }
    }
}