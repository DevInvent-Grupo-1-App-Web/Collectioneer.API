using Azure;
using Azure.Communication.Email;
using Collectioneer.API.Shared.Application.Exceptions;
using Collectioneer.API.Shared.Infrastructure.Configuration;

namespace Collectioneer.API.Shared.Application.External.Services;

public class CommunicationService 
{
	private readonly AppKeys _appKeys;
	private readonly EmailClient _emailClient;

	public CommunicationService(AppKeys appKeys)
	{
		_appKeys = appKeys;
		_emailClient = new EmailClient(_appKeys.ExternalCommunication.ConnectionString);
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