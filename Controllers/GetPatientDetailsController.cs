using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Stikers.Controllers
{
    public class GetPatientDetailsController : ApiController
    {
        public class PersonalDetails
        {
            public string PatientNumber { get; set; }
            public string DateIn { get; set; }
            public string TimeIn { get; set; }
            public string FirstName { get; set; }
            public string FatherName { get; set; }
            public string LastName { get; set; }
            public string PersonID { get; set; }
            public string DOB { get; set; }
            public string Gender { get; set; }
            public string PhoneNumber { get; set; }
            public string Email { get; set; }
            public string Address { get; set; }
            public string CaseNumber { get; set; }
            public string Depart { get; set; }
            public string DepartDescription { get; set; }
            public string SeodeDepart { get; set; }
            public string SeodeDepartDescription { get; set; }
            public string DischargeDate { get; set; }
            public string InDate { get; set; }
            public string DischargeTime { get; set; }
            public string InTime { get; set; }
            public string Age { get; set; }
            public string PASSNR { get; set; }
            public string ZNE_FNAMEZZ { get; set; }
            public string ZNE_LNAMEZZ { get; set; }
            public string DOTIM { get; set; }
            public string DODAT { get; set; }
            public string SurgeryID { get; set; }
            public string Kupa { get; set; }
            public string KupaName { get; set; }
        }

        // GET api/values/5
        SqlConnection cnn;
        string connetionString = "Data Source=******;Initial Catalog=****;User ID=****;Password=***;";


        public PersonalDetails GetPersonalDetailsFromNPAT(string EXTNR )
        {
            PersonalDetails mPersonalDetails = new PersonalDetails();
            cnn = new SqlConnection(connetionString);
            cnn.Open();

            EXTNR = EXTNR.Replace("-", "");
            string Query = "SELECT [PATNR], [GSCHL], [NNAME], [GBNAM], [VNAME], FORMAT([GBDAT], 'yyyy-MM-dd') GBDAT, [EXTNR], DATEDIFF(hour, [GBDAT], getdate())/8766 Age  FROM [dbo].[NPAT]  WHERE CONVERT(bigint, REPLACE([EXTNR], '-','')) = CONVERT(bigint, REPLACE('" + EXTNR + "', '-',''))";

            SqlCommand commandDropDownsOptions = new SqlCommand(Query, cnn);
            commandDropDownsOptions.CommandTimeout = 360;
            SqlDataReader dataDropDownsOptions = commandDropDownsOptions.ExecuteReader();
            if (dataDropDownsOptions.HasRows)
            {
                while (dataDropDownsOptions.Read())
                {
                    mPersonalDetails.CaseNumber = "";
                    mPersonalDetails.Depart = "";
                    mPersonalDetails.DepartDescription = "";
                    mPersonalDetails.SeodeDepart = "";
                    mPersonalDetails.SeodeDepartDescription = "";
                    mPersonalDetails.PatientNumber = dataDropDownsOptions["PATNR"].ToString().Trim();
                    if (dataDropDownsOptions["GSCHL"].ToString().Trim() == "2")
                    {
                        mPersonalDetails.Gender = "נ";
                    }
                    else
                    {
                        mPersonalDetails.Gender = "ז";
                    }
                    mPersonalDetails.LastName = dataDropDownsOptions["NNAME"].ToString().Trim();
                    mPersonalDetails.FirstName = dataDropDownsOptions["VNAME"].ToString().Trim();
                    mPersonalDetails.FatherName = dataDropDownsOptions["GBNAM"].ToString().Trim();
                    mPersonalDetails.DOB = dataDropDownsOptions["GBDAT"].ToString().Trim();
                    mPersonalDetails.InTime = "";
                    mPersonalDetails.InDate = "";
                    mPersonalDetails.Age = dataDropDownsOptions["Age"].ToString().Trim();
                    mPersonalDetails.PersonID = dataDropDownsOptions["EXTNR"].ToString().Trim();
                    mPersonalDetails.PhoneNumber = "";
                    mPersonalDetails.Address = "";
                }
            }
            cnn.Close();
            return mPersonalDetails;
        }
    }
}