using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace com.breakthen.kaleidoscope.mapper
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class MappingAttribute : Attribute
    {
        public string Name { get; set; }

        public bool Ignored { get; set; }

        public MappingAttribute(string name) : this(name, false) { }

        public MappingAttribute(string name, bool ignored) { }

        public MappingAttribute() { }
    }
}
