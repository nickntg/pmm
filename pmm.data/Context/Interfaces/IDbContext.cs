using System;
using NHibernate;
using pmm.data.Repositories.Interfaces;

namespace pmm.data.Context.Interfaces
{
	public interface IDbContext : IDisposable
	{
		void BeginTransaction();
		void Commit();
		void Rollback();
		ISession GetSession();
		ISession CreateNewSession();
		void ClearSession();
		IMobileDataRepository MobileDatRepository { get; }
	}
}