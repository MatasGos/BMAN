using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class BlockFactorySingleton : BlockFactory
    {
        private static BlockFactorySingleton instance = null;
        private static object lockObject = new object();

        public BlockFactorySingleton()
        {
        }

        public static BlockFactorySingleton GetInstance()
        {
            if (instance == null)
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new BlockFactorySingleton();
                    }
                }
            }
            return instance;
        }
    }
}
