using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AntBoxFrontEnd.Services.Tasks;

namespace AntBoxFrontEnd.Models
{

    public class AntBoxTasksViewModel
    {

        public AntBoxTasksViewModel()
        {
            
        }

        public virtual PaginationCustomerTask Tareas { get; set; }

        

    }




}