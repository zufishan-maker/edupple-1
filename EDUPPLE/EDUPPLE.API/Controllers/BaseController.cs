using EDUPPLE.INFRASTRUCTURE.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EDUPPLE.API.Controllers
{
    [ApiController]  
    public abstract class BaseController : ControllerBase
    {
        protected BaseController(IMediator mediator, ICurrentUser user)
        {
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            CurrentUser = user ?? throw new ArgumentNullException(nameof(user));
        }      
      
        public IMediator Mediator { get; }
        public ICurrentUser CurrentUser { get; }


    }
}
