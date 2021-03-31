using EDUPPLE.APPLICATION.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDUPPLE.API.Helper
{
    public static class ResponseHelper<T>
    {
        public static ObjectResult GenerateResponse(EntityResponseModel<T> response)
        {
            ObjectResult objectResult = new ObjectResult(response);
            var result = response.StatusCode switch
            {
                100 => StatusCodes.Status100Continue,
                101 => StatusCodes.Status101SwitchingProtocols,
                102 => StatusCodes.Status102Processing,
                200 => StatusCodes.Status200OK,
                201 => StatusCodes.Status201Created,
                203 => StatusCodes.Status203NonAuthoritative,
                204 => StatusCodes.Status204NoContent,
                205 => StatusCodes.Status205ResetContent,
                206 => StatusCodes.Status206PartialContent,
                207 => StatusCodes.Status207MultiStatus,
                208 => StatusCodes.Status208AlreadyReported,
                226 => StatusCodes.Status226IMUsed,
                300 => StatusCodes.Status300MultipleChoices,
                301 => StatusCodes.Status301MovedPermanently,
                302 => StatusCodes.Status302Found,
                303 => StatusCodes.Status303SeeOther,
                304 => StatusCodes.Status304NotModified,
                305 => StatusCodes.Status305UseProxy,
                306 => StatusCodes.Status306SwitchProxy,
                307 => StatusCodes.Status307TemporaryRedirect,
                308 => StatusCodes.Status308PermanentRedirect,
                400 => StatusCodes.Status400BadRequest,
                401 => StatusCodes.Status401Unauthorized,
                402 => StatusCodes.Status402PaymentRequired,
                403 => StatusCodes.Status403Forbidden,
                404 => StatusCodes.Status404NotFound,
                405 => StatusCodes.Status405MethodNotAllowed,
                406 => StatusCodes.Status406NotAcceptable,
                407 => StatusCodes.Status407ProxyAuthenticationRequired,
                408 => StatusCodes.Status408RequestTimeout,
                409 => StatusCodes.Status409Conflict,
                410 => StatusCodes.Status410Gone,
                411 => StatusCodes.Status411LengthRequired,
                412 => StatusCodes.Status412PreconditionFailed,
                413 => StatusCodes.Status413PayloadTooLarge,
                414 => StatusCodes.Status414RequestUriTooLong,
                415 => StatusCodes.Status415UnsupportedMediaType,
                416 => StatusCodes.Status416RangeNotSatisfiable,
                417 => StatusCodes.Status417ExpectationFailed,
                418 => StatusCodes.Status418ImATeapot,
                419 => StatusCodes.Status419AuthenticationTimeout,
                422 => StatusCodes.Status422UnprocessableEntity,
                423 => StatusCodes.Status423Locked,
                424 => StatusCodes.Status424FailedDependency,
                426 => StatusCodes.Status426UpgradeRequired,
                428 => StatusCodes.Status428PreconditionRequired,
                429 => StatusCodes.Status429TooManyRequests,
                431 => StatusCodes.Status431RequestHeaderFieldsTooLarge,
                451 => StatusCodes.Status451UnavailableForLegalReasons,
                500 => StatusCodes.Status500InternalServerError,
                501 => StatusCodes.Status501NotImplemented,
                502 => StatusCodes.Status502BadGateway,
                503 => StatusCodes.Status503ServiceUnavailable,
                504 => StatusCodes.Status504GatewayTimeout,
                505 => StatusCodes.Status505HttpVersionNotsupported,
                506 => StatusCodes.Status506VariantAlsoNegotiates,
                510 => StatusCodes.Status510NotExtended,
                511 => StatusCodes.Status511NetworkAuthenticationRequired,
                _ => StatusCodes.Status500InternalServerError
            };
            objectResult.StatusCode = result;
            return objectResult;
          
        }

        public static ObjectResult GenerateResponsePagination(EntityResponseListModel<T> response)
        {
            ObjectResult objectResult = new ObjectResult(response);
            var result = response.StatusCode switch
            {
                100 => StatusCodes.Status100Continue,
                101 => StatusCodes.Status101SwitchingProtocols,
                102 => StatusCodes.Status102Processing,
                200 => StatusCodes.Status200OK,
                201 => StatusCodes.Status201Created,
                203 => StatusCodes.Status203NonAuthoritative,
                204 => StatusCodes.Status204NoContent,
                205 => StatusCodes.Status205ResetContent,
                206 => StatusCodes.Status206PartialContent,
                207 => StatusCodes.Status207MultiStatus,
                208 => StatusCodes.Status208AlreadyReported,
                226 => StatusCodes.Status226IMUsed,
                300 => StatusCodes.Status300MultipleChoices,
                301 => StatusCodes.Status301MovedPermanently,
                302 => StatusCodes.Status302Found,
                303 => StatusCodes.Status303SeeOther,
                304 => StatusCodes.Status304NotModified,
                305 => StatusCodes.Status305UseProxy,
                306 => StatusCodes.Status306SwitchProxy,
                307 => StatusCodes.Status307TemporaryRedirect,
                308 => StatusCodes.Status308PermanentRedirect,
                400 => StatusCodes.Status400BadRequest,
                401 => StatusCodes.Status401Unauthorized,
                402 => StatusCodes.Status402PaymentRequired,
                403 => StatusCodes.Status403Forbidden,
                404 => StatusCodes.Status404NotFound,
                405 => StatusCodes.Status405MethodNotAllowed,
                406 => StatusCodes.Status406NotAcceptable,
                407 => StatusCodes.Status407ProxyAuthenticationRequired,
                408 => StatusCodes.Status408RequestTimeout,
                409 => StatusCodes.Status409Conflict,
                410 => StatusCodes.Status410Gone,
                411 => StatusCodes.Status411LengthRequired,
                412 => StatusCodes.Status412PreconditionFailed,
                413 => StatusCodes.Status413PayloadTooLarge,
                414 => StatusCodes.Status414RequestUriTooLong,
                415 => StatusCodes.Status415UnsupportedMediaType,
                416 => StatusCodes.Status416RangeNotSatisfiable,
                417 => StatusCodes.Status417ExpectationFailed,
                418 => StatusCodes.Status418ImATeapot,
                419 => StatusCodes.Status419AuthenticationTimeout,
                422 => StatusCodes.Status422UnprocessableEntity,
                423 => StatusCodes.Status423Locked,
                424 => StatusCodes.Status424FailedDependency,
                426 => StatusCodes.Status426UpgradeRequired,
                428 => StatusCodes.Status428PreconditionRequired,
                429 => StatusCodes.Status429TooManyRequests,
                431 => StatusCodes.Status431RequestHeaderFieldsTooLarge,
                451 => StatusCodes.Status451UnavailableForLegalReasons,
                500 => StatusCodes.Status500InternalServerError,
                501 => StatusCodes.Status501NotImplemented,
                502 => StatusCodes.Status502BadGateway,
                503 => StatusCodes.Status503ServiceUnavailable,
                504 => StatusCodes.Status504GatewayTimeout,
                505 => StatusCodes.Status505HttpVersionNotsupported,
                506 => StatusCodes.Status506VariantAlsoNegotiates,
                510 => StatusCodes.Status510NotExtended,
                511 => StatusCodes.Status511NetworkAuthenticationRequired,
                _ => StatusCodes.Status500InternalServerError
            };
            objectResult.StatusCode = result;
            return objectResult;
        }

        public static ObjectResult GenerateResponseList(EntityResponseListModel<T> response)
        {
            ObjectResult objectResult = new ObjectResult(response);
            var result = response.StatusCode switch
            {
                100 => StatusCodes.Status100Continue,
                101 => StatusCodes.Status101SwitchingProtocols,
                102 => StatusCodes.Status102Processing,
                200 => StatusCodes.Status200OK,
                201 => StatusCodes.Status201Created,
                203 => StatusCodes.Status203NonAuthoritative,
                204 => StatusCodes.Status204NoContent,
                205 => StatusCodes.Status205ResetContent,
                206 => StatusCodes.Status206PartialContent,
                207 => StatusCodes.Status207MultiStatus,
                208 => StatusCodes.Status208AlreadyReported,
                226 => StatusCodes.Status226IMUsed,
                300 => StatusCodes.Status300MultipleChoices,
                301 => StatusCodes.Status301MovedPermanently,
                302 => StatusCodes.Status302Found,
                303 => StatusCodes.Status303SeeOther,
                304 => StatusCodes.Status304NotModified,
                305 => StatusCodes.Status305UseProxy,
                306 => StatusCodes.Status306SwitchProxy,
                307 => StatusCodes.Status307TemporaryRedirect,
                308 => StatusCodes.Status308PermanentRedirect,
                400 => StatusCodes.Status400BadRequest,
                401 => StatusCodes.Status401Unauthorized,
                402 => StatusCodes.Status402PaymentRequired,
                403 => StatusCodes.Status403Forbidden,
                404 => StatusCodes.Status404NotFound,
                405 => StatusCodes.Status405MethodNotAllowed,
                406 => StatusCodes.Status406NotAcceptable,
                407 => StatusCodes.Status407ProxyAuthenticationRequired,
                408 => StatusCodes.Status408RequestTimeout,
                409 => StatusCodes.Status409Conflict,
                410 => StatusCodes.Status410Gone,
                411 => StatusCodes.Status411LengthRequired,
                412 => StatusCodes.Status412PreconditionFailed,
                413 => StatusCodes.Status413PayloadTooLarge,
                414 => StatusCodes.Status414RequestUriTooLong,
                415 => StatusCodes.Status415UnsupportedMediaType,
                416 => StatusCodes.Status416RangeNotSatisfiable,
                417 => StatusCodes.Status417ExpectationFailed,
                418 => StatusCodes.Status418ImATeapot,
                419 => StatusCodes.Status419AuthenticationTimeout,
                422 => StatusCodes.Status422UnprocessableEntity,
                423 => StatusCodes.Status423Locked,
                424 => StatusCodes.Status424FailedDependency,
                426 => StatusCodes.Status426UpgradeRequired,
                428 => StatusCodes.Status428PreconditionRequired,
                429 => StatusCodes.Status429TooManyRequests,
                431 => StatusCodes.Status431RequestHeaderFieldsTooLarge,
                451 => StatusCodes.Status451UnavailableForLegalReasons,
                500 => StatusCodes.Status500InternalServerError,
                501 => StatusCodes.Status501NotImplemented,
                502 => StatusCodes.Status502BadGateway,
                503 => StatusCodes.Status503ServiceUnavailable,
                504 => StatusCodes.Status504GatewayTimeout,
                505 => StatusCodes.Status505HttpVersionNotsupported,
                506 => StatusCodes.Status506VariantAlsoNegotiates,
                510 => StatusCodes.Status510NotExtended,
                511 => StatusCodes.Status511NetworkAuthenticationRequired,
                _ => StatusCodes.Status500InternalServerError
            };
            objectResult.StatusCode = result;
            return objectResult;
        }
    }
}
