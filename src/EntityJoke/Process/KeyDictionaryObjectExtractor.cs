using EntityJoke.Core;
using EntityJoke.Structure.Fields;

namespace EntityJoke.Process
{
    public class KeyDictionaryObjectExtractor
    {
        private readonly object obj;

        public KeyDictionaryObjectExtractor(object obj)
        {
            this.obj = obj;
        }

        public  string Extract()
        {
            return $"{GetTypeName()}_{GetIdValue()}";
        }

        private string GetTypeName()
        {
            return DictionaryEntitiesMap.INSTANCE.GetEntity(obj.GetType()).Type.FullName;
        }

        private string GetIdValue()
        {
            return GetIdField().GetExtractor(obj).Extract().ToString();
        }

        private Field GetIdField()
        {
            return DictionaryEntitiesMap.INSTANCE.GetEntity(obj.GetType()).FieldDictionary["id"];
        }
    }
}
