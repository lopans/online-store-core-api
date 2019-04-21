using Base.DAL;
using Base.Enums;
using Base.Exceptions;
using Common;
using System;

namespace Base.Services
{
    public interface ICheckAccessService
    {
        void ThrowIfAccessDenied(IUnitOfWork uofw, AccessModifier permission, Type entityType);
    }
    public class CheckAccessService : ICheckAccessService
    {
        private readonly IApplicationContext _appContext;
        public CheckAccessService(IApplicationContext appContext)
        {
            _appContext = appContext;
        }
        public void ThrowIfAccessDenied(IUnitOfWork uofw, AccessModifier permission, Type entityType)
        {
            var isClientEntity = typeof(Type).IsAssignableFrom(typeof(IClientEntity));
            var isManageEntity = typeof(Type).IsAssignableFrom(typeof(IManageEntity));
            if(isClientEntity)
                switch (permission)
                {
                    case AccessModifier.Read:
                        break;
                    default:
                        {
                            if(_appContext.IsEditor() || _appContext.IsAdmin())
                                break;
                            throw new AccessDeniedException();
                        }
                }
            else
                if(!_appContext.IsAdmin())
                    throw new AccessDeniedException();
        }
    }
}
