using Comrade.Api;
using Comrade.Domain.Bases.Interfaces;
using Comrade.Persistence.DataAccess;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Respawn;
using System;
using System.Threading;
using Xunit;

namespace Comrade.IntegrationTests
{
    [CollectionDefinition(nameof(SliceFixture))]
    public class SliceFixtureCollection : ICollectionFixture<SliceFixture> { }

    public class SliceFixture : IAsyncLifetime
    {
        private readonly Checkpoint _checkpoint;
        private readonly IConfiguration _configuration;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly WebApplicationFactory<Startup> _factory;

        public SliceFixture()
        {
            _factory = new ComradeTestApplicationFactory();

            _configuration = _factory.Services.GetRequiredService<IConfiguration>();
            _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();

            _checkpoint = new Checkpoint();
        }

        public class ComradeTestApplicationFactory
            : WebApplicationFactory<Startup>
        {
            protected override void ConfigureWebHost(IWebHostBuilder builder)
            {
                builder.ConfigureAppConfiguration((_, configBuilder) =>
                {
                    configBuilder.AddInMemoryCollection(new Dictionary<string, string>
                    {
                        {"ConnectionStrings:DefaultConnection", _connectionString}
                    });
                });
            }

            private readonly string _connectionString = "Host=localhost;Port=5432;Pooling=true;Database=comrades;User Id=postgres ;Password=qwe123;";
        }

        public async Task ExecuteScopeAsync(Func<IServiceProvider, Task> action)
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ComradeContext>();

            try
            {
                await dbContext.BeginTransactionAsync();

                await action(scope.ServiceProvider);

                await dbContext.CommitTransactionAsync();
            }
            catch (Exception)
            {
                dbContext.RollbackTransaction();
                throw;
            }
        }

        public async Task<T> ExecuteScopeAsync<T>(Func<IServiceProvider, Task<T>> action)
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ComradeContext>();

            try
            {
                await dbContext.BeginTransactionAsync();

                var result = await action(scope.ServiceProvider);

                await dbContext.CommitTransactionAsync();

                return result;
            }
            catch (Exception)
            {
                dbContext.RollbackTransaction();
                throw;
            }
        }

        public Task ExecuteDbContextAsync(Func<ComradeContext, Task> action)
            => ExecuteScopeAsync(sp => action(sp.GetService<ComradeContext>()!));

        public Task ExecuteDbContextAsync(Func<ComradeContext, ValueTask> action)
            => ExecuteScopeAsync(sp => action(sp.GetService<ComradeContext>()!).AsTask());

        public Task ExecuteDbContextAsync(Func<ComradeContext, IMediator, Task> action)
            => ExecuteScopeAsync(sp => action(sp.GetService<ComradeContext>()!, sp.GetService<IMediator>()!));

        public Task<T> ExecuteDbContextAsync<T>(Func<ComradeContext, Task<T>> action)
            => ExecuteScopeAsync(sp => action(sp.GetService<ComradeContext>()!));

        public Task<T> ExecuteDbContextAsync<T>(Func<ComradeContext, ValueTask<T>> action)
            => ExecuteScopeAsync(sp => action(sp.GetService<ComradeContext>()!).AsTask());

        public Task<T> ExecuteDbContextAsync<T>(Func<ComradeContext, IMediator, Task<T>> action)
            => ExecuteScopeAsync(sp => action(sp.GetService<ComradeContext>()!, sp.GetService<IMediator>()!));

        public Task InsertAsync<T>(params T[] entities) where T : class
        {
            return ExecuteDbContextAsync(db =>
            {
                foreach (var entity in entities)
                {
                    db.Set<T>().Add(entity);
                }
                return db.SaveChangesAsync();
            });
        }

        public Task InsertAsync<TEntity>(TEntity entity) where TEntity : class
        {
            return ExecuteDbContextAsync(db =>
            {
                db.Set<TEntity>().Add(entity);

                return db.SaveChangesAsync();
            });
        }

        public Task InsertAsync<TEntity, TEntity2>(TEntity entity, TEntity2 entity2)
            where TEntity : class
            where TEntity2 : class
        {
            return ExecuteDbContextAsync(db =>
            {
                db.Set<TEntity>().Add(entity);
                db.Set<TEntity2>().Add(entity2);

                return db.SaveChangesAsync();
            });
        }

        public Task InsertAsync<TEntity, TEntity2, TEntity3>(TEntity entity, TEntity2 entity2, TEntity3 entity3)
            where TEntity : class
            where TEntity2 : class
            where TEntity3 : class
        {
            return ExecuteDbContextAsync(db =>
            {
                db.Set<TEntity>().Add(entity);
                db.Set<TEntity2>().Add(entity2);
                db.Set<TEntity3>().Add(entity3);

                return db.SaveChangesAsync();
            });
        }

        public Task InsertAsync<TEntity, TEntity2, TEntity3, TEntity4>(TEntity entity, TEntity2 entity2, TEntity3 entity3, TEntity4 entity4)
            where TEntity : class
            where TEntity2 : class
            where TEntity3 : class
            where TEntity4 : class
        {
            return ExecuteDbContextAsync(db =>
            {
                db.Set<TEntity>().Add(entity);
                db.Set<TEntity2>().Add(entity2);
                db.Set<TEntity3>().Add(entity3);
                db.Set<TEntity4>().Add(entity4);

                return db.SaveChangesAsync();
            });
        }

        public Task<T?> FindAsync<T>(int id)
            where T : class, IEntity
        {
            return ExecuteDbContextAsync(db => db.Set<T>().FindAsync(id).AsTask());
        }

        public Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            return ExecuteScopeAsync(sp =>
            {
                var mediator = sp.GetRequiredService<IMediator>();

                return mediator.Send(request);
            });
        }

        public Task SendAsync(IRequest request)
        {
            return ExecuteScopeAsync(sp =>
            {
                var mediator = sp.GetRequiredService<IMediator>();

                return mediator.Send(request);
            });
        }

        private int _courseNumber = 1;

        public int NextCourseNumber() => Interlocked.Increment(ref _courseNumber);

        public Task InitializeAsync()
            => _checkpoint.Reset(_configuration.GetConnectionString("DefaultConnection"));

        public Task DisposeAsync()
        {
            _factory?.Dispose();
            return Task.CompletedTask;
        }
    }
}