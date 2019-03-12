using System;
using System.Collections.Generic;
using System.Text;

namespace MVVMBasicPJK.Models
{
    public class LoginMo : BaseDataObject
    {
        public string Account { get; set; } //USERID

        public string Name { get; set; }    //USERNAME

        public string StaffYN { get; set; }

        public string TypeOfUser { get; set; }

        public string Etc { get; set; }

        string companyCode = string.Empty;
        public string CompanyCode
        {
            get { return companyCode; }
            set { SetProperty(ref companyCode, value); }
        }

        string companyName = string.Empty;
        public string CompanyName
        {
            get { return companyName; }
            set { SetProperty(ref companyName, value); }
        }

        public string CompanyType { get; set; }

        public string ConnNum { get; set; }

        public string TelNum { get; set; }  //TelNum

        public string Email { get; set; }

        public string FaxNum { get; set; }

        public string HpNum { get; set; }

        public string GroupAuthoriry { get; set; }

        public string ManagerID { get; set; }

        public string ApproverName { get; set; }

        public string IsManager { get; set; }

        public string OfficeSCode { get; set; } //OfficeCode

        public string OfficeName { get; set; }

        public string OrgSCode { get; set; }

        public string OrgName { get; set; }

        public string DeptCode { get; set; }

        public string UserIP { get; set; }

        public string IPExceptionYN { get; set; }

        public string EmergencyYN { get; set; }

        public string ReturnCD { get; set; }

        public string ReturnMsg { get; set; }

        public string LoginEnvir { get; set; }

        public string SmsEnableYN { get; set; }

        public string DeployVersion { get; set; }

        public string compareMac { get; set; }

        public string NonWorkingDayYN { get; set; }

        public string Duty_CD { get; set; }

        public string LastLoginDT { get; set; }

        string securityUnMaskingYN = string.Empty;
        public string SecurityUnMaskingYN
        {
            get { return securityUnMaskingYN; }
            set { SetProperty(ref securityUnMaskingYN, value); }
        }

        public string IsNeOSSTokenExceptUser { get; set; }

        public string NeOSSToken { get; set; }

        //설정 정보 항목
        string unMaskingOnOff = string.Empty;
        public string UnMaskingOnOff
        {
            get { return unMaskingOnOff; }
            set { SetProperty(ref unMaskingOnOff, value); }
        }

    }
}
