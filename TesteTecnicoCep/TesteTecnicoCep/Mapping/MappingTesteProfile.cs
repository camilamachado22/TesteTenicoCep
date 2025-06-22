using AutoMapper;
using TesteTecnicoCep.Models;

namespace TesteTecnicoCep.Mapping
{
    public class MappingTesteProfile : Profile
    {
        public MappingTesteProfile()
        {
            CreateMap<DTOs.ClienteCadastroDTO, Models.Cliente>()
            .ForMember(dest => dest.nome, opt => opt.MapFrom(src => src.Nome))
            .ForMember(dest => dest.Contatos, opt => opt.MapFrom(src => new Contato
            {
                 tipo = src.TipoContato, texto = src.TextoContato 
            }))
            .ForMember(dest => dest.Endereco, opt => opt.MapFrom(src => new Endereco
            {
                cep = src.Cep,
                numero = src.Numero,
                
            }));
            CreateMap<DTOs.ContatoDTO, Models.Contato>()
                .ReverseMap();
            CreateMap<DTOs.ClienteDTO, Models.Cliente>().ReverseMap();
            CreateMap<DTOs.EnderecoDTO, Models.Endereco>()
                .ReverseMap();
        }
    }
    
}
