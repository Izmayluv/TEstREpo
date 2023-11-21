namespace Bot
{

    //configurations
    internal static class Config
    {
        private const String greeting = "Привет, я тестовый бот для обратной связи ветеринарной клиники <b>ГВЛДЦ№1</b>\n Здесь ты можешь оставить заявку на приём или узнать ответ на интересующие вопросы!\n <i>P.S. поддержка отвечает с 8:00-24:00</i>";
        private const String nightMessage = "Ваше сообщение будет обработано в ближайшее рабочее время.\n\nМы отвечаем на обращения с <b>10:00</b> до <b>22:00</b>. По срочным вопросам, звоните <b><u>(812) 660-77-80</u></b> круглосуточно.";
        private const String nightMessageMark = "🙄 пришло ночью";

        public static String getGreeting() { return greeting; }
        public static String getNightMessage() { return nightMessage; }
        public static String getNightMessageMark() { return nightMessageMark; }

    }
}
