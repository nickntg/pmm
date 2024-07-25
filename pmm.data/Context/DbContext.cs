using System;
using System.Data;
using NHibernate;
using pmm.data.Context.Interfaces;
using pmm.data.Repositories;
using pmm.data.Repositories.Interfaces;

namespace pmm.data.Context
{
	public class DbContext : IDbContext
	{
		private readonly ISessionFactory _sessionFactory;
		private readonly ISession _session;
		private ITransaction _transaction;
		
        public DbContext(ISessionFactory sessionFactory)
        {
			_sessionFactory = sessionFactory;

            _session = sessionFactory.OpenSession();
			_session = CreateNewSession();

            MobileDatRepository = new MobileDataRepository(_session);
        }
        public IMobileDataRepository MobileDatRepository { get; }

        public void BeginTransaction()
		{
			if (_transaction is { IsActive: true })
			{
				throw new InvalidOperationException("Transaction already in progress");
			}

			_transaction = _session.BeginTransaction(IsolationLevel.ReadCommitted);
		}

		public void Commit()
		{
			_transaction?.Commit();
		}

		public void Rollback()
		{
			_transaction?.Rollback();
		}

		public ISession GetSession()
		{
			return _session;
		}

		public ISession CreateNewSession()
		{
			return _sessionFactory.OpenSession();
		}

		public void Dispose()
        {
            GC.SuppressFinalize(this);
			_transaction?.Dispose();
			_session?.Dispose();
		}

		public void ClearSession()
		{
			_session.Flush();
			_session.Clear();
		}
	}
}