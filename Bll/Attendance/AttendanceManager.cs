using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Dto.Attendance;
using Dal;
using Modobay;
using Microsoft.EntityFrameworkCore;

namespace Bll.Attendance
{
    //[Modobay.Api.NonController]
    [System.ComponentModel.Description("考勤管理")]
    public class AttendanceManager : IAttendanceManager
    {
        private readonly MLSDbContext db;
        private readonly IAppContext app;

        public AttendanceManager(Dal.MLSDbContext dbContext, Modobay.IAppContext appContext)
        {
            this.db = dbContext;
            this.app = appContext;
        }

        #region 无效逻辑

        //public List<AttendanceRemindDto> GetAttendanceRemindList()
        //{
        //    //var now = DateTime.Now;
        //    //var now = DateTime.Parse("2020-02-20 08:50:01");
        //    var now = DateTime.Parse("2020-02-20 18:05:01");
        //    var checkInDateS = DateTime.Parse(now.AddMinutes(10).ToString("yyyy-MM-dd HH:mm:00"));
        //    var checkInDateE = checkInDateS.AddMinutes(1).AddSeconds(-1);
        //    var checkOutDateS = DateTime.Parse(now.AddMinutes(-5).ToString("yyyy-MM-dd HH:mm:00"));
        //    var checkOutDateE = checkOutDateS.AddMinutes(1).AddSeconds(-1);
        //    var query = GetAttendanceCheckInRemindQuery(1, checkInDateS, checkInDateE);
        //    query = query.Concat(GetAttendanceCheckInRemindQuery(2, checkOutDateS, checkOutDateE));
        //    return query.ToList();
        //}

        //private IQueryable<AttendanceRemindDto> GetAttendanceCheckInRemindQuery(int checkTypeValue,DateTime remindDateS,DateTime remindDateE)
        //{
        //    //Dto.QueryDto.Today(out var dateS, out var dateE);
        //    var query = from attendance in db.Attendance_ClockInsQuery
        //                where attendance.UserCheckTime == null && attendance.CheckType == checkTypeValue
        //                && attendance.IsWorkDate == true && attendance.IsLeave == false
        //                //&& (attendance.CreateTime >= dateS && attendance.CreateTime <= dateE)
        //                && (attendance.WorkTime >= remindDateS && attendance.WorkTime <= remindDateE)
        //                select new AttendanceRemindDto
        //                {
        //                    UserId = attendance.UserId,
        //                    CheckType = attendance.CheckType
        //                };
        //    return query;
        //}

        #endregion

        public List<AttendanceRemindDto> GetAttendanceRemindList(string now)
        {
            // todo pxg 优化点1：增加日期条件，减少匹配的考勤班次记录
            // todo pxg 优化点2：考勤使用人数多时，数据量大
            Dto.QueryDto.Today(out var dateS, out var dateE);
            var query = from userGroup in db.Attendance_UserGroupQuery
                        join attendanceGroup in db.Attendance_GroupQuery on userGroup.AG_Id equals attendanceGroup.Id into g
                        from attendanceGroup in g.DefaultIfEmpty()
                        join attendanceClass in db.Attendance_ClassQuery on attendanceGroup.Id equals attendanceClass.AG_Id into c
                        from attendanceClass in c.DefaultIfEmpty()
                        where userGroup.IsWorkDate == true 
                        && (userGroup.Date >= dateS && userGroup.Date <= dateE)
                        select new 
                        {
                            userGroup.UserId,
                            ClassId = attendanceClass.Id,
                            attendanceClass.OnDutyTime,
                            attendanceClass.OffDutyTime,
                        };

            var userTimeList = query.ToList();
            var timeGroupList = userTimeList.GroupBy(x => new { x.ClassId,x.OnDutyTime, x.OffDutyTime }).ToList();
            var list = new List<AttendanceRemindDto>();
            var date = now.Substring(0, 10);

            foreach (var timeGroup in timeGroupList)
            {
                var classId = timeGroup.Key.ClassId;                
                var checkInDate = DateTime.Parse(date + " " +  timeGroup.Key.OnDutyTime);
                var checkInDateS = DateTime.Parse(DateTime.Parse(now).AddMinutes(10).ToString("yyyy-MM-dd HH:mm:00"));
                var checkInDateE = checkInDateS.AddMinutes(1).AddSeconds(-1);
                if (checkInDate >= checkInDateS && checkInDate <= checkInDateE)
                {
                    var checkedInUserIdList = GetCheckedUserIdList(1, classId, date);                  
                    var userList = userTimeList.Where(x => x.ClassId == classId && !checkedInUserIdList.Contains(x.UserId))
                                    .Select(x => new AttendanceRemindDto { UserId = x.UserId, CheckType = 1 }).ToList();
                    list.AddRange(userList);
                }

                var checkOutDate = DateTime.Parse(date + " " + timeGroup.Key.OffDutyTime);
                var checkOutDateS = DateTime.Parse(DateTime.Parse(now).AddMinutes(-5).ToString("yyyy-MM-dd HH:mm:00"));
                var checkOutDateE = checkOutDateS.AddMinutes(1).AddSeconds(-1);
                if (checkOutDate >= checkOutDateS && checkOutDate <= checkOutDateE)
                {
                    var checkedInUserIdList = GetCheckedUserIdList(2, classId, date);
                    var userList = userTimeList.Where(x => x.ClassId == classId && !checkedInUserIdList.Contains(x.UserId))
                                    .Select(x => new AttendanceRemindDto { UserId = x.UserId, CheckType = 2 }).ToList();
                    list.AddRange(userList);
                }
            }

            return list;
        }

        private List<long> GetCheckedUserIdList(int checkTypeValue,int classId,string date)
        {
            //var sql = "select UserId as Id from Attendance_ClockIns where UserCheckTime>0 and CheckType={0} and AC_Id={1} and WorkDate='2020-02-21' ";
            //var idList = db.IdDto.FromSqlRaw(sql, checkTypeValue, classId, date);
            var sql = string.Format("select UserId as Id from Attendance_ClockIns where UserCheckTime>0 and CheckType={0} and AC_Id={1} and WorkDate='{2}' ", checkTypeValue, classId, date);
            var idList = db.IdDto.FromSqlRaw(sql);
            return idList.Select(x => x.Id).ToList();
            //var query = from attendance in db.Attendance_ClockInsQuery
            //            where attendance.UserCheckTime != null && attendance.CheckType == checkTypeValue
            //            && attendance.IsWorkDate == true && attendance.IsLeave == false && attendance.AC_Id == classId
            //            && (attendance.CreateTime >= dateS && attendance.CreateTime <= dateE)
            //            select attendance;
            //return query.Select(x => x.UserId).ToList();
        }
    }
}
