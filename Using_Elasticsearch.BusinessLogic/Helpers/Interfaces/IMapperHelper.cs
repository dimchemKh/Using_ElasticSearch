

namespace Using_Elasticsearch.BusinessLogic.Helpers.Interfaces
{
    public interface IMapperHelper
    {
        MapTo Map<MapFrom, MapTo>(MapFrom source) where MapTo : new();
    }
}
