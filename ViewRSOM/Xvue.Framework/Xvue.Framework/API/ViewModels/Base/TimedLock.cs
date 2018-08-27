﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Xvue.Framework.API.ViewModels.Base
{
    //Taken From: http://www.interact-sw.co.uk/iangblog/2004/03/23/locking

    // Thanks to Eric Gunnerson for recommending this be a struct rather
    // than a class - avoids a heap allocation.
    // (In Debug mode, we make it a class so that we can add a finalizer
    // in order to detect when the object is not freed.)
    // Thanks to Chance Gillespie and Jocelyn Coulmance for pointing out
    // the bugs that then crept in when I changed it to use struct...
    [ComVisibleAttribute(false)]

//#if DEBUG
//    public class TimedLock : IDisposable
//#else
    public sealed class TimedLock : IDisposable
//#endif
    {
        public static TimedLock Lock(object lockTarget)
        {
            return Lock(lockTarget, TimeSpan.FromSeconds(10));
        }

        public static TimedLock Lock(object lockTarget, TimeSpan timeout)
        {
            TimedLock tl = null;
            try
            {
                tl = new TimedLock(lockTarget);
                if (!System.Threading.Monitor.TryEnter(lockTarget, timeout))
                {
#if DEBUG
                System.GC.SuppressFinalize(tl);
#endif
                    throw new LockTimeoutException();
                }
            }
            finally
            {
                if(tl!=null)
                {
                    tl.Dispose();
                    tl = null;
                }
            }

            return tl;
        }

        private TimedLock(object lockTarget)
        {
            target = lockTarget;
        }
        private object target;

        public void Dispose()
        {
            System.Threading.Monitor.Exit(target);

            // It's a bad error if someone forgets to call Dispose,
            // so in Debug builds, we put a finalizer in to detect
            // the error. If Dispose is called, we suppress the
            // finalizer.
#if DEBUG
            GC.SuppressFinalize(this);
#endif
        }

//#if DEBUG
//        ~TimedLock()
//        {
//            // If this finalizer runs, someone somewhere failed to
//            // call Dispose, which means we've failed to leave
//            // a monitor!
//            System.Diagnostics.Debug.Fail("Undisposed lock");
//        }
//#endif

    }
    [SerializableAttribute]
    public class LockTimeoutException : Exception
    {
        public LockTimeoutException()
            : base("Timeout waiting for lock")
        {
        }

        public LockTimeoutException(String message):base(message)
        {            
        }

        public LockTimeoutException(String message, Exception innerException):base(message, innerException)
        {
        }

        protected LockTimeoutException(SerializationInfo info, StreamingContext context) :base(info, context)
        {
        }
    }
}
