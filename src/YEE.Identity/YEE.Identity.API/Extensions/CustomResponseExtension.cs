using FastEndpoints;
using System.Text.Json.Serialization;
using YEE.Identity.API.Models;

namespace YEE.Identity.API.Extensions
{
    public static class CustomResponseExtension
    {
        public static SerializerOptions UseCustomResponse(this SerializerOptions serializer)
        {
            serializer.Options.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            serializer.Options.WriteIndented = true;
            serializer.ResponseSerializer = (rsp, dto, cType, jCtx, ct) =>
            {
                rsp.ContentType = cType;
                return rsp.WriteAsJsonAsync(ApiResult<object>.Success(dto), serializer.Options);
            };

            return serializer;
        }
    }
}
