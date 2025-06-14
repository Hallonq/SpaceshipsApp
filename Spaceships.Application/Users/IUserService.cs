﻿using Spaceships.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spaceships.Application.Users;

public interface IUserService
{
    Task<UserResultDto> CreateUserAsync(UserProfileDto user, string password);
    Task<UserProfileDto> GetUserByEmailAsync(string email);
    Task<UserResultDto> SignInAsync(string email, string password);
    Task SignOutAsync();
}
