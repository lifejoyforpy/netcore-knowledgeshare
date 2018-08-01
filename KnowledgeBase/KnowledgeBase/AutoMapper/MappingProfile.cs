using AutoMapper;
using BoeSj.KnowledgeBase.Application.DTO.Input;
using BoeSj.KnowledgeBase.Application.DTO.Output;
using BoeSj.KnowledgeBase.Domain.Model;

namespace BoeSj.KnowledgeBase.Web.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //ent to dto

            CreateMap<QuestionsPublish, QAInfoOutput>().ForMember(
                dest => dest.Content, option => option.Ignore()).ForMember(
                dest => dest.Files, option => option.Ignore());

            CreateMap<Category, CategoryDto>();

            CreateMap<Category, CategoryTree>().ForMember(d=>d.Id, o=>o.MapFrom(m=>m.Guid))
                .ForMember(d=>d.Text, o=>o.MapFrom(m=>m.CategoryName))
                .ForMember(d=>d.State, o=>o.MapFrom(m=>new State { CategoryDescription=m.CategoryDescription, Departments=m.Departments, Id=m.Id }));
            //dto to ent
        }
    }
}