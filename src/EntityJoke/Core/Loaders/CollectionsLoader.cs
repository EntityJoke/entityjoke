using EntityJoke.Structure.Entities;
using EntityJoke.Structure.Fields;
using System.Collections.Generic;

namespace EntityJoke.Core.Loaders
{
    public class CollectionsLoader
    {
        private object obj;
        private Dictionary<string, object> dictionary;

        public CollectionsLoader(object obj, Dictionary<string, object> dictionary)
        {
            this.obj = obj;
            this.dictionary = dictionary;
        }

        public void Load()
        {
            GetFieldsCollections().ForEach(f => ProcessCollection(f));
        }

        private List<FieldCollectionEntity> GetFieldsCollections()
        {
            return GetEntity().GetFieldsCollection();
        }

        private Entity GetEntity()
        {
            return DictionaryEntitiesMap.INSTANCE.GetEntity(obj.GetType());
        }

        private void ProcessCollection(FieldCollectionEntity field)
        {
            field.GetSetter(obj, GenerateList(field)).Set();
        }

        private object GenerateList(FieldCollectionEntity field)
        {
            return new CollectionLoader(obj, dictionary, field).Load();
        }
    }
}
