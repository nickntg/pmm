using System.Threading.Tasks;
using AutoMapper;
using pmm.core.Models;
using pmm.core.Services.Interfaces;
using pmm.data.Context.Interfaces;
using pmm.data.Entities;

namespace pmm.core.Services
{
    public class MobileDataService(IMapper mapper, IDbContext dbContext) : IMobileDataService
    {
        public async Task SaveMobileDataAsync(MobileDataDto mobileData)
        {
            await dbContext.MobileDatRepository.SaveAsync(mapper.Map<MobileData>(mobileData));
        }
    }
}
