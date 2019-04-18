﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AMS.Core.Interfaces;
using AMS.Core.Models;
using AMS.Services.Interfaces;

namespace AMS.Services.Services
{
    public class PaymentService: BaseService, IPaymentService
    {
        public PaymentService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<Payment> GetPaymentAsync(int id)
        {
            return await _uow.Payments.GetAsync(id);
        }

        public async Task<ICollection<Payment>> GetAllPaymentsAsync()
        {
            return await _uow.Payments.GetAllAsync();
        }

        public async Task AddPaymentAsync(Payment payment)
        {
            await _uow.Payments.AddAsync(payment);
        }

        public async Task UpdatePaymentAsync(Payment payment)
        {
            await _uow.Payments.UpdateAsync(payment);
        }

        public async Task RemovePaymentAsync(Payment payment)
        {
            await _uow.Payments.RemoveAsync(payment);
        }
    }
}