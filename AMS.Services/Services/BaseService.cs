using System;
using AMS.Core.Interfaces;

namespace AMS.Services.Services
{
    public class BaseService/*: IDisposable*/
    {
        protected readonly IUnitOfWork _uow;

        public BaseService(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }

        //public void Dispose()
        //{
        //    _uow.Dispose();
        //}
    }
}