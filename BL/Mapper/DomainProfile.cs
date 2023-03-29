using AutoMapper;
using BLL.ViewModel;
using Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapper
{
   public class DomainProfile:Profile
    {
        public DomainProfile() {
            CreateMap<Customer, CustomerVM>().ReverseMap();
            CreateMap<Order, OrderVM>().ReverseMap();
            CreateMap<Product, ProductVM>().ReverseMap();
            CreateMap<Expense, ExpenseVM>().ReverseMap();

        }
    }
}
