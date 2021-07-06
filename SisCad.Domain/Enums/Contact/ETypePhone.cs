using System.ComponentModel.DataAnnotations;

namespace SisCad.Domain.Enums.Contact
{
    public enum ETypePhone
    {
        [Display(Name = "Celular")]
        Cellular = 1,

        [Display(Name = "Trabalho")]
        Work = 2,

        [Display(Name = "E-mail")]
        Email = 3,

        [Display(Name = "Casa")]
        Home = 4
    }
}