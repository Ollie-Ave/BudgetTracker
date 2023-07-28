namespace BudgetTracker.DataAccess.Context
{
	using BudgetTracker.DataAccess.Models;
	using Microsoft.EntityFrameworkCore;

    /// <summary>An application database context.</summary>
    /// <seealso cref="T:Microsoft.EntityFrameworkCore.DbContext"/>
	public class ApplicationDbContext : DbContext
	{
        /// <summary>Initialises a new instance of the <see cref="BudgetTracker.DataAccess.Context.ApplicationDbContext"/> class.</summary>
        /// <param name="options">Options for controlling the operation.</param>
		public ApplicationDbContext(DbContextOptions options)
			: base(options)
		{
		}

        /// <summary>Gets or sets the accounts.</summary>
        /// <value>The accounts.</value>
		public DbSet<Account> Accounts { get; set; } = null!;

        /// <summary>Gets or sets the transactions.</summary>
        /// <value>The transactions.</value>
        public DbSet<Transaction> Transactions { get; set; } = null!;

        /// <summary>Gets or sets the income streams.</summary>
        /// <value>The income streams.</value>
		public DbSet<IncomeStream> IncomeStreams { get; set; } = null!;

        /// <summary>Override this method to further configure the model that was discovered by convention from the entity types
        /// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
        /// and re-used for subsequent instances of your derived context.</summary>
        /// <remarks><para>
        ///                 If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
        ///                 then this method will not be run. However, it will still run when creating a compiled model.
        ///             </para>
        /// <para>
        ///                 See <see href="https://aka.ms/efcore-docs-modeling">Modeling entity types and relationships</see> for more information and
        ///                 examples.
        ///             </para></remarks>
        /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
        /// define extension methods on this object that allow you to configure aspects of the model that are specific
        /// to a given database.</param>
        /// <seealso cref="M:Microsoft.EntityFrameworkCore.DbContext.OnModelCreating(ModelBuilder)"/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Account>()
				.HasKey(a => a.Email);
		}
	}
}