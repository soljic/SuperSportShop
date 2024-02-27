using System.Collections.Generic;
using System.Web.Profile;
using AutoMapper;
using Domain.Models;
using FluentNHibernate.Automapping;
using FluentNHibernate.Conventions;
using Web.Models;

namespace Web.Utilities.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ApplicationUser, UsersViewModel>();
            // Dodajte dodatne mape ovdje po potrebi
        }
    }
    
}