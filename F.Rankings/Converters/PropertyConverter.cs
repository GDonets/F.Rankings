using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F.Rankings.Converters
{
    public class PropertyConverter : IModelDtoConverter<Models.Property, DTO.Property>
    {
        private readonly IModelDtoConverter<Models.Price, DTO.Price> _priceConverter;

        public PropertyConverter(
            IModelDtoConverter<Models.Price, DTO.Price> priceConverter)
        {
            _priceConverter = priceConverter;
        }

        public DTO.Property ToDto(Models.Property model)
        {
            return new DTO.Property
            {
                AangebodenSindsTekst = model.AangebodenSindsTekst,
                AanmeldDatum = model.AanmeldDatum,
                AantalKamers = model.AantalKamers,
                AantalKavels = model.AantalKavels,
                Aanvaarding = model.Aanvaarding,
                Adres = model.Adres,
                Foto = model.Foto,
                FotoLarge = model.FotoLarge,
                FotoLargest = model.FotoLargest,
                FotoMedium = model.FotoMedium,
                FotoSecure = model.FotoSecure,
                GlobalId = model.GlobalId,
                Id = model.Id,
                Prijs = _priceConverter.ToDto(model.Prijs),
                PublicatieDatum = model.PublicatieDatum,
                Land = model.Land,
                WGS84_X = model.WGS84_X,
                WGS84_Y = model.WGS84_Y,
                Woonplaats = model.Woonplaats,
                URL = model.URL,
            };
        }

        public Models.Property ToModel(DTO.Property dto)
        {
            return new Models.Property
            {
                AangebodenSindsTekst = dto.AangebodenSindsTekst,
                AanmeldDatum = dto.AanmeldDatum,
                AantalKamers = dto.AantalKamers,
                AantalKavels = dto.AantalKavels,
                Aanvaarding = dto.Aanvaarding,
                Adres = dto.Adres,
                Foto = dto.Foto,
                FotoLarge = dto.FotoLarge,
                FotoLargest = dto.FotoLargest,
                FotoMedium = dto.FotoMedium,
                FotoSecure = dto.FotoSecure,
                GlobalId = dto.GlobalId,
                Id = dto.Id,
                Prijs = _priceConverter.ToModel(dto.Prijs),
                PublicatieDatum = dto.PublicatieDatum,
                Land = dto.Land,
                WGS84_X = dto.WGS84_X,
                WGS84_Y = dto.WGS84_Y,
                Woonplaats = dto.Woonplaats,
                URL = dto.URL,
            };
        }
    }
}
