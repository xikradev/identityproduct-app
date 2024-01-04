using System.ComponentModel.DataAnnotations;

namespace identityproduct_app.Domain.Dto.Create
{
    public class ProductCreateDto
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Description { get; set; }
        [Range(0.01, 999.99, ErrorMessage = "O valor deve estar entre 0.01 e 999.99")]
        public decimal Price { get; set; }
    }
}
