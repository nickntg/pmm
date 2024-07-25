using System.Threading.Tasks;
using pmm.data.Entities;

namespace pmm.data.Repositories.Interfaces
{
    public interface IMobileDataRepository
    {
        Task<MobileData> SaveAsync(MobileData mobileData);
    }
}
