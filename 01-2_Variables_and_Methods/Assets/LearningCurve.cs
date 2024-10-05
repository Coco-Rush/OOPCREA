using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LearningCurve : MonoBehaviour
{
    private bool _isCool;
    private bool _areNumbersCalculated;
    private int _firstNumber;
    private int _lastNumber;
    private int _numForty;
    private int _numTwo;
    private int _oddOrEvenTest;
    private int[] _factorialTestingNumbers;

    // Start is called before the first frame update
    void Start()
    {
        
        CalculateTheNumbers(FillTheNumbers());
        DebugLogs();
        GetReverseFactorial();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    public bool FillTheNumbers()
    {
        this._isCool = true;
        this._firstNumber = 5;
        this._lastNumber = 3;
        this._numForty = 40;
        this._numTwo = 2;
        this._oddOrEvenTest = 16;
        return true;
    }

    public void CalculateTheNumbers(bool areNumbersWithValues)
    {
        if (areNumbersWithValues)
        {
            this._numForty += this._numTwo;
            this._firstNumber %= this._lastNumber;
            this._areNumbersCalculated = true;
        }
    }
    public void DebugLogs()
    {
        if (this._areNumbersCalculated)
        {
            Debug.Log(this._isCool);
            Debug.Log(this._firstNumber);
            Debug.Log(this._numForty);
            Debug.Log("End " + 42 + " bool: " + this._isCool);
            Debug.Log(OddOrEven(this._oddOrEvenTest));
            Debug.Log(GetFactorial(13));
            Debug.Log(GetFactorial(0));
            Debug.Log(GetFactorial(6));
            Debug.Log(GetFactorial(1));
            for (int i = 0; i < 10; i++)
            {
                Debug.Log(OddOrEven(i));
            }
        }
    }
    public int IntSqrtRoot(int sqrtBase)
    {
        return Convert.ToInt32(Math.Sqrt(sqrtBase));

    }
    public static string OddOrEven(int number)
    {
        int i = number % 2;
        if (i == 1)
        {
            return "Odd: " + number.ToString();
        }
        else
        {
            return "Even: " + number.ToString();
        }
    }
    public static int GetFactorial(int number)    // Number could be negative. Need to add a condition and call the absolute value function
    {
        if ((number < 0) || (number > 12))
        {
            return 0;
        }
        int result = 1;
        for (int i = 1; i <= number; i++)
        {
            result *= i;
        }
        return result;
    }
    public void GetReverseFactorial()
    {
        int finalFactorial;
        _factorialTestingNumbers = new int[] { 1,22,333,4444,55555,666666,7777777 };
        foreach (var factorialNum in _factorialTestingNumbers)
        {
            finalFactorial = 1;
            while(GetFactorial(finalFactorial) <= factorialNum) { finalFactorial++; }
            Debug.Log(finalFactorial - 1);
        }
    }
    public int IntSquareRoot(int number)
    {
        for (int i = 1; i < number; i++)
        {
            if (Math.Pow(i, 2) > number) { return i-1; }
        }
        return 0;
    }
    public bool IsItPrime(int number)
    {
        for(int i = 2; i < number; i++)
        {
            if(number % i == 0) { return false; }
        }
        return true;
    }

    public float CalcCubeVolume(float x)
    {
        return CalcCubeVolume(x, x, x);
    }
    public float CalcCubeVolume(float x, float y)
    {
        return CalcCubeVolume(x, x, y);
    }
    public float CalcCubeVolume(float x,float y,float z)
    {
        return x * y * z;
    }

    public float AvgOfAnArray(int[] calcAvg)
    {
        int sum = 0;
        float length = calcAvg.Length;
        foreach (int element in calcAvg)
        {
            sum += element;
        }
        return sum/length;
    }
    // redundant method
    public void TheUltimateAnswer()
    {
        CalculateTheNumbers(FillTheNumbers());
        DebugLogs();
    }
}
