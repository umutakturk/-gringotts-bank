using AutoMapper;
using GringottsBank.Application.Features.Account.DTOs;
using GringottsBank.Application.Features.Customer.DTOs;
using GringottsBank.Domain.Entities;

namespace GringottsBank.Application.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Customer, CustomerResponse>();
            CreateMap<Account, AccountResponse>();
            CreateMap<Transaction, TransactionResponse>();
        }
    }
}
