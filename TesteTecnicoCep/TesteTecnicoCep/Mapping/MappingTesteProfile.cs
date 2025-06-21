using AutoMapper;

namespace TesteTecnicoCep.Mapping
{
    public class MappingTesteProfile : Profile
    {
        public MappingTesteProfile()
        {
            CreateMap<DTOs.ContatoDTO, Models.Contato>()
                .ReverseMap();
            CreateMap<DTOs.ClienteDTO, Models.Cliente>().ReverseMap();
            CreateMap<DTOs.EnderecoDTO, Models.Endereco>()
                .ReverseMap();
        }
    }
    
}
