//namespace BillsManagement.Repository.Repositories
//{
//    using AutoMapper;
//    using BillsManagement.DAL.Models;
//    using BillsManagement.Repository.RepositoryContracts;
//    using System;
//    using System.Collections.Generic;
//    using System.Linq;

//    public class ChargesRepository : BaseRepository<Charge>, IChargesRepository
//    {
//        public ChargesRepository(BillsManager_DevContext dbContext, IMapper mapper)
//            : base(dbContext, mapper)
//        {

//        }

//        public List<DomainModel.Charge> GetCharges()
//        {
//            List<Charge> charges = this._dbContext.Charges.ToList();

//            List<DomainModel.Charge> mappedCharges = new List<DomainModel.Charge>();
//            foreach (var charge in charges)
//            {
//                var mappedCharge = this._mapper.Map<Charge, DomainModel.Charge>(charge);
//                mappedCharges.Add(mappedCharge);
//            }

//            return mappedCharges;

//            throw new NotImplementedException();
//        }
//    }
//}
