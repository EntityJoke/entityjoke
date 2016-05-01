using EntityJoke.Process.Generators;
using EntityJoke.Structure.Fields;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityJoke.Structure.Entities
{
    public class Entity
    {
        public EntityJoin TreeJoins;
        public Dictionary<string, Field> FieldDictionary;
        public Type Type;
        public string Name;
        public List<EntityJoin> Joins { get { return TreeJoins.Joins; } }

        public Entity(Type type)
        {
            Type = type;
            LoadEntity();
        }

        private void LoadEntity()
        {
            LoadName();
            LoadFields();
            LoadAliases();
        }

        private void LoadName()
        {
            Name = NameGenerator.INSTANCE.Generate(Type.Name);
        }

        private void LoadFields()
        {
            FieldDictionary = new FieldExtractor(Type).Extract();
        }

        private void LoadAliases()
        {
            TreeJoins = new EntityJoinsGenerator().Generate(this);
        }

        public override string ToString()
        {
            return String.Format("{0}: {1}", Name, Type);
        }

        public List<Field> GetFields()
        {
            return FieldDictionary.Values.ToList();
        }

        public List<Field> GetFieldsJoins()
        {
            return GetFields()
                .Where(f => IsFieldsJoins(f))
                .ToList();
        }

        private bool IsFieldsJoins(Field f)
        {
            return IsFieldEntity(f) && !IsFieldCollectionEntity(f);
        }

        private bool IsFieldEntity(Field f)
        {
            return f is FieldEntity;
        }

        private bool IsFieldCollectionEntity(Field f)
        {
            return f is FieldCollectionEntity;
        }
    }



}
