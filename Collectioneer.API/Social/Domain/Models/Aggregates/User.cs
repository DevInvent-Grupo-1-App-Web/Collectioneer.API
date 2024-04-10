using System.Text.RegularExpressions;
using Collectioneer.API.Operational.Domain.Models.Aggregates;
using Collectioneer.API.Operational.Domain.Models.Entities;
using Collectioneer.API.Operational.Domain.Models.ValueObjects;
using Collectioneer.API.Social.Domain.Exceptions;

namespace Collectioneer.API.Social.Domain.Models.Aggregates;

public class User
{
	public int Id { get; private set; }
	public string Username { get; private set; } = string.Empty;
	public string Email { get; private set; } = string.Empty;
	public string Name { get; private set; } = string.Empty;
	public string Password { get; private set; } = string.Empty;
	public ICollection<Collectible> Collectibles { get; set; } = [];
	public ICollection<Auction> Auctions { get; set; } = [];
	public ICollection<Bid> Bids { get; set; } = [];

	public User(
		string username,
		string email,
		string name,
		string password
	)
	{
		SetUsername(username);
		SetEmail(email);
		SetName(name);
		SetPassword(password);
    }

	private void SetPassword(string hashedPassword)
    {
		if (hashedPassword.Length != 64)
		{
			throw new UserModelException("Hashed password is not of expected length. This is likely an internal service error. Contact app support");
		}

		Password = hashedPassword;
	}

	private void SetName(string name)
	{
		if (name.Length < 3 || name.Length > 50)
		{
            throw new UserModelException("Name must be between 3 and 50 characters long.");
        }

        Name = name;
	}

	private void SetEmail(string email)
	{
		if (!Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
		{
            throw new UserModelException("Email is not in a valid format.");
        }

        Email = email;
	}

	private void SetUsername(string username)
	{
		if (username.Length < 3 || username.Length > 50)
		{
			throw new UserModelException("Username must be between 3 and 50 characters long.");
		}

		if (!Regex.IsMatch(username, @"^[a-zA-Z0-9_.]+$"))
		{
			throw new UserModelException("Username can only contain alphanumeric characters, dots, and underscores.");
		}
		Username = username;
	}

	public bool CheckPassword(string password)
	{
		if (password.Length != 64)
		{
			throw new UserModelException("Argument doesn't have expected length. Verify if properly hashed before using this method.");
		}

        return Password == password;
    }
}