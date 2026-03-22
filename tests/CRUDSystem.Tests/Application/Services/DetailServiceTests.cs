using CRUDSystem.Application.Abstractions;
using CRUDSystem.Application.Services;
using CRUDSystem.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRUDSystem.Tests.Application.Services
{
    [TestClass]
    public class DetailServiceTests
    {
        [TestMethod]
        public void Constructor_Throws_WhenRepositoryFactoryIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new DetailService(null));
        }

        [TestMethod]
        public async Task GetDetailsAsync_ReturnsRepositoryData_AndDisposesRepository()
        {
            var repository = new FakeDetailRepository
            {
                Details = new List<Detail>
                {
                    new Detail { ID = 1, Fname = "Ada", Lname = "Lovelace", Age = 36 }
                }
            };

            var service = new DetailService(() => repository);

            var result = await service.GetDetailsAsync();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Ada", result[0].Fname);
            Assert.IsTrue(repository.IsDisposed);
        }

        [TestMethod]
        public async Task GetDetailAsync_ReturnsRepositoryItem()
        {
            var repository = new FakeDetailRepository
            {
                DetailToReturn = new Detail { ID = 7, Fname = "Grace", Lname = "Hopper", Age = 85 }
            };

            var service = new DetailService(() => repository);

            var result = await service.GetDetailAsync(7);

            Assert.IsNotNull(result);
            Assert.AreEqual(7, result.ID);
            Assert.AreEqual(7, repository.RequestedId);
            Assert.IsTrue(repository.IsDisposed);
        }

        [TestMethod]
        public async Task SaveDetailAsync_Throws_WhenDetailIsNull()
        {
            var service = new DetailService(() => new FakeDetailRepository());

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => service.SaveDetailAsync(null));
        }

        [TestMethod]
        public async Task SaveDetailAsync_CallsAdd_ForNewDetail()
        {
            var repository = new FakeDetailRepository();
            var service = new DetailService(() => repository);
            var detail = new Detail { ID = 0, Fname = "New", Lname = "Record", Age = 21 };

            await service.SaveDetailAsync(detail);

            Assert.AreEqual(1, repository.AddCalls);
            Assert.AreEqual(0, repository.UpdateCalls);
            Assert.AreSame(detail, repository.LastSavedDetail);
            Assert.IsTrue(repository.IsDisposed);
        }

        [TestMethod]
        public async Task SaveDetailAsync_CallsUpdate_ForExistingDetail()
        {
            var repository = new FakeDetailRepository();
            var service = new DetailService(() => repository);
            var detail = new Detail { ID = 42, Fname = "Existing", Lname = "Record", Age = 30 };

            await service.SaveDetailAsync(detail);

            Assert.AreEqual(0, repository.AddCalls);
            Assert.AreEqual(1, repository.UpdateCalls);
            Assert.AreSame(detail, repository.LastSavedDetail);
            Assert.IsTrue(repository.IsDisposed);
        }

        [TestMethod]
        public async Task DeleteDetailAsync_CallsDelete_AndDisposesRepository()
        {
            var repository = new FakeDetailRepository();
            var service = new DetailService(() => repository);

            await service.DeleteDetailAsync(19);

            Assert.AreEqual(19, repository.DeletedId);
            Assert.IsTrue(repository.IsDisposed);
        }

        private sealed class FakeDetailRepository : IDetailRepository
        {
            public List<Detail> Details { get; set; } = new List<Detail>();

            public Detail DetailToReturn { get; set; }

            public int AddCalls { get; private set; }

            public int UpdateCalls { get; private set; }

            public int DeletedId { get; private set; }

            public int RequestedId { get; private set; }

            public Detail LastSavedDetail { get; private set; }

            public bool IsDisposed { get; private set; }

            public Task AddAsync(Detail detail)
            {
                AddCalls++;
                LastSavedDetail = detail;
                return Task.CompletedTask;
            }

            public Task DeleteAsync(int id)
            {
                DeletedId = id;
                return Task.CompletedTask;
            }

            public void Dispose()
            {
                IsDisposed = true;
            }

            public Task<List<Detail>> GetAllAsync()
            {
                return Task.FromResult(Details);
            }

            public Task<Detail> GetByIdAsync(int id)
            {
                RequestedId = id;
                return Task.FromResult(DetailToReturn);
            }

            public Task UpdateAsync(Detail detail)
            {
                UpdateCalls++;
                LastSavedDetail = detail;
                return Task.CompletedTask;
            }
        }
    }
}
