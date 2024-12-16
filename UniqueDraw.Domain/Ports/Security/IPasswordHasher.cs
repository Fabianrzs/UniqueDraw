﻿namespace UniqueDraw.Domain.Ports.Security;
public interface IPasswordHasher
{
    string HashPassword(string password);
    bool VerifyPassword(string hashedPassword, string providedPassword);
}
