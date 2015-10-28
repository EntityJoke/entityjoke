using EntityJoke.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EntityJoke.Core
{
    class FieldValueSetter
    {
        private object obj;
        private Field field;
        private object value;

        public FieldValueSetter(object obj, Field field, object value)
        {
            this.obj = obj;
            this.field = field;
            this.value = value;
        }

        public void Set()
        {
            if (field.IsProperty)
                SetPropertyMethod(value, field.Name);
            else
                SetField(value, field.Name);
        }

        private void SetPropertyMethod(object value, string fieldName)
        {
            PropertyInfo property = obj.GetType().GetProperty(fieldName);
            property.SetValue(obj, value);
        }

        private void SetField(object value, string fieldName)
        {
            FieldInfo field = obj.GetType().GetField(fieldName);
            field.SetValue(obj, value);
        }
    }
}
