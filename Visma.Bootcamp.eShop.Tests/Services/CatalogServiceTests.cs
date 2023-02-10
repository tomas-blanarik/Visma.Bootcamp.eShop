using System.Security.Cryptography.X509Certificates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.Domain;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.DTO;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.Models;
using Visma.Bootcamp.eShop.ApplicationCore.Services;
using Visma.Bootcamp.eShop.Tests.Database;
using Xunit;
using Visma.Bootcamp.eShop.ApplicationCore.Exceptions;
using Visma.Bootcamp.eShop.ApplicationCore.Services.Interfaces;

namespace Visma.Bootcamp.eShop.Tests.Services
{
    public class CatalogServiceTests
    {
        private readonly TestApplicationContext _context;
        private CatalogService _service;

        public CatalogServiceTests()
        {
            _context = new TestApplicationContext();
            _service = new CatalogService(_context);
        }

        [Fact]
        public async Task CreateAsync_ExpectedResult()
        {
            // arrange
            var model = new CatalogModel
            {
                Name = "test",
                Description = "test description"
            };

            // act
            CatalogDto dto = await _service.CreateAsync(model);

            // assert
            Assert.NotNull(dto);
            Assert.Equal(model.Name, dto.Name);
            Assert.Equal(model.Description, dto.Description);
            Assert.NotNull(dto.PublicId);
        }

        [Fact]
        public async Task CreateAsync_Duplicate_Error()
        {
            // arrange
            var model = new CatalogModel
            {
                Name = "duplicate",
                Description = "test description"
            };

            var duplicateCatalog = new Catalog
            {
                PublicId = Guid.NewGuid(),
                Name = model.Name,
                Description = "existing description"
            };

            await _context.Catalogs.AddAsync(duplicateCatalog);
            await _context.SaveChangesAsync();

            // act + assert
            await Assert.ThrowsAsync<ConflictException>(
                async () => await _service.CreateAsync(model));
        }
    }
}