
namespace F.Rankings.Converters
{
    public class PriceConverter : IModelDtoConverter<Models.Price, DTO.Price>
    {
        public DTO.Price ToDto(Models.Price model)
        {
            return new DTO.Price
            {
                HuurAbbreviation = model.HuurAbbreviation,
                GeenExtraKosten = model.GeenExtraKosten,
                Huurprijs = model.Huurprijs,
                HuurprijsOpAanvraag = model.HuurprijsOpAanvraag,
                HuurprijsTot = model.HuurprijsTot,
                KoopAbbreviation = model.KoopAbbreviation,
                Koopprijs = model.Koopprijs,
                KoopprijsOpAanvraag = model.KoopprijsOpAanvraag,
                KoopprijsTot = model.KoopprijsTot,
                OriginelePrijs = model.OriginelePrijs,
                VeilingText = model.VeilingText,
            };
        }

        public Models.Price ToModel(DTO.Price dto)
        {
            return new Models.Price
            {
                HuurAbbreviation = dto.HuurAbbreviation,
                GeenExtraKosten = dto.GeenExtraKosten,
                Huurprijs = dto.Huurprijs,
                HuurprijsOpAanvraag = dto.HuurprijsOpAanvraag,
                HuurprijsTot = dto.HuurprijsTot,
                KoopAbbreviation = dto.KoopAbbreviation,
                Koopprijs = dto.Koopprijs,
                KoopprijsOpAanvraag = dto.KoopprijsOpAanvraag,
                KoopprijsTot = dto.KoopprijsTot,
                OriginelePrijs = dto.OriginelePrijs,
                VeilingText = dto.VeilingText,
            };
        }
    }
}
