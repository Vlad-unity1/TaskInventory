using System.Collections;
using TMPro;
using UnityEngine;

namespace MessageInfo
{
    public static class Message
    {
        public const string ITEM_ADD_FAILED = "Не удалось добавить предмет!";
        public const string ITEM_USE_FAILED = "Не удалось использовать предмет!";
        public const string ITEM_ADDED = "Предмет успешно добавлен!";
        public const string ITEM_REMOVED = "Предмет успешно удален!";
        public static string ITEM_USED = "Предмет успешно использован!";
        public static string BOOK_USED = "Книга не может быть повтороно использована!";

        public static IEnumerator ShowMessage(TextMeshProUGUI messageText, string message, float displayTime = 2f)
        {
            messageText.text = message;
            messageText.gameObject.SetActive(true);
            yield return new WaitForSeconds(displayTime);
            messageText.gameObject.SetActive(false);
        }
    }
}