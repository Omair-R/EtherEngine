using System;


namespace EtherEngine.Utils.Pattern
{
    public abstract class LazySingleton<T> where T : LazySingleton<T>
    {
        protected static readonly Lazy<T> _lazyInstance = new Lazy<T>(
            ()=> (Activator.CreateInstance(typeof(T), true) as T)!);
        public static T Instance
        {
            get { return _lazyInstance.Value; }
        }

    }

    public abstract class Singleton<T> where T : Singleton<T>
    {
        protected static readonly T _instance = Activator.CreateInstance(typeof(T), true) as T;
        public static T Instance
        {
            get { return _instance; }
        }

    }
}
