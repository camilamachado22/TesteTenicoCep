using AutoMapper;
using TesteTecnicoCep.DTOs;
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

            CreateMap<Cliente, ClienteCadastroDTO>()
    .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.nome))
    .ForMember(dest => dest.TipoContato, opt => opt.MapFrom(src =>
        src.Contatos.tipo))
    .ForMember(dest => dest.TextoContato, opt => opt.MapFrom(src =>
        src.Contatos.texto))
    .ForMember(dest => dest.Cep, opt => opt.MapFrom(src => src.Endereco.cep))
    .ForMember(dest => dest.Numero, opt => opt.MapFrom(src => src.Endereco.numero))
    .ForMember(dest => dest.Logradouro, opt => opt.MapFrom(src => src.Endereco!.logradouro))
    .ForMember(dest => dest.Cidade, opt => opt.MapFrom(src => src.Endereco!.cidade))
    .ForMember(dest => dest.Complemento, opt => opt.MapFrom(src => src.Endereco!.complemento))
    .ForMember(dest => dest.DataCadastro, opt => opt.MapFrom(src => src.data_cadastro));

            CreateMap<DTOs.ContatoDTO, Models.Contato>()
                .ReverseMap();
            CreateMap<DTOs.ClienteDTO, Models.Cliente>().ReverseMap();
            CreateMap<DTOs.EnderecoDTO, Models.Endereco>()
                .ReverseMap();
        }
    }
    
}
