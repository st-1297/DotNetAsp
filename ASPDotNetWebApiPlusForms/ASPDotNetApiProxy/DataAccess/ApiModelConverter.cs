using ASPDotNetApiProxy.ApiModels;
using ASPDotNetApiProxy.DbModels;
using System;

namespace ASPDotNetApiProxy.DataAccess
{
    /// <summary>
    /// APIモデルとDBモデルの変換機能を提供します。
    /// </summary>
    internal static class ApiModelConverter
    {

        /// <summary>
        /// リクエストトークンをAPIモデルに変換します。
        /// </summary>
        /// <param name="dbModel">データベースモデル</param>
        /// <returns>APIモデル</returns>
        public static ApiModels.RequestToken ToApiModel(this T_REQUEST_TOKEN dbModel)
        {
            if (dbModel == null)
                throw new ArgumentNullException("dbModel");

            return new RequestToken
            {
                Value = dbModel.VALUE,
                CreationTime = dbModel.CREATION_TIME.Value,
                ExpirationTime = dbModel.EXPIRATION_TIME.Value,
            };
        }

        /// <summary>
        /// アクセストークンをAPIモデルに変換します。
        /// </summary>
        /// <param name="dbModel">データベースモデル</param>
        /// <returns>APIモデル</returns>
        public static AccessToken ToApiModel(this T_ACCESS_TOKEN dbModel)
        {
            if (dbModel == null)
                throw new ArgumentNullException("dbModel");

            return new AccessToken
            {
                Value = dbModel.VALUE,
                UserId = dbModel.USR_ID,
                CreationTime = dbModel.CREATION_TIME.Value,
                ExpirationTime = dbModel.EXPIRATION_TIME.Value,
            };
        }

        /// <summary>
        /// 社員/部署/役職マスターを社員APIモデルに変換します。
        /// </summary>
        /// <param name="user">社員</param>
        /// <param name="section">部署</param>
        /// <param name="position">役職</param>
        /// <returns>社員APIモデル</returns>
        //public static Employee ToEmployee(NSM_M_AC_USR user, NSM_M_SECTION section, NSM_M_POSITION position)
        //{
        //    if (user == null) throw new ArgumentNullException("user");
        //    if (section == null) throw new ArgumentNullException("section");
        //    if (position == null) throw new ArgumentNullException("position");

        //    return new Employee
        //    {
        //        Id = user.USR_ID,
        //        Name = user.USR_NM,
        //        Section = section.ToApiModel(),
        //        Position = position.ToApiModel(),
        //    };
        //}


        /// <summary>
        /// 部署マスターをAPIモデルに変換します。
        /// </summary>
        /// <param name="dbModel">データベースモデル</param>
        /// <returns>APIモデル</returns>
        //public static Section ToApiModel(this NSM_M_SECTION dbModel)
        //{
        //    if (dbModel == null)
        //        throw new ArgumentNullException("dbModel");

        //    return new Section
        //    {
        //        Id = dbModel.SECTION_ID,
        //        Name = dbModel.SECTION_NM,
        //        Category = (SectionCategory)dbModel.SECTION_KBN,
        //        DisplayOrder = dbModel.DSP_ODR,
        //    };
        //}


        /// <summary>
        /// 部署マスターをAPIモデルに変換します。
        /// </summary>
        /// <param name="dbModel">データベースモデル</param>
        /// <returns>APIモデル</returns>
        //public static Position ToApiModel(this NSM_M_POSITION dbModel)
        //{
        //    if (dbModel == null)
        //        throw new ArgumentNullException("dbModel");

        //    return new Position
        //    {
        //        Id = dbModel.POSITION_ID,
        //        Name = dbModel.POSITION_NM,
        //        DisplayOrder = dbModel.DSP_ODR,
        //    };
        //}




        //public static T ToApiModel<T>(this AC_COM_BUSINESS dbModel)
        //{
        //    T t = default(T);
        //    try
        //    {

        //        if (dbModel == null)
        //            throw new ArgumentNullException("dbModel");
        //        if (typeof(T) == typeof(BusinessInfo))
        //        {
        //            return (T)Convert.ChangeType(new BusinessInfo
        //            {
        //                LockUserNm = dbModel.LOCK_USER_NM,
        //                ReceiptId = dbModel.RECEIPT == null ? (Int32?)null : dbModel.RECEIPT.RECEIPT_ID,
        //                AplId = dbModel.APPLICATION == null ? (Int32?)null : dbModel.APPLICATION.APL_ID,
        //                RouteName = dbModel.ROUTE == null ? null : dbModel.ROUTE.ROUTE_DSP_NM,
        //                SalesPgs = dbModel.SALES_PROGRESS_NM,
        //                ManagementPgs = dbModel.MANAGE_PROGRESS_NM,
        //                ContactDts = dbModel.RECEIPT == null ? null : dbModel.RECEIPT.CONTACT_DTS.DateTimeToString(Constant.YYYYMMDDHHMMSS),
        //                AplDts = dbModel.APPLICATION == null ? null : dbModel.APPLICATION.APL_DTS.DateTimeToString(Constant.YYYYMMDD),
        //                NextCallDt = dbModel.NEXT_CALL_DT,
        //                SalesCompany = dbModel.COMPANY == null ? null : dbModel.COMPANY.SALES_COMPANY_DSP_NM,
        //                Departmant = dbModel.ROUTE_USR == null ? null : dbModel.ROUTE_USR.SECTION_ID.ToString(),
        //                ContractorNm = dbModel.CONTRACTOR_NAME,
        //                LatestOpeDts = dbModel.LATESTOPEDTS,
        //                ContractorType = dbModel.APPLICATION == null ? null : dbModel.APPLICATION.AGR_TYPE == null ? null : dbModel.APPLICATION.AGR_TYPE.Int32ToString().Equals("0") ? "個人" : "法人",// 契約者タイプ
        //                ReqPgs = dbModel.REQUEST_PROGRESS == null ? null : dbModel.REQUEST_PROGRESS.PROGRESS_NM,
        //                RequestDtlNm = dbModel.REQUEST_DTL_NM,
        //                CreateDts = dbModel.CREATE_DTS,
        //                LimitDts = dbModel.LIMIT_DTS,
        //                CreateNm = dbModel.CREATENM,
        //                CauseNm = dbModel.CAUSENAME,
        //                ContactPersonnel = dbModel.CONTACT_PERSONNEL,
        //                AcquireNm = dbModel.USR_NM,
        //                CallCnt = dbModel.CALLCNT,
        //                GroupNm = dbModel.GROUP_NM,
        //                ReqType = dbModel.REQUEST_CLS == null ? null : dbModel.REQUEST_CLS.REQUEST_CLS_NM,
        //                OwnerNm = dbModel.OWNNER_NAME,
        //                OwnerContactAdr = dbModel.CONTACT_ADDR == null ? null : dbModel.CONTACT_ADDR.PHONE_NUM,
        //                Staff = dbModel.STAFF,
        //                Group = dbModel.NEXTCALL == null ? null : dbModel.NEXTCALL.CALL_GROUP,
        //                FletsSchema = dbModel.FLETS_SCHEMA,
        //                KojiDate = dbModel.KOJI_DATE,
        //                TeamFlg = dbModel.TEAM_FLG,
        //            }, typeof(T));
        //        }
        //        else if (typeof(T) == typeof(InitializeInfo))
        //        {
        //            //TODO:
        //            return (T)Convert.ChangeType(new InitializeInfo
        //            {
        //                Time = dbModel.NEXT_CALL_DT,
        //            }, typeof(T));
        //        }
        //    }
        //    catch
        //    {
        //        return t;
        //    }
        //    return t;

        //}




    }
}
