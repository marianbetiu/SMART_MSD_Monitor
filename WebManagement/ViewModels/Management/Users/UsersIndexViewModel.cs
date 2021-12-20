using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebManagement.ViewModels.Management.Users
{
    public class UsersIndexViewModel : Data.ViewModels.Shared._BaseEntityViewModel
    {
        public List<UserIndexViewModel> Items { get; set; }

        public UsersIndexViewModel()
        {
        }
    }
}