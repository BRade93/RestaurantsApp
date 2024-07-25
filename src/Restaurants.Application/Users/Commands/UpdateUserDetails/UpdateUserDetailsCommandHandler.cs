﻿using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Restaurants.Domain;

namespace Restaurants.Application.Users.Commands.UpdateUserDetails;

public class UpdateUserDetailsCommandHandler(ILogger<UpdateRestaurantCommandHandler> logger, IUserContext userContext, IUserStore<User> userStore) : IRequestHandler<UpdateUserDetailsCommand>
{
    public async Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser();
        logger.LogInformation("Updating user: {UserId}, with {@Request}", user!.Id, request);

        var dbUser = await userStore.FindByIdAsync(user!.Id, cancellationToken) ?? throw new NotFoundException(nameof(User), user!.Id);

        dbUser.Nationality = request.Nationality;
        dbUser.DateOfBirth = request.DateOfBirth;

        await userStore.UpdateAsync(dbUser, cancellationToken);
    }
}
