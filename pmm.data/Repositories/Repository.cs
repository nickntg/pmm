using NHibernate;

namespace pmm.data.Repositories
{
	public class Repository(ISession session)
    {
		protected readonly ISession Session = session;
    }
}