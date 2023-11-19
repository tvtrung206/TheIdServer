// Project: Aguafrommars/TheIdServer
// Copyright (c) 2023 @Olivier Lefebvre
using Aguacongas.IdentityServer.Store;
using Aguacongas.IdentityServer.Store.Entity;
using Aguacongas.TheIdServer.BlazorApp.Services;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Aguacongas.TheIdServer.BlazorApp.Test.Services
{
    public class UserAdminStoreTest
    {
        [Fact]
        public void Construtor_should_check_dependencies()
        {
            Assert.Throws<ArgumentNullException>(() => new UserAdminStore(null));
        }

        [Fact]
        public async Task GetAsync_page_should_not_be_implememted()
        {
            var storeMock = new Mock<IAdminStore<User>>();
            var sut = new UserAdminStore(storeMock.Object);
            await Assert.ThrowsAsync<NotImplementedException>(() => sut.GetAsync(new PageRequest()));
            await Assert.ThrowsAsync<NotImplementedException>(() => sut.GetAsync(new PageRequest(), CancellationToken.None));
        }
    }
}
