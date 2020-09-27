using Entities.Models;
using Entities.DataTransferObjects;
using AutoMapper;

namespace AkuSuka
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Owner, OwnerDto>();

            CreateMap<Account, AccountDto>();

            CreateMap<OwnerForCreationDto, Owner>();

            CreateMap<OwnerForUpdateDto, Owner>();

            CreateMap<AccountForCreationDto, Account>();
        }
    }
}
