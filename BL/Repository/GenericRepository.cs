using BLL.inteface;
using Dal.Context;
using Dal.Entities;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repository
{
  public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity

    {
        public EcommerceContext Context { get; }
        public GenericRepository(EcommerceContext context) {
            Context = context;
        }

      

        public async Task<int> Create(T entity)
        {
        await Context.Set<T>().AddAsync(entity);
         return  await Context.SaveChangesAsync();
        }

        public async Task<int> Delete(T item)
        {
             Context.Set<T>().Remove(item);
            return await Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            if(typeof(T) ==typeof(Order))
            {
               
                return (IEnumerable<T>)await Context.Set<Order>().Include(d=>d.Customer).ToListAsync();
            }
           return await Context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int? id)
        {
            if (typeof(T)==typeof(Order)) {
              Order result = Context.Orders.Where(o => o.Id == id).Include(o => o.Customer).FirstOrDefault();
                return (T)Convert.ChangeType(result, typeof(T));
            }

            if (typeof(T) == typeof(Product))
            {
                Product result = Context.Products.Where(o => o.Id == id).FirstOrDefault();
                return (T)Convert.ChangeType(result, typeof(T));
            }

            return  await Context.Set<T>().FindAsync(id);
        }

        public async Task<int> Update(T entity)
        {
            Context.Set<T>().Update(entity);
            return await Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> MakeReport(string peroid)
        {
            if (typeof(T) == typeof(Expense))
            {
                if (peroid == "Month")
                {
                    return (IEnumerable<T>)await Context.Set<Expense>().Where(e => e.Date >= DateTime.Today.AddMonths(-1)).ToListAsync();
                }
                else if (peroid == "Week")
                {
                    return (IEnumerable<T>)await Context.Set<Expense>().Where(e => e.Date >= DateTime.Today.AddDays(-7)).ToListAsync();
                }
                else
                {
                    return (IEnumerable<T>)await Context.Set<Expense>().Where(e => e.Date >= DateTime.Today).ToListAsync();
                }
            }
            else if (typeof(T) == typeof(Order))
            {
                if (peroid == "Month")
                {
                    return (IEnumerable<T>)await Context.Set<Order>().Where(e => e.OrderTime >= DateTime.Today.AddMonths(-1)).Include(e=> e.Customer).ToListAsync();
                }
                else if (peroid == "Week")
                {
                    return (IEnumerable<T>)await Context.Set<Order>().Where(e => e.OrderTime >= DateTime.Today.AddDays(-7)).Include(e => e.Customer).ToListAsync();
                }
                else
                {
                    return (IEnumerable<T>)await Context.Set<Order>().Where(e => e.OrderTime >= DateTime.Today).Include(e => e.Customer).ToListAsync();
                }
            }

            


            else
                return await Context.Set<T>().ToListAsync();
        }




        public async Task<IEnumerable<T>> MakeReportforcust(string custid)
        {
            
            if (typeof(T) == typeof(Order))
            {
                
                    return (IEnumerable<T>)await Context.Set<Order>().Where(e => e.Customer.Name== custid).Include(e => e.Customer).ToListAsync();
               
                
            }




            else
                return await Context.Set<T>().ToListAsync();
        }


        public async Task<IEnumerable<T>> MakeReportforcustnoone()
        {

            if (typeof(T) == typeof(Order))
            {

                return (IEnumerable<T>)await Context.Set<Order>().Include(e => e.Customer).ToListAsync();


            }




            else
                return await Context.Set<T>().ToListAsync();
        }



    }
}
