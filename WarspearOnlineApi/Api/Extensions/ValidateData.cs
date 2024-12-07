using System.Text.RegularExpressions;
using WarspearOnlineApi.Api.Models.Dto;

namespace WarspearOnlineApi.Api.Extensions
{
    /// <summary>
    /// Валидация данных.
    /// </summary>
    public static class ValidateData
    {
        /// <summary>
        /// Валидация данных игрока.
        /// </summary>
        public static void Validate(this PlayerDto player)
            => player.ThrowOnCondition(x => x.Nick.IsNullOrDefault(), "Не указан ник игрока")
            .ThrowOnCondition(x => x.Nick.Length < 3 || x.Nick.Length > 10, "Неверный размер ника игрока")
            .ThrowOnCondition(x => !Regex.IsMatch(x.Nick, @"^[a-zA-Z]+$"), "Ник должен содержать только английские буквы")
            .ThrowOnCondition(x => (x?.Class?.Id).IsNullOrDefault(), "Не указан класс игрока");
    }
}
