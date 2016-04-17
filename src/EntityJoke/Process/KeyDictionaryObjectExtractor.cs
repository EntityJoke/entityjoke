using EntityJoke.Core;
using EntityJoke.Structure.Fields;
using System;

namespace EntityJoke.Process
{
    public class KeyDictionaryObjectExtractor
    {
        private object obj;

        public KeyDictionaryObjectExtractor(object obj)
        {
            this.obj = obj;
        }

        public  string Extract()
        {
            return String.Format("{0}_{1}", GetTypeName(), GetIdValue());
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
