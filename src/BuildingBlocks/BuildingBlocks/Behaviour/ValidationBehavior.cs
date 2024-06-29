using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Behaviour
{
    public class ValidationBehavior<TRequest, TResponse>(IEnumerable <IValidator<TRequest>> validators) :
        IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(validators.Select(x => x.ValidateAsync(context, cancellationToken)));
            var failures = validationResults.Where(x=>x.Errors.Any()).SelectMany(r=>r.Errors).ToList();
            if(failures.Any())
            {
                throw new ValidationException(failures.Select(x => x.ErrorMessage).FirstOrDefault());
            }
           return await next();
        }
    }
}
