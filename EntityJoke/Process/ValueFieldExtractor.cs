using EntityJoke.Core;
using EntityJoke.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EntityJoke.Process
{
    public class ValueFieldExtractor
    {
        private object objectValue;
        private Field field;

        public ValueFieldExtractor(object objectValue, Field idField)
        {
            this.objectValue = objectValue;
            this.field = idField;
        }

        public object Extract()
        {
            return GetValue();
        }

        private object GetValue()
        {
            if (field.IsProperty)
                return GetPropertyValue();

            return GetFieldValue();
        }

        private object GetPropertyValue()
        {
            PropertyInfo value = objectValue.GetType().GetProperty(field.Name);
            return value.GetValue(objectValue, null);
        }

        private object GetFieldValue()
        {
            FieldInfo value = objectValue.GetType().GetField(field.Name);
            return value.GetValue(objectValue);
        }
    }
}
