using Asys.Application.Features.Videos.Queries.GetVideosList;
using Asys.Domain;
using AutoMapper;

namespace Asys.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // CreateMap<Domain.Entities.Video, VideoVm>().ReverseMap();
            CreateMap<Video, VideosVm>();
        }
    }
}