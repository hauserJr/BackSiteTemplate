using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackSiteTemplate.Models.ViewModel
{
    public class vmChangePwd
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "請輸入舊密碼")]
        public string OldPwd { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "{0} 的長度必須為 {2} ~ {1}個字元。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "新密碼")]
        public string NewPwd { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "確認密碼")]
        [Compare("NewPwd", ErrorMessage = "密碼和確認密碼不相符。")]
        public string CheckPwd { get; set; }
    }

}
