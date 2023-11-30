using AutoMapper;
using Core.Domain;
using Core.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Services.Helpers
{
    public class MappingUserDtTOProfile : Profile
    {

        public MappingUserDtTOProfile()
        {

            CreateMap<UserDTO, ApplicationUser>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src =>  $"{src.FirstName}, {src.LastName}"))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => HashPassord(src.Password)))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => UploadImage(src.Image)))
                .ReverseMap();
        }

        //private string SaveImage(IFormFile? image)
        //{
        //    if (image == null)
        //        return null;

        //    string uploadsFolder = Path.Combine(_webHostEnvironment, "Images");
        //    throw new NotImplementedException();
        //}


        public byte[] UploadImage(IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    imageFile.CopyTo(ms);
                    return ms.ToArray();
                }
            }
            return null;
        }

        private string HashPassord(string password)
        {
            return new PasswordHasher<ApplicationUser>().HashPassword(null, password);
        }
    }
}
