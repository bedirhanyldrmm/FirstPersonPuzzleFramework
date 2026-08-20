using UnityEngine;
using UnityEngine.Events;

public class PuzzleCondition : MonoBehaviour
{
    [SerializeField]
    private bool conditionA;

    [SerializeField]
    private bool conditionB;

    [SerializeField]
    private UnityEvent onSolved;

    private bool isSolved;

    public void SetConditionA(bool value)
    {
        conditionA = value;
        Evaluate();
    }

    public void SetConditionB(bool value)
    {
        conditionB = value;
        Evaluate();
    }

    public void ActivateConditionA()
    {
        SetConditionA(true);
    }

    public void DeactivateConditionA()
    {
        SetConditionA(false);
    }

    public void ActivateConditionB()
    {
        SetConditionB(true);
    }

    public void DeactivateConditionB()
    {
        SetConditionB(false);
    }

    private void Evaluate()
    {
        bool solved = conditionA && conditionB;

        if (solved && !isSolved)
        {
            isSolved = true;
            onSolved?.Invoke();
        }
        else if (!solved)
        {
            isSolved = false;
        }
    }
}