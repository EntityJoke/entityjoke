
using System;

namespace EntityJoke.Structure.Fields
{
    public class Field
    {
        public string ColumnName;
        public string Name;
        public bool IsEntity;
        public bool IsKey { get { return Name == "Id"; } }
        public Type Type;

        protected bool isProperty;

        public Field(FieldInfoCreator creator)
        {
            SetColumnName(creator);
            SetName(creator);
            SetType(creator);
            SetProperty(creator);

        }

        protected virtual void SetColumnName(FieldInfoCreator creator)
        {
            this.ColumnName = creator.ColumnName;
        }

        protected virtual void SetName(FieldInfoCreator creator)
        {
            this.Name = creator.Name;
        }

        protected virtual void SetType(FieldInfoCreator creator)
        {
            this.Type = creator.Type;
        }

        protected virtual void SetProperty(FieldInfoCreator creator)
        {
            isProperty = creator.IsProperty;
        }

        public override string ToString()
        {
            return String.Format("[{0}]{1}: {2}", ColumnName, Name, Type);
        }

        public virtual void SetValue(object obj, object value)
        {
            throw new NotImplementedException();
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
