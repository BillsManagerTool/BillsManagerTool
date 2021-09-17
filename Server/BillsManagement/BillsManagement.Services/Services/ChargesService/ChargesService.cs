﻿namespace BillsManagement.Services.Services.ChargesService
{
    using BillsManagement.Data.Contracts;
    using BillsManagement.Services.ServiceContracts;

    public partial class ChargesService : IChargesService
    {
        private readonly IChargesRepository _chargesRepository;

        public ChargesService(IChargesRepository chargesRepository)
        {
            this._chargesRepository = chargesRepository;
        }
    }
}
