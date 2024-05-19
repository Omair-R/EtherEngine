using System;


namespace EtherEngine.Utils.Pattern
{
    public abstract class LazySingleton<T> where T : LazySingleton<T>
    {
        protected static readonly Lazy<T> lazyInstance = new Lazy<T>(
            ()=> (Activator.CreateInstance(typeof(T), true) as T)!);
        public static T GetInstance
        {
            get { return lazyInstance.Value; }
        }

    }

    public abstract class Singleton<T> where T : Singleton<T>
    {
        protected static readonly T instance = Activator.CreateInstance(typeof(T), true) as T;
        public static T GetInstance
        {
            get { return instance; }
        }

    }
}
