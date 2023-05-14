using FreeCourse.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace FreeCourse.Web.Handler
{
    public class ClientCredentialTokenHander : DelegatingHandler
    {
        private readonly IClientCredentialsTokenService _clientCredentialsTokenService;
        public ClientCredentialTokenHander(IClientCredentialsTokenService clientCredentialsTokenService)
        {
            _clientCredentialsTokenService = clientCredentialsTokenService;

        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _clientCredentialsTokenService.GetToken());
            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException();
            }

            return response;
        }
    }
}
