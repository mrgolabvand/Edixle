namespace _0_Framework.Application.Sms
{
    public interface ISmsService
    {
        //void SendWithSms_Ir(string number, string message);
        void SendWithMeliPayamak_Com(string number, string message);
        void SendRegisterCodeWithMeliPayamak_Com(string number, string code);
        void Send_NewProjectAddedNotif_ToEditors_MeliPayamak_Com(List<string> numbers);
        void Send_NewProjectOfferNotif_ToEmployer_MeliPayamak_Com(string number);
        void Send_NewJobOfferNotif_ToEditor_MeliPayamak_Com(string number);
        void Send_YourJobOfferIsAccepted_ToEmployer_MeliPayamak_Com(string number);
        void Send_YourProjectOfferIsAccepted_ToEditor_MeliPayamak_Com(string number);
        void Send_NewProjectMessage_MeliPayamak_Com(string number, string projectTitle, string senderRole);
    }
}