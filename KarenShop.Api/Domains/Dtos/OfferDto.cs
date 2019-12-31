using System.Collections.Generic;

namespace KarenShop.Api.Domains.Dtos
{
    public class OfferDto
    {
        public string OfferImage { get; set; }
        public string Name { get; set; }
        public long ProductId { get; set; }
        public PricesDto Prices { get; set; }
    }

    public class OffersDto : BaseResponseDto
    {
        public List<OfferDto> Offers { get; set; }
    }
}
