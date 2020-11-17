using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public interface Iterator
    {
        public bool hasNext();     
        public Object next();
    }
}
