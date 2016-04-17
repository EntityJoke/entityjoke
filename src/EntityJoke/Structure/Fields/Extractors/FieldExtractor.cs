using EntityJoke.Core;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace EntityJoke.Structure.Fields
{
    public class FieldExtractor
    {
        private Dictionary<string, MethodInfo> dictionaryMethods;

        private Type type;

        public FieldExtractor(Type type)
        {
            this.type = type;
        }

        public Dictionary<string, Field> Extract()
        {
            Dictionary<string, Field> fields = new Dictionary<string, Field>();
            LoadFieldsByFieldsInfo(fields);
            LoadFieldsByMethods(fields);
            return fields;
        }

        private void LoadFieldsByFieldsInfo(Dictionary<string, Field> fields)
        {
            foreach (FieldInfo fieldInfo in type.GetFields())
            {
                Field field = FieldFactory.Get(fieldInfo);
                fields.Add(field.ColumnName, field);
                VerifyIsEntity(field);
            }
        }

        private static void VerifyIsEntity(Field field)
        {
            if (field.IsEntity)
                DictionaryEntitiesMap.INSTANCE.TryAddEntity(field.Type);
        }

        private void LoadFieldsByMethods(Dictionary<string, Field> fields)
        {
            foreach (MethodInfo method in type.GetMethods())
            {
                if (isPropertyMethod(method))
                {
                    Field field = FieldFactory.Get(method);
                    fields.Add(field.ColumnName, field);
                    VerifyIsEntity(field);
                }
            }
        }

        private bool isPropertyMethod(MethodInfo method)
        {
            return method.Name.Contains("get_") && existsMethodSet(method.Name);
        }

        private bool existsMethodSet(string method)
        {
            if (dictionaryMethods == null)
                LoadDictionaryMethods();

            return dictionaryMethods.ContainsKey(method.Replace("get_", "set_"));
        }

        private void LoadDictionaryMethods()
        {
            dictionaryMethods = new Dictionary<string,MethodInfo>();
            foreach (MethodInfo method in type.GetMethods())
                dictionaryMethods.Add(method.Name, method);
        }
    }
}
