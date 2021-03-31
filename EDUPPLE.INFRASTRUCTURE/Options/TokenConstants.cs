﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDUPPLE.INFRASTRUCTURE.Options
{
    public static class TokenConstants
    {
        public static class Algorithms
        {
            public const string EcdsaSha256 = "ES256";
            public const string EcdsaSha384 = "ES384";
            public const string EcdsaSha512 = "ES512";
            public const string HmacSha256 = "HS256";
            public const string HmacSha384 = "HS384";
            public const string HmacSha512 = "HS512";
            public const string RsaSha256 = "RS256";
            public const string RsaSha384 = "RS384";
            public const string RsaSha512 = "RS512";
            public const string RsaSsaPssSha256 = "PS256";
            public const string RsaSsaPssSha384 = "PS384";
            public const string RsaSsaPssSha512 = "PS512";
        }
        public static class Claims
        {
            public const string AccessTokenHash = "at_hash";
            public const string Active = "active";
            public const string Address = "address";
            public const string Audience = "aud";
            public const string AuthorizedParty = "azp";
            public const string Birthdate = "birthdate";
            public const string ClientId = "client_id";
            public const string CodeHash = "c_hash";
            public const string ConfidentialityLevel = "cfd_lvl";
            public const string Country = "country";
            public const string Email = "email";
            public const string EmailVerified = "email_verified";
            public const string ExpiresAt = "exp";
            public const string FamilyName = "family_name";
            public const string Formatted = "formatted";
            public const string Gender = "gender";
            public const string GivenName = "given_name";
            public const string IssuedAt = "iat";
            public const string Issuer = "iss";
            public const string Locale = "locale";
            public const string Locality = "locality";
            public const string JwtId = "jti";
            public const string KeyId = "kid";
            public const string MiddleName = "middle_name";
            public const string Name = "name";
            public const string Nickname = "nickname";
            public const string Nonce = "nonce";
            public const string NotBefore = "nbf";
            public const string PhoneNumber = "phone_number";
            public const string PhoneNumberVerified = "phone_number_verified";
            public const string Picture = "picture";
            public const string PostalCode = "postal_code";
            public const string PreferredUsername = "preferred_username";
            public const string Profile = "profile";
            public const string Region = "region";
            public const string Role = "role";
            public const string Scope = "scope";
            public const string StreetAddress = "street_address";
            public const string Subject = "sub";
            public const string TokenType = "token_type";
            public const string TokenUsage = "token_usage";
            public const string UpdatedAt = "updated_at";
            public const string Username = "username";
            public const string Website = "website";
            public const string Zoneinfo = "zoneinfo";

            public const string UserId = "uid";
            public const string Holding = "tenant";
            public const string Company = "company";
            public const string Branch = "branch";
            public const string Permission = "permission";
            public const string Roles = "roles";
            public const string DefaultAgent = "default_agent";
            public const string UserOrganizations = "user_organizations";
            public const string IsIntegration = "is_integration";
            public const string UserType = "user_type";
            public const string FireBase = "localId";
            public const string Employee = "employee";
            public const string EmployeeId = "employeeId";
            public const string AccessRights = "accessrights";
            public const string UserRoles = "userroles";

        }
        public static class Parameters
        {
            public const string AccessToken = "access_token";
            public const string Active = "active";
            public const string AcrValues = "acr_values";
            public const string Assertion = "assertion";
            public const string Claims = "claims";
            public const string ClaimsLocales = "claims_locales";
            public const string ClientAssertion = "client_assertion";
            public const string ClientAssertionType = "client_assertion_type";
            public const string ClientId = "client_id";
            public const string ClientSecret = "client_secret";
            public const string Code = "code";
            public const string CodeChallenge = "code_challenge";
            public const string CodeChallengeMethod = "code_challenge_method";
            public const string CodeVerifier = "code_verifier";
            public const string Display = "display";
            public const string Error = "error";
            public const string ErrorDescription = "error_description";
            public const string ErrorUri = "error_uri";
            public const string ExpiresIn = "expires_in";
            public const string GrantType = "grant_type";
            public const string IdentityProvider = "identity_provider";
            public const string IdToken = "id_token";
            public const string IdTokenHint = "id_token_hint";
            public const string LoginHint = "login_hint";
            public const string Keys = "keys";
            public const string MaxAge = "max_age";
            public const string Nonce = "nonce";
            public const string Password = "password";
            public const string PostLogoutRedirectUri = "post_logout_redirect_uri";
            public const string Prompt = "prompt";
            public const string RedirectUri = "redirect_uri";
            public const string RefreshToken = "refresh_token";
            public const string Registration = "registration";
            public const string Request = "request";
            public const string RequestId = "request_id";
            public const string RequestUri = "request_uri";
            public const string Resource = "resource";
            public const string ResponseMode = "response_mode";
            public const string ResponseType = "response_type";
            public const string Scope = "scope";
            public const string State = "state";
            public const string Token = "token";
            public const string TokenType = "token_type";
            public const string TokenTypeHint = "token_type_hint";
            public const string UiLocales = "ui_locales";
            public const string Username = "username";
            public const string Auth = "auth";
        }
        public static class GrantTypes
        {
            public const string AuthorizationCode = "authorization_code";
            public const string ClientCredentials = "client_credentials";
            public const string Implicit = "implicit";
            public const string Password = "password";
            public const string RefreshToken = "refresh_token";
        }
    }
}
