using EntityJoke.Core;
using EntityJoke.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityJoke.Process
{
    public class ClonerObject
    {
        private object origin;

        public ClonerObject(object obj)
        {
            this.origin = obj;
        }

        public object clone()
        {
            return GetCopyObj();
        }

        private object GetCopyObj()
        {
            object copy = Activator.CreateInstance(origin.GetType());

            DictionaryEntitiesMap.INSTANCE.GetEntity(origin.GetType())
                .GetFields().ForEach(f => CopyFieldValue(copy, f));

            return copy;
        }

        private void CopyFieldValue(object copy, Field field)
        {
            new FieldValueSetter(copy, field, GetValue(field)).Set();
        }

        private object GetValue(Field field)
        {
            return new ValueFieldExtractor(origin, field).Extract();
        }
    }
}
