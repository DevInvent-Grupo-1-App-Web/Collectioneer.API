using Azure;
using Azure.Communication.Email;
using Collectioneer.API.Shared.Application.Exceptions;

namespace Collectioneer.API.Shared.Application.Internal.Services;

public class CommunicationService 
{
	private readonly IConfiguration _configuration;
	private readonly EmailClient _emailClient;

	public CommunicationService(IConfiguration configuration)
	{
		_configuration = configuration;
		_emailClient = new(_configuration["COMMUNICATION_SERVICES_CONNECTION_STRING"]);
	}

	public async Task SendEmail(string to, string subject, string body)
{
    try
    {
        EmailSendOperation emailSendOperation =  await _emailClient.SendAsync(
            WaitUntil.Completed,
            senderAddress: "DoNotReply@5ecf333e-0cd7-4809-afe0-444615482c94.azurecomm.net",
            recipientAddress: to,
            subject: subject,
            htmlContent: body
        );	
    }
    catch (Exception ex)
    {
        throw new ExposableException("Couldn't send access recovery email.", 500, ex);
    }
}
}