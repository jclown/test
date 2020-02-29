 
 







  
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Model;  

namespace Dal
{
    /// <summary>
    /// 
    /// </summary>
	public partial class MLSDbContext : DbContextBase
	{ 

		public MLSDbContext(DbContextOptions<MLSDbContext> options) : base(options)
        {
            
        }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            base.OnConfiguring(optionsBuilder);
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
 
			modelBuilder.Entity<ChineseUserVip>().HasKey(t => new { t.UserId,t.VipId });
 
			modelBuilder.Entity<ChineseCommunityFeatures>().HasKey(t => new { t.CommunityId,t.FeatureId });
 
			modelBuilder.Entity<Sys_UserLogins>().HasKey(t => new { t.LoginProvider,t.ProviderKey });
 
			modelBuilder.Entity<Sys_UserRoles>().HasKey(t => new { t.UserId,t.RoleId });
 
			modelBuilder.Entity<ChineseShopArea>().HasKey(t => new { t.ShopId,t.CityId,t.DistrictId,t.SubDistrictId });
 
			modelBuilder.Entity<Sys_UserTokens>().HasKey(t => new { t.UserId,t.LoginProvider,t.Name });
 
			modelBuilder.Entity<ChineseTraderInfoArea>().HasKey(t => new { t.TraderInfoId,t.CityId,t.DistrictId,t.SubDistrictId });
 
			modelBuilder.Entity<UserLogins>().HasKey(t => new { t.LoginProvider,t.ProviderKey,t.UserId });
 
			modelBuilder.Entity<UserRoles>().HasKey(t => new { t.UserId,t.RoleId });
 
			OnModelCreatingExt(modelBuilder);
			base.OnModelCreating(modelBuilder);
		}

  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseAdManage> ChineseAdManage { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseAdManage> ChineseAdManageQuery=> ChineseAdManage.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseAppServers> ChineseAppServers { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseAppServers> ChineseAppServersQuery=> ChineseAppServers.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Comments> Comments { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Comments> CommentsQuery=> Comments.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseCmsShare> ChineseCmsShare { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseCmsShare> ChineseCmsShareQuery=> ChineseCmsShare.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseShopPhoto> ChineseShopPhoto { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseShopPhoto> ChineseShopPhotoQuery=> ChineseShopPhoto.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Themes> Themes { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Themes> ThemesQuery=> Themes.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Likes> Likes { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Likes> LikesQuery=> Likes.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseAreas> ChineseAreas { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseAreas> ChineseAreasQuery=> ChineseAreas.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Attendance_ApplyBusinessCard> Attendance_ApplyBusinessCard { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Attendance_ApplyBusinessCard> Attendance_ApplyBusinessCardQuery=> Attendance_ApplyBusinessCard.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseCmsContent> ChineseCmsContent { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseCmsContent> ChineseCmsContentQuery=> ChineseCmsContent.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseTransTradeLog> ChineseTransTradeLog { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseTransTradeLog> ChineseTransTradeLogQuery=> ChineseTransTradeLog.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Temp_ChineseCommunities> Temp_ChineseCommunities { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Temp_ChineseCommunities> Temp_ChineseCommunitiesQuery=> Temp_ChineseCommunities.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseLikeOper> ChineseLikeOper { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseLikeOper> ChineseLikeOperQuery=> ChineseLikeOper.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<UserDefaultSettings> UserDefaultSettings { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<UserDefaultSettings> UserDefaultSettingsQuery=> UserDefaultSettings.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Agent_TotalStatisticsToday> Agent_TotalStatisticsToday { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Agent_TotalStatisticsToday> Agent_TotalStatisticsTodayQuery=> Agent_TotalStatisticsToday.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Attendance_ApplyOut> Attendance_ApplyOut { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Attendance_ApplyOut> Attendance_ApplyOutQuery=> Attendance_ApplyOut.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseUserAccount> ChineseUserAccount { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseUserAccount> ChineseUserAccountQuery=> ChineseUserAccount.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<PropertyOpenDates> PropertyOpenDates { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<PropertyOpenDates> PropertyOpenDatesQuery=> PropertyOpenDates.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Agent_ChangeStatisticsToDay> Agent_ChangeStatisticsToDay { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Agent_ChangeStatisticsToDay> Agent_ChangeStatisticsToDayQuery=> Agent_ChangeStatisticsToDay.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Attendance_AuditOutApply> Attendance_AuditOutApply { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Attendance_AuditOutApply> Attendance_AuditOutApplyQuery=> Attendance_AuditOutApply.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<PropertyVirtualTours> PropertyVirtualTours { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<PropertyVirtualTours> PropertyVirtualToursQuery=> PropertyVirtualTours.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseBrowsingHistory> ChineseBrowsingHistory { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseBrowsingHistory> ChineseBrowsingHistoryQuery=> ChineseBrowsingHistory.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseUserAccountFlow> ChineseUserAccountFlow { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseUserAccountFlow> ChineseUserAccountFlowQuery=> ChineseUserAccountFlow.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseNewHouse> ChineseNewHouse { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseNewHouse> ChineseNewHouseQuery=> ChineseNewHouse.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseHousesStatisticsClass> ChineseHousesStatisticsClass { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseHousesStatisticsClass> ChineseHousesStatisticsClassQuery=> ChineseHousesStatisticsClass.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Attendance_CommonStatus> Attendance_CommonStatus { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Attendance_CommonStatus> Attendance_CommonStatusQuery=> Attendance_CommonStatus.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseUserDisable> ChineseUserDisable { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseUserDisable> ChineseUserDisableQuery=> ChineseUserDisable.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<TopLevelDomains> TopLevelDomains { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<TopLevelDomains> TopLevelDomainsQuery=> TopLevelDomains.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Projects> Projects { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Projects> ProjectsQuery=> Projects.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Attendance_Meeting> Attendance_Meeting { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Attendance_Meeting> Attendance_MeetingQuery=> Attendance_Meeting.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///客户跟进表
		///</summary>
        public DbSet<FollowUp> FollowUp { get; set; } 

		///<summary>
		///客户跟进表
		///</summary>
		public IQueryable<FollowUp> FollowUpQuery=> FollowUp.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Roles> Roles { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Roles> RolesQuery=> Roles.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseHousesStatistics> ChineseHousesStatistics { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseHousesStatistics> ChineseHousesStatisticsQuery=> ChineseHousesStatistics.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<CustomerDemands> CustomerDemands { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<CustomerDemands> CustomerDemandsQuery=> CustomerDemands.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseCmsColumn> ChineseCmsColumn { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseCmsColumn> ChineseCmsColumnQuery=> ChineseCmsColumn.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Attendance_MeetingMember> Attendance_MeetingMember { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Attendance_MeetingMember> Attendance_MeetingMemberQuery=> Attendance_MeetingMember.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Estate> Estate { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Estate> EstateQuery=> Estate.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseTraderInfo> ChineseTraderInfo { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseTraderInfo> ChineseTraderInfoQuery=> ChineseTraderInfo.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Companies> Companies { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Companies> CompaniesQuery=> Companies.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<AccountFeatures> AccountFeatures { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<AccountFeatures> AccountFeaturesQuery=> AccountFeatures.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseUserVip> ChineseUserVip { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseUserVip> ChineseUserVipQuery=> ChineseUserVip.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ContactDetails> ContactDetails { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ContactDetails> ContactDetailsQuery=> ContactDetails.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseGeneralService> ChineseGeneralService { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseGeneralService> ChineseGeneralServiceQuery=> ChineseGeneralService.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseJoinApplication> ChineseJoinApplication { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseJoinApplication> ChineseJoinApplicationQuery=> ChineseJoinApplication.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseUserWatch> ChineseUserWatch { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseUserWatch> ChineseUserWatchQuery=> ChineseUserWatch.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseTraderIdentification> ChineseTraderIdentification { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseTraderIdentification> ChineseTraderIdentificationQuery=> ChineseTraderIdentification.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Contacts> Contacts { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Contacts> ContactsQuery=> Contacts.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseShopDepartment> ChineseShopDepartment { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseShopDepartment> ChineseShopDepartmentQuery=> ChineseShopDepartment.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<CustomServiceForm> CustomServiceForm { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<CustomServiceForm> CustomServiceFormQuery=> CustomServiceForm.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<PropertyDomain> PropertyDomain { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<PropertyDomain> PropertyDomainQuery=> PropertyDomain.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<AccountTypeFeatures> AccountTypeFeatures { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<AccountTypeFeatures> AccountTypeFeaturesQuery=> AccountTypeFeatures.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseVip> ChineseVip { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseVip> ChineseVipQuery=> ChineseVip.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<AccountTypes> AccountTypes { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<AccountTypes> AccountTypesQuery=> AccountTypes.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<AdditionalWebsitePlans> AdditionalWebsitePlans { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<AdditionalWebsitePlans> AdditionalWebsitePlansQuery=> AdditionalWebsitePlans.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseWeChatApiLogs> ChineseWeChatApiLogs { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseWeChatApiLogs> ChineseWeChatApiLogsQuery=> ChineseWeChatApiLogs.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseCmsPoster> ChineseCmsPoster { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseCmsPoster> ChineseCmsPosterQuery=> ChineseCmsPoster.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<CustomerFeedback> CustomerFeedback { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<CustomerFeedback> CustomerFeedbackQuery=> CustomerFeedback.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseTransactionTransfer> ChineseTransactionTransfer { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseTransactionTransfer> ChineseTransactionTransferQuery=> ChineseTransactionTransfer.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<BillingInfo> BillingInfo { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<BillingInfo> BillingInfoQuery=> BillingInfo.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseWeChatOrders> ChineseWeChatOrders { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseWeChatOrders> ChineseWeChatOrdersQuery=> ChineseWeChatOrders.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseAskAnswer> ChineseAskAnswer { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseAskAnswer> ChineseAskAnswerQuery=> ChineseAskAnswer.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseCmsPosterClass> ChineseCmsPosterClass { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseCmsPosterClass> ChineseCmsPosterClassQuery=> ChineseCmsPosterClass.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseCapitalCustody> ChineseCapitalCustody { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseCapitalCustody> ChineseCapitalCustodyQuery=> ChineseCapitalCustody.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseLegalServiceApplication> ChineseLegalServiceApplication { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseLegalServiceApplication> ChineseLegalServiceApplicationQuery=> ChineseLegalServiceApplication.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseWeChatUsers> ChineseWeChatUsers { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseWeChatUsers> ChineseWeChatUsersQuery=> ChineseWeChatUsers.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseAskQuestion> ChineseAskQuestion { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseAskQuestion> ChineseAskQuestionQuery=> ChineseAskQuestion.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseAdviserFollow> ChineseAdviserFollow { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseAdviserFollow> ChineseAdviserFollowQuery=> ChineseAdviserFollow.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<HouseCommonAttr> HouseCommonAttr { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<HouseCommonAttr> HouseCommonAttrQuery=> HouseCommonAttr.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<HouseCommission> HouseCommission { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<HouseCommission> HouseCommissionQuery=> HouseCommission.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Attendance_Class> Attendance_Class { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Attendance_Class> Attendance_ClassQuery=> Attendance_Class.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Invoice> Invoice { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Invoice> InvoiceQuery=> Invoice.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ScanLoginInfo> ScanLoginInfo { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ScanLoginInfo> ScanLoginInfoQuery=> ScanLoginInfo.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseAskQuestionTemp> ChineseAskQuestionTemp { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseAskQuestionTemp> ChineseAskQuestionTempQuery=> ChineseAskQuestionTemp.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<HouseQuestion> HouseQuestion { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<HouseQuestion> HouseQuestionQuery=> HouseQuestion.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<UserCollect> UserCollect { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<UserCollect> UserCollectQuery=> UserCollect.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseLoans> ChineseLoans { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseLoans> ChineseLoansQuery=> ChineseLoans.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<InvoiceTransactions> InvoiceTransactions { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<InvoiceTransactions> InvoiceTransactionsQuery=> InvoiceTransactions.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseAskSetting> ChineseAskSetting { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseAskSetting> ChineseAskSettingQuery=> ChineseAskSetting.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Attendance_Group> Attendance_Group { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Attendance_Group> Attendance_GroupQuery=> Attendance_Group.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<UserGoodHouse> UserGoodHouse { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<UserGoodHouse> UserGoodHouseQuery=> UserGoodHouse.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseCustomerInfos> ChineseCustomerInfos { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseCustomerInfos> ChineseCustomerInfosQuery=> ChineseCustomerInfos.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<PaymentPeriodTypes> PaymentPeriodTypes { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<PaymentPeriodTypes> PaymentPeriodTypesQuery=> PaymentPeriodTypes.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseAskUserGrade> ChineseAskUserGrade { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseAskUserGrade> ChineseAskUserGradeQuery=> ChineseAskUserGrade.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<PaymentStatus> PaymentStatus { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<PaymentStatus> PaymentStatusQuery=> PaymentStatus.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseAskUserPractice> ChineseAskUserPractice { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseAskUserPractice> ChineseAskUserPracticeQuery=> ChineseAskUserPractice.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseTradeItem> ChineseTradeItem { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseTradeItem> ChineseTradeItemQuery=> ChineseTradeItem.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseBidRemark> ChineseBidRemark { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseBidRemark> ChineseBidRemarkQuery=> ChineseBidRemark.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Sys_Role> Sys_Role { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Sys_Role> Sys_RoleQuery=> Sys_Role.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Attendance_Members> Attendance_Members { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Attendance_Members> Attendance_MembersQuery=> Attendance_Members.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ReferralChart> ReferralChart { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ReferralChart> ReferralChartQuery=> ReferralChart.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseAskUserRecord> ChineseAskUserRecord { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseAskUserRecord> ChineseAskUserRecordQuery=> ChineseAskUserRecord.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ContactDetailLabels> ContactDetailLabels { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ContactDetailLabels> ContactDetailLabelsQuery=> ContactDetailLabels.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseContract> ChineseContract { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseContract> ChineseContractQuery=> ChineseContract.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseSysRole> ChineseSysRole { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseSysRole> ChineseSysRoleQuery=> ChineseSysRole.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Sys_User> Sys_User { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Sys_User> Sys_UserQuery=> Sys_User.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Attendance_ClockIns> Attendance_ClockIns { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Attendance_ClockIns> Attendance_ClockInsQuery=> Attendance_ClockIns.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseShopSetting> ChineseShopSetting { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseShopSetting> ChineseShopSettingQuery=> ChineseShopSetting.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ContactDetailTypes> ContactDetailTypes { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ContactDetailTypes> ContactDetailTypesQuery=> ContactDetailTypes.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Sys_RoleClaims> Sys_RoleClaims { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Sys_RoleClaims> Sys_RoleClaimsQuery=> Sys_RoleClaims.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseAppVersions> ChineseAppVersions { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseAppVersions> ChineseAppVersionsQuery=> ChineseAppVersions.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<UserTransactions> UserTransactions { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<UserTransactions> UserTransactionsQuery=> UserTransactions.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseCommunityFeatures> ChineseCommunityFeatures { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseCommunityFeatures> ChineseCommunityFeaturesQuery=> ChineseCommunityFeatures.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<UserTransactionTypes> UserTransactionTypes { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<UserTransactionTypes> UserTransactionTypesQuery=> UserTransactionTypes.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Items> Items { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Items> ItemsQuery=> Items.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseSysMenu> ChineseSysMenu { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseSysMenu> ChineseSysMenuQuery=> ChineseSysMenu.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Sys_UserClaims> Sys_UserClaims { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Sys_UserClaims> Sys_UserClaimsQuery=> Sys_UserClaims.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Attendance_ClockInsCount> Attendance_ClockInsCount { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Attendance_ClockInsCount> Attendance_ClockInsCountQuery=> Attendance_ClockInsCount.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseHouseRecordSetting> ChineseHouseRecordSetting { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseHouseRecordSetting> ChineseHouseRecordSettingQuery=> ChineseHouseRecordSetting.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<WithdrawTransactionDetails> WithdrawTransactionDetails { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<WithdrawTransactionDetails> WithdrawTransactionDetailsQuery=> WithdrawTransactionDetails.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseCustomerImage> ChineseCustomerImage { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseCustomerImage> ChineseCustomerImageQuery=> ChineseCustomerImage.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Users> Users { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Users> UsersQuery=> Users.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Attendance_Managers> Attendance_Managers { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Attendance_Managers> Attendance_ManagersQuery=> Attendance_Managers.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseFeatures> ChineseFeatures { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseFeatures> ChineseFeaturesQuery=> ChineseFeatures.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Sys_UserLogins> Sys_UserLogins { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Sys_UserLogins> Sys_UserLoginsQuery=> Sys_UserLogins.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<WithdrawTransactions> WithdrawTransactions { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<WithdrawTransactions> WithdrawTransactionsQuery=> WithdrawTransactions.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Attendance_UserClockInsCount> Attendance_UserClockInsCount { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Attendance_UserClockInsCount> Attendance_UserClockInsCountQuery=> Attendance_UserClockInsCount.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseGraphCodeVerify> ChineseGraphCodeVerify { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseGraphCodeVerify> ChineseGraphCodeVerifyQuery=> ChineseGraphCodeVerify.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Sys_UserRoles> Sys_UserRoles { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Sys_UserRoles> Sys_UserRolesQuery=> Sys_UserRoles.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Attendance_UserGroup> Attendance_UserGroup { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Attendance_UserGroup> Attendance_UserGroupQuery=> Attendance_UserGroup.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<WithdrawTransactionStatus> WithdrawTransactionStatus { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<WithdrawTransactionStatus> WithdrawTransactionStatusQuery=> WithdrawTransactionStatus.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseSysDataPerm> ChineseSysDataPerm { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseSysDataPerm> ChineseSysDataPermQuery=> ChineseSysDataPerm.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Folders> Folders { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Folders> FoldersQuery=> Folders.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseShopArea> ChineseShopArea { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseShopArea> ChineseShopAreaQuery=> ChineseShopArea.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseHouseAdvancePrice> ChineseHouseAdvancePrice { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseHouseAdvancePrice> ChineseHouseAdvancePriceQuery=> ChineseHouseAdvancePrice.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<UsersLoginRecord> UsersLoginRecord { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<UsersLoginRecord> UsersLoginRecordQuery=> UsersLoginRecord.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseNewHouseDistRule> ChineseNewHouseDistRule { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseNewHouseDistRule> ChineseNewHouseDistRuleQuery=> ChineseNewHouseDistRule.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseTipoff> ChineseTipoff { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseTipoff> ChineseTipoffQuery=> ChineseTipoff.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseHouseAdvancePriceTemp> ChineseHouseAdvancePriceTemp { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseHouseAdvancePriceTemp> ChineseHouseAdvancePriceTempQuery=> ChineseHouseAdvancePriceTemp.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Sys_UserTokens> Sys_UserTokens { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Sys_UserTokens> Sys_UserTokensQuery=> Sys_UserTokens.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseTraderInfoArea> ChineseTraderInfoArea { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseTraderInfoArea> ChineseTraderInfoAreaQuery=> ChineseTraderInfoArea.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Feedback> Feedback { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Feedback> FeedbackQuery=> Feedback.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<VR_Companys> VR_Companys { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<VR_Companys> VR_CompanysQuery=> VR_Companys.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseHouseType> ChineseHouseType { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseHouseType> ChineseHouseTypeQuery=> ChineseHouseType.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Sys_UserInfo> Sys_UserInfo { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Sys_UserInfo> Sys_UserInfoQuery=> Sys_UserInfo.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ProjectTags> ProjectTags { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ProjectTags> ProjectTagsQuery=> ProjectTags.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseTask> ChineseTask { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseTask> ChineseTaskQuery=> ChineseTask.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<TransactionReport> TransactionReport { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<TransactionReport> TransactionReportQuery=> TransactionReport.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseCompany> ChineseCompany { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseCompany> ChineseCompanyQuery=> ChineseCompany.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseCommission> ChineseCommission { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseCommission> ChineseCommissionQuery=> ChineseCommission.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseCustomerDemand> ChineseCustomerDemand { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseCustomerDemand> ChineseCustomerDemandQuery=> ChineseCustomerDemand.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<SavedProjects> SavedProjects { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<SavedProjects> SavedProjectsQuery=> SavedProjects.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseVrService> ChineseVrService { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseVrService> ChineseVrServiceQuery=> ChineseVrService.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseHouseFillInfoTemp> ChineseHouseFillInfoTemp { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseHouseFillInfoTemp> ChineseHouseFillInfoTempQuery=> ChineseHouseFillInfoTemp.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<CustomerDemandMatchQueue> CustomerDemandMatchQueue { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<CustomerDemandMatchQueue> CustomerDemandMatchQueueQuery=> CustomerDemandMatchQueue.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Scenes> Scenes { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Scenes> ScenesQuery=> Scenes.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<InvitationCodes> InvitationCodes { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<InvitationCodes> InvitationCodesQuery=> InvitationCodes.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseLegalConsult> ChineseLegalConsult { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseLegalConsult> ChineseLegalConsultQuery=> ChineseLegalConsult.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<UnitOfMeasurements> UnitOfMeasurements { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<UnitOfMeasurements> UnitOfMeasurementsQuery=> UnitOfMeasurements.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseHouseFillMoreInfo> ChineseHouseFillMoreInfo { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseHouseFillMoreInfo> ChineseHouseFillMoreInfoQuery=> ChineseHouseFillMoreInfo.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseHouseFillMoreInfoTemp> ChineseHouseFillMoreInfoTemp { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseHouseFillMoreInfoTemp> ChineseHouseFillMoreInfoTempQuery=> ChineseHouseFillMoreInfoTemp.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<CommonAnswer> CommonAnswer { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<CommonAnswer> CommonAnswerQuery=> CommonAnswer.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseHouseLandMarks> ChineseHouseLandMarks { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseHouseLandMarks> ChineseHouseLandMarksQuery=> ChineseHouseLandMarks.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseHouseNews> ChineseHouseNews { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseHouseNews> ChineseHouseNewsQuery=> ChineseHouseNews.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseHouseProgress> ChineseHouseProgress { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseHouseProgress> ChineseHouseProgressQuery=> ChineseHouseProgress.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseCustomerStatistics> ChineseCustomerStatistics { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseCustomerStatistics> ChineseCustomerStatisticsQuery=> ChineseCustomerStatistics.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Tags> Tags { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Tags> TagsQuery=> Tags.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<UserPromoCodes> UserPromoCodes { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<UserPromoCodes> UserPromoCodesQuery=> UserPromoCodes.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Categories> Categories { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Categories> CategoriesQuery=> Categories.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseHouses> ChineseHouses { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseHouses> ChineseHousesQuery=> ChineseHouses.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<UserClaims> UserClaims { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<UserClaims> UserClaimsQuery=> UserClaims.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseHouseBaseInfo> ChineseHouseBaseInfo { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseHouseBaseInfo> ChineseHouseBaseInfoQuery=> ChineseHouseBaseInfo.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<UserActionLog> UserActionLog { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<UserActionLog> UserActionLogQuery=> UserActionLog.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseContractAffirm> ChineseContractAffirm { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseContractAffirm> ChineseContractAffirmQuery=> ChineseContractAffirm.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<UserLogins> UserLogins { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<UserLogins> UserLoginsQuery=> UserLogins.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseHousesPhotoTemp> ChineseHousesPhotoTemp { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseHousesPhotoTemp> ChineseHousesPhotoTempQuery=> ChineseHousesPhotoTemp.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseHouseBase> ChineseHouseBase { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseHouseBase> ChineseHouseBaseQuery=> ChineseHouseBase.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<UserRoles> UserRoles { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<UserRoles> UserRolesQuery=> UserRoles.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseCommunities> ChineseCommunities { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseCommunities> ChineseCommunitiesQuery=> ChineseCommunities.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseHousesFeatures> ChineseHousesFeatures { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseHousesFeatures> ChineseHousesFeaturesQuery=> ChineseHousesFeatures.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseContractCope> ChineseContractCope { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseContractCope> ChineseContractCopeQuery=> ChineseContractCope.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseHousesModos> ChineseHousesModos { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseHousesModos> ChineseHousesModosQuery=> ChineseHousesModos.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseContractPerformance> ChineseContractPerformance { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseContractPerformance> ChineseContractPerformanceQuery=> ChineseContractPerformance.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseContractReceived> ChineseContractReceived { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseContractReceived> ChineseContractReceivedQuery=> ChineseContractReceived.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseHousesPhotos> ChineseHousesPhotos { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseHousesPhotos> ChineseHousesPhotosQuery=> ChineseHousesPhotos.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseHouseNew> ChineseHouseNew { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseHouseNew> ChineseHouseNewQuery=> ChineseHouseNew.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseHousesPhotosReview> ChineseHousesPhotosReview { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseHousesPhotosReview> ChineseHousesPhotosReviewQuery=> ChineseHousesPhotosReview.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseUserNotifications> ChineseUserNotifications { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseUserNotifications> ChineseUserNotificationsQuery=> ChineseUserNotifications.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseCustomerCollect> ChineseCustomerCollect { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseCustomerCollect> ChineseCustomerCollectQuery=> ChineseCustomerCollect.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseIMRecord> ChineseIMRecord { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseIMRecord> ChineseIMRecordQuery=> ChineseIMRecord.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseHouseResource> ChineseHouseResource { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseHouseResource> ChineseHouseResourceQuery=> ChineseHouseResource.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseHouseBaseTest> ChineseHouseBaseTest { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseHouseBaseTest> ChineseHouseBaseTestQuery=> ChineseHouseBaseTest.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Media> Media { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Media> MediaQuery=> Media.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseIMSendQueues> ChineseIMSendQueues { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseIMSendQueues> ChineseIMSendQueuesQuery=> ChineseIMSendQueues.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseIMToken> ChineseIMToken { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseIMToken> ChineseIMTokenQuery=> ChineseIMToken.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseHouseAppointment> ChineseHouseAppointment { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseHouseAppointment> ChineseHouseAppointmentQuery=> ChineseHouseAppointment.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseShopPosition> ChineseShopPosition { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseShopPosition> ChineseShopPositionQuery=> ChineseShopPosition.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseHouseRecordsFlowApproval> ChineseHouseRecordsFlowApproval { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseHouseRecordsFlowApproval> ChineseHouseRecordsFlowApprovalQuery=> ChineseHouseRecordsFlowApproval.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Features> Features { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Features> FeaturesQuery=> Features.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseSysFile> ChineseSysFile { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseSysFile> ChineseSysFileQuery=> ChineseSysFile.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseCustomerFollowUp> ChineseCustomerFollowUp { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseCustomerFollowUp> ChineseCustomerFollowUpQuery=> ChineseCustomerFollowUp.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<MlsContactRecordLog> MlsContactRecordLog { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<MlsContactRecordLog> MlsContactRecordLogQuery=> MlsContactRecordLog.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseJPushQueue> ChineseJPushQueue { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseJPushQueue> ChineseJPushQueueQuery=> ChineseJPushQueue.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseHouseFillInfo> ChineseHouseFillInfo { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseHouseFillInfo> ChineseHouseFillInfoQuery=> ChineseHouseFillInfo.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Properties> Properties { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Properties> PropertiesQuery=> Properties.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ShopDimissionRecord> ShopDimissionRecord { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ShopDimissionRecord> ShopDimissionRecordQuery=> ShopDimissionRecord.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseLandMarks> ChineseLandMarks { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseLandMarks> ChineseLandMarksQuery=> ChineseLandMarks.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseHouseRecordsPost> ChineseHouseRecordsPost { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseHouseRecordsPost> ChineseHouseRecordsPostQuery=> ChineseHouseRecordsPost.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<CompanyContacts> CompanyContacts { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<CompanyContacts> CompanyContactsQuery=> CompanyContacts.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<MLSApiRequestLog> MLSApiRequestLog { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<MLSApiRequestLog> MLSApiRequestLogQuery=> MLSApiRequestLog.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseDownloadCenter> ChineseDownloadCenter { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseDownloadCenter> ChineseDownloadCenterQuery=> ChineseDownloadCenter.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseCustomerShare> ChineseCustomerShare { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseCustomerShare> ChineseCustomerShareQuery=> ChineseCustomerShare.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseBid> ChineseBid { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseBid> ChineseBidQuery=> ChineseBid.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseHouseRecordsFiled> ChineseHouseRecordsFiled { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseHouseRecordsFiled> ChineseHouseRecordsFiledQuery=> ChineseHouseRecordsFiled.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseMobileHistory> ChineseMobileHistory { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseMobileHistory> ChineseMobileHistoryQuery=> ChineseMobileHistory.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<CompanyDomains> CompanyDomains { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<CompanyDomains> CompanyDomainsQuery=> CompanyDomains.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseCustomerShareFollowUp> ChineseCustomerShareFollowUp { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseCustomerShareFollowUp> ChineseCustomerShareFollowUpQuery=> ChineseCustomerShareFollowUp.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseMyFavorite> ChineseMyFavorite { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseMyFavorite> ChineseMyFavoriteQuery=> ChineseMyFavorite.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseSurroundingCities> ChineseSurroundingCities { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseSurroundingCities> ChineseSurroundingCitiesQuery=> ChineseSurroundingCities.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseShopJoin> ChineseShopJoin { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseShopJoin> ChineseShopJoinQuery=> ChineseShopJoin.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseNotifications> ChineseNotifications { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseNotifications> ChineseNotificationsQuery=> ChineseNotifications.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<SysDayCount> SysDayCount { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<SysDayCount> SysDayCountQuery=> SysDayCount.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseHouseRecordsFlow> ChineseHouseRecordsFlow { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseHouseRecordsFlow> ChineseHouseRecordsFlowQuery=> ChineseHouseRecordsFlow.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseProducts> ChineseProducts { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseProducts> ChineseProductsQuery=> ChineseProducts.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ServiceTypes> ServiceTypes { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ServiceTypes> ServiceTypesQuery=> ServiceTypes.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseCustomerDemandNew> ChineseCustomerDemandNew { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseCustomerDemandNew> ChineseCustomerDemandNewQuery=> ChineseCustomerDemandNew.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<HouseStaffSet> HouseStaffSet { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<HouseStaffSet> HouseStaffSetQuery=> HouseStaffSet.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseShopMember> ChineseShopMember { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseShopMember> ChineseShopMemberQuery=> ChineseShopMember.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ServicePrices> ServicePrices { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ServicePrices> ServicePricesQuery=> ServicePrices.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<HouseStatisticsToDay> HouseStatisticsToDay { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<HouseStatisticsToDay> HouseStatisticsToDayQuery=> HouseStatisticsToDay.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseRegion> ChineseRegion { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseRegion> ChineseRegionQuery=> ChineseRegion.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<HouseGroup> HouseGroup { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<HouseGroup> HouseGroupQuery=> HouseGroup.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseSmsQueue> ChineseSmsQueue { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseSmsQueue> ChineseSmsQueueQuery=> ChineseSmsQueue.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<CustomerDemandMatches> CustomerDemandMatches { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<CustomerDemandMatches> CustomerDemandMatchesQuery=> CustomerDemandMatches.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseShop> ChineseShop { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseShop> ChineseShopQuery=> ChineseShop.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<CompanyServiceTypes> CompanyServiceTypes { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<CompanyServiceTypes> CompanyServiceTypesQuery=> CompanyServiceTypes.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseFollowUp> ChineseFollowUp { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseFollowUp> ChineseFollowUpQuery=> ChineseFollowUp.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<HouseGroupByCompany> HouseGroupByCompany { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<HouseGroupByCompany> HouseGroupByCompanyQuery=> HouseGroupByCompany.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///消息模版表
		///</summary>
        public DbSet<MessageTemplate> MessageTemplate { get; set; } 

		///<summary>
		///消息模版表
		///</summary>
		public IQueryable<MessageTemplate> MessageTemplateQuery=> MessageTemplate.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseSmsValidate> ChineseSmsValidate { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseSmsValidate> ChineseSmsValidateQuery=> ChineseSmsValidate.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<TLDNames> TLDNames { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<TLDNames> TLDNamesQuery=> TLDNames.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<CompanyServicePrices> CompanyServicePrices { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<CompanyServicePrices> CompanyServicePricesQuery=> CompanyServicePrices.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseHousesCollect> ChineseHousesCollect { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseHousesCollect> ChineseHousesCollectQuery=> ChineseHousesCollect.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<PropertyContacts> PropertyContacts { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<PropertyContacts> PropertyContactsQuery=> PropertyContacts.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseTrade> ChineseTrade { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseTrade> ChineseTradeQuery=> ChineseTrade.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<PrintingTypes> PrintingTypes { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<PrintingTypes> PrintingTypesQuery=> PrintingTypes.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseHouseRecords> ChineseHouseRecords { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseHouseRecords> ChineseHouseRecordsQuery=> ChineseHouseRecords.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseHouseRecordsLog> ChineseHouseRecordsLog { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseHouseRecordsLog> ChineseHouseRecordsLogQuery=> ChineseHouseRecordsLog.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<PropertyFeatures> PropertyFeatures { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<PropertyFeatures> PropertyFeaturesQuery=> PropertyFeatures.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<PrintingDesigns> PrintingDesigns { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<PrintingDesigns> PrintingDesignsQuery=> PrintingDesigns.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseHousesShare> ChineseHousesShare { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseHousesShare> ChineseHousesShareQuery=> ChineseHousesShare.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<UserOpenPlatform> UserOpenPlatform { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<UserOpenPlatform> UserOpenPlatformQuery=> UserOpenPlatform.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseCustomerContacts> ChineseCustomerContacts { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseCustomerContacts> ChineseCustomerContactsQuery=> ChineseCustomerContacts.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<PropertyGarageTypes> PropertyGarageTypes { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<PropertyGarageTypes> PropertyGarageTypesQuery=> PropertyGarageTypes.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseViewHistory> ChineseViewHistory { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseViewHistory> ChineseViewHistoryQuery=> ChineseViewHistory.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<EmailTemplates> EmailTemplates { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<EmailTemplates> EmailTemplatesQuery=> EmailTemplates.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseTradeHistory> ChineseTradeHistory { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseTradeHistory> ChineseTradeHistoryQuery=> ChineseTradeHistory.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<WeiXinBasicInfo> WeiXinBasicInfo { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<WeiXinBasicInfo> WeiXinBasicInfoQuery=> WeiXinBasicInfo.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<PropertyPhotos> PropertyPhotos { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<PropertyPhotos> PropertyPhotosQuery=> PropertyPhotos.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<PropertyPrints> PropertyPrints { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<PropertyPrints> PropertyPrintsQuery=> PropertyPrints.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<PropertyTest> PropertyTest { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<PropertyTest> PropertyTestQuery=> PropertyTest.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseHousesShareFollowUp> ChineseHousesShareFollowUp { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseHousesShareFollowUp> ChineseHousesShareFollowUpQuery=> ChineseHousesShareFollowUp.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ShareRecord> ShareRecord { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ShareRecord> ShareRecordQuery=> ShareRecord.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<PropertyTypes> PropertyTypes { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<PropertyTypes> PropertyTypesQuery=> PropertyTypes.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<InquiryTest> InquiryTest { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<InquiryTest> InquiryTestQuery=> InquiryTest.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<VisitorBehaviorRecord> VisitorBehaviorRecord { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<VisitorBehaviorRecord> VisitorBehaviorRecordQuery=> VisitorBehaviorRecord.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseTradeItemLocation> ChineseTradeItemLocation { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseTradeItemLocation> ChineseTradeItemLocationQuery=> ChineseTradeItemLocation.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<EmailQueue> EmailQueue { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<EmailQueue> EmailQueueQuery=> EmailQueue.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<PropertyPrintPhotos> PropertyPrintPhotos { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<PropertyPrintPhotos> PropertyPrintPhotosQuery=> PropertyPrintPhotos.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<VisitorBringRecord> VisitorBringRecord { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<VisitorBringRecord> VisitorBringRecordQuery=> VisitorBringRecord.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseUserFavorite> ChineseUserFavorite { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseUserFavorite> ChineseUserFavoriteQuery=> ChineseUserFavorite.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseTradeQuestionSubmit> ChineseTradeQuestionSubmit { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseTradeQuestionSubmit> ChineseTradeQuestionSubmitQuery=> ChineseTradeQuestionSubmit.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseReport> ChineseReport { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseReport> ChineseReportQuery=> ChineseReport.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<LanguageData> LanguageData { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<LanguageData> LanguageDataQuery=> LanguageData.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseHouseKey> ChineseHouseKey { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseHouseKey> ChineseHouseKeyQuery=> ChineseHouseKey.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<WechatUserInfo> WechatUserInfo { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<WechatUserInfo> WechatUserInfoQuery=> WechatUserInfo.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<HouesQuestion> HouesQuestion { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<HouesQuestion> HouesQuestionQuery=> HouesQuestion.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChineseAdZone> ChineseAdZone { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChineseAdZone> ChineseAdZoneQuery=> ChineseAdZone.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Languages> Languages { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Languages> LanguagesQuery=> Languages.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<PropertyPrintContacts> PropertyPrintContacts { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<PropertyPrintContacts> PropertyPrintContactsQuery=> PropertyPrintContacts.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<WechatBasicInfo> WechatBasicInfo { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<WechatBasicInfo> WechatBasicInfoQuery=> WechatBasicInfo.AsNoTracking().AsQueryable();			
 	 	 

	}
} 
