using EntityJoke.Core;
using EntityJoke.Structure.Fields;
using System;

namespace EntityJoke.Process
{
    public class ClonerObject
    {
        private readonly object origin;

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
            var copy = Activator.CreateInstance(origin.GetType());

            DictionaryEntitiesMap.INSTANCE.GetEntity(origin.GetType())
                .GetFields().ForEach(f => CopyFieldValue(copy, f));

            return copy;
        }

        private void CopyFieldValue(object copy, Field field)
        {
            field.GetSetter(copy, GetValue(field)).Set();
        }

        private object GetValue(Field field)
        {
            return field.GetExtractor(origin).Extract();
        }
    }
}
