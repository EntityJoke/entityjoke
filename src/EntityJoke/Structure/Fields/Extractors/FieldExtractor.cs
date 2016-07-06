using System;
using System.Collections.Generic;
using System.Reflection;

namespace EntityJoke.Structure.Fields
{
    public class FieldExtractor
    {
        private Dictionary<string, MethodInfo> dictionaryMethods;

        private readonly Type type;
        private Dictionary<string, Field> fields;

        public FieldExtractor(Type type)
        {
            this.type = type;
        }

        public Dictionary<string, Field> Extract()
        {
            fields = new Dictionary<string, Field>();
            LoadFieldsByFieldsInfo();
            LoadFieldsByMethods();
            return fields;
        }

        private void LoadFieldsByFieldsInfo()
        {
            foreach (FieldInfo fieldInfo in type.GetFields())
                AddField(fieldInfo);
        }

        private void AddField(FieldInfo fieldInfo)
        {
            var field = FieldFactory.Get(fieldInfo);
            fields.Add(field.ColumnName, field);
        }

        private void LoadFieldsByMethods()
        {
            foreach (MethodInfo method in type.GetMethods())
                if (IsPropertyMethod(method))
                    AddPropertyField(method);
        }

        private bool IsPropertyMethod(MethodInfo method)
        {
            return method.Name.Contains("get_") && ExistsMethodSet(method.Name);
        }

        private bool ExistsMethodSet(string method)
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

        private void AddPropertyField(MethodInfo method)
        {
            var field = FieldFactory.Get(method);
            fields.Add(field.ColumnName, field);
        }
    }
}
