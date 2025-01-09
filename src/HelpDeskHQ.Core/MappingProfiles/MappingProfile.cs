using AutoMapper;
using HelpDeskHQ.Core.Models;

namespace HelpDeskHQ.Core.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, AccountVm>();
        }
    }
}
