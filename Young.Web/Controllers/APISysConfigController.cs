using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Mvc;
using Young.Web.Models;
using Young.Web.Models.EasyUIView;

namespace Young.Web.Controllers
{
    public class APISysConfigController : ApiController
    {
        /// <summary>
        /// 获取系统配置信息
        /// </summary>
        /// <returns></returns>
        public PropertyGridModel GetSysConfigPropertyGrid()
        {
            var model = new SysConfigModel();
            PropertyGridModel pgModel = new PropertyGridModel();
            var propList = new[]
                {
                    new PropertyGridRowModel
                        {
                            group = "Membership",
                            name = GetModelDisplay(f => f.MembershipApplicationName),
                            value = model.MembershipApplicationName
                        },
                    new PropertyGridRowModel
                        {
                            group = "Membership",
                            name = GetModelDisplay(f => f.MembershipEnablePasswordReset),
                            value = model.MembershipEnablePasswordReset.ToString()
                        },
                    new PropertyGridRowModel
                        {
                            group = "Membership",
                            name = GetModelDisplay(f => f.MembershipEnablePasswordRetrieval),
                            value = model.MembershipEnablePasswordRetrieval.ToString()
                        },
                    new PropertyGridRowModel
                        {
                            group = "Membership",
                            name = GetModelDisplay(f => f.MembershipMaxInvalidPasswordAttempts),
                            value = model.MembershipMaxInvalidPasswordAttempts.ToString()
                        },
                    new PropertyGridRowModel
                        {
                            group = "Membership",
                            name = GetModelDisplay(f => f.MembershipMinRequiredNonAlphanumericCharacters),
                            value = model.MembershipMinRequiredNonAlphanumericCharacters.ToString()
                        },
                    new PropertyGridRowModel
                        {
                            group = "Membership",
                            name = GetModelDisplay(f => f.MembershipMinRequiredPasswordLength),
                            value = model.MembershipMinRequiredPasswordLength.ToString()
                        },
                    new PropertyGridRowModel
                        {
                            group = "Membership",
                            name = GetModelDisplay(f => f.MembershipPasswordAttemptWindow),
                            value = model.MembershipPasswordAttemptWindow.ToString()
                        },
                    new PropertyGridRowModel
                        {
                            group = "Membership",
                            name = GetModelDisplay(f => f.MembershipPasswordStrengthRegularExpression),
                            value = model.MembershipPasswordStrengthRegularExpression
                        },
                    new PropertyGridRowModel
                        {
                            group = "Membership",
                            name = GetModelDisplay(f => f.MembershipRequiresQuestionAndAnswer),
                            value = model.MembershipRequiresQuestionAndAnswer.ToString()
                        },
                    new PropertyGridRowModel
                        {
                            group = "Membership",
                            name = GetModelDisplay(f => f.MembershipUserIsOnlineTimeWindow),
                            value = model.MembershipUserIsOnlineTimeWindow.ToString()
                        }
                };



            pgModel.rows.AddRange(propList);
            pgModel.total = pgModel.rows.Count;
            return pgModel;
        }

        private string GetModelDisplay(Expression<Func<SysConfigModel, object>> expression)
        {
            var model = new SysConfigModel();
            var sysCongifModel = expression.Parameters[0].Name;

            var propName =
                new Regex("(?<=" + sysCongifModel + @"\.)([a-zA-z_]+)",
                          RegexOptions.Singleline | RegexOptions.ExplicitCapture).Match(expression.Body.ToString())
                                                                                 .Value;
            var prop = model.GetType().GetProperty(propName);
            var objs = prop.GetCustomAttributes(typeof (DisplayAttribute), false);
            if (objs.Any())
            {
                return ((DisplayAttribute) objs[0]).Name;
            }
            return string.Empty;

        }
    }
}
