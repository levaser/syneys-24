using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private AnimationCurve animationCurve;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float waitTimer;
    private float currentWaitTime;

    private Vector3 targetPosition;             // Целевая позиция
    private Quaternion targetRotation;          // Целевое вращение    

    private bool isMoving = false;
    private bool isRotating = false;
    private bool isInteracting = false;
    private bool isAttacking = false;

    private int tick = 0;
    private List<int> currentActions = new List<int>();

    private void Start()
    {
        currentWaitTime = waitTimer;
        #region TEST
        List<int> testMoving = new List<int>(){1, 1, 3, 1, 2, 1, 4, 1, 5};
        MovingByAlgorithm(testMoving);
        #endregion
    }

    /// <summary>
    /// Движение по заданному алгоритму
    /// </summary>
    /// <param name="listActions"> список действий</param>
    public void MovingByAlgorithm(List<int> listActions)
    {
        currentActions = listActions;
        switch (currentActions[tick])
        {
            case 1:
                isMoving = true;
                targetPosition = transform.position + transform.forward;
                break;
            case 2:
                targetRotation = transform.rotation * Quaternion.Euler(0f, -90f, 0f);
                isRotating = true;
                break;
            case 3:
                targetRotation = transform.rotation * Quaternion.Euler(0f, 90f, 0f);
                isRotating = true;
                break;
            case 4:
                isInteracting = true;
                break;
            case 5:
                isAttacking = true;
                break;
        }
    }

    private void MoveForward()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, animationCurve.Evaluate(speed) * speed * Time.deltaTime);
        if (transform.position == targetPosition)
        {
            transform.position = targetPosition;                        // Для точности
            isMoving = false;

            if (currentActions.Count > 1)
            {
                currentActions.RemoveAt(0);
                MovingByAlgorithm(currentActions);
            }
            else
            {
                currentActions.Clear();
                                                                       //добавить проверку на win/game over
            }
        }
    }

    private void Rotate()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        if (transform.rotation == targetRotation)
        {
            transform.rotation = targetRotation;                       // Для точности
            isRotating = false;

            if (currentActions.Count > 1)
            {
                currentActions.RemoveAt(0);
                MovingByAlgorithm(currentActions);
            }
            else
            {
                currentActions.Clear();
                                                                    //добавить проверку на win/game over
            }
        }
    }

    private void interaction()
    {
        currentWaitTime -= Time.deltaTime;
        if (currentWaitTime <= 0)
        {
            Debug.Log("Interaction finished");
            isInteracting = false;
            currentWaitTime = waitTimer;
            if (currentActions.Count > 1)
            {
                currentActions.RemoveAt(0);
                MovingByAlgorithm(currentActions);
            }
            else
            {
                currentActions.Clear();
                                                                    //добавить проверку на win/game over
            }
        }
    }

    private void Attack()
    {
        currentWaitTime -= Time.deltaTime;
        if (currentWaitTime <= 0)
        {
            Debug.Log("Attack finished");
            isAttacking = false;
            currentWaitTime = waitTimer;
            if (currentActions.Count > 1)
            {
                currentActions.RemoveAt(0);
                MovingByAlgorithm(currentActions);
            }
            else
            {
                currentActions.Clear();
            }
        }
    }

    private void Update()
    {
        if (isMoving)
            MoveForward();
        if (isRotating)
            Rotate();
        if (isInteracting)
            interaction();
        if (isAttacking)
            Attack();      
    }
}
