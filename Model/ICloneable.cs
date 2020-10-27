using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    interface ICloneable<T>
    {
        public abstract T Clone(bool deepCopy);
    }
}
