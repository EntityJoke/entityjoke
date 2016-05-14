﻿using EntityJoke.Linq.Generator;
using EntityJoke.Process.Commands;
using EntityJoke.Structure.Fields;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace EntityJoke.Core.Loaders
{
    public class CollectionLoader
    {
        private readonly Type LOADER_TYPE = typeof(EntitiesLoaderReflectionAdapter);

        private Dictionary<string, object> dictionary;
        private FieldCollectionEntity field;
        private object obj;

        public CollectionLoader(object obj, Dictionary<string, object> dictionary, FieldCollectionEntity field)
        {
            this.obj = obj;
            this.dictionary = dictionary;
            this.field = field;
        }

        public object Load()
        {
            return GetMethod().Invoke(GenerateLoaderInstance(), null);
        }

        private MethodInfo GetMethod()
        {
            return LOADER_TYPE.GetMethod("Load").MakeGenericMethod(field.Type);
        }

        private object GenerateLoaderInstance()
        {
            return GetConstructor().Invoke(new object[] { GetDataTable(), dictionary });
        }

        private ConstructorInfo GetConstructor()
        {
            return LOADER_TYPE.GetConstructor(new[] { DataTableType(), DictionaryType() });
        }

        private Type DataTableType()
        {
            return typeof(DataTable);
        }

        private Type DictionaryType()
        {
            return typeof(Dictionary<string, object>);
        }

        private DataTable GetDataTable()
        {
            return DataTableGeneratorFactory.Get(CommandSql()).Generate();
        }

        private string CommandSql()
        {
            return new CollectionSelectGenerator(obj, field).Generate();
        }
    }
}