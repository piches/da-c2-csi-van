using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CsiApi
{
    public partial class PhoneCall
    {
        [Key]
        public long PhoneCallId { get; set; }
        public long? TargetPhoneId { get; set; }
        public long? ContactPhoneId { get; set; }
        public string CallDirection { get; set; }
        public string CallType { get; set; }
        public string CallStartDateTime { get; set; }
        public int? CallDuration { get; set; }
        public string StartCellTowerAddress { get; set; }
        public double? StartCellTowerLatitude { get; set; }
        public double? StartCellTowerLongitude { get; set; }
        public string EndCellTowerAddress { get; set; }
        public double? EndCellTowerLatitude { get; set; }
        public double? EndCellTowerLongitude { get; set; }

        public DateTime CallStartTime
        {
            get
            {

                
                DateTime dateTime = new DateTime(1900, 1, 1);
                
                var parts = CallStartDateTime.Split('.',2, StringSplitOptions.None);
                if(parts.Length == 2)
                {
                    var date = parts[0]; 
                    var time = "0." + parts[1]; 
                    if(time.Length > 6)
                        time = time.Substring(0, 6);
                    var dateInt = 0;
                    double timeVal;

                    if(int.TryParse(date, out dateInt))
                        dateTime = dateTime.AddDays(dateInt);

                    if(double.TryParse(time, out timeVal))
                    {
                        var noOfSeconds = timeVal * (24*60*60);
                        dateTime = dateTime.AddSeconds(noOfSeconds);
                    }
                }

               
                return dateTime;

            }
        }
    }
}
