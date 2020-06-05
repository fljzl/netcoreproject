using System;
using System.Collections.Generic;
using System.Text;

namespace apple.Infrastructure
{
    public class Singleton<T> where T : new()
    {
        private static T _instance;

        private static object mylogc = new object();

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (mylogc)
                    {
                        _instance = new T();
                    }
                }
                return _instance;
            }
        }
    }
}
