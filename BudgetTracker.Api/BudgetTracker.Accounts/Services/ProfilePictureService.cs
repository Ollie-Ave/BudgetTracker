namespace BudgetTracker.Accounts.Services
{
    using System;
    using System.IO;
    using BudgetTracker.Accounts.Interfaces;
    using BudgetTracker.Api.Models;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// The profile picture service.
    /// </summary>
    public class ProfilePictureService : IProfilePictureService
	{
        /// <summary>
        /// The image options.
        /// </summary>
        private readonly ImageOptions imageOptions;

        /// <summary>
        /// Initialises a new instance of the <see cref="ProfilePictureService"/> class.
        /// </summary>
        /// <param name="imageOptions"></param>
        public ProfilePictureService(IOptions<ImageOptions> imageOptions)
		{
            this.imageOptions = imageOptions.Value;
        }
		
        /// <summary>
        /// Returns the profile picture for the user with the specified ID.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
		public byte[] GetProfilePicture(int userId)
		{
            byte[] returnValue;

            try
            {
                returnValue = File.ReadAllBytes($"{imageOptions.ProfilePicturePath}/profile-picture-id-{userId}.jpg");
            }
            catch (FileNotFoundException)
            {
                returnValue = File.ReadAllBytes($"{imageOptions.ProfilePicturePath}/placeholder.jpg");
            }

            return returnValue;
        }
	}
}