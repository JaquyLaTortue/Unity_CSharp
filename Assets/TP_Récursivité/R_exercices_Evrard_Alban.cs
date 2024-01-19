using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class R_exercices_Evrard_Alban : MonoBehaviour
{

    private void Start()
    {
        MotAnalyser(mot);

        Additions(N);

        factorielle(i);

        Fibo_Result = Fibonacci(NB);

        PairBool = Pair(Nb);

        max = Tab_Max(Tableau.Length - 1);


    }

    [Header("Ex_1")]

    public string mot;
    public int Length_Mot;

    private void MotAnalyser(string word, int counter = 0)
    {
        if (word == "")
        {
            Length_Mot = counter;
            return;
        }
        else
        {
            counter++;
        }
        MotAnalyser(word.Substring(1), counter);
    }


    [Header("Ex_2")]

    public int N;
    public int Addition_result;

    void Additions(int n, int total = 0)
    {
        if (n <= 0)
        {
            Addition_result = total;
            return;
        }
        else
        {
            total += n;
            n--;
        }
        Additions(n, total);
    }


    [Header("Ex_3")]

    public int i;
    public int factorielle_result;

    void factorielle(int n, int total = 1)
    {
        if (n <= 0)
        {
            factorielle_result = total;
            return;
        }
        else
        {
            total *= n;
        }
        factorielle(n -= 1, total);
    }


    [Header("Ex_4")]
    public int NB;
    public int Fibo_Result;

    int Fibonacci(int n)
    {
        if (n <= 0)
        {
            return 0;
        }
        if (n == 1)
        {
            return 1;
        }
        else
        {
            return Fibonacci(n - 2) + Fibonacci(n - 1);
        }
    }


    [Header("Ex_5")]
    public int Nb;
    public bool PairBool;

    bool Pair(int n)
    {
        if (n == 0)
        {
            return true;
        }
        else
        {
            return Impair(n - 1);
        }
    }

    bool Impair(int n)
    {
        if (n == 0)
        {
            return false;
        }
        else
        {
            return Pair(n - 1);
        }
    }


    [Header("Ex_6")]
    public int[] Tableau = new int[4];
    public int max;

    int Tab_Max(int i)
    {
        if (Tableau.Length == 0)
        {
            return 0;
        }
        if (i == 0)
        {
            return Tableau[0];
        }
        else
        {
            if (Tab_Max(i - 1) > Tableau[i])
            {
                return Tab_Max(i - 1);
            }
            else
            {
                return Tableau[i];
            }
        }
    }
}
