using Array2DEditor;
using Game;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private AnimationCurve animationCurve;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float waitTimer;
    private readonly LevelStarter _levelStarter;
    private float currentWaitTime;

    private Vector3 targetPosition;
    private Quaternion targetRotation;

    private bool isMoving = false;
    private bool isRotating = false;
    private bool isInteracting = false;
    private bool isAttacking = false;

    private int[,] currentPosition;
    private int currentRow = 0;
    private int currentColumn = 0;

    private int testSwitcher = 4;//test

    private int currentDirection = 0;
    private void Start()
    {
        currentPosition = new int[5, 5];
        currentWaitTime = waitTimer;
        InitializationGrid();        
    }

    #region TEST BUTTONS

    public void Forward()
    {
        if (CheckNextStep(currentDirection) == true)
        {
            MovingByAlgorithm(1);
        }
    }

    public void Left()
    {
        ChangeDirection(false);
        if (CheckNextStep(currentDirection) == true)
        {
            MovingByAlgorithm(2);
        }
        else
        {
            ChangeDirection(true);
        }
    }

    public void Right()
    {
        ChangeDirection(true);
        if (CheckNextStep(currentDirection) == true)
        {
            MovingByAlgorithm(3);
        }
        else
        {
            ChangeDirection(false);
        }
    }
    public void Action()
    {       
        MovingByAlgorithm(4);
        if (CheckSwitcher() == true)
        {
            Debug.Log("Switcher on");
                     
        }
        else
        {
            Debug.Log("Try better");
        }
    }
    #endregion

    private void InitializationGrid()
    {
        for (int i = 0; i < currentPosition.GetLength(0);  i++)
        {
            for (int b = 0; b < currentPosition.GetLength(1); b++)
            {
                currentPosition[i, b] = b;                
            }
        }
    }

    private bool CheckNextStep(int currentDirection)
    {
        bool isChecked = false;
        switch (currentDirection)
        {
            case 0:                                     //Go Up
                if (currentColumn < currentPosition.GetLength(1)-1)
                {
                    currentColumn++;
                    isChecked = true;
                }
                break;
            case 1:                                    //Go Right
                if (currentRow < currentPosition.GetLength(0)-1)
                {
                    currentRow++;
                    isChecked = true;
                }
                break;
            case 2:                                    //Go Down
                if (currentColumn > 0)
                {
                    currentColumn--;
                    isChecked = true;
                }
                break;

            case 3:                                    //Go Left
                if (currentRow > 0)
                {
                    currentRow--;
                    isChecked = true;
                }
                break;
        }
        return isChecked;
    }

    private bool CheckSwitcher()
    {
        int column = currentColumn;
        int row = currentRow;
        bool isSwitcher = false;
        
        if (column < currentPosition.GetLength(1) - 1)
        {
            column++;
            if (currentPosition[row, column] == testSwitcher)
            {//  if (_levelStarter.CurrentLevelConfig.Grid.GetCell(row, column) == CellType.Switcher)
                isSwitcher = true;
            }
        }
        column = currentColumn;
        row = currentRow;
        if (column > 0)
        {
            column--;
            if (currentPosition[row, column] == testSwitcher)
            {//  if (_levelStarter.CurrentLevelConfig.Grid.GetCell(row, column) == CellType.Switcher)
                isSwitcher = true;
            }
        }
        column = currentColumn;
        row = currentRow;
        if (row < currentPosition.GetLength(0) - 1)
        {
            row++;
            if (currentPosition[row, column] == testSwitcher)
            {//  if (_levelStarter.CurrentLevelConfig.Grid.GetCell(row, column) == CellType.Switcher)
                isSwitcher = true;
            }
        }
        column = currentColumn;
        row = currentRow;
        if (row > 0)
        {
            row--;
            if (currentPosition[row, column] == testSwitcher)
            {//  if (_levelStarter.CurrentLevelConfig.Grid.GetCell(row, column) == CellType.Switcher)
                isSwitcher = true;
            }
        }
        return isSwitcher;

    }

    private void ChangeDirection(bool right)
    {
        if (right)
        {
            if (currentDirection < 3)
                currentDirection++;
            else
                currentDirection = 0;
        }
        if (right == false)
        {
            if (currentDirection > 0)
                currentDirection--;
            else
                currentDirection = 3;
        }
    }

    public void MovingByAlgorithm(int Action)
    {
        switch (Action)
        {
            case 1:
                isMoving = true;
                targetPosition = transform.position + transform.forward;
                break;
            case 2:               
                targetRotation = transform.rotation * Quaternion.Euler(0f, -90f, 0f);
                isRotating = true;
                targetPosition = transform.position + transform.right * -1;
                isMoving = true;                
                break;
            case 3:
                targetRotation = transform.rotation * Quaternion.Euler(0f, 90f, 0f);
                isRotating = true;
                targetPosition = transform.position + transform.right;
                isMoving = true;
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
        //if (_levelStarter.CurrentLevelConfig.Grid.GetCell(currentRow, currentColumn) == CellType.None ||
        //     _levelStarter.CurrentLevelConfig.Grid.GetCell(currentRow, currentColumn) == CellType.Trap)
        //{
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, animationCurve.Evaluate(speed) * speed * Time.deltaTime);
            if (transform.position == targetPosition)
            {
                transform.position = targetPosition;
                isMoving = false;                
            }
        //}
    }

    private void Rotate()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        if (transform.rotation == targetRotation)
        {
            transform.rotation = targetRotation;
            isRotating = false;
        }
    }

    private void Interaction()
    {
        currentWaitTime -= Time.deltaTime;
        if (currentWaitTime <= 0)
        {
            Debug.Log("Interaction finished");
            isInteracting = false;
            currentWaitTime = waitTimer;
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
        }
    }

    private void Update()
    {
        if (isMoving)
            MoveForward();
        if (isRotating)
            Rotate();
        if (isInteracting)
            Interaction();
        if (isAttacking)
            Attack();
    }
}
