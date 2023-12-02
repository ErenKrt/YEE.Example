using FastEndpoints;
using FluentValidation;
using YEE.Identity.Application.Models.Services.Auth;

namespace YEE.Identity.API.Endpoints.Auth.Register
{
    public class Validator : Validator<RegisterRequest>
    {
        public Validator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("E-mail adresiniz geçerli değil !");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Şifreniz boş olamaz !");

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("Adınız boş olamaz !");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Adınız boş olamaz !");
        }
    }
}
