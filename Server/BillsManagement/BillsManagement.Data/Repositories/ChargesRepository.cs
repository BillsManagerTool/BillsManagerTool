namespace BillsManagement.Data.Repositories
{
    using AutoMapper;
    using BillsManagement.Data.Contracts;
    using BillsManagement.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ChargesRepository : BaseRepository<DomainModel.Charge>, IChargesRepository
    {
        public ChargesRepository(BillsManager_DevContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {

        }

        public List<DomainModel.Charge> GetCharges()
        {
            List<Charge> charges = this._dbContext.Charges.ToList();

            List<DomainModel.Charge> mappedCharges = new List<DomainModel.Charge>();
            foreach (var charge in charges)
            {
                var mappedCharge = this._mapper.Map<Charge, DomainModel.Charge>(charge);
                mappedCharges.Add(mappedCharge);
            }

            return mappedCharges;

            throw new NotImplementedException();
        }
    }
}
