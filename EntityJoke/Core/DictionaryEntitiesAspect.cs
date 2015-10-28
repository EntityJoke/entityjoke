using EntityJoke.Process;
using EntityJoke.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityJoke.Core
{
    public class DictionaryEntitiesAspect
    {
        private static DictionaryEntitiesAspect instance = new DictionaryEntitiesAspect();

        private Dictionary<string, object> entityes = new Dictionary<string, object>();
        public int CountObjects { get { return entityes.Count; } }

        private DictionaryEntitiesAspect() { }

        public static DictionaryEntitiesAspect GetInstance()
        {
            return instance;
        }

        public void Clear()
        {
            entityes.Clear();
        }


        public bool TryAddObject(object obj)
        {
            if (IsObjectProcessed(obj))
                return false;

            ProcessJoins(obj);

            entityes.Add(GetKey(obj), obj);

            return true;
        }

        private void ProcessJoins(object obj)
        {
            Entity ent = DictionaryEntitiesMap.INSTANCE.GetEntity(obj.GetType());

            ent.GetFields().Where(f => f.IsEntity).ToList()
                .ForEach(f => ProcessJoins(obj, f));
        }

        private void ProcessJoins(object obj, Field f)
        {
            var objJoin = new ValueFieldExtractor(obj, f).Extract();
            DictionaryEntitiesAspect.GetInstance().TryAddObject(objJoin);
        }

        private bool IsObjectProcessed(object obj)
        {
            return entityes.ContainsKey(GetKey(obj));
        }

        private string GetKey(object obj)
        {
            return String.Format("{0}_{1}", GetTypeName(obj), GetIdValue(obj));
        }

        private string GetTypeName(object obj)
        {
            return DictionaryEntitiesMap.INSTANCE.GetEntity(obj.GetType()).Type.FullName;
        }

        private string GetIdValue(object obj)
        {
            return new ValueFieldExtractor(obj, GetIdField(obj)).Extract().ToString();
        }

        private Field GetIdField(object obj)
        {
            return DictionaryEntitiesMap.INSTANCE.GetEntity(obj.GetType()).FieldDictionary["id"];
        }

        private object GetCopyObj(object obj)
        {
            object copy = Activator.CreateInstance(obj.GetType());

            DictionaryEntitiesMap.INSTANCE.GetEntity(obj.GetType())
                .GetFields().ForEach(f => CopyFieldValue(obj, copy, f));

            return copy;
        }

        private void CopyFieldValue(object origin, object copy, Field field)
        {
            new FieldValueSetter(copy, field, GetValue(origin, field)).Set();
        }

        private object GetValue(object origin, Field field)
        {
            return new ValueFieldExtractor(origin, field).Extract();
        }

        public object GetAspect(object obj)
        {
            if (IsObjectProcessed(obj))
                return entityes[GetKey(obj)];

            return null;
        }

        public void CopyObjects()
        {
            entityes.Keys.ToList()
                .ForEach(k => CopyObject(k));
        }

        private void CopyObject(string k)
        {
            entityes[k] = GetCopyObj(entityes[k]);
        }
    }
}
