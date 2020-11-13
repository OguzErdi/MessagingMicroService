using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Message.Application.Constants
{
    public class Messages
    {
        public static string MessageSended = "Mesaj başarıyla gönderildi";
        public static string NoMessage = "Mesaj yok";
        public static string UserNotFound = "Kullanıcı bulunamadı";
        public static string UserBlocked = "Kullanıcı bloklanmış";

        public static string PasswordError = "Şifre hatalı";
        public static string SuccessfulLogin = "Sisteme giriş başarılı";
        public static string UserAlreadyExists = "Bu kullanıcı zaten mevcut";
        public static string UserRegistered = "Kullanıcı başarıyla kaydedildi";
        public static string AccessTokenCreated = "Access token başarıyla oluşturuldu";
        public static string MessageAddedToHistory = "Mesaj Historye eklendi";

        public static string GetMessageHistory = "Mesaj Historyisini al";
        public static string ThereIsNoMessageInQueue = "Message Queue'da mesaj yok";
        public static string GetMessageFromQueue = "Message Queue'dan son mesajı al";
    }
}
