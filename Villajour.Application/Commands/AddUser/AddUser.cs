using MediatR;
using Villajour.Application.Commands.Interface;
using Villajour.Domain.Common;

namespace Villajour.Application.Commands.AddUser;

public record class AddUserCommand : IRequest<UserEntity>
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public int Status { get; set; }
    public string? City { get; set; }
    public int PostalCode { get; set; }
    public string? Country { get; set; }
    public string? Address { get; set; }
    public int Phone { get; set; }
}

public class AddUserCommandHandler : IRequestHandler<AddUserCommand, UserEntity>
{
    private readonly IVilleajourDbContext _context;

    public AddUserCommandHandler(IVilleajourDbContext context)
    {
        this._context = context;
    }

    public async Task<UserEntity> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var entity = new UserEntity
        {
            Name = request.Name,
            Email = request.Email,
            Password = "",
            Status = request.Status,
            City = request.City,
            PostalCode = request.PostalCode,
            Country = request.Country,
            Address = request.Address,
            Phone = request.Phone,


        };

        //_context.Users.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;

    }

}
