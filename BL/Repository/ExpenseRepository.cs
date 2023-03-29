using BLL.inteface;
using BLL.ViewModel;
using Dal.Context;
using Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repository
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly EcommerceContext context;

        public ExpenseRepository(EcommerceContext context)
        {
            this.context = context;
        }

    }
}
