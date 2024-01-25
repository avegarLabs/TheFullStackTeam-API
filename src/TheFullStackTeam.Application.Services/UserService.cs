using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using System.Security.Cryptography;
using System.Text.Encodings.Web;
using TheFullStackTeam.Application.Model.EntityModel;
using TheFullStackTeam.Application.Services.Abstract;
using TheFullStackTeam.Application.Services.Contracts;

namespace TheFullStackTeam.Application.Services
{
    /// <summary>
    /// The user service.
    /// </summary>
    // TODO: add propper loggin and exceptyion handling...
    public class UserService : IUserService
    {
        private readonly GraphServiceClient _graphServiceClient;
        private readonly string _aadB2CIssuerDomain;
      
        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="aadB2cSettings">The aad b2c settings.</param>
        public UserService(IConfiguration configuration)
        {
            string[] scopes = configuration.GetValue<string>("GraphApi:Scopes")?.Split(' ');
            var tenantId = configuration.GetValue<string>("GraphApi:TenantId");

            // Values from app registration
            var clientId = configuration.GetValue<string>("GraphApi:ClientId");
            var clientSecret = configuration.GetValue<string>("GraphApi:ClientSecret");
            _aadB2CIssuerDomain = configuration.GetValue<string>("AzureAdB2C:Domain");

            var options = new TokenCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
            };

            // https://docs.microsoft.com/dotnet/api/azure.identity.clientsecretcredential
            var clientSecretCredential = new ClientSecretCredential(
                tenantId, clientId, clientSecret, options);

            _graphServiceClient = new GraphServiceClient(clientSecretCredential, scopes);
        }

        /// <summary>
        /// Creates a user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>A Task.</returns>
        public async Task<User> CreateUser(UserModel user)
        {
            try
            {
                // Create user
                var result = await this._graphServiceClient.Users
                .Request()
                .AddAsync(new User
                {
                    GivenName = user.FirstName,
                    Surname = user.LastName,
                    DisplayName = user.FirstName + " " + user.LastName,
                    Identities = new List<ObjectIdentity>
                    {
                        new ObjectIdentity()
                        {
                            SignInType = "emailAddress",
                            Issuer = _aadB2CIssuerDomain,
                            IssuerAssignedId = user.ContactEmail                       }
                    },
                    PasswordProfile = new PasswordProfile()
                    {
                        Password = GetEncodedRandomString(),
                        ForceChangePasswordNextSignIn = false
                    },
                    PasswordPolicies = "DisablePasswordExpiration"
                });
                return result;
            }
            catch (ServiceException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {

                }
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        /// <summary>
        /// Gets the user by email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>A Task.</returns>
        public async Task<IGraphServiceUsersCollectionPage> GetUserByEmail(string email)
        {
            try
            {
                // Get user by sign-in name
                var result = await this._graphServiceClient.Users
                    .Request()
                    .Filter($"identities/any(c:c/issuerAssignedId eq '{email}' and c/issuer eq '{_aadB2CIssuerDomain}')")
                    .Select(e => new
                    {
                        e.DisplayName,
                        e.Id,
                        e.Identities
                    })
                    .GetAsync();

                if (result != null)
                {
                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> DeleteUser(string email)
        {
            var user = await this.GetUserByEmail(email);
            var userId = user.CurrentPage[0].Id;

            try
            {
                // Delete user by object ID
                await _graphServiceClient.Users[userId]
                   .Request()
                   .DeleteAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Gets an encoded random string.
        /// </summary>
        /// <returns>A string.</returns>
        private static string GetEncodedRandomString()
        {
            var base64 = Convert.ToBase64String(GenerateRandomBytes(20));
            return HtmlEncoder.Default.Encode(base64);
        }

        /// <summary>
        /// Generates some random bytes.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <returns>An array of byte.</returns>
        private static byte[] GenerateRandomBytes(int length)
        {
            var item = RandomNumberGenerator.Create();
            var byteArray = new byte[length];
            item.GetBytes(byteArray);
            return byteArray;
        }

    }
}