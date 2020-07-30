using System;
using System.Collections.Generic;
using System.Text;

namespace St.DoMain.Entity
{
    public class RootEntity<T>
    {
        public T Id { get; protected set; }
    }
}
