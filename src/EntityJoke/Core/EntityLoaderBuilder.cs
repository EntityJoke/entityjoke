﻿using EntityJoke.Structure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EntityJoke.Core
{
    public class EntityLoaderBuilder
    {
        private DataRow row;
        private int indexColumn;
        private Entity entity;
        private DataColumnCollection columns;
        private Dictionary<string, object> dictionaryEntities = new Dictionary<string, object>();

        public EntityLoaderBuilder Entity(Entity entity)
        {
            this.entity = entity;
            return this;
        }

        public EntityLoaderBuilder Columns(DataColumnCollection columns)
        {
            this.columns = columns;
            return this;
        }

        public EntityLoaderBuilder Row(DataRow row)
        {
            this.row = row;
            return this;
        }

        public EntityLoaderBuilder IndexColumn(int indexColumn)
        {
            this.indexColumn = indexColumn;
            return this;
        }

        public EntityLoaderBuilder Dictionary(Dictionary<string, object> dictionaryEntities)
        {
            this.dictionaryEntities = dictionaryEntities;
            return this;
        }

        public object Build()
        {
            EntityLoader loader = new EntityLoader();
            loader.Row = row;
            loader.IndexColumn = indexColumn;
            loader.Entity = entity;
            loader.Columns = columns;
            loader.DictionaryObjectsProcessed = dictionaryEntities;
            return loader.LoadInstance();
        }

    }
}
