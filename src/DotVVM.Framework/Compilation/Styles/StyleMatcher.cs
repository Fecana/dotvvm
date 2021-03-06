﻿using System;
using System.Collections.Generic;
using System.Linq;
using DotVVM.Framework.Compilation.ControlTree.Resolved;
using System.Reflection;

namespace DotVVM.Framework.Compilation.Styles
{
    public class StyleMatcher
    {
        public StyleMatchContext Context { get; set; }

        public ILookup<Type, IStyle> Styles { get; set; }

        public StyleMatcher(IEnumerable<IStyle> styles)
        {
            Styles = styles.ToLookup(s => s.ControlType);
        }

        public void PushControl(ResolvedControl control)
        {
            Context = new StyleMatchContext() { Control = control, Parent = Context };
        }

        public void PopControl()
        {
            if (Context == null) throw new InvalidOperationException("Stack is already empty");
            Context = Context.Parent;
        }

        public IEnumerable<IStyleApplicator> GetMatchingStyles()
        {
            return GetStyleCandidatesForControl().Where(s => s.Matches(Context)).Select(s => s.Applicator);
        }

        protected IEnumerable<IStyle> GetStyleCandidatesForControl()
        {
            var type = Context.Control.Metadata.Type;
            foreach (var s in Styles[type])
                yield return s;
            do
            {
                type = type.GetTypeInfo().BaseType;

                foreach (var s in Styles[type])
                    if (!s.ExactTypeMatch)
                        yield return s;

            } while (type != typeof(object));
        }
    }
}
