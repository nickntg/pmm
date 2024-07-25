using System.Threading.Tasks;
using pmm.core.Models;

namespace pmm.core.Services.Interfaces
{
    public interface IMobileDataService
    {
        Task SaveMobileDataAsync(MobileDataDto mobileData);
    }
}
