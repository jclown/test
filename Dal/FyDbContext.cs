 
 







  
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Model;  

namespace Dal
{
    /// <summary>
    /// 
    /// </summary>
	public partial class FyDbContext : DbContextBase
	{ 

		public FyDbContext(DbContextOptions<FyDbContext> options) : base(options)
        {
            
        }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            base.OnConfiguring(optionsBuilder);
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
 
			modelBuilder.Entity<District>().HasKey(t => new { t.CityName,t.DistrictName });
 
			modelBuilder.Entity<InquiryBook>().HasKey(t => new { t.InquiryID,t.EmpID });
 
			modelBuilder.Entity<PropertyBook>().HasKey(t => new { t.PropertyID,t.EmpID });
 
			OnModelCreatingExt(modelBuilder);
			base.OnModelCreating(modelBuilder);
		}

  
        ///<summary>
		///-
		///</summary>
        public DbSet<ContractComm> ContractComm { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ContractComm> ContractCommQuery=> ContractComm.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Developer> Developer { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Developer> DeveloperQuery=> Developer.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<PropertyData> PropertyData { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<PropertyData> PropertyDataQuery=> PropertyData.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<PropertyInquiry> PropertyInquiry { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<PropertyInquiry> PropertyInquiryQuery=> PropertyInquiry.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<EmpFollow> EmpFollow { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<EmpFollow> EmpFollowQuery=> EmpFollow.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<FlowType> FlowType { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<FlowType> FlowTypeQuery=> FlowType.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<BlackList> BlackList { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<BlackList> BlackListQuery=> BlackList.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<FlowDef> FlowDef { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<FlowDef> FlowDefQuery=> FlowDef.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Guide> Guide { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Guide> GuideQuery=> Guide.AsNoTracking().AsQueryable();			
 	 	 
        ///<summary>
		///-
		///</summary>
        public DbSet<Photo> Photo { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Photo> PhotoQuery=> Photo.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<SMS> SMS { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<SMS> SMSQuery=> SMS.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Flow> Flow { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Flow> FlowQuery=> Flow.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Phrase> Phrase { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Phrase> PhraseQuery=> Phrase.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Advertisement> Advertisement { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Advertisement> AdvertisementQuery=> Advertisement.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Schedule> Schedule { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Schedule> ScheduleQuery=> Schedule.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Inquiry> Inquiry { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Inquiry> InquiryQuery=> Inquiry.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Roll> Roll { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Roll> RollQuery=> Roll.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Message> Message { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Message> MessageQuery=> Message.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Follow2> Follow2 { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Follow2> Follow2Query=> Follow2.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Position> Position { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Position> PositionQuery=> Position.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<InquiryFollow2> InquiryFollow2 { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<InquiryFollow2> InquiryFollow2Query=> InquiryFollow2.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Target> Target { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Target> TargetQuery=> Target.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Find> Find { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Find> FindQuery=> Find.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ContractFollow> ContractFollow { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ContractFollow> ContractFollowQuery=> ContractFollow.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<School> School { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<School> SchoolQuery=> School.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Rent> Rent { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Rent> RentQuery=> Rent.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Plan> Plan { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Plan> PlanQuery=> Plan.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Question> Question { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Question> QuestionQuery=> Question.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<SchoolEstate> SchoolEstate { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<SchoolEstate> SchoolEstateQuery=> SchoolEstate.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<SurveyQuestion> SurveyQuestion { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<SurveyQuestion> SurveyQuestionQuery=> SurveyQuestion.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Property> Property { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Property> PropertyQuery=> Property.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Prepare> Prepare { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Prepare> PrepareQuery=> Prepare.AsNoTracking().AsQueryable();			
 	 	 
  
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
        public DbSet<Area> Area { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Area> AreaQuery=> Area.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Building> Building { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Building> BuildingQuery=> Building.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ChinesePY> ChinesePY { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ChinesePY> ChinesePYQuery=> ChinesePY.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<City> City { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<City> CityQuery=> City.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<InquirySchedule> InquirySchedule { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<InquirySchedule> InquiryScheduleQuery=> InquirySchedule.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Comment> Comment { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Comment> CommentQuery=> Comment.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Contract> Contract { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Contract> ContractQuery=> Contract.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Department> Department { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Department> DepartmentQuery=> Department.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<District> District { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<District> DistrictQuery=> District.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Trust> Trust { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Trust> TrustQuery=> Trust.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<FAQ> FAQ { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<FAQ> FAQQuery=> FAQ.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Follow> Follow { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Follow> FollowQuery=> Follow.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<TrustCon> TrustCon { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<TrustCon> TrustConQuery=> TrustCon.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Employee> Employee { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Employee> EmployeeQuery=> Employee.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<InquiryBook> InquiryBook { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<InquiryBook> InquiryBookQuery=> InquiryBook.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<InquiryFollow> InquiryFollow { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<InquiryFollow> InquiryFollowQuery=> InquiryFollow.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Map> Map { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Map> MapQuery=> Map.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<News> News { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<News> NewsQuery=> News.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<TrustAct> TrustAct { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<TrustAct> TrustActQuery=> TrustAct.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Note> Note { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Note> NoteQuery=> Note.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<PropertyBook> PropertyBook { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<PropertyBook> PropertyBookQuery=> PropertyBook.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Trent> Trent { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Trent> TrentQuery=> Trent.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Reference> Reference { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Reference> ReferenceQuery=> Reference.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Fee> Fee { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Fee> FeeQuery=> Fee.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ExchangeLog> ExchangeLog { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ExchangeLog> ExchangeLogQuery=> ExchangeLog.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<SysSet> SysSet { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<SysSet> SysSetQuery=> SysSet.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Complain> Complain { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Complain> ComplainQuery=> Complain.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<TrentCon> TrentCon { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<TrentCon> TrentConQuery=> TrentCon.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<TrentAct> TrentAct { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<TrentAct> TrentActQuery=> TrentAct.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Rule> Rule { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Rule> RuleQuery=> Rule.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Survey> Survey { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Survey> SurveyQuery=> Survey.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<SysUser> SysUser { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<SysUser> SysUserQuery=> SysUser.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<LogClient> LogClient { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<LogClient> LogClientQuery=> LogClient.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Intro> Intro { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Intro> IntroQuery=> Intro.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ContractFee> ContractFee { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ContractFee> ContractFeeQuery=> ContractFee.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<SpareRequest> SpareRequest { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<SpareRequest> SpareRequestQuery=> SpareRequest.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Spare> Spare { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Spare> SpareQuery=> Spare.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Honest> Honest { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Honest> HonestQuery=> Honest.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<Report> Report { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<Report> ReportQuery=> Report.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ContractCon> ContractCon { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ContractCon> ContractConQuery=> ContractCon.AsNoTracking().AsQueryable();			
 	 	 
  
        ///<summary>
		///-
		///</summary>
        public DbSet<ContractAct> ContractAct { get; set; } 

		///<summary>
		///-
		///</summary>
		public IQueryable<ContractAct> ContractActQuery=> ContractAct.AsNoTracking().AsQueryable();			
 	 	 

	}
} 
