using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AntBoxFrontEnd.Models
{

    public class AntBoxAddressViewModel
    {

        public AntBoxAddressViewModel()
        {
            Colonias = new List<SelectListItem> { new SelectListItem { Value = "", Text = "CAPTURA EL CÓDIGO POSTAL" } };
            Delegaciones = new List<SelectListItem> { new SelectListItem { Value = "", Text = "CAPTURA EL CÓDIGO POSTAL" } };
        }
        [HiddenInput(DisplayValue = false)]
        public string Customer_id { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "Alias")]
        public string Alias { get; set; }

        [Required]
        [StringLength(5, ErrorMessage = "El {0} deber ser de {2} digitos.", MinimumLength = 5)]
        [Display(Name = "Código Postal")]
        public string Zipcode { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El {0} deber ser de al menos {2} digitos.", MinimumLength = 2)]
        [Display(Name = "Calle")]
        public string Street { get; set; }

        [Display(Name = "Núm. exterior")]
        public string External_number { get; set; }

        [Display(Name = "Núm Interior")]
        public string Internal_number { get; set; }

        [Required]
        [Display(Name = "Colonia")]
        public string Neighborhood { get; set; }

        [Required]
        [Display(Name = "Estado")]
        public string State { get; set; }

        [Required]
        [Display(Name = "Delegacion")]
        public string Delegation { get; set; }

        [Display(Name = "Ciudad")]
        public string City { get; set; }

        [Required]
        [Display(Name = "País")]
        public string Country { get; set; }

        public string Rfc_id { get; set; }

        [Required]
        [Display(Name = "Notas")]
        public string References { get; set; }
        

        public virtual List<SelectListItem> Colonias { get; set; }

        public virtual List<SelectListItem> Delegaciones { get; set; }

    }

    public class AgendTaskModel
    {
        [Required]
        [StringLength(5, ErrorMessage = "El {0} deber ser de {2} digitos.", MinimumLength = 5)]
        [Display(Name = "Código Postal")]
        public string Zipcode1 { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El {0} deber ser de al menos {2} digitos.", MinimumLength = 2)]
        [Display(Name = "Calle")]
        public string Street1 { get; set; }

        [Display(Name = "Núm. exterior")]
        public string External_number1 { get; set; }

        [Display(Name = "Núm Interior")]
        public string Internal_number1 { get; set; }

        [Required]
        [Display(Name = "Colonia")]
        public string Neighborhood1 { get; set; }

        [Required]
        [Display(Name = "Estado")]
        public string State1 { get; set; }

        [Required]
        [Display(Name = "Delegacion")]
        public string Delegation1 { get; set; }

        [Display(Name = "Ciudad")]
        public string City1 { get; set; }

        [Required]
        [Display(Name = "País")]
        public string Country1 { get; set; }

        public string Rfc_id1 { get; set; }

        [Required]
        [Display(Name = "Notas")]
        public string References1 { get; set; }

        [StringLength(5, ErrorMessage = "El {0} deber ser de {2} digitos.", MinimumLength = 5)]
        [Display(Name = "Código Postal")]
        public string Zipcode2 { get; set; }

        [StringLength(100, ErrorMessage = "El {0} deber ser de al menos {2} digitos.", MinimumLength = 2)]
        [Display(Name = "Calle")]
        public string Street2 { get; set; }

        [Display(Name = "Núm. exterior")]
        public string External_number2 { get; set; }

        [Display(Name = "Núm Interior")]
        public string Internal_number2 { get; set; }
    
        [Display(Name = "Colonia")]
        public string Neighborhood2 { get; set; }

        [Display(Name = "Estado")]
        public string State2 { get; set; }

        [Display(Name = "Delegacion")]
        public string Delegation2 { get; set; }

        [Display(Name = "Ciudad")]
        public string City2 { get; set; }

        [Display(Name = "País")]
        public string Country2 { get; set; }

        public string Rfc_id2 { get; set; }

        [Display(Name = "Notas")]
        public string References2 { get; set; }

        [Display(Name = "Horario")]
        public string Horario1 { get; set; }

        [Display(Name = "Horario")]
        public string Horario2 { get; set; }

        [Display(Name = "Fecha Recoleccion")]
        public string Fecha_recoleccion { get; set; }

        [Display(Name = "Fecha Entrega")]
        public string Fecha_entrega { get; set; }
        

    }




}