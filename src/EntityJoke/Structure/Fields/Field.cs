
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
            this.ColumnName = creator.ColumnName;
            this.Name = creator.Name;
            this.Type = creator.Type;
            isProperty = creator.IsProperty; 

            VerifyIsEntity();
        }

        private void VerifyIsEntity()
        {
            if (IsSystemClass())
                return;

            ColumnName = String.Format("id_{0}", ColumnName);
            IsEntity = true;
        }

        private bool IsSystemClass()
        {
            return Type.FullName.Contains("System.");
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
