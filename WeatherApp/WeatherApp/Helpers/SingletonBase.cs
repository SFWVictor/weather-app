namespace WeatherApp.Helpers
{
    using System;

    public abstract class SingletonBase<T> where T : class
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance is null)
                {
                    _instance = CreateInstance();
                }

                return _instance;
            }
        }

        private static T CreateInstance()
        {
            // Get non-public constructors for T.
            var ctors = typeof(T).GetConstructors(System.Reflection.BindingFlags.Instance |
                                  System.Reflection.BindingFlags.NonPublic);
            if (!Array.Exists(ctors, (ci) => ci.GetParameters().Length == 0))
                throw new InvalidOperationException("Non-public ctor() was not found.");
            var ctor = Array.Find(ctors, (ci) => ci.GetParameters().Length == 0);
            // Invoke constructor and return resulting object.
            return ctor.Invoke(new object[] { }) as T;
        }
    }
}
