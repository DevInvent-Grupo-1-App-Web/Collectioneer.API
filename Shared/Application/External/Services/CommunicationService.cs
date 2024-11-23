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
				senderAddress: "DoNotReply@889c56fc-587b-4b15-8311-5b51aaf1a777.azurecomm.net",
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
		Console.WriteLine("Checking email connection...");
		try
		{
			await _emailClient.SendAsync(
				WaitUntil.Started,
				senderAddress: "DoNotReply@889c56fc-587b-4b15-8311-5b51aaf1a777.azurecomm.net",
				recipientAddress: "internal@dittobox.com",
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
