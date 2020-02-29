using Dto.Contact;
using Dto.Demand;
using System;

namespace Bll.Contact
{
    public interface IContactManager
    {
        PagedList<ContactListDto> Search(ContactQueryDto queryDto);
        ContactByResaleHouseDto SearchByResaleHouseId(ContactByResaleHouseQueryDto queryDto);
    }
}