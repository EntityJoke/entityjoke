using EntityJoke.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityJoke.Process
{
    public class AliasGenerator
    {
        private Entity entity;
        private Dictionary<string, Alias> dictionaryAliases;
        string alias = "";

        public AliasGenerator(Entity ent, Dictionary<string, Alias> dictionaryAliases)
        {
            this.entity = ent;
            this.dictionaryAliases = dictionaryAliases;
        }

        public string Generate()
        {
            return GenerateAlias();
        }

        public string GenerateAlias(int length = 1)
        {
            ExtractAlias(length);
            return IsAValidAlias() ? alias : GenerateAlias(++length);
        }

        private void ExtractAlias(int length)
        {
            alias = entity.Name.Substring(0, length);
        }

        private bool IsAValidAlias()
        {
            return NotIsAliasProcessed() && NotIsEmptyAlias();
        }

        private bool NotIsAliasProcessed()
        {
            return !dictionaryAliases.ContainsKey(alias);
        }

        private bool NotIsEmptyAlias()
        {
            return alias.Length > 0;
        }
    }
}
