using System.ComponentModel;

namespace Call_Center_Test.Dto
{
    public enum OperatorStates
    {
        [Description("Пауза")]
        Pause,
        [Description("Готов")]
        Ready,
        [Description("Разговор")]
        Talk,
        [Description("Перезвон")]
        ReСall,
        [Description("Обработка")]
        Processing
    }
}