using System.Activities.Expressions;
using System.Collections.Generic;
using Serializer.Annotations;
using Serializer.Attributes;

namespace Serializer.SerializerI
{
    public abstract class Assembly
    {
        [UsedImplicitly]
        protected List<AssemblyPart> _parts;

        protected Assembly(List<AssemblyPart> parts)
        {
            _parts = parts;
        }

        public IEnumerable<AssemblyPart> Parts
        {
            get { return _parts; }
            set { _parts = new List<AssemblyPart>(value); }
        }

        public void AddPart(AssemblyPart part)
        {
            _parts.Add(part);
        }

        public abstract void Serialize(object destination);
    }
}
