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
                CLEARSKIES.StatsTab.AddLine("==> begin action in thread: " + method.ToString(), CLEARSKIES.StatsTab.DebugType.LOG);
                method();
                CLEARSKIES.StatsTab.AddLine("==> the action in thread: " + method.ToString() + "is end.", CLEARSKIES.StatsTab.DebugType.LOG);
                return;
            };
            Execute(threadAction); 
        }
        public static void BeginInBackground<T>(Action<T> method, T val)
        {
            Action threadAction = () =>
            {
                CLEARSKIES.StatsTab.AddLine("==> begin action in thread: " + method.ToString(), CLEARSKIES.StatsTab.DebugType.LOG);
                method(val);
                CLEARSKIES.StatsTab.AddLine("==> the action in thread: " + method.ToString() + "is end.", CLEARSKIES.StatsTab.DebugType.LOG);//BRM.StatsTab.AddLine("State thread: " + _thread.ThreadState.ToString(), BRM.StatsTab.DebugType.LOG);
                return;
            };
            Execute(threadAction);
        }
        public static void BeginInBackground<T, T2>(Action<T, T2> method, T val, T2 val2)
        {
            Action threadAction = () =>
            {
                CLEARSKIES.StatsTab.AddLine("==> begin action in thread: " + method.ToString(), CLEARSKIES.StatsTab.DebugType.LOG);
                method(val, val2);
                CLEARSKIES.StatsTab.AddLine("==> the action in thread: " + method.ToString() + "is end.", CLEARSKIES.StatsTab.DebugType.LOG);
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
        //        BRM.StatsTab.AddLine("The action in thread is end.");
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
        //        BRM.StatsTab.AddLine("The action in thread is end.");
        //    }
        //    finally { }
        //}
    }
}
