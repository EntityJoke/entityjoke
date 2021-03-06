﻿using EntityJoke.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityJoke.Linq
{
    public abstract class SQLGeneratorBuilder<T> where T : SQLGeneratorBuilder<T>
    {
        protected Entity entity;

        public T SetEntity(Entity entity)
        {
            this.entity = entity;
            return (T)this;
        }

        public abstract string Build();
    }
}
