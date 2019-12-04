using System;
using Assure.Core.RepositoryLayer.Implementation;
using Assure.Core.RepositoryLayer.Implementation.Profiles;
using Assure.Core.RepositoryLayer.Implementation.Serialization;
using Assure.Core.RepositoryLayer.Interfaces.Models;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Xunit;


namespace Assure.Core.RepositoryLayer.Tests.Unit
{
    public class ObjectDocumentSerializerTests
    {
        private readonly IObjectDocumentSerializer _objectDocumentSerializer;

        public ObjectDocumentSerializerTests()
        {
            var services = new ServiceCollection();
            services.AddOptions();
            services.AddLogging();
            //services.Boot();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<ObjectDocumentSerializer, ObjectDocumentSerializer>();

            _objectDocumentSerializer = services.BuildServiceProvider().GetService<ObjectDocumentSerializer>();
        }

        //[Fact]
        //public void Serialize_Account()
        //{
        //    var descendent = A.New<Account>();
        //    var result = _objectDocumentSerializer.Serialize<Account, Account>(descendent);
        //}

        //[Fact]
        //public void Serialize()
        //{
        //    var descendent = A.New<Grandchild>();
        //    descendent.Contained = A.New<ContainedClass>();
        //    descendent.Contained2 = A.New<ContainedClass>();
        //    var result = _objectDocumentSerializer.Serialize<DescendentClass, BaseClass>(descendent);
        //}
    }
}