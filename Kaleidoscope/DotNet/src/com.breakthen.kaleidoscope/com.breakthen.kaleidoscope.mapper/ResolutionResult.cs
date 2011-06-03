using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace com.breakthen.kaleidoscope.mapper
{
    public class ResolutionResult
    {
        private ResolutionContext context;

        private List<MemberBinding> memberBindings;

        public ResolutionResult(ResolutionContext context)
        {
            this.context = context;
            this.memberBindings = new List<MemberBinding>();
        }

        public ResolutionContext Context
        {
            get
            {
                return this.context;
            }
        }

        public List<MemberBinding> MemberBindings
        {
            get
            {
                return this.memberBindings;
            }
            set
            {
                this.memberBindings = value;
            }
        }

        public Type SourceType
        {
            get
            {
                return this.context.SourceType;
            }
        }

        public Type TargetType
        {
            get
            {
                return this.context.TargetType;
            }
        }
    }
}
