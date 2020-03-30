using Acme.Data.DataModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace Acme.Web.Api.Models
{
    public class UpdateDescription
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public Guid Id { get; set; }

        public void UpdateDataModel(ProductDataModel dataModel)
        {
            dataModel.Description = Description;
        }
    }
}