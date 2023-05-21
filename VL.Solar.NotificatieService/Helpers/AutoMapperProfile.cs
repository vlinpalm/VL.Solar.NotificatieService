using AutoMapper;
using VL.Solar.NotificatieService.Models;
using VL.Solar.NotificatieService.Models.Data;

namespace VL.Solar.NotificatieService.Helpers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CreateMedewerkerNotificatie, MedewerkerNotificatie>();
    }
}