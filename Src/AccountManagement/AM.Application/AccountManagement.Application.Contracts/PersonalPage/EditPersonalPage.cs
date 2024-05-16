using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using _0_Framework.Application.CreateDto;
using Microsoft.AspNetCore.Http;

namespace AccountManagement.Application.Contracts.PersonalPage
{
    public class EditPersonalPage : CreatePersonalPage
    {
        public long Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsConfirm { get; set; }
    }
}