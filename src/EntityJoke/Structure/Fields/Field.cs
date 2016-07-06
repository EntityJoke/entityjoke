
using System;

namespace EntityJoke.Structure.Fields
{
    public class Field
    {
        public string ColumnName;
        public bool IsEntity;
        public bool IsKey { get { return Name == "Id"; } }
        public Type Type;

        public readonly string Name;

        protected readonly bool isProperty;

        public Field(FieldInfoCreator creator)
        {
            ColumnName = creator.ColumnName;
            Name = creator.Name;
            Type = creator.Type;
            isProperty = creator.IsProperty;
        }

        public override string ToString()
        {
            return $"[{ColumnName}]{Name}: {Type}";
        }

        public virtual FieldValueSetter GetSetter(object obj, object value)
        {
            if (isProperty)
                return new FieldPropertyValueSetter(obj, this, value);
            return new FieldValueSetter(obj, this, value);
        }

        public virtual FieldValueExtractor GetExtractor(object obj)
        {
            if (isProperty)
                return new FieldPropertyValueExtractor(obj, this);
            return new FieldValueExtractor(obj, this);
        }
    }
}
