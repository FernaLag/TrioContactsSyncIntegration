using AutoMapper;
using Trio.ContactSync.Application.Constants;
using Trio.ContactSync.Domain;

namespace Trio.ContactSync.Application.Profiles
{
    public class MailchimpProfile : Profile
    {
        public MailchimpProfile()
        {
            CreateMap<Contact, MailchimpMember>()
                .ForMember(destination => destination.EmailAddress, options => options.MapFrom(source => source.Email))
                .ForMember(destination => destination.Status, options => options.MapFrom(source => MailchimpConstants.MemberStatus.Subscribed))
                .ForMember(destination => destination.MergeFields, options => options.MapFrom(source => new MergeFields
                {
                    FNAME = source.FirstName,
                    LNAME = source.LastName
                }));
        }
    }
}