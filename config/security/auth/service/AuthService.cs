using Microsoft.AspNetCore.Identity;

public class AuthService : IAuthService
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly IJWTService _jwtService;
    private readonly IStripeService _stripeService;

    public AuthService(
        SignInManager<User> signInManager,
        UserManager<User> userManager,
        IJWTService jwtService,
        IStripeService stripeService
    )
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _jwtService = jwtService;
        _stripeService = stripeService;
    }

    public async Task<Response<TokenResponseDTO>> LoginUser(UserLoginDTO dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);

        if (user == null)
        {
            Console.WriteLine("USER == NULL");
            return new()
            {
                isSuccessful = false,
                Body = new TokenResponseDTO() { },
                Message = "Email or password is incorrect",
            };
        }

        var result = await _signInManager.PasswordSignInAsync(dto.Email, dto.Password, false, true);
        if (!result.Succeeded)
        {
            Console.WriteLine("RESULT IS NOT Successful");
            return new()
            {
                isSuccessful = false,
                Body = new TokenResponseDTO() { },
                Message = "Email or password is incorrect",
            };
        }

        var token = _jwtService.generateJWT(user);

        return new Response<TokenResponseDTO>()
        {
            isSuccessful = result.Succeeded,
            Message = result.Succeeded ? "Login Successfull!" : "Email or password is incorrect",
            Body = new TokenResponseDTO
            {
                AccessToken = result.Succeeded ? _jwtService.generateJWT(user) : String.Empty
            }
        };
    }

    public async Task<Response<TokenResponseDTO>> RegisterUser(UserRegistrationDTO dto)
    {
        User user = new()
        {
            UserName = dto.Email,
            Email = dto.Email,
            CreatedAt = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now),
            UpdatedAt = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now)
        };

        var result = await _userManager.CreateAsync(user, dto.Password);
        StripeUserAccountDetails stripeUserAccountDetails = null;

        if (result != null)
        {
            stripeUserAccountDetails = await _stripeService.createStripeAccount(user);
        }

        return new Response<TokenResponseDTO>()
        {
            isSuccessful = result.Succeeded,
            Body = new TokenResponseDTO
            {
                AccessToken = result.Succeeded ? _jwtService.generateJWT(user) : String.Empty,
                StripeOnboardingUrl = stripeUserAccountDetails.OnboardingAccountLinkUrl != null ? stripeUserAccountDetails.OnboardingAccountLinkUrl : null
            },
            Message = result.Succeeded ? "User Registration Successfull!" : "User Registration Failed!" + string.Join("; ", result.Errors.Select(e => e.Description))
        };
    }
}