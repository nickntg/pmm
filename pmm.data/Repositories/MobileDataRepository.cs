using System;
using System.Threading.Tasks;
using NHibernate;
using pmm.data.Entities;
using pmm.data.Repositories.Interfaces;

namespace pmm.data.Repositories
{
    public class MobileDataRepository(ISession session) : Repository(session), IMobileDataRepository
    {
        public async Task<MobileData> SaveAsync(MobileData mobileData)
        {
            mobileData.CreatedAt = DateTime.UtcNow;

            await Session.SaveAsync(mobileData);
            await Session.FlushAsync();

            return mobileData;
        }
    }
}
