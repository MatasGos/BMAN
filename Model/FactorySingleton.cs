using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    //TODO: Rename to BlockFactorySingleton???
    public class FactorySingleton : BlockFactory
    {
        private static FactorySingleton instance = null;
        private static object lockObject = new object();

        public FactorySingleton()
        {
        }

        public static FactorySingleton GetInstance()
        {
            if (instance == null)
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new FactorySingleton();
                    }
                }
            }
            return instance;
        }
    }
}
