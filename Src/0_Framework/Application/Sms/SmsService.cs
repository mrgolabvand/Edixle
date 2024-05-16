using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using PayamakPanel.Core;
//using SmsIrRestful;

namespace _0_Framework.Application.Sms
{
    public class SmsService : ISmsService
    {
        private readonly IConfiguration _configuration;

        public SmsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendWithMeliPayamak_Com(string number, string message)
        {
            string userName, password;
            GetUserNameAndPassword(out userName, out password);

            var fara = new FaraApi();
            var userNumberList = fara.GetUserNumberList(userName, password);
            var from = userNumberList.Data.Last().Number;
            var result = fara.SendSms(userName, password, from, number, message);

            if (result.RetStatus == 1) return;

            from = userNumberList.Data.First().Number;
            fara.SendSms(userName, password, from, number, message);
        }

        public void SendRegisterCodeWithMeliPayamak_Com(string number, string code)
        {
            string userName, password;
            GetUserNameAndPassword(out userName, out password);
            var fara = new FaraApi();
            var bodyId = 93969;
            var result = fara.UseBaseService(userName, password, code, number, bodyId);

            if (result.RetStatus == 1) return;

            fara.UseBaseService(userName, password, code, number, bodyId);
        }

        public void Send_NewProjectAddedNotif_ToEditors_MeliPayamak_Com(List<string> numbers)
        {
            string userName, password;
            GetUserNameAndPassword(out userName, out password);
            var fara = new FaraApi();
            var text = "ادیتور";
            var bodyId = 97106;
            foreach (var number in numbers)
            {
                var result = fara.UseBaseService(userName, password, text, number, bodyId);

                if (result.RetStatus == 1) continue;

                fara.UseBaseService(userName, password, text, number, bodyId);
            }
        }

        public void Send_NewProjectOfferNotif_ToEmployer_MeliPayamak_Com(string number)
        {
            string userName, password;
            GetUserNameAndPassword(out userName, out password);
            var fara = new FaraApi();
            var text = "کارفرما";
            var bodyId = 97107;
            var result = fara.UseBaseService(userName, password, text, number, bodyId);

            if (result.RetStatus == 1) return;

            fara.UseBaseService(userName, password, text, number, bodyId);
        }

        public void Send_NewJobOfferNotif_ToEditor_MeliPayamak_Com(string number)
        {
            string userName, password;
            GetUserNameAndPassword(out userName, out password);
            var fara = new FaraApi();
            var text = "ادیتور";
            var bodyId = 97109;
            var result = fara.UseBaseService(userName, password, text, number, bodyId);

            if (result.RetStatus == 1) return;

            fara.UseBaseService(userName, password, text, number, bodyId);
        }

        public void Send_YourJobOfferIsAccepted_ToEmployer_MeliPayamak_Com(string number)
        {
            string userName, password;
            GetUserNameAndPassword(out userName, out password);
            var fara = new FaraApi();
            var text = "کارفرما";
            var bodyId = 97110;
            var result = fara.UseBaseService(userName, password, text, number, bodyId);

            if (result.RetStatus == 1) return;

            fara.UseBaseService(userName, password, text, number, bodyId);
        }

        public void Send_YourProjectOfferIsAccepted_ToEditor_MeliPayamak_Com(string number)
        {
            string userName, password;
            GetUserNameAndPassword(out userName, out password);
            var fara = new FaraApi();
            var text = "ادیتور";
            var bodyId = 97111;
            var result = fara.UseBaseService(userName, password, text, number, bodyId);

            if (result.RetStatus == 1) return;

            fara.UseBaseService(userName, password, text, number, bodyId);
        }

        public void Send_NewProjectMessage_MeliPayamak_Com(string number, string projectTitle, string senderRole)
        {
            string userName, password;
            GetUserNameAndPassword(out userName, out password);
            var fara = new FaraApi();
            var text = $"{projectTitle} ; {senderRole}";
            var bodyId = 102134;
            var result = fara.UseBaseService(userName, password, text, number, bodyId);

            if (result.RetStatus == 1) return;

            fara.UseBaseService(userName, password, text, number, bodyId);
        }

        //public void SendWithSms_Ir(string number, string message)
        //{
        //    var token = GetToken();
        //    var lines = new SmsLine().GetSmsLines(token);
        //    if (lines == null) return;

        //    var line = lines.SMSLines.Last().LineNumber.ToString();
        //    var data = new MessageSendObject
        //    {
        //        Messages = new List<string>
        //            {message}.ToArray(),
        //        MobileNumbers = new List<string> { number }.ToArray(),
        //        LineNumber = line,
        //        SendDateTime = DateTime.Now,
        //        CanContinueInCaseOfError = true
        //    };
        //    var messageSendResponseObject =
        //        new MessageSend().Send(token, data);

        //    if (messageSendResponseObject.IsSuccessful) return;

        //    line = lines.SMSLines.First().LineNumber.ToString();
        //    data.LineNumber = line;
        //    new MessageSend().Send(token, data);
        //}

        //private string GetToken()
        //{
        //    var smsSecrets = _configuration.GetSection("SmsIrSecrets");
        //    var tokenService = new Token();
        //    return tokenService.GetToken(smsSecrets["ApiKey"], smsSecrets["SecretKey"]);
        //}
        private void GetUserNameAndPassword(out string userName, out string password)
        {
            var smsSecrets = _configuration.GetSection("SmsMeliPayamakSecrets");
            userName = smsSecrets["UserName"];
            password = smsSecrets["Password"];
        }
    }
}