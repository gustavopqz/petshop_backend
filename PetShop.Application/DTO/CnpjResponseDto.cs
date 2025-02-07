using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PetShop.Application.DTO
{
    public class CnpjResponseDto
    {

        [JsonPropertyName("cnpj")]
        public string RegistrationNumber { get; set; }

        [JsonPropertyName("razao_social")]
        public string CompanyName { get; set; }

        [JsonPropertyName("nome_fantasia")]
        public string TradeName { get; set; }

        [JsonPropertyName("descricao_situacao_cadastral")]
        public string DescriptionRegistrationStatus { get; set; }

        [JsonPropertyName("cep")]
        public int PostalCode { get; set; }

        [JsonPropertyName("uf")]
        public string State { get; set; }

        [JsonPropertyName("logradouro")]
        public string Country { get; set; }

        [JsonPropertyName("municipio")]
        public string City { get; set; }

        [JsonPropertyName("ddd_telefone_1")]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("capital_social")]
        public int ShareCapital { get; set; }

       
        [JsonPropertyName("descricao_porte")]
        public string DescriptionSize { get; set; }

    }
}
