using ApplicationLayer.Interfaces;
using ApplicationLayer.Interfaces5;
using DomainLayer.Common;
using MediatR;

public class LoginQueryHandler : IRequestHandler<LoginQuery, OperationResult<string>>
{
    private readonly IAuthRepository _authRepository;
    private readonly IJwtGenerator _jwtGenerator;

    public LoginQueryHandler(IAuthRepository authRepository, IJwtGenerator jwtGenerator)
    {
        _authRepository = authRepository;
        _jwtGenerator = jwtGenerator;
    }

    public async Task<OperationResult<string>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        try
        {
            // Look up the user by username
            var user = await _authRepository.GetUserByUsernameAsync(request.UserName);

            // Validate password
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return OperationResult<string>.Failure("Invalid credentials.");

            // Generate a token and return it
            var token = _jwtGenerator.JWTTokenGenerate(user);

            // Return the token wrapped in a success result
            return OperationResult<string>.Success(token);
        }
        catch (Exception ex)
        {
            // Handle unexpected errors
            return OperationResult<string>.Failure($"Error during login: {ex.Message}");
        }
    }
}