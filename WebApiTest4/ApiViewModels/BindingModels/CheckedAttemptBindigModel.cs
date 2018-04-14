using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApiTest4.ApiViewModels.BindingModels
{
    public class CheckedAttemptBindigModel
    {
        [Required]
        public int attempt_id { get; set; }
        [Required]
        public int points { get; set; }
    }
}