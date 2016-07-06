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
        public string Name;
        public readonly Type Type;

        public List<EntityJoin> Joins { get { return TreeJoins.Joins; } }

        private NameGenerator nameGenerator;

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
            nameGenerator = new NameGenerator(Type.Name);
            Name = nameGenerator.Generate();
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
            return $"{Name}: {Type}";
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

        private static bool IsFieldsJoins(Field f)
        {
            return IsFieldEntity(f) && !IsFieldCollectionEntity(f);
        }

        private static bool IsFieldEntity(Field f)
        {
            return f is FieldEntity;
        }

        private static bool IsFieldCollectionEntity(Field f)
        {
            return f is FieldCollectionEntity;
        }

        public List<FieldCollectionEntity> GetFieldsCollection()
        {
            return GetFields()
                .Where(f => IsFieldCollectionEntity(f))
                .Cast<FieldCollectionEntity>()
                .ToList();
        }
    }



}
