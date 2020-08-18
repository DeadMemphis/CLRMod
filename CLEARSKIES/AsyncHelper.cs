using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Web;

namespace CLEARSKIES
{
    public static class AsyncHelper
    {
        //static List<Action> QueueActions;
        public static void BeginInBackground(Action method)
        {
            Action threadAction = () =>
            {
                UnityEngine.Debug.Log("==> begin action in thread: " + method.ToString());
                method();
                UnityEngine.Debug.Log("==> the action in thread: " + method.ToString() + "is end.");
                return;
            };
            Execute(threadAction);
        }
        public static void BeginInBackground<T>(Action<T> method, T val)
        {
            Action threadAction = () =>
            {
                UnityEngine.Debug.Log("==> begin action in thread: " + method.ToString());
                method(val);
                UnityEngine.Debug.Log("==> the action in thread: " + method.ToString() + "is end.");//UnityEngine.Debug.Log("State thread: " + _thread.ThreadState.ToString());
                return;
            };
            Execute(threadAction);
        }
        public static void BeginInBackground<T, T2>(Action<T, T2> method, T val, T2 val2)
        {
            Action threadAction = () =>
            {
                UnityEngine.Debug.Log("==> begin action in thread: " + method.ToString());
                method(val, val2);
                UnityEngine.Debug.Log("==> the action in thread: " + method.ToString() + "is end.");
                return;
            };
            Execute(threadAction);
        }
        private static void Execute(Action method)
        {
            Thread _thread = new Thread(new ThreadStart(method))
            {
                IsBackground = true
            };
            _thread.Start();
        }
        //private static void Execute<T>(Action<T> method, T val)
        //{
        //    try
        //    {
        //        method(val);
        //    }
        //    catch (ThreadInterruptedException)
        //    {
        //        UnityEngine.Debug.Log("The action in thread is end.");
        //    }
        //    finally { }
        //}
        //private static void Execute<T, T2>(Action<T, T2> method, T val, T2 val2)
        //{
        //    try
        //    {
        //        method(val, val2);
        //    }
        //    catch (ThreadInterruptedException) 
        //    {
        //        UnityEngine.Debug.Log("The action in thread is end.");
        //    }
        //    finally { }
        //}
    }
}
