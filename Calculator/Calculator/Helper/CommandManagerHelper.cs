using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Calculator
{
    internal class CommandManagerHelper
    {
        internal static void AddHandlersToRequerySuggested(List<WeakReference> handlers)
        {
            if (handlers == null)
            {
                return;
            }

            foreach (EventHandler item in handlers.Select((WeakReference handlerRef) => handlerRef.Target).OfType<EventHandler>())
            {
                CommandManager.RequerySuggested += item;
            }
        }

        internal static void AddWeakReferenceHandler(ref List<WeakReference> handlers, EventHandler handler)
        {
            AddWeakReferenceHandler(ref handlers, handler, -1);
        }

        internal static void AddWeakReferenceHandler(ref List<WeakReference> handlers, EventHandler handler, int defaultListSize)
        {
            if (handlers == null)
            {
                handlers = ((defaultListSize > 0) ? new List<WeakReference>(defaultListSize) : new List<WeakReference>());
            }

            handlers.Add(new WeakReference(handler));
        }

        internal static void CallWeakReferenceHandlers(List<WeakReference> handlers)
        {
            if (handlers == null)
            {
                return;
            }

            EventHandler[] array = new EventHandler[handlers.Count];
            int num = 0;
            for (int num2 = handlers.Count - 1; num2 >= 0; num2--)
            {
                WeakReference weakReference = handlers[num2];
                EventHandler eventHandler = weakReference.Target as EventHandler;
                if (eventHandler == null)
                {
                    handlers.RemoveAt(num2);
                }
                else
                {
                    array[num] = eventHandler;
                    num++;
                }
            }

            for (int num2 = 0; num2 < num; num2++)
            {
                EventHandler eventHandler = array[num2];
                eventHandler(null, EventArgs.Empty);
            }
        }

        internal static void RemoveHandlersFromRequerySuggested(List<WeakReference> handlers)
        {
            if (handlers == null)
            {
                return;
            }

            foreach (EventHandler item in handlers.Select((WeakReference handlerRef) => handlerRef.Target).OfType<EventHandler>())
            {
                CommandManager.RequerySuggested -= item;
            }
        }

        internal static void RemoveWeakReferenceHandler(List<WeakReference> handlers, EventHandler handler)
        {
            if (handlers == null)
            {
                return;
            }

            for (int num = handlers.Count - 1; num >= 0; num--)
            {
                WeakReference weakReference = handlers[num];
                EventHandler eventHandler = weakReference.Target as EventHandler;
                if (eventHandler == null || eventHandler == handler)
                {
                    handlers.RemoveAt(num);
                }
            }
        }
    }
}
