using API.DTOs;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // Creating a map between our Products and ProductToReturnDTO class.
            CreateMap<Product, ProductToReturnDTO>()
            .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
            .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
            .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductURLResolver>());


            // ForMember(destination member, MapFrom(source from where want to get the property))

            /* 
                We did this becasue we had the type difference in product class and productToReturnDto
                becasue of which our output wasnt as we needed.
             */


            /* Before
               {
                   "id": 3,
                   "name": "Core Red Boots",
                   "description": "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                   "price": 189.99,
                   "pictureUrl": "images/products/boot-core2.png",
                   "productType": "Core.Entities.ProductType",   <--
                   "productBrand": "Core.Entities.ProductBrand"  <---
               }
             */


            /* 

            {
              "id": 3,
              "name": "Core Red Boots",
              "description": "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
              "price": 189.99,
              "pictureUrl": "images/products/boot-core2.png",
              "productType": "Boots",   <--
              "productBrand": "NetCore"  <--
              }
             */

        }
    }
}