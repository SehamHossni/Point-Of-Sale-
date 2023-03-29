using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModel
{
    public class RoleViewModel
    {
        [Required]
        [MinLength(3)]
        public string RoleName { get; set; }
    }
}
