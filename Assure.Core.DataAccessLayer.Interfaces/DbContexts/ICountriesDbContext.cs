﻿using Assure.Core.DataAccessLayer.Interfaces.Models;

namespace Assure.Core.DataAccessLayer.Interfaces.DbContexts
{
    public interface ICountriesDbContext : IReferenceDbContext<Country>
    {
    }
}