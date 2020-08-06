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
            Thread _thread = null;
            Action threadAction = () =>
            {
                BRM.StatsTab.AddLine("Begin action in thread", BRM.StatsTab.DebugType.LOG);
                //Execute(method);
                Thread.Sleep(100);
                Execute(() => { 
                    method();
                    _thread.Interrupt();
                });
                BRM.StatsTab.AddLine("State thread: " + _thread.ThreadState.ToString(), BRM.StatsTab.DebugType.LOG);
                return;
            };
            _thread = new Thread(new ThreadStart(threadAction));
            //{
            //    IsBackground = true
            //}
            
            _thread.Start();
            BRM.StatsTab.AddLine("State thread: " + _thread.ThreadState.ToString(), BRM.StatsTab.DebugType.LOG);
            
        }
        public static void BeginInBackground<T>(Action<T> method, T val)
        {
            Thread _thread = null;
            Action threadAction = () =>
            {
                BRM.StatsTab.AddLine("Begin action in thread");
                Thread.Sleep(1000);
                Execute(method, val);
                _thread.Interrupt();
            };
            _thread = new Thread(new ThreadStart(threadAction))
            {
                IsBackground = true
            };
            _thread.Start();
        }
        public static void BeginInBackground<T, T2>(Action<T, T2> method, T val, T2 val2)
        {
            Thread _thread = null;
            Action threadAction = () =>
            {
                BRM.StatsTab.AddLine("Begin action in thread");
                Thread.Sleep(1000);
                Execute<T, T2>(method, val, val2);
                _thread.Interrupt();
            };
            _thread = new Thread(new ThreadStart(threadAction))
            {
                IsBackground = true
            };
            _thread.Start();
        }
        private static void Execute(Action method)
        {
            method();
            try
            {
                Thread.Sleep(1000);
            }
            catch (ThreadInterruptedException)
            {
                BRM.StatsTab.AddLine("The action in thread is end.");
            }
            finally { }

        }
        private static void Execute<T>(Action<T> method, T val)
        {
            try
            {
                method(val);
            }
            catch (ThreadInterruptedException)
            {
                BRM.StatsTab.AddLine("The action in thread is end.");
            }
            finally { }

        }
        private static void Execute<T, T2>(Action<T, T2> method, T val, T2 val2)
        {
            try
            {
                method(val, val2);
            }
            catch (ThreadInterruptedException) 
            {
                BRM.StatsTab.AddLine("The action in thread is end.");
            }
            finally { }
            
        }
    }
}
