using System;
using System.Collections.Generic;
using System.Text;
using Dto.Attendance;

namespace Bll.Attendance
{
    public interface IAttendanceManager
    {
        List<AttendanceRemindDto> GetAttendanceRemindList(string now);
    }
}
