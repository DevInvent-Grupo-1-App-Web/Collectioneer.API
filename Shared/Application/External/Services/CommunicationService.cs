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
			EmailSendOperation emailSendOperation = await _emailClient.SendAsync(
				WaitUntil.Completed,
				senderAddress: "DoNotReply@f130d3f5-1b6d-4fdf-b2c5-b0cc0dfc0734.azurecomm.net",
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

	public async Task<bool> IsEmailConnectionOk()
	{
		try
		{
			await _emailClient.SendAsync(
				WaitUntil.Started,
				senderAddress: "DoNotReply@f130d3f5-1b6d-4fdf-b2c5-b0cc0dfc0734.azurecomm.net",
				recipientAddress: "",
				subject: "Connection test",
				htmlContent: "This is a test email to check the connection executed at " + DateTime.Now
			);
			return true;
		}
		catch
		{
			return false;
		}
	}
	
}
