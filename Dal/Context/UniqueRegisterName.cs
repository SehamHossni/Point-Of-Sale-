using Dal.Context;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Context
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class UniqueRegisterName:ValidationAttribute
    {
        EcommerceContext context;
        public UniqueRegisterName(EcommerceContext context)
        {
            this.context = context;
        }
        public string Message { get; set; }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string Name = value.ToString();
            ApplicationUser AppUser = context.Users.FirstOrDefault(U => U.UserName == Name);
            if (AppUser == null) 
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("User Name Already Exists");
        }
    }
}
