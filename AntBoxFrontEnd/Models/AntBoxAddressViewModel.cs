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
        [Display(Name = "* Código Postal")]
        public string Zipcode { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El {0} deber ser de al menos {2} digitos.", MinimumLength = 2)]
        [Display(Name = "* Calle")]
        public string Street { get; set; }

        [Required]
        [Display(Name = "* Núm. exterior")]
        public string External_number { get; set; }

        [Display(Name = "Núm Interior")]
        public string Internal_number { get; set; }

        [Required]
        [Display(Name = "* Colonia")]
        public string Neighborhood { get; set; }

        [Required]
        [Display(Name = "* Estado")]
        public string State { get; set; }

        [Required]
        [Display(Name = "* Delegacion")]
        public string Delegation { get; set; }

        [Required]
        [Display(Name = "* Ciudad")]
        public string City { get; set; }

        [Required]
        [Display(Name = "* País")]
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
        public string Zipcode { get; set; }

        public string Street { get; set; }

        public string External_number { get; set; }

        public string Internal_number { get; set; }

        public string Neighborhood { get; set; }

        public string State { get; set; }

        public string Delegation { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Rfc_id { get; set; }

        public string References { get; set; }

        public string Horario { get; set; }

        public string Fecha_recoleccion { get; set; }

        public string Paso { get; set; }

        public string Esperar { get; set; }

        public string Hora_recoleccion { get; set; }

        public string HoraRecoleccionString { get; set; }

    }




}