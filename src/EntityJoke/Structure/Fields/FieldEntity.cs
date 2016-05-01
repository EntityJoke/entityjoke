using EntityJoke.Core;
using System;

namespace EntityJoke.Structure.Fields
{
    public class FieldEntity : Field
    {
        string DeclaringTypeName;

        public FieldEntity(FieldInfoCreator creator) : base(creator)
        {
            SetDeclaringTypeName(creator);
            SetEntity();
            IsEntity = true;
        }

        protected virtual void SetDeclaringTypeName(FieldInfoCreator creator)
        {
            this.DeclaringTypeName = creator.DeclaringTypeName;
        }

        protected override void SetColumnName(FieldInfoCreator creator)
        {
            this.ColumnName = String.Format("id_{0}", creator.ColumnName);
        }

        private void SetEntity()
        {
            if(ShouldAddEntity())
                DictionaryEntitiesMap.INSTANCE.TryAddEntity(DictionaryType());
        }

        protected virtual bool ShouldAddEntity()
        {
            return true;
        }

        protected virtual Type DictionaryType()
        {
            return this.Type;
        }
    }
}
