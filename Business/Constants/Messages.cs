using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string CarIsNotAppropriate = "Araba kiranlanmış durumdadır.";
        public static string CarImageLimitExceeded="Bir arabanın en fazla 5 adet resmi olabilir.";
        public static string UserNotFound="Kullanıcı Bulunamadı";
        public static string PasswordError="Kullanıcı Parola Hatalı";
        public static string SuccessfulLogin="Giriş Başarılı";
        public static string UserRegistered="Kullanıcı kaydedildi.";
        public static string EmailIsUsed="Email sistemde kayıtlıdır.";
        public static string EmailIsNotUsed="Email kullanılabilir";
        public static string AccessTokenCreated="Token Oluşturuldu";
        public static string WeakPassword="Şifreniz uygun değil.";
        public static string PasswordRequirement="Parola Min 5 karakter maksimum 8 karakterden oluşmalı.En az bir büyük harf,bir küçük harf ve bir rakam içermelidir.";
        public static string AuthorizationDenied="Yetkiniz Yok";
    }
}
