using System;
using System.Collections;
using UnityEngine;

namespace CLEARSKIES
{
    internal class Yield : MonoBehaviour // rly blyat ya ne ebu otkuda u menya that class, but it bolshe chem v DM
    {
        private void Awake()
        {
            UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
        }
        internal static void Begin(Action method)
        {
            if (Yield.yield == null)
            {
                Yield.yield = new GameObject("yield").AddComponent<Yield>();
            }
            Yield.yield.Invoke(method);
        }
        internal static void Begin<T>(System.Action<T> method, T var)
        {
            if (Yield.yield == null)
            {
                Yield.yield = new GameObject("yield").AddComponent<Yield>();
            }
            Yield.yield.Invoke<T>(method, var);
        }
        internal static void Begin<T, T2>(Action<T, T2> method, T var, T2 var2)
        {
            if (Yield.yield == null)
            {
                Yield.yield = new GameObject("yield").AddComponent<Yield>();
            }
            Yield.yield.Invoke<T, T2>(method, var, var2);
        }
        internal static void Begin<T, T2, T3>(Action<T, T2, T3> method, T var, T2 var2, T3 var3)
        {
            if (Yield.yield == null)
            {
                Yield.yield = new GameObject("yield").AddComponent<Yield>();
            }
            Yield.yield.Invoke<T, T2, T3>(method, var, var2, var3);
        }
        internal static void Begin<T, T2, T3, T4>(Action<T, T2, T3, T4> method, T var, T2 var2, T3 var3, T4 var4)
        {
            if (Yield.yield == null)
            {
                Yield.yield = new GameObject("yield").AddComponent<Yield>();
            }
            Yield.yield.Invoke<T, T2, T3, T4>(method, var, var2, var3, var4);
        }
        internal static void Begin(YieldInstruction yieldInstruction, Action method)
        {
            if (Yield.yield == null)
            {
                Yield.yield = new GameObject("yield").AddComponent<Yield>();
            }
            Yield.yield.Invoke(yieldInstruction, method);
        }
        internal static void Begin<T>(YieldInstruction yieldInstruction, System.Action<T> method, T var)
        {
            if (Yield.yield == null)
            {
                Yield.yield = new GameObject("yield").AddComponent<Yield>();
            }
            Yield.yield.Invoke<T>(yieldInstruction, method, var);
        }
        internal static void Begin<T, T2>(YieldInstruction yieldInstruction, Action<T, T2> method, T var, T2 var2)
        {
            if (Yield.yield == null)
            {
                Yield.yield = new GameObject("yield").AddComponent<Yield>();
            }
            Yield.yield.Invoke<T, T2>(yieldInstruction, method, var, var2);
        }
        internal static void Begin<T, T2, T3>(YieldInstruction yieldInstruction, Action<T, T2, T3> method, T var, T2 var2, T3 var3)
        {
            if (Yield.yield == null)
            {
                Yield.yield = new GameObject("yield").AddComponent<Yield>();
            }
            Yield.yield.Invoke<T, T2, T3>(yieldInstruction, method, var, var2, var3);
        }
        internal static void Begin<T, T2, T3, T4>(YieldInstruction yieldInstruction, Action<T, T2, T3, T4> method, T var, T2 var2, T3 var3, T4 var4)
        {
            if (Yield.yield == null)
            {
                Yield.yield = new GameObject("yield").AddComponent<Yield>();
            }
            Yield.yield.Invoke<T, T2, T3, T4>(yieldInstruction, method, var, var2, var3, var4);
        }
        internal void Invoke(Action method)
        {
            base.StartCoroutine(Yield.Coroutine(method));
        }
        private static System.Collections.IEnumerator Coroutine(Action method)
        {
            yield return new WaitForEndOfFrame();
            method();
            yield break;
        }
        internal void Invoke<T>(System.Action<T> method, T var)
        {
            base.StartCoroutine(Yield.Coroutine<T>(method, var));
        }
        private static System.Collections.IEnumerator Coroutine<T>(System.Action<T> method, T var)
        {
            yield return new WaitForEndOfFrame();
            method(var);
            yield break;
        }
        internal void Invoke<T, T2>(Action<T, T2> method, T var, T2 var2)
        {
            base.StartCoroutine(Yield.Coroutine<T, T2>(method, var, var2));
        }
        private static System.Collections.IEnumerator Coroutine<T, T2>(Action<T, T2> method, T var, T2 var2)
        {
            yield return new WaitForEndOfFrame();
            method(var, var2);
            yield break;
        }
        internal void Invoke<T, T2, T3>(Action<T, T2, T3> method, T var, T2 var2, T3 var3)
        {
            base.StartCoroutine(Yield.Coroutine<T, T2, T3>(method, var, var2, var3));
        }
        private static System.Collections.IEnumerator Coroutine<T, T2, T3>(Action<T, T2, T3> method, T var, T2 var2, T3 var3)
        {
            yield return new WaitForEndOfFrame();
            method(var, var2, var3);
            yield break;
        }
        internal void Invoke<T, T2, T3, T4>(Action<T, T2, T3, T4> method, T var, T2 var2, T3 var3, T4 var4)
        {
            base.StartCoroutine(Yield.Coroutine<T, T2, T3, T4>(method, var, var2, var3, var4));
        }
        private static System.Collections.IEnumerator Coroutine<T, T2, T3, T4>(Action<T, T2, T3, T4> method, T var, T2 var2, T3 var3, T4 var4)
        {
            yield return new WaitForEndOfFrame();
            method(var, var2, var3, var4);
            yield break;
        }
        internal void Invoke(YieldInstruction yieldInstruction, Action method)
        {
            base.StartCoroutine(Yield.Coroutine(yieldInstruction, method));
        }
        private static System.Collections.IEnumerator Coroutine(YieldInstruction yieldInstruction, Action method)
        {
            yield return yieldInstruction;
            method();
            yield break;
        }
        internal void Invoke<T>(YieldInstruction yieldInstruction, System.Action<T> method, T var)
        {
            base.StartCoroutine(Yield.Coroutine<T>(yieldInstruction, method, var));
        }
        private static System.Collections.IEnumerator Coroutine<T>(YieldInstruction yieldInstruction, System.Action<T> method, T var)
        {
            yield return yieldInstruction;
            method(var);
            yield break;
        }
        internal void Invoke<T, T2>(YieldInstruction yieldInstruction, Action<T, T2> method, T var, T2 var2)
        {
            base.StartCoroutine(Yield.Coroutine<T, T2>(yieldInstruction, method, var, var2));
        }
        private static System.Collections.IEnumerator Coroutine<T, T2>(YieldInstruction yieldInstruction, Action<T, T2> method, T var, T2 var2)
        {
            yield return yieldInstruction;
            method(var, var2);
            yield break;
        }
        internal void Invoke<T, T2, T3>(YieldInstruction yieldInstruction, Action<T, T2, T3> method, T var, T2 var2, T3 var3)
        {
            base.StartCoroutine(Yield.Coroutine<T, T2, T3>(yieldInstruction, method, var, var2, var3));
        }
        private static System.Collections.IEnumerator Coroutine<T, T2, T3>(YieldInstruction yieldInstruction, Action<T, T2, T3> method, T var, T2 var2, T3 var3)
        {
            yield return yieldInstruction;
            method(var, var2, var3);
            yield break;
        }
        internal void Invoke<T, T2, T3, T4>(YieldInstruction yieldInstruction, Action<T, T2, T3, T4> method, T var, T2 var2, T3 var3, T4 var4)
        {
            base.StartCoroutine(Yield.Coroutine<T, T2, T3, T4>(yieldInstruction, method, var, var2, var3, var4));
        }
        private static System.Collections.IEnumerator Coroutine<T, T2, T3, T4>(YieldInstruction yieldInstruction, Action<T, T2, T3, T4> method, T var, T2 var2, T3 var3, T4 var4)
        {
            yield return yieldInstruction;
            method(var, var2, var3, var4);
            yield break;
        }
        internal static Yield yield = new GameObject("yield").AddComponent<Yield>();
        internal delegate void Create();
        internal delegate T Create<out T>();
        internal delegate void CreateParams(params object[] parameters);
        internal delegate T CreateParams<out T>(params object[] parameters);
    }
}
