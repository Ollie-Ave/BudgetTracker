namespace BudgetTracker.Accounts.Interfaces
{
	public interface IProfilePictureService
	{
		public byte[] GetProfilePicture(int userId);
	}
}