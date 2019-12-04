using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assure.Core.RepositoryLayer.Implementation.Serialization
{
    public sealed class IgnoreNamespacePropertiesResolver : DefaultContractResolver
    {
        private readonly string _namespaceToIgnore;

        public IgnoreNamespacePropertiesResolver(string namespaceToIgnore)
        {
            _namespaceToIgnore = namespaceToIgnore;
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            return base.CreateProperties(type, memberSerialization).Where(property => !string.Equals(_namespaceToIgnore, property.DeclaringType.Namespace, StringComparison.InvariantCultureIgnoreCase)).ToList();
        }
    }
}
