
namespace F.Rankings.Converters
{
    public interface IModelDtoConverter<TModel, TDto>
    {
        TModel ToModel(TDto dto);
        TDto ToDto(TModel model);
    }
}