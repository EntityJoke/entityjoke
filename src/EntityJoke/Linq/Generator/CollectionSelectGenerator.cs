using EntityJoke.Core;
using EntityJoke.Structure.Entities;
using EntityJoke.Structure.Fields;
using System.Linq;

namespace EntityJoke.Linq.Generator
{
    public class CollectionSelectGenerator
    {
        private readonly object obj;
        private readonly FieldCollectionEntity field;

        public CollectionSelectGenerator(object obj, FieldCollectionEntity field)
        {
            this.obj = obj;
            this.field = field;
        }

        public string Generate()
        {
            var query = QuerySimpleGeneratorFactory.Get();
            query.SetEntity(GetEntityCollection());
            query.SetWhere(GetWhere());

            return query.GetSqlCommand();
        }

        private Entity GetEntityCollection()
        {
            return DictionaryEntitiesMap.INSTANCE.GetEntity(field.Type);
        }

        private string GetWhere()
        {
            return $"{GetCollectionEntityName()}.{GetEntityMainName()}.{GetNameField()} = {GetIdEntityMain()}";
        }

        private string GetCollectionEntityName()
        {
            return GetEntityCollection().Name;
        }

        private string GetEntityMainName()
        {
            return GetEntityMainField().Name;
        }

        private Field GetEntityMainField()
        {
            return GetEntityCollection().Joins
                .Where(j => IsEntityMain(j))
                .ToList()[0].Field;
        }

        private bool IsEntityMain(EntityJoin join)
        {
            return join.Entity == GetEntityMain();
        }

        private string GetNameField()
        {
            return GetIdField().Name;
        }

        private Field GetIdField()
        {
            return GetEntityMain().GetFields().Where(f => f.IsKey).ToList()[0];
        }

        private Entity GetEntityMain()
        {
            return DictionaryEntitiesMap.INSTANCE.GetEntity(obj.GetType());
        }

        private string GetIdEntityMain()
        {
            return new FieldValueFormater(obj, GetIdField()).Format();
        }
    }
}
